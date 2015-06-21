using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySleepy
{
    public partial class LiquidarPendiente : Form
    {
        private ConnectDB conexion;
        private int idUsuario;
        private int idPendiente;
        private InsertHistorial insert;
        Caja caja;
        public LiquidarPendiente(ConnectDB c, int idUsuario, int idPendiente)
        {
            InitializeComponent();
            this.conexion = c;
            this.idUsuario = idUsuario;
            this.idPendiente = idPendiente;
            this.insert = new InsertHistorial(c);
            this.caja = new Caja(conexion,idUsuario);
            txtImporte.LostFocus += new EventHandler(txtImporte_LostFocus);
            cargarComboTipo();
        }

        private void cajaDecimal(TextBox sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            Boolean IsDec = false;
            int nroDec = 0;
            int cuentapuntos = 0;
            if (sender.Text.Length > 6)
            {
                e.Handled = true;
                return;
            }
            for (int i = 0; i < sender.Text.Length; i++)
            {
                if (sender.Text[i] == '.' || sender.Text[i] == ',')
                {
                    IsDec = true;
                    cuentapuntos++;
                }
                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == '-')
                e.Handled = false;
            else if (e.KeyChar == 46 || e.KeyChar == 44)
            {
                if (sender.Text.Length < 1)
                {
                    e.Handled = true;
                    return;
                }
                if (cuentapuntos >= 1)
                {
                    e.Handled = true;
                }
                else
                {
                    e.KeyChar = ',';
                    e.Handled = false;
                }
            }
            else
                e.Handled = true;
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            //extraemos el importe (comprobamos que no sea superior al importe de la deuda)
            //si coincide con la totalidad de la deuda la ponemos a liquidada
            String importe = txtImporte.Text;
            Double imp = Math.Round(Convert.ToSingle(importe),2);
            Double importeTotalSql = Math.Round(Convert.ToSingle(conexion.DLookUp("importetotal", "pendientes", " idPendiente = " + idPendiente)),2);
            Double importePagadoSql = Math.Round(Convert.ToSingle(conexion.DLookUp("importepagado", "pendientes", " idPendiente = " + idPendiente)), 2);
            String concepto = Convert.ToString(conexion.DLookUp("CONCEPTO", "PENDIENTES", " idpendiente = " + idPendiente));
            int idOperacion = MetodosAuxiliares.ultimoID(conexion, "IDOPERACION", "OPERACIONES");
            String tipo = comboTipo.SelectedItem.ToString();
            //MessageBox.Show(Convert.ToString(imp));
            
            if (rbPorcentual.Checked == true && imp > 100)
            {
                MessageBox.Show("El porcentaje no puede ser superior a 100");
                txtImporte.Text = "";
            }
            else if (rbPorcentual.Checked == true && imp <= 100)
            {
                //calculamos el porcentaje del importe que corresponde
                imp = (importeTotalSql-importePagadoSql) * (imp / 100);
                //MessageBox.Show("importe ->" + imp);
            }
            //Si es mayor de lo que debes
            if (imp + importePagadoSql > importeTotalSql)
            {
                MessageBox.Show("El importe excede al pendiente de pago que desea abonar");
            }
            else
            {
                String update = "";
                //Si es igual al total de lo que debes
                
                if (imp + importePagadoSql == importeTotalSql)
                {
                    update = "Update pendientes set Liquidada = 'S',importepagado='" + importeTotalSql + "' where idpendiente = " + idPendiente;
                    //MessageBox.Show(update);
                    conexion.setData(update);
                    
                    //insert en la tabla operaciones
                    if (comboTipo.SelectedIndex != 0)
                    {
                        concepto = "Pendiente pagado: " + concepto;
                        String insertsql = "Insert into operaciones values(" + idOperacion + ",1,'" + tipo + "','" + concepto + "','" + imp + "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + idUsuario + ",'E')";
                        //MessageBox.Show(insertsql);
                        conexion.setData(insertsql);
                        MessageBox.Show("Apunte insertado y pendiente de pago liquidado totalmente");
                        //insert historial cambios
                        insert.insertHistorialCambio(idUsuario, 5, "Pendiente de pago liquidado " + concepto);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar el tipo de ingreso");
                    }
                    
                }//Si es menor al total que debes
                else if (imp + importePagadoSql < importeTotalSql)
                {
                    String conceptoModificado = Convert.ToString(conexion.DLookUp("concepto", "pendientes", " idpendiente = " + idPendiente));
                    if (concepto.ToLower().StartsWith("pendiente liquidado parcialmente"))
                    {
                        //no hacemos nada, se queda el concepto tal cual
                    }
                    else
                    {
                        conceptoModificado = " liquidada parcialmente " + Convert.ToString(conexion.DLookUp("CONCEPTO", "PENDIENTES", " idPendiente = " + idPendiente));
                    }
                    update = "Update pendientes set importepagado = '" + Math.Round((importePagadoSql + imp),2) + "', concepto= '"+conceptoModificado+"' where idpendiente = " + idPendiente;
                    conexion.setData(update);

                    //insert en la tabla operaciones                   
                    if (comboTipo.SelectedIndex != 0)
                    {                     
                        concepto = "Pendiente pagado: " + concepto;                      
                        String insertsql = "Insert into operaciones values(" + idOperacion + ",1,'" + tipo + "','" + concepto + "','" + imp + "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + idUsuario + ",'E')";
                        //MessageBox.Show(insertsql);
                        conexion.setData(insertsql);
                        caja.calcularTotales();
                        MessageBox.Show("Apunte Insertado y pendiente de pago liquidado parcialmente");

                        //insert historial cambios
                        insert.insertHistorialCambio(idUsuario, 8, "Pendiente de pago liquidado parcialmente " + concepto);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar el tipo de ingreso");
                    }
                }
            }
            
        }

        public void txtImporte_LostFocus(object sender, EventArgs e)
        {
            if (MetodosAuxiliares.cajaDecimalCorrecta(txtImporte) == false)
            {
                txtImporte.Focus();
                txtImporte.Text = "";
            }
        }
        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            MetodosAuxiliares.cajaDecimal(txtImporte, e, 6, 2);
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cargarComboTipo()
        {
            String[] tipo = { "-Seleccione tipo de ingreso", "Efectivo", "Cheque", "Recibo" };
            for (int i = 0; i < tipo.Length; i++)
            {
                comboTipo.Items.Add(tipo[i]);
            }
            comboTipo.SelectedIndex = 0;
        }

        private void LiquidarPendiente_Load(object sender, EventArgs e)
        {
            rbDirecta.Checked = true;
            ToolTip pend = new ToolTip();
            pend.SetToolTip(botonAceptar, "Liquidar Apunte");
            pend.SetToolTip(botonSalir, "Salir");
        }

        private void rbDirecta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDirecta.Checked == true)
            {
                rbPorcentual.Checked = false;
            }
        }

        private void rbPorcentual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPorcentual.Checked == true)
            {
                rbDirecta.Checked = false;
            }
        }




    }
}
