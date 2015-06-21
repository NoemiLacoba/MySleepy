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
    public partial class ArticulosCompuestos : Form
    {

        private int señal;
        public ConnectDB conexion;
        //patron singleton
        private static ArticulosCompuestos instance;
        private int idArticulo;
        //private int idRol;
        //private ConnectDB conexion1;
        private int idUsuario;

        internal static ArticulosCompuestos Instance(ConnectDB conexion, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ArticulosCompuestos(conexion, idUsuario);
            }
            return instance;
        }
 
        public ArticulosCompuestos()
        {
            InitializeComponent();
            
        }

        public ArticulosCompuestos(ConnectDB con, int idUsuario)
        {
            InitializeComponent();
            this.conexion = con;
            this.idUsuario = idUsuario;
            ToolTip tool = new ToolTip();
            tool.SetToolTip(botonSalir, "Salir del menú");
            tool.SetToolTip(btnAñadir, "Añadir datos del articulo a la tabla");
            tool.SetToolTip(btnBuscarArticulo, "Buscar un articulo en el formulario de articulos");
            tool.SetToolTip(btnCancelar, "Eliminar un articulo de la tabla");
            tool.SetToolTip(btnLimpiarFiltros, "Limpiar los datos del formulario");
            tool.SetToolTip(btnGuardar, "Guardar los datos y crear un nuevo PACK");
        }

        public Boolean comprobarReferencia(int refAdd)
        {
            Boolean existe=true;
            int acierto = Convert.ToInt32(conexion.DLookUp("referencia", "ARTICULOS", "referencia=" + refAdd));
            if (acierto == -1)
            {
                existe = false;
            }
            return existe;
        }

        private void ArticulosCompuestos_Load(object sender, EventArgs e)
        {

        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            //Le introducimos un 1 que es la señal para que cargue la tabla de inicio
            ArticulosForm artic = ArticulosForm.Instance(5,conexion,this);
            artic.Show();
            this.Close();
        }

        private void btnBuscarArticulo_Click(object sender, EventArgs e)
        {
           
            ArticulosForm articulos = ArticulosForm.Instance(2,conexion,this);
            articulos.Show();
            
        }
        public void cargarArticuloSimple(int idArticulo){
            this.idArticulo = idArticulo;
            String sql = "Select A.NOMBRE,A.REFERENCIA,A.PRECIO,S.STOCKREAL,S.STOCKIDEAL,C.COMPOSICION,M.MEDIDA"
                        +" from ARTICULOS A, ARTICULOSSTOCK S, COMPOSICIONES C, MEDIDAS M"
                        +" where A.REFCOMPOSICION = C.IDCOMPOSICION and A.REFMEDIDA = M.IDMEDIDA"
                        +" and S.IDARTICULO = A.IDARTICULO and A.IDARTICULO = " + idArticulo;
            DataSet resultado = conexion.getData(sql, "ARTICULOSIMPLE");
            DataTable tArticulos = resultado.Tables["ARTICULOSIMPLE"];
            foreach (DataRow row in tArticulos.Rows)
            {
                txtNombre.Text = Convert.ToString(row[0]);//Nombre articulo
                txtReferencia.Text = Convert.ToString(row[1]);//Referencia articulo
                txtPrecio.Text = Convert.ToString(row[2]);//precio articulo
                caja_StockReal.Value = Convert.ToInt32(row[3]);//stockreal articulostock
                caja_StockIdeal.Value = Convert.ToInt32(row[4]);//stockideal articulostock
                caja_composicion.Text = Convert.ToString(row[5]);//composicion composicion
                caja_medida.Text = Convert.ToString(row[6]);//medida medida
            } // Fin del bucle for each

            txtNombre.Enabled = false;
            txtReferencia.Enabled = false;
            txtPrecio.Enabled = false;
            caja_StockReal.Enabled = false;
            caja_StockIdeal.Enabled = false;
            caja_composicion.Enabled = false;
            caja_medida.Enabled = false;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            if (caja_cantidad.Value > 0)
            {
                double precio = Math.Round(Convert.ToSingle(txtPrecio.Text) * Convert.ToInt32(caja_cantidad.Value),2);
                dgvCompuestos.Rows.Add(idArticulo, txtReferencia.Text, txtNombre.Text, caja_composicion.Text, caja_medida.Text, caja_StockReal.Value, caja_StockIdeal.Value, caja_cantidad.Value,precio);
                limpiar(false);
                caja_precioTotal.Text = Convert.ToString(sumarPrecios());
            }
            else
            {
                MessageBox.Show(this, "Debe introducir minimo 1 articulo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Metodo que limpia la interfaz
        /// </summary>
        /// <param name="todo">-true si se quiere limpiar todos los campos -false si solo se limpian los campos del articulo simple</param>
        private void limpiar(Boolean todo)
        {
            txtNombre.Text = "";//Nombre articulo
            txtReferencia.Text = "";//Referencia articulo
            txtPrecio.Text = "";//precio articulo
            caja_StockReal.Value = 0;//stockreal articulostock
            caja_StockIdeal.Value = 0;//stockideal articulostock
            caja_cantidad.Value = 1;//cantidad del articulostock
            caja_composicion.Text = "";//composicion composicion
            caja_medida.Text = "";//medida medida
            
            if (todo == true)
            {
                caja_referencia.Text = "";//Referencia del compuesto
                caja_nombreCompuesto.Text = "";//Nombre articulo compuesto
                caja_precioCompuesto.Text = "";//Precio articulo compuesto
                //Borramos las filas de las tablas
                dgvCompuestos.Rows.Clear();
                caja_precioTotal.Text = "0";
            }
            
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            limpiar(true);
        }

        private float sumarPrecios()
        {
            String precioT;
            float precio = 0;
            for (int i = 0; i < dgvCompuestos.RowCount; i++)
            {
                precioT = dgvCompuestos.Rows[i].Cells[8].Value.ToString();
                precio = precio + Convert.ToSingle(precioT);
            }
            return precio;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Recorrer datagridview, comprobar que no está vacio
            //Si no lo está guardar el nombre en la tabla compuestos
            //Y despues en la tabla articulos partes con el id del compuesto
            int numeroFilas = dgvCompuestos.RowCount;
            if (numeroFilas == 1)
            {
                MessageBox.Show("Debe seleccionar más articulos para forma el PACK o Compuesto");
            }
            else { 
            if (numeroFilas > 1)
            {
                if (caja_nombreCompuesto.Text.Equals(""))
                {
                    MessageBox.Show("Introduzca nombre del Articulo Compuesto o PACK");
                }
                else
                {
                    if (caja_precioCompuesto.Text.Equals(""))
                    {
                        MessageBox.Show("Introduzca el precio del Articulo Compuesto o PACK");
                    }
                    else
                    {
                        if (caja_referencia.Text.Equals(""))
                        {
                            MessageBox.Show("Introduzca referencia del Articulo Compuesto o Pack");
                        }
                        else
                        {
                            String referencia = caja_referencia.Text.Trim();
                            Boolean seguir = comprobarReferencia(Convert.ToInt32(referencia));
                            if (seguir == false)
                            {


                                String nombreC = caja_nombreCompuesto.Text.Trim();
                                String precio = caja_precioCompuesto.Text.Trim();
                                //TABLA ARTICULOS
                                //REFComposicion= PACK 12
                                //REFMedica= GENERICA 23
                                int contadorA = MetodosAuxiliares.ultimoID(conexion, "idarticulo", "ARTICULOS");
                                String insert = "insert into articulos values(" + contadorA + ",12,23,'" + precio + "',0,'" + nombreC + "'," + referencia + ")";
                                conexion.setData(insert);

                                //TABLA ARTICULOSPARTES
                                String insertAP;
                                int idArticulo, cantidad, contadorAP;
                                //Por cada fila de la tabla la agregamos a la tabla articulospartes
                                for (int i = 0; i < dgvCompuestos.RowCount; i++)
                                {
                                    contadorAP = MetodosAuxiliares.ultimoID(conexion, "idarticulopartes", "ARTICULOSPARTES");
                                    idArticulo = Convert.ToInt32(dgvCompuestos.Rows[i].Cells[0].Value.ToString());
                                    cantidad = Convert.ToInt32(dgvCompuestos.Rows[i].Cells[7].Value.ToString());
                                    insertAP = "insert into articulospartes values(" + contadorAP + "," + contadorA + "," + idArticulo + "," + cantidad + ")";
                                    conexion.setData(insertAP);
                                }

                                MessageBox.Show("Articulo Compuesto Registrado");
                                limpiar(true);
                            }
                            else
                            {
                                MessageBox.Show("Referencia Repetida");
                            }

                        }
                    }
                        
                    }
                }
            }
        }
    }
}
