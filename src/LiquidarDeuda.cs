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
    public partial class LiquidarDeuda : Form
    {
        private ConnectDB conexion;
        private int idUsuario;
        private int idDeuda;
        private InsertHistorial insert;
        Caja caja;
        public LiquidarDeuda(ConnectDB c,int idUsuario,int idDeuda)
        {
            InitializeComponent();
            this.conexion = c;
            this.idUsuario = idUsuario;
            this.idDeuda = idDeuda;
            insert = new InsertHistorial(c);
            txtImporte.LostFocus += new EventHandler(txtImporte_LostFocus);
        }

        public void txtImporte_LostFocus(object sender, EventArgs e)
        {
            if (MetodosAuxiliares.cajaDecimalCorrecta(txtImporte) == false)
            {
                txtImporte.Focus();
                txtImporte.Text = "";
            }
        }
        

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            //extraemos el importe (comprobamos que no sea superior al importe de la deuda)
            //si coincide con la totalidad de la deuda la ponemos a liquidada
            String importe = txtImporte.Text;
            Double imp = Math.Round(Convert.ToSingle(importe), 2);
            Double importeTotalSql = Math.Round(Convert.ToSingle(conexion.DLookUp("importetotal", "deudas", " idDeuda = " + idDeuda)), 2);
            Double importePagadoSql = Math.Round(Convert.ToSingle(conexion.DLookUp("importepagado", "deudas", " idDeuda = " + idDeuda)), 2);
            int idOperacion = MetodosAuxiliares.ultimoID(conexion, "IDOPERACION", "OPERACIONES");
            String concepto = Convert.ToString(conexion.DLookUp("concepto", "deudas", " iddeuda = " + idDeuda));
            String tipo = Convert.ToString(conexion.DLookUp("tipo", "deudas", " iddeuda = " + idDeuda));
            //MessageBox.Show("importe " + imp + " importeTotal" + importeTotalSql + " importePagado " + importePagadoSql);
            if (rbPorcentual.Checked == true && imp > 100)
            {
                MessageBox.Show("El porcentaje no puede ser superior a 100");
            }
            else if (rbPorcentual.Checked == true && imp <= 100)
            {
                //calculamos el porcentaje del importe total que queremos pagar
                imp = (importeTotalSql-importePagadoSql) * (imp / 100);
                //MessageBox.Show("importe ->"+imp);
            }
            if (imp + importePagadoSql > importeTotalSql)
            {
                MessageBox.Show("El importe excede a la deuda que quiere liquidar");
            }
            else
            {
                String update = "";
                if (imp+importePagadoSql == importeTotalSql)
                {
                    update = "Update deudas set Liquidada = 'S', importepagado= '"+importeTotalSql+"' where iddeuda = " + idDeuda;
                    conexion.setData(update);

                    
                    //realizamos la salida de capital, compra de mercaderias -> idDestino  = 2
                    String sql = "insert into operaciones values(" + idOperacion
                    + ",'" + 2 + "','" + tipo + "','Deuda " + concepto + "','" + imp +
                    "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + idUsuario + ",'S')";
                    conexion.setData(sql);
                    caja.calcularTotales();
                    MessageBox.Show("Apunte Insertado y deuda liquidada totalmente");

                    //insert en tabla historial cambios -> salida
                    insert.insertHistorialCambio(idUsuario, 1, "Salida añadida " + concepto);
                }
                else if (imp < importeTotalSql)
                {
                    String conceptoD = Convert.ToString(conexion.DLookUp("concepto", "deudas", " iddeuda = " + idDeuda));
                    if (concepto.ToLower().StartsWith("deuda liquidada parcialmente"))
                    {
                        //no hacemos nada, se queda el concepto tal cual
                    }
                    else
                    {
                        conceptoD = " liquidada parcialmente " + Convert.ToString(conexion.DLookUp("CONCEPTO", "DEUDAS", " iddeuda = " + idDeuda));
                    }
                   
                    update = "Update deudas set importepagado = '" + (importePagadoSql+imp) + "',concepto = '"+conceptoD+"' where iddeuda = " + idDeuda;
                    conexion.setData(update);

                    
                    //realizamos la salida de capital, compra de mercaderias -> idDestino  = 2
                    String sql = "insert into operaciones values(" + idOperacion
                    + ",'" + 2 + "','" + tipo + "','" + conceptoD + "','" + importe +
                    "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + idUsuario + ",'S')";
                    conexion.setData(sql);

                    MessageBox.Show("Apunte Insertado");


                    //insert en tabla historial cambios -> salida
                    insert.insertHistorialCambio(idUsuario, 1, "Salida añadida " + concepto);

                    
                    //insert historial cambios
                    insert.insertHistorialCambio(idUsuario, 8,concepto);
                }
            }
            this.Close();
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            MetodosAuxiliares.cajaDecimal(txtImporte, e, 6, 2);
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        private void LiquidarDeuda_Load(object sender, EventArgs e)
        {
            ToolTip deuda = new ToolTip();
            deuda.SetToolTip(botonAceptar, "Liquidar Apunte");
            deuda.SetToolTip(botonSalir, "Salir");
        }

        private void txtImporte_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
