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
    public partial class PrincipalForm : Form
    {
        ConnectDB conexion;
        ClientesForm clientes;
        UsuariosForm usuarios;
        ArticulosForm articulos;
        PedidosForm pedidos;
        Proveedores proveedores;
        HistorialForm historial;
        Facturas facturacion;
        GestionProveedor gestion;
        Stock stock;
        DataSet ds;
        Caja caja;
        int idRol;
        int idUsuario;
        public PrincipalForm(int idUsuario,int idRol, ConnectDB c,String nombre)
        {
            InitializeComponent();
            this.BackColor = Color.Azure;
            this.conexion = c;
            this.idRol = idRol;
            this.idUsuario = idUsuario;
            tipoRol(nombre);
            XML_proveedor.cargarBBDDXML("proveedor.xml", "PROVEEDORES", c);
            ds = new DataSet();
            ds = c.getData("SELECT * FROM PROVEEDORES", "PROVEEDORES");
        }

        public PrincipalForm()
        {
            //InitializeComponent();
        }

        private void tipoRol(String nombre)
        {
            String etiqueta = "Usted se ha identificado como "+nombre;
            lblTipoUsuario.Text = etiqueta;
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (idRol == 1 || idRol == 2)
            {
                if (historial == null || historial.IsDisposed)
                {
                    usuarios = UsuariosForm.Instance(idRol, conexion, idUsuario);
                    usuarios.MdiParent = this;
                    usuarios.SetDesktopLocation(-1, -1); // saldra en la esquina
                    usuarios.WindowState = FormWindowState.Normal;
                    usuarios.Show();
                    usuarios.Focus();
                }
                else
                {
                    MessageBox.Show("Debe cerrar la ventana historial");
                }
            }
            else
            {
                MessageBox.Show("Los operativos no pueden acceder al menú Usuarios");
            }
           
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] ventanas = { pedidos,historial};
            if (ventanasCerradas(ventanas) == true)
            {
                clientes = ClientesForm.Instance(idRol, conexion, idUsuario);
                clientes.MdiParent = this;
                clientes.SetDesktopLocation(-1, -1); // saldra en la esquina
                clientes.WindowState = FormWindowState.Normal;
                clientes.Show();
                clientes.Focus();
            }
            else
            {
                MessageBox.Show("Debe cerrar las ventanas Pedidos y/o Historial");
            }
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] ventanas = { pedidos,historial};
            if (ventanasCerradas(ventanas) == true)
            {
                articulos = ArticulosForm.Instance(idRol, conexion, idUsuario);
                articulos.MdiParent = this;
                articulos.SetDesktopLocation(-1, -1); // saldra en la esquina
                articulos.WindowState = FormWindowState.Normal;
                articulos.Show();
                articulos.Focus();
            }
            else
            {
                MessageBox.Show("Debe cerrar las ventanas Pedidos y/o Historial");
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cargarProveedorBBDD("proveedor.xml");
            Application.Exit();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            cargarProveedorBBDD("proveedor.xml");
            Application.Exit();
        }
        private void PrincipalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargarProveedorBBDD("proveedor.xml");
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] ventanas = { clientes, articulos, historial, proveedores };
            if (ventanasCerradas(ventanas) == true)
            {
                pedidos = PedidosForm.Instance(idRol, conexion, idUsuario);
                pedidos.MdiParent = this;
                pedidos.SetDesktopLocation(-1, -1); // saldra en la esquina
                pedidos.WindowState = FormWindowState.Normal;
                pedidos.Show();
                pedidos.Focus();
            }
            else
            {
                MessageBox.Show("Debe cerrar todas las ventanas exceptuando Usuarios");
            }    
            
                
           
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.fondo;
         
            if (idRol == 3)
            {
                historialToolStripMenuItem.Visible = false;
            }
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pedimos confirmacion
                DialogResult opcion = MessageBox.Show("¿Desea cerrar sesión?", "Confirmación",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (opcion == DialogResult.OK)
                {
                    //Cerramos la ventana principal y mostramos la ventana de AccesoForm
                    AccesoForm acceso = AccesoForm.Instance();
                    acceso.Show();

                    this.Visible = false;
                }
        }

        private void historialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] ventanas = { pedidos,usuarios,articulos,clientes,proveedores,caja};
            if (ventanasCerradas(ventanas) == true)
            {
                historial = HistorialForm.Instance(idUsuario, conexion);
                historial.MdiParent = this;
                historial.SetDesktopLocation(-1, -1); // saldra en la esquina
                historial.WindowState = FormWindowState.Normal;
                historial.Show();
                historial.Focus();
                
            }
            else
            {
                MessageBox.Show("Debe cerrar el resto de ventanas para acceder al historial");
            }
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] ventanas = { pedidos, historial };
            if (ventanasCerradas(ventanas))
            {
                proveedores = Proveedores.Instance(idRol, conexion, idUsuario,ds); //le pasamos ademas el DataSet
                proveedores.MdiParent = this;
                proveedores.SetDesktopLocation(-1, -1);
                proveedores.WindowState = FormWindowState.Normal;
                proveedores.Show();
                proveedores.Focus();
            }
            else
            {
                MessageBox.Show("Debe cerrar las ventanas Pedidos e Historial");
            }
        }

        private Boolean ventanasCerradas(Form[] ventanas)
        {
            Boolean cerrado =false;
            for (int i = 0; i < ventanas.Length; i++)
            {
                if (ventanas[i] == null || ventanas[i].IsDisposed )
                {
                    cerrado = true;
                }
                else
                {
                    return false;
                }
            }
            return cerrado;
        }

        private void cargarProveedorBBDD(String rutaXml)
        {
            try
            {
                String[,] datos = XML_proveedor.leerXMLDataSet(rutaXml, true);
                if (datos != null)
                {
                    int idProveedor;
                    String sentencia;
                    for (int i = 0; i < datos.GetLength(0); i++)
                    {

                        idProveedor = Convert.ToInt32(conexion.DLookUp("IDPROVEEDOR", "PROVEEDORES", "IDPROVEEDOR = 1"));
                        if (idProveedor == -1) { idProveedor = 0; Console.WriteLine(idProveedor); }
                        else { idProveedor = Convert.ToInt32(conexion.DLookUp("MAX(IDPROVEEDOR)", "PROVEEDORES", "")) + 1; }
                        if (Convert.ToInt32(datos[i, 0]) < idProveedor)
                        {
                            Console.WriteLine("UPDATE " + idProveedor);
                            Console.WriteLine(datos[i, 0]);
                            sentencia = "UPDATE PROVEEDORES set CIF = '" + datos[i, 1] + "',NOMBRE = '" + datos[i, 2] +
                                "', DIRECCION = '" + datos[i, 3] + "', REFCPPOBLACIONES = " + datos[i, 4] + ",TELEFONO = " + datos[i, 5] +
                                ",ELIMINADO= " + datos[i, 6] + ", NIF ='" + datos[i, 7] + "', MOVIL ="+datos[i,8]+", PAIS ='"+datos[i,9] +
                                "', EMAIL = '"+datos[i,10]+"', CONTACTO='"+datos[i,11]+"', OBSERVACIONES='"+datos[i,12]+"' WHERE IDPROVEEDOR=" + datos[i, 0];
                        }
                        else
                        {
                            Console.WriteLine("ENTRO EN INSERT");
                            Console.Write(" " + datos[i, 0]);
                            sentencia = "INSERT INTO PROVEEDORES (IDPROVEEDOR,CIF,NOMBRE,DIRECCION,REFCPPOBLACIONES,TELEFONO,ELIMINADO,NIF,MOVIL,PAIS,EMAIL)" +
                                        " VALUES(" + datos[i, 0] + ",'" + datos[i, 1] + "','" + datos[i, 2] + "','" + datos[i, 3] + "'," + datos[i, 4] +
                                        "," + datos[i, 5] + "," + datos[i, 6] + ",'" + datos[i, 7] + "',"+datos[i,8]+",'"+datos[i,9]+"','"+datos[i,10]+"','"+datos[i,11]+"','"+datos[i,12]+"')";
                        }
                        conexion.setData(sentencia);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex.Message);
            }
        }

        private void cAJAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caja = Caja.Instance(conexion, idUsuario);
            caja.MdiParent = this;
            caja.SetDesktopLocation(-1, -1); // saldra en la esquina
            caja.WindowState = FormWindowState.Normal;
            caja.Show();
            caja.Focus();
        }

        private void cAJAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void facturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            facturacion = Facturas.Instance(idRol,conexion, idUsuario);
            facturacion.MdiParent = this;
            facturacion.SetDesktopLocation(-1, -1); // saldra en la esquina
            facturacion.WindowState = FormWindowState.Normal;
            facturacion.Show();
            facturacion.Focus();
        }

        private void gestiónDeProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gestion = GestionProveedor.Instance(idRol,conexion, idUsuario);
            gestion.MdiParent = this;
            gestion.SetDesktopLocation(-1, -1); // saldra en la esquina
            gestion.WindowState = FormWindowState.Normal;
            gestion.Show();
            gestion.Focus();
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stock = Stock.Instance(idRol, conexion, idUsuario);
            stock.MdiParent = this;
            stock.SetDesktopLocation(-1, -1); // saldra en la esquina
            stock.WindowState = FormWindowState.Normal;
            stock.Show();
            stock.Focus();
        }
    }
}
