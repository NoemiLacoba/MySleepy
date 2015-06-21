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
    public partial class AddSalidas : Form
    {
        /// <summary>
        /// Atributo que almacena la conexion usada
        /// </summary>
        private ConnectDB conexion;
        /// <summary>
        /// Atributo que almacena el id del usuario que esta usando la interfaz
        /// </summary>
        private int idUsuario;
        /// <summary>
        /// Atributo que almacenara el mensaje de error si existe alguna campo sin rellenar;
        /// </summary>
        private String mensajeCampos;

        InsertHistorial insert;
        public AddSalidas(ConnectDB conexion, int idUsuario)
        {
            
            InitializeComponent();
            this.conexion = conexion;
            this.idUsuario = idUsuario;
            this.mensajeCampos = "Los siguientes campos están sin rellenar: \n";
            cargarComboDestino();
            cargarComboTipo();
            cajaImporte.LostFocus += new EventHandler(cajaImporte_LostFocus);
            insert = new InsertHistorial(conexion);            
        }
        public AddSalidas(ConnectDB conexion, int idUsuario,int codigo)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.idUsuario = idUsuario;
            this.mensajeCampos = "Los siguientes campos están sin rellenar: \n";
            cargarComboDestino();
            cargarComboTipo();
            cajaImporte.LostFocus += new EventHandler(cajaImporte_LostFocus);
            comboDestino.SelectedIndex = codigo;
            comboDestino.Enabled = false;
            insert = new InsertHistorial(conexion);
        }

        public void cajaImporte_LostFocus(object sender, EventArgs e)
        {
            if (MetodosAuxiliares.cajaDecimalCorrecta(cajaImporte) == false)
            {
                cajaImporte.Focus();
                cajaImporte.Text = "";
            }
        }
        /// <summary>
        /// Metodo que carga el combobox correspondiente al destino
        /// </summary>
        private void cargarComboDestino()
        {
            String procedencia = "-Seleccione destino-";
            comboDestino.Items.Add(procedencia);
            DataSet data = conexion.getData("SELECT DESTINO FROM DESTINOS ORDER BY IDDESTINO", "DESTINOS");
            DataTable tClientes = data.Tables["DESTINOS"];
            foreach (DataRow row in tClientes.Rows)
            {
                comboDestino.Items.Add(row["DESTINO"]);
            } // Fin del bucle for each
            comboDestino.SelectedIndex = 0;
        }
       
        public int numeroProveedores()
        {
            return Convert.ToInt32(conexion.DLookUp("count(*)", "PROVEEDORES", ""));
        }
        /// <summary>
        /// Metodo que carga el combobox correspondiente a los tipo de pagos
        /// </summary>
        private void cargarComboTipo()
        {
            String[] tipo = { "-Seleccione tipo de ingreso", "Efectivo", "Cheque", "Recibo" };
            for (int i = 0; i < tipo.Length; i++)
            {
                comboTipo.Items.Add(tipo[i]);
            }
            comboTipo.SelectedIndex = 0;
        }
        /// <summary>
        /// Metodo que limpia los campos
        /// </summary>
        private void limpiar()
        {
            comboDestino.SelectedIndex = 0;
            comboTipo.SelectedIndex = 0;
            cajaConcepto.Text = "";
            cajaImporte.Text = "";
        }

        /// <summary>
        /// Metodo que comprueba que se hallan rellenado los campos necesarios
        /// </summary>
        /// <returns></returns>
        private Boolean compruebaCamposRellenos()
        {
            Boolean rellenados = true;
            if (comboDestino.SelectedIndex <= 0)
            {
                rellenados = false;
                this.mensajeCampos = mensajeCampos + "-Ha de selecionar un Destino \n";
            }
            if (comboTipo.SelectedIndex <= 0)
            {
                rellenados = false;
                this.mensajeCampos = mensajeCampos + "-Ha de selecionar un Tipo \n";
            }
            if (cajaImporte.Text.Length <= 0)
            {
                rellenados = false;
                this.mensajeCampos = mensajeCampos + "-Ha de indicar un Importe \n";
            }
            if (cajaConcepto.Text.Length <= 0)
            {
                rellenados = false;
                this.mensajeCampos = mensajeCampos + "-Ha de rellenar el Concepto";
            }
            if (comboDestino.SelectedItem.ToString().ToLower().Equals("compras de mercaderías") && txtProveedor.Text.Equals(""))
            {
                rellenados = false;
                this.mensajeCampos = mensajeCampos + "-Ha de seleccionar un proveedor";
            }
            return rellenados;
        }
        public void cargarProveedor(String nombreProveedor)
        {
            txtProveedor.Text = nombreProveedor;
            
        }
        /// <summary>
        /// Metodo que añade el apunte a la base de datos
        /// </summary>
        private void annadirApunte()
        {
            if (compruebaCamposRellenos() == true)
            {
                int iddestino = comboDestino.SelectedIndex;
                int idTipo = comboTipo.SelectedIndex;
                String concepto = cajaConcepto.Text;
                String cantidadImporte = cajaImporte.Text;
                double importe = Math.Round(Convert.ToSingle(cajaImporte.Text),2);
                //MessageBox.Show("importe "+importe);
                //importe = Math.Round(importe,2);
                String tipo = comboTipo.SelectedItem.ToString();
                int idOperacion = MetodosAuxiliares.ultimoID(conexion,"IDOPERACION","OPERACIONES");

                //la salida se añade cuando se liquide la deuda en caso de ser compra de mercaderias
                //si se han comprado mercaderias -> insert en tabla deudas
                if (comboDestino.SelectedItem.ToString().ToLower().Equals("compras de mercaderías"))
                {

                    int idProv = Convert.ToInt32(conexion.DLookUp("IDPROVEEDOR","PROVEEDORES"," upper(NOMBRE) = '"+txtProveedor.Text.ToUpper()+"'"));
                    
                    //insert en deudas, el importe -> el que recibimos que es el total de la deuda, importe pagado -> 0 por el momento o no xD
            
                    String insertDeudas = "Insert into deudas values(" + MetodosAuxiliares.ultimoID(conexion,"IDDEUDA", "DEUDAS") 
                        + "," + idUsuario +","+idProv+",'" + Convert.ToInt32(MetodosAuxiliares.devolverHora()) 
                        + "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + ",'0','" + concepto +
                        "','N','"+tipo+"' ,'"+importe+"')";

                    MessageBox.Show("Deuda añadida");

                    //MessageBox.Show(insertDeudas);
                    conexion.setData(insertDeudas);

                    //insert en historial cambios
                    insert.insertHistorialCambio(idUsuario, 1, "Deuda añadida " + concepto);
                }
                else
                {
                    String sql = "insert into operaciones values(" + idOperacion
                    + ",'" + iddestino + "','" + tipo + "','" + concepto + "','" + importe +
                    "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + idUsuario + ",'S')";
                    conexion.setData(sql);
                    MessageBox.Show("Apunte Insertado");
                    
                    //insert en tabla historial cambios -> salida
                    insert.insertHistorialCambio(idUsuario, 1, "Salida añadida " + concepto);
                }
                DialogResult opcion = MessageBox.Show("¿Desea crear otro apunte?", "QUESTION", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (opcion == DialogResult.Yes)
                {
                    limpiar();
                }
                else this.Close();
            }
            else
            {
                MessageBox.Show(this, this.mensajeCampos, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mensajeCampos = "Los siguientes campos están sin rellenar: \n";
            }
        }

        ////////////////
        ////Eventos/////
        ////////////////
        private void AddSalidas_Load(object sender, EventArgs e)
        {
            ToolTip tTAñadirApunte = new ToolTip();
            tTAñadirApunte.SetToolTip(botonAceptar, "Añadir Apunte");
            tTAñadirApunte.SetToolTip(botonLimpiar, "Limpiar Campos");
            tTAñadirApunte.SetToolTip(botonSalir, "Salir");
        }
        private void cajaImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            MetodosAuxiliares.cajaDecimal(cajaImporte, e, 6, 2);
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            annadirApunte();
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = conexion.getData("SELECT * FROM PROVEEDORES", "PROVEEDORES");
            //abrimos ProveedoresForm
            Proveedores prov = Proveedores.Instance(ds,conexion, idUsuario,this,1);
            prov.Show();
            return;
        }

        private void comboDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDestino.SelectedItem.ToString().ToLower().Equals("compras de mercaderías"))
            {
                //desbloqueamos el boton de proveedor
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }

        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void cajaConcepto_TextChanged(object sender, EventArgs e)
        {
        }

        private void cajaImporte_TextChanged(object sender, EventArgs e)
        {

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

        ////////////////////
        ////Fin Eventos/////
        ////////////////////
    }
}
