using Microsoft.VisualBasic;
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
    public partial class ArticulosForm : Form
    {
        // Atributos a nivel de clase
        ConnectDB conexion;
        int rolUsuario;
        int numero;
        AddPedido pedido;
        private int idRol;
        private InsertHistorial insert;
        private int idUsuario;
        private int señal;
        //patron singleton
        private static ArticulosForm instance;
        private ArticulosCompuestos articComp;
        private int p;
        private AddNuevoArticulo addNuevoArticulo;
       

        public static ArticulosForm Instance(int idRol, int numero, AddPedido ped, ConnectDB c, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ArticulosForm(idRol, numero, ped, c, idUsuario);
            }
            return instance;
        }
        public static ArticulosForm Instance(int idRol, ConnectDB conexion, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ArticulosForm(idRol, conexion, idUsuario);
            }
            return instance;
        }
        public static ArticulosForm Instance(int señal, ConnectDB conexion,AddFactura addf,int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ArticulosForm(señal, conexion,addf,idUsuario);
            }
            return instance;
        }

        public static ArticulosForm Instance(int numero, AddNuevoArticulo articuloNuevo, ConnectDB c)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ArticulosForm(numero, articuloNuevo, c);
            }
            return instance;
        }

        //Instancia de articulos compuestos
        public static ArticulosForm Instance(int numero, ConnectDB c, ArticulosCompuestos articComp)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ArticulosForm(numero,c,articComp);
            }
            return instance;
        }
        //Se llama desde pedidos con un 1=numero
        private ArticulosForm(int idRol, int numero, AddPedido ped, ConnectDB c, int idUsuario)
        {
            InitializeComponent();
            this.conexion = c;
            rolUsuario = idRol;
            this.numero = numero;
            this.pedido = ped;
            cargarDGVInicio();
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
        }

        private ArticulosForm(int idRol, ConnectDB conexion, int idUsuario)
        {
            InitializeComponent();
            this.idRol = idRol;
            this.conexion = conexion;
            cargarDGVInicio();
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
        }

        //Instancia que llama desde addfacturas
        private ArticulosForm(int s, ConnectDB conexion,AddFactura addf,int idUsuario)
        {
            InitializeComponent();
            this.señal = s;
            this.conexion = conexion;
            cargarDGVInicio();
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
        }

        //Llamada de articulos compuestos con los simples
        public ArticulosForm(int numero, ConnectDB con,ArticulosCompuestos articComp)
        {
            InitializeComponent();
            this.señal = numero;
            this.articComp = articComp;
            this.conexion = con;
            //carga todos los articulos
            if (numero != 2) cargarDGVInicio();
            if(señal==2){
                //Cargar la tabla solo con los articulos simples
                String sentencia = "select * from articulos where refcomposicion<>12 and refmedida<>23 and eliminado=0 order by nombre";
                actualizarDGV(sentencia);
            }
            
            
        }

        //Instancia que se llama al cerrar articulo simple
        public ArticulosForm(int p, AddNuevoArticulo addNuevoArticulo, ConnectDB conexion)
        {
            InitializeComponent();
            this.numero = p;
            this.addNuevoArticulo = addNuevoArticulo;
            this.conexion = conexion;
            cargarDGVInicio();
        }

        private void cargarDGVInicio()
        {
            String sentencia = "SELECT * FROM ARTICULOS WHERE ELIMINADO=0 order by nombre";
            actualizarDGV(sentencia);
            dgvArticulos.ClearSelection();
        }

        private void cargarCombos(ComboBox cbo)
        {
            DataSet data;
            DataTable tabla = null;
            String sentencia = "";
            cbo.Items.Add("Seleccione medida");
            sentencia = "SELECT MEDIDA FROM MEDIDAS order by idmedida";
            data = conexion.getData(sentencia, "MEDIDAS");
            tabla = data.Tables["MEDIDAS"];
            foreach (DataRow row in tabla.Rows)
            {
                cbo.Items.Add(Convert.ToString(row["MEDIDA"]));
            }
            cbo.SelectedIndex = 0;

        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        
        private void limpiarCampos()
        {
            txtPrecio.Text = "";
            txtReferencia.Text = "";
            cbMedida.SelectedIndex = -1;
            txtNombre.Text = "";
            rbEliminados.Checked = false;
            rbNoEliminados.Checked = true;
            actualizarDGV("select * from ARTICULOS where eliminado=0 order by nombre");
        }

        public void filtrar()
        {
            
            int medida = cbMedida.SelectedIndex;
            String nombre = txtNombre.Text;
            String referencia = txtReferencia.Text;
            String precio = txtPrecio.Text;
            String sentencia="";
            double precioA;
            int referenciaA;
            if (rbNoEliminados.Checked)
            {
                sentencia = "SELECT * FROM ARTICULOS WHERE ELIMINADO=0";
            }
            
            if (rbEliminados.Checked)
            {
               
               sentencia = "SELECT * FROM ARTICULOS WHERE ELIMINADO=1";
                
            }

            if (medida > 0)
            {
                sentencia = sentencia + " AND REFMEDIDA=" + medida;

            }

            if (!nombre.Equals(""))
            {
                sentencia = sentencia + " AND upper(NOMBRE) LIKE upper('%" + nombre + "%')";

            }

            if (!referencia.Equals(""))
            {
                referenciaA = Convert.ToInt32(referencia);
                sentencia = sentencia + " AND REFERENCIA>=" + referenciaA;

            }

            if (!precio.Equals(""))
            {
                precioA = Convert.ToInt32(precio);
                sentencia = sentencia + " AND PRECIO >=" + precioA;

            }
            sentencia = sentencia + " order by nombre";
            actualizarDGV(sentencia);
        }

        public void actualizarDGV(String sentencia)
        {
            limpiarTabla();
            DataSet resultado = conexion.getData(sentencia, "ARTICULOS");
            DataTable tArticulos = resultado.Tables["ARTICULOS"];
            foreach (DataRow row in tArticulos.Rows)
            {
                int referencia = Convert.ToInt32(row["REFERENCIA"]);
                //int stock = Convert.ToInt32(row["STOCK"]);
                int id = Convert.ToInt32(row["IDARTICULO"]);
                String nombre = Convert.ToString(row["NOMBRE"]);
                String composicion = Convert.ToString(conexion.DLookUp("COMPOSICION", "COMPOSICIONES", "IDCOMPOSICION=" + row["REFCOMPOSICION"]));
                String medida = Convert.ToString(conexion.DLookUp("MEDIDA", "MEDIDAS", "IDMEDIDA=" + row["REFMEDIDA"]));
                String precio = Convert.ToString(row["PRECIO"]);
                dgvArticulos.Rows.Add(referencia, nombre,composicion, medida, precio, id);

            } // Fin del bucle for each
            limpiarSeleccion();
        }

        public void limpiarTabla()
        {
            // Limpiamos el datagridView
            while (dgvArticulos.RowCount > 0)
            {
                dgvArticulos.Rows.Remove(dgvArticulos.CurrentRow);
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            limpiarCampos();
            rbNoEliminados.Checked = true;
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            ArticulosTotales add = ArticulosTotales.Instance(conexion, 0, this, idUsuario,0);
            add.Show();
            this.Close();
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (dgvArticulos.CurrentRow == null || dgvArticulos.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                int fila = dgvArticulos.CurrentRow.Index;
                int idarticulo = Convert.ToInt32(dgvArticulos.Rows[fila].Cells[5].Value.ToString());
                //VAMOS A COMPROBAR QUE SEA COMPUESTO O SIMPLE
                int simple = Convert.ToInt32(conexion.DLookUp("refcomposicion", "ARTICULOS", "idarticulo=" + idarticulo));
                //Lo comparacmos con 12 porque es el idcomposicion de PACK, luego es compuesto
                if (simple != 12)
                {
                    AddNuevoArticulo add = new AddNuevoArticulo(conexion, 1, this, idUsuario, 0);
                    add.activarReferencia(false);
                    add.rellenar(dgvArticulos.CurrentRow);
                    add.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No se pueden modificar PACK");
                }
                cargarDGVInicio();
            }   
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.RowCount == 0)
            {
                MessageBox.Show("No hay articulos para borrar");
                return;
            }
            if (dgvArticulos.CurrentRow == null || dgvArticulos.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                DialogResult result = MessageBox.Show("¿Seguro que desea eliminar el articulo?", "Eliminar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    
                    int idarticuloseleccionado = Convert.ToInt32(dgvArticulos.CurrentRow.Cells[5].Value.ToString());
                    String sentencia = "UPDATE ARTICULOS SET ELIMINADO=1 WHERE IDARTICULO=" + idarticuloseleccionado;

                    MessageBox.Show(""+idarticuloseleccionado);
                    conexion.setData(sentencia);
                
                    //insert en la tabla historial de cambios
                    //pedimos confirmacion
                    String nombreArticulo = Convert.ToString(conexion.DLookUp("NOMBRE", "ARTICULOS", " IDARTICULO = " + idarticuloseleccionado));
                    String mensaje = Interaction.InputBox("¿Motivo por el cual se elimina?", "Motivo", "");
                    mensaje = "Articulo borrado ->" + nombreArticulo + " Motivo ->" + mensaje;

                    insert.insertHistorialCambio(idUsuario, 3, mensaje);
                    // Actualiza tabla
                    cargarDGVInicio();
                    //dgvArticulos.Rows.RemoveAt(dgvArticulos.CurrentRow.Index);
                    limpiarSeleccion();
                    filtrar();
                }
            }
        }

        
        private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
            if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //filtrar();
        }

       
        private void limpiarSeleccion()
        {
            dgvArticulos.ClearSelection();
            dgvArticulos.Update();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.RowCount == 0)
            {
                MessageBox.Show("No hay articulos para restaurar");
                return;
            }
            if (dgvArticulos.CurrentRow == null || dgvArticulos.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                DialogResult result = MessageBox.Show("¿Seguro que desea restaurar el articulo?", "Restaurar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    int idarticuloseleccionado = Convert.ToInt32(dgvArticulos.CurrentRow.Cells[5].Value.ToString());
                    String sentencia = "UPDATE ARTICULOS SET ELIMINADO=0 WHERE IDARTICULO=" + idarticuloseleccionado;

                    conexion.setData(sentencia);

                    //insert en tabla historial cambios
                    String nombreArticulo = Convert.ToString
                                            (conexion.DLookUp("NOMBRE", "ARTICULOS", " IDARTICULO = " + idarticuloseleccionado));
                    //MessageBox.Show("sentencia" + nombreArticulo);
                    insert.insertHistorialCambio(idUsuario, 4, "Articulo restaurado ->" + nombreArticulo);
                    rbNoEliminados.Checked = true;
                    // Actualiza tabla
                    filtrar();
                }
            }

        }

        

        private void rbEliminados_Click(object sender, EventArgs e)
        {
            if (rbEliminados.Checked == true)
            {
                btnBorrar.Enabled = false;
                btnRestaurar.Enabled = true;
                rbNoEliminados.Checked = false;
                rbEliminados.Checked = true;
            }
        }

        private void rbNoEliminados_CheckedChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void ArticulosForm_Shown(object sender, EventArgs e)
        {
            dgvArticulos.ClearSelection();
            dgvArticulos.Update();

        }

        
        
        private void ArticulosForm_Load(object sender, EventArgs e)
        {
            cargarCombos(cbMedida);
            if (numero == 1)
            {
                txtCantidad.Visible = true;
                lblCantidad.Visible = true;
                label1.Visible = true;
            }

            ToolTip tTArticulos = new ToolTip();
            tTArticulos.SetToolTip(btnAñadir, "Añadir Artículo");
            tTArticulos.SetToolTip(btnModificar, "Modificar Artículo");
            tTArticulos.SetToolTip(btnRestaurar, "Restaurar Artículo");
            tTArticulos.SetToolTip(btnLimpiar, "Limpiar Campos");
            tTArticulos.SetToolTip(btnBorrar, "Borrar Artículo");
            tTArticulos.SetToolTip(botonImprimir, "Mostrar datos de un articulo de la tabla");
            tTArticulos.SetToolTip(botonTSimples, "Mostrar todos los articulos indivisibles que existen");
            tTArticulos.SetToolTip(botonTCompuestos, "Mostrar todos los pack de articulos que existen");
            tTArticulos.SetToolTip(btnSalir, "Salir");
        }

        private void cbMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Es igual a 2 porque es la señal que le pasamos desde articuloscompuestos para que 
             no seleccione los pack*/
            if(señal!=2 && cbMedida.SelectedIndex>0)
                filtrar();
        }

        private void rbEliminados_CheckedChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void rbNoEliminados_Click(object sender, EventArgs e)
        {
            if (rbNoEliminados.Checked == true)
            {
                btnBorrar.Enabled = true;
                btnRestaurar.Enabled = false;
                rbNoEliminados.Checked = true;
                rbEliminados.Checked = false;
            }
        }

        
        private void dgvArticulos_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int stock = Convert.ToInt32(conexion.DLookUp("STOCKREAL", "ARTICULOSSTOCK", "IDARTICULO=" + Convert.ToInt32(dgvArticulos.CurrentRow.Cells[5].Value.ToString())));
            
            /*PEDIDOS*/
            if (numero == 1)
            {
                //controlamos que no se pueda realizar un pedido de un articulo por una cantidad superior al stock
                if (txtCantidad.Value > stock)
                {
                    MessageBox.Show("No se pueden pedir tantos articulos, el max en stock es: " + stock);
                    return;
                }
                if (txtCantidad.Value != 0)
                {
                    lblCantidad.ForeColor = Color.Green;
                    /*SEGUNDA COSA QUE HACE AL AÑADIR ARTICULO AL PEDIDO*/
                    AddPedido.idArticulo = Convert.ToInt32(dgvArticulos.CurrentRow.Cells[5].Value.ToString());
                    AddPedido.nombreArticulo = dgvArticulos.CurrentRow.Cells[1].Value.ToString();
                    AddPedido.cantidadArticulo = Convert.ToInt32(txtCantidad.Value.ToString());
                    AddPedido.precioArticulo = Math.Round(Convert.ToSingle(dgvArticulos.CurrentRow.Cells[4].Value.ToString()), 2);
                    pedido.nuevoArticulo();
                    return;

                }
                else
                {
                    MessageBox.Show("La cantidad no puede tener un valor de 0");
                    lblCantidad.ForeColor = Color.Red;
                    dgvArticulos.ClearSelection();
                    return;
                }

            }
            /*FACTURAS*/
            if (señal == 3)
            {
                AddFactura.nombreArticulo = dgvArticulos.CurrentRow.Cells[1].Value.ToString();
                AddFactura.cantidad = Convert.ToString(txtCantidad.Value.ToString());
                AddFactura.precio = Convert.ToString(Math.Round(Convert.ToSingle(dgvArticulos.CurrentRow.Cells[5].Value.ToString()), 2));
                MessageBox.Show("Articulo añadido");

                this.Close();
            }
            //Llamada de Articulos compuestos
            if (señal == 2)
            {
                
                this.articComp.cargarArticuloSimple(Convert.ToInt32(dgvArticulos.CurrentRow.Cells[5].Value.ToString()));
                MessageBox.Show(this, "Articulo añadido", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        
        private void botonImprimir_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null || dgvArticulos.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                int fila = dgvArticulos.CurrentRow.Index;
                int idarticulo = Convert.ToInt32(dgvArticulos.Rows[fila].Cells[5].Value.ToString());
                //VAMOS A COMPROBAR QUE SEA COMPUESTO O SIMPLE
                int simple = Convert.ToInt32(conexion.DLookUp("refcomposicion", "ARTICULOS", "idarticulo=" + idarticulo));
                //Lo comparacmos con 12 porque es el idcomposicion de PACK, luego es compuesto
                if (simple != 12)
                {
                    ImprimirArticulosSimples imprimir = new ImprimirArticulosSimples(conexion, idarticulo);
                    imprimir.ShowDialog();
                }
                else
                {
                    //Sacamos el nombre de la tabla
                    String nombre = Convert.ToString(dgvArticulos.Rows[fila].Cells[1].Value.ToString());
                    //Buscamos el idcompuesto
                    //FROM-->articulos art, articulospartes ap
                    //WHERE-->art.idarticulo=ap.idcompuesto and art.idarticulo<>ap.idarticulo and art.nombre like 'nombre'
                    //int idarticuloCompuesto = Convert.ToInt32(conexion.DLookUp("idarticulo", "articulos art, articulospartes ap", "art.idarticulo=ap.idcompuesto and art.idarticulo<>ap.idarticulo and art.nombre like '"+nombre+"'"));
                    ImprimirCompuestos imprimir = new ImprimirCompuestos(conexion, idarticulo);
                    imprimir.ShowDialog();
                }
            }
        }

        private void botonTSimples_Click(object sender, EventArgs e)
        {
            ImprimirTodosSimples simples = new ImprimirTodosSimples(conexion);
            simples.ShowDialog();
        }

        private void botonTCompuestos_Click(object sender, EventArgs e)
        {
            ImprimirTodosCompuestos compuestos = new ImprimirTodosCompuestos(conexion);
            compuestos.ShowDialog();
        }

        private void txtReferencia_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        
    }
}
