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
    public partial class AddEntradas : Form
    {
        ConnectDB conexion;
        private int usuarioConexion;
        private int idCliente;
        InsertHistorial insert;
        private ClientesForm clientes;
        public AddEntradas(ConnectDB conn,int idUsuario)
        {
            InitializeComponent();
            this.conexion = conn;
            usuarioConexion = idUsuario;
            insert = new InsertHistorial(conn);
            cajaImporte.LostFocus += new EventHandler(cajaImporte_LostFocus);
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEntradas_Load(object sender, EventArgs e)
        {
            cargarComboProcedencia();
            cargarComboTipo();
            cargarComboProcedenciaPend();
            rbAhora.Checked = true;

            ToolTip tTAñadirApunte = new ToolTip();
            tTAñadirApunte.SetToolTip(botonAceptar, "Añadir Apunte");
            tTAñadirApunte.SetToolTip(botonLimpiar, "Limpiar Campos");
            tTAñadirApunte.SetToolTip(botonSalir, "Salir");
        }

        private void cargarComboProcedenciaPend()
        {
            comboAgencia.Items.Clear();
            String agencia = "-Seleccione agencia-";
            comboAgencia.Items.Add(agencia);
            DataSet data = conexion.getData("SELECT nombre FROM agencias ORDER BY idagencia", "AGENCIAS");
            DataTable tAgencia = data.Tables["AGENCIAS"];
            foreach (DataRow row in tAgencia.Rows)
            {
                comboAgencia.Items.Add(row["NOMBRE"]);
            } // Fin del bucle for each
            comboAgencia.SelectedIndex = 0;
        }
        private void cargarComboProcedencia()
        {
            String[] procedencia = { "-Seleccione procedencia-", "Venta de productos", "Ingreso directo administrativo", "Variación de saldo", "Varios" };
            for (int i = 0; i < procedencia.Length; i++)
            {
                comboProcedencia.Items.Add(procedencia[i]);
            }
            comboProcedencia.SelectedIndex = 0;
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

        public void cajaImporte_LostFocus(object sender, EventArgs e)
        {
            if (MetodosAuxiliares.cajaDecimalCorrecta(cajaImporte) == false)
            {
                cajaImporte.Focus();
                cajaImporte.Text = "";
            }
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            comboProcedencia.SelectedIndex = 0;
            comboTipo.SelectedIndex = 0;
            cajaConcepto.Text = "";
            cajaImporte.Text = "";
            txtCliente.Text = "";
        }


        //Método que sirve para añadir en la tabla operaciones el apunte de entrada
        private void botonAceptar_Click(object sender, EventArgs e)
        {
            int idProcedencia=comboProcedencia.SelectedIndex;
            int idTipo=comboTipo.SelectedIndex;
            String concepto=cajaConcepto.Text;
            String cantidadImporte = cajaImporte.Text;
            if (cantidadImporte.Equals(""))
            {
                MessageBox.Show("Debe introducir el importe");
            }
            else
            {
                if((Convert.ToSingle(cajaImporte.Text))<=0)
                {
                    cajaImporte.Text = "";
                    MessageBox.Show("No se permiten números negativos");
                }
                else {
                double importe = Convert.ToSingle(cajaImporte.Text);
                importe = Math.Round(importe, 2);
                //MessageBox.Show(Convert.ToString(importe));
                if (concepto.Equals(""))
                {
                    MessageBox.Show("Obligatorio introducir concepto en el apunte");
                }
                else
                {
                    if (idProcedencia == 0)
                    {
                        MessageBox.Show("Seleccione procedencia");
                    }
                    else
                    {
                        if (idTipo == 0)
                        {
                            MessageBox.Show("Seleccione el tipo");
                        }
                        else
                        {
                            String tipo = comboTipo.SelectedItem.ToString();
                            String procedencia = comboProcedencia.SelectedItem.ToString();
                            //DialogResult dialogresult = MessageBox.Show("¿Desea pagar ahora el importe total del producto?"
                            //                             , "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            //Si decide pagar ahora
                            //if (dialogresult == DialogResult.Yes)
                            if(rbAhora.Checked==true)
                            {
                                int idOperacion = MetodosAuxiliares.ultimoID(conexion, "IDOPERACION", "OPERACIONES");
                                String sql = "insert into operaciones values(" + idOperacion + "," + idProcedencia + ",'" + tipo + "','" + concepto + "','" + importe + "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + usuarioConexion + ",'E')";
                                
                                conexion.setData(sql);
                                MessageBox.Show("Apunte Insertado");
                                limpiar();

                                //insert en tabla historial cambios
                                insert.insertHistorialCambio(usuarioConexion, 1, "Entrada añadida " + concepto);
                            }
                            else //if (dialogresult == DialogResult.No)
                            {
                                if (txtCliente.Text.Equals(""))
                                {
                                    MessageBox.Show("Debe introducir el cliente");
                                }
                                else
                                {
                                    if (comboProcedencia.SelectedItem.ToString().ToLower().Equals("venta de productos") ||
                                        comboProcedencia.SelectedItem.ToString().ToLower().Equals("varios"))
                                    {

                                        //int idOperacion = MetodosAuxiliares.ultimoID(conexion, "IDOPERACION", "OPERACIONES");

                                        String agencia = comboAgencia.SelectedItem.ToString();
                                        int idAgencia = Convert.ToInt32(conexion.DLookUp("idagencia", "agencias", "upper(nombre) like upper('" + agencia + "')"));
                                        //MessageBox.Show(Convert.ToString(idAgencia));
                                        int idPendiente = MetodosAuxiliares.ultimoID(conexion, "IDPENDIENTE", "PENDIENTES");
                                        String sql = "insert into PENDIENTES values(" + idPendiente + "," + usuarioConexion + "," + idCliente + ","+idAgencia+","+ Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + ",'" + importe + "','0','" + tipo + "','" + concepto + "','N')";
                                        //MessageBox.Show(sql);
                                        conexion.setData(sql);
                                        MessageBox.Show("Pendiente de pago Insertado");
                                        limpiar();

                                        //insert en tabla historial cambios
                                        insert.insertHistorialCambio(usuarioConexion, 1, "Añadida Pendiente de pago:" + concepto);
                                    }
                                }
                                
                            }

                        }
                    }
                }
                }
            }
        }

        private void cajaImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            MetodosAuxiliares.cajaDecimal(cajaImporte, e, 6, 2);  
            
        }

        private void comboTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboProcedencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cajaImporte_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int idRol = MetodosAuxiliares.ultimoID(conexion, "IDROL", "ROLES");
            clientes = ClientesForm.Instance(idRol, 2, conexion, this, usuarioConexion);
            clientes.Show();
            return;
        }

        public void setIdCliente(int id)
        {
            this.idCliente = id;
        }
        public void setNombre(String nombreCliente)
        {
            txtCliente.Text = nombreCliente;
        }

        private void comboProcedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProcedencia.SelectedItem.ToString().ToLower().Equals("venta de productos") ||
                comboProcedencia.SelectedItem.ToString().ToLower().Equals("varios"))
            {
                //desbloqueamos el boton de proveedor
                btnBuscar.Enabled = true;
                rbPendiente.Visible = true;
            }
            else
            {
                btnBuscar.Enabled = false;
                rbPendiente.Visible = false;
            }
        }

        private void cajaConcepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar.Equals(' ') || Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void rbPendiente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPendiente.Checked == true)
            {
                agencia.Visible= true;
                comboAgencia.Visible = true;
            }
            else
            {
                agencia.Visible = false;
                comboAgencia.Visible = false;
            }
        }

        private void rbAhora_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAhora.Checked == true)
            {
                agencia.Visible = false;
                comboAgencia.Visible = false;
            }
            else
            {
                agencia.Visible = true;
                comboAgencia.Visible = true;
            }
        }

        private void comboAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
