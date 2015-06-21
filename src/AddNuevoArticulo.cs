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
    public partial class AddNuevoArticulo : Form
    {
        ConnectDB conexion;
        int numero;
        int idmodificar;
        ArticulosTotales articulos;
        AddNuevoArticulo add;
        AddPedido addPedido;
        InsertHistorial insert;
        int idUsuario,cerrarForm;
    
        private ArticulosForm articulosForm;
        private int p2;
        
        public AddNuevoArticulo(ConnectDB c, int señal, ArticulosTotales articulos, int idUsuario,int cerrar)
        {
            InitializeComponent();
            activarReferencia(true);
            this.idmodificar = 0;
            this.conexion = c;
            this.articulos = articulos; 
            this.cerrarForm = cerrar;
            numero = señal;
            cargarCombos(cboComposicion, 0);
            cargarCombos(cboMedida, 1);
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
        }

        public AddNuevoArticulo(ConnectDB conexion, int p1, ArticulosForm articulosForm, int idUsuario, int p2)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.numero = p1;
            this.articulosForm = articulosForm;
            this.idUsuario = idUsuario;
            this.p2 = p2;
            cargarCombos(cboComposicion, 0);
            cargarCombos(cboMedida, 1);
            insert = new InsertHistorial(conexion);
            caja_StockIdeal.Visible = false;
            caja_StockReal.Visible = false;
            lbIdeal.Visible = false;
            lbReal.Visible = false;
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ArticulosForm articulos = ArticulosForm.Instance(0,this,conexion);
            articulos.Show();
            this.Close();
            //articulosForm.actualizarTabla();
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            String sentencia = "";
            String nombreArticulo = txtNombre.Text;
            int tipoCambio = 0;
            String mensajeHistorial = "";
            
            //Añadir nuevo
            if (numero == 0)
            {
                if (comprobarCajasTexto(numero))
                {
                    
                    //añadirArticulo(idmodificar);
                    if (comprobarReferencia(Convert.ToInt32(txtReferencia.Text)))
                    {
                        int idArticulo = Convert.ToInt32(MetodosAuxiliares.ultimoID(conexion, "IDARTICULO", "ARTICULOS"));
                        int composicion = cboComposicion.SelectedIndex;
                        int medida = cboMedida.SelectedIndex;
                        /*Insertar en articulos*/
                        sentencia = "INSERT INTO ARTICULOS VALUES(" + idArticulo + "," + (composicion + 1) + "," + (medida + 1) + ",'" + txtPrecio.Text + "',0,'" + nombreArticulo.ToUpper() + "'," + Convert.ToInt32(txtReferencia.Text)+")";
                        conexion.setData(sentencia);
                         
                        tipoCambio = 1;
                        mensajeHistorial = "Articulo añadido ->" + nombreArticulo;
                        //insert en la tabla historial de cambios
                        insert.insertHistorialCambio(idUsuario, tipoCambio, mensajeHistorial);
                        // + "," + Convert.ToInt32(caja_StockReal.Value.ToString()) +
                        int contador=MetodosAuxiliares.ultimoID(conexion,"idstock","ARTICULOSSTOCK");
                        int real,ideal;
                        real=Convert.ToInt32(caja_StockReal.Value.ToString());
                        ideal=Convert.ToInt32(caja_StockIdeal.Value.ToString());
                        sentencia = "insert into articulosstock values("+contador+","+idArticulo+","+real+","+ideal+")";
                        conexion.setData(sentencia);
                        limpiarCampos();
                        MessageBox.Show("Articulo añadido");
                    }
                    else
                    {
                        MessageBox.Show("Referencia repetida");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Tienes que rellenar los campos que faltan");
                }
            }
            else
            {
               
                if (comprobarCajasTexto(numero))
                {

                    
                    //**PASAR STOCK
                    sentencia = "UPDATE ARTICULOS SET REFCOMPOSICION=" + (cboComposicion.SelectedIndex + 1) + ",REFMEDIDA=" + (cboComposicion.SelectedIndex + 1) + ",PRECIO='" + txtPrecio.Text + "',NOMBRE='" + nombreArticulo.ToUpper() + "'" +
                                      " WHERE IDARTICULO=" + idmodificar;
                    conexion.setData(sentencia);
                    
                    tipoCambio = 2;
                    mensajeHistorial = "Articulo modificado ->" + nombreArticulo;

                    //insert en la tabla historial de cambios
                    insert.insertHistorialCambio(idUsuario, tipoCambio, mensajeHistorial);
                    limpiarCampos();
                    MessageBox.Show("Articulo modificado");
                }
                else
                {
                    MessageBox.Show("Tienes que rellenar los campos que faltan");
                }
            }

        }

        

        private bool comprobarCajasTexto(int señalModificado)
        {
            Boolean devolver = true;
            Label[] idsLabel = { lblReferencia, lblNombre, lbReal, lblComposicion, lblMedida, lblPrecio,lbIdeal };
            TextBox[] idsTextBox = { txtReferencia, txtNombre, txtPrecio };
            ComboBox[] idsCombos = { cboMedida, cboComposicion };
            if (idsCombos[0].SelectedIndex == -1)
            {
                idsLabel[4].ForeColor = Color.Red;
            }
            else
            {
                idsLabel[4].ForeColor = Color.Green;
            }
            if (idsCombos[1].SelectedIndex == -1)
            {
                idsLabel[3].ForeColor = Color.Red;
            }
            else
            {
                idsLabel[3].ForeColor = Color.Green;
            }
            //Comprueba solo en el caso que sea para añadir
            if (señalModificado!=1)
            {
                if (caja_StockReal.Value == 0)
                {
                    idsLabel[2].ForeColor = Color.Red;
                }
                else
                {
                    idsLabel[2].ForeColor = Color.Green;
                }
                if (caja_StockIdeal.Value == 0)
                {
                    idsLabel[6].ForeColor = Color.Red;
                }
                else
                {
                    idsLabel[6].ForeColor = Color.Green;
                }
            }
            if (idsTextBox[0].Text == "")
            {
                idsLabel[0].ForeColor = Color.Red;
            }
            else
            {
                idsLabel[0].ForeColor = Color.Green;
            }
            if (idsTextBox[1].Text == "")
            {
                idsLabel[1].ForeColor = Color.Red;
            }
            else
            {
                idsLabel[1].ForeColor = Color.Green;
            }
            if (idsTextBox[2].Text == "")
            {
                idsLabel[5].ForeColor = Color.Red;
            }
            else
            {
                idsLabel[5].ForeColor = Color.Green;
            }
            for (int i = 0; i < idsLabel.Length; i++)
            {
                if (idsLabel[i].ForeColor == Color.Red)
                {
                    devolver = false;
                }
            }

            return devolver;
        }

        public void rellenar(DataGridViewRow fila)
        {
            int referencia = Convert.ToInt32(fila.Cells[0].Value);
            String nombre = Convert.ToString(fila.Cells[1].Value);
            //int stock = Convert.ToInt32(fila.Cells[2].Value.ToString());
            int composicion = Convert.ToInt32(conexion.DLookUp("IDCOMPOSICION", "COMPOSICIONES", "COMPOSICION like '" + fila.Cells[2].Value.ToString() + "'"));
            int medida = Convert.ToInt32(conexion.DLookUp("IDMEDIDA", "MEDIDAS", "MEDIDA like '" + fila.Cells[3].Value.ToString() + "'"));
            String precio = Convert.ToString(fila.Cells[4].Value);
            conseguirId(referencia);
            
            escribir(referencia, nombre,0,composicion, medida, precio);
        }

        private void conseguirId(int referencia)
        {
            String select = "SELECT IDARTICULO FROM ARTICULOS WHERE REFERENCIA=" + referencia;
            DataSet data = conexion.getData(select, "ARTICULOS");

            DataTable tArticulos = data.Tables["ARTICULOS"];

            foreach (DataRow row in tArticulos.Rows)
            {
                idmodificar = Convert.ToInt32(row["IDARTICULO"]);
            }

        }

        public Boolean comprobarReferencia(int refAdd)
        {
            String select = "SELECT REFERENCIA FROM ARTICULOS";
            DataSet data = conexion.getData(select, "ARTICULOS");

            DataTable tArticulos = data.Tables["ARTICULOS"];
            foreach (DataRow row in tArticulos.Rows)
            {
                int refTabla = Convert.ToInt32(row["REFERENCIA"]);
                if (refAdd == refTabla)
                {
                    // Existe y la encuentra devuelve false para que vuelva a introducir el usuario una nueva referencia
                    return false;
                }
            }
            return true;
        }

        public void limpiarCampos()
        {
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtReferencia.Text = "";
            cboComposicion.SelectedIndex = -1;
            cboMedida.SelectedIndex = -1;
            caja_StockReal.Value = 0;
            caja_StockIdeal.Value = 0;
        }

        private void escribir(int referencia, string nombre, int stock,int composicion, int medida, string precio)
        {
            txtReferencia.Text = "" + referencia;
            txtNombre.Text = nombre;
            cboComposicion.SelectedIndex = composicion-1;
            cboMedida.SelectedIndex = medida-1;
            txtPrecio.Text = precio;
            caja_StockReal.Value = stock;
        }

        public void activarReferencia(Boolean valor)
        {
            txtReferencia.Enabled = valor;
        }

        private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.GetNumericValue(e.KeyChar) == -1)
            {
                e.Handled = false;
                return;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.GetNumericValue(e.KeyChar) == -1)
            {
                e.Handled = false;
                return;
            }
            if (!Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void AddNuevoArticulo_Load(object sender, EventArgs e)
        {
            btnAnadir.NotifyDefault(true);

        }

        private void cargarCombos(ComboBox cbo, int señal)
        {
            DataSet data;
            DataTable tabla = null;
            String sentencia = "";
            if (señal == 0)
            {
                sentencia = "SELECT COMPOSICION FROM COMPOSICIONES order by idcomposicion";
                data = conexion.getData(sentencia, "COMPOSICIONES");
                tabla = data.Tables["COMPOSICIONES"];
                foreach (DataRow row in tabla.Rows)
                {
                    cbo.Items.Add(Convert.ToString(row["COMPOSICION"]));
                }
            }
            if (señal == 1)
            {
                sentencia = "SELECT MEDIDA FROM MEDIDAS order by idmedida";
                data = conexion.getData(sentencia, "MEDIDAS");
                tabla = data.Tables["MEDIDAS"];
                foreach (DataRow row in tabla.Rows)
                {
                    cbo.Items.Add(Convert.ToString(row["MEDIDA"]));
                }
            }


        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.GetNumericValue(e.KeyChar) == -1 || Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
                return;
            }
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtReferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit(Convert.ToChar(e.KeyCode)))
            {
                e.Handled = true;
            }
        }


    }
}
