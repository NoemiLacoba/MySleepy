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
    public partial class AddFactura : Form
    {
        ConnectDB conexion;
        private static int idfactura;
        //idpedido -> a partir del idFactura
        private static int idpedido;
        //idpedidoArticulo -> a partir del idpedido
        private static int idpedidoarticulo;
        //el idarticulo se guardara cuando le de al boton "+"


        //DATOS DE ARTICULO
        public static String nombreArticulo;
        public static String cantidad;
        public static String precio;

        //Para guardar en el historial de cambios
        InsertHistorial insertHistorial;
        /*Variables de la interfaz del cliente*/
        public static String nombreCliente;
        public static String apellido1Cliente;
        public static String apellido2Cliente;
        public static int idCliente;
        private int idUsuario;
        private int añadirEnFacturasArticulos;
        private static int señal;
        
        public AddFactura(ConnectDB conexionFA, int facturaArticulo,int idpedido,int señalFA,int idUser)
        {
            InitializeComponent();
            señal = señalFA;
            idUsuario = idUser;
            conexion = conexionFA;
            ToolTip tool = new ToolTip();
            añadirEnFacturasArticulos = facturaArticulo;
            tool.SetToolTip(btnAñadir, "Añadir Factura");
            tool.SetToolTip(btnBuscarCliente, "Buscar cliente para factura");
            tool.SetToolTip(btnSalir, "Salir al menu");
            tool.SetToolTip(btnRealizar, "Guardar datos en factura");
            tool.SetToolTip(btnLimpiar, "Limpiar campos");
            tool.SetToolTip(btnBuscarArticulo, "Buscar articulo");
            insertHistorial = new InsertHistorial(conexion);
            //MessageBox.Show("IDFACTURA=" + facturaArticulo);
            //Esto es para añadir factura
            if (señal == 0)
            {
                idfactura = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "idfacturaarticulos=" + facturaArticulo));
                if (idfactura == -1)
                    idfactura = facturaArticulo;
                else
                    idfactura = MetodosAuxiliares.ultimoID(conexion, "idfacturaarticulos", "FACTURASARTICULOS");
                
            }
            //Para modificar factura
            if (señal == 1)
            {
                if (idpedido != 0)
                {
                    idfactura = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "idfacturaarticulos=" + facturaArticulo));
                    if (idfactura == -1)
                    {
                        idfactura = facturaArticulo;
                        idCliente = Convert.ToInt32(conexion.DLookUp("idcliente", "FACTURAS", "idfactura=" + facturaArticulo));
                        cargarCliente(idCliente);
                    }
                    else
                    {
                        idfactura = MetodosAuxiliares.ultimoID(conexion, "idfacturaarticulos", "FACTURASARTICULOS");
                        idpedido = Convert.ToInt32(conexion.DLookUp("idpedido", "facturas", " idfactura = " + facturaArticulo));
                        idCliente = Convert.ToInt32(conexion.DLookUp("idcliente", "FACTURAS", "idfactura=" + facturaArticulo));
                        cargarCliente(idCliente);
                    }
                        
                }
                else
                {
                    
                    idfactura = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "idfacturaarticulos=" + idfactura));
                    if (idfactura == -1){
                        idfactura = facturaArticulo;
                         idCliente = Convert.ToInt32(conexion.DLookUp("idcliente", "FACTURASARTICULOS", "idfacturaarticulos=" + facturaArticulo));
                        cargarCliente(idCliente);
                    } else
                    {
                        idfactura = MetodosAuxiliares.ultimoID(conexion, "idfacturaarticulos", "FACTURASARTICULOS");
                        idCliente = Convert.ToInt32(conexion.DLookUp("idcliente", "FACTURASARTICULOS", "idfacturaarticulos=" + facturaArticulo));
                        cargarCliente(idCliente);
                    }
                        
                }
                
            }
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            btnRealizar.Enabled = true;
            String nombre = txtNombre.Text;
            String refer = txtCantidad.Text;
            String  p = txtPrecio.Text;
            int referencia,cantidad;
            double precio;

            if (txtNombre.Text.Equals("") || txtPrecio.Text.Equals("") || txtCantidad.Text.Equals(""))
            {
                MessageBox.Show("Rellene todos los campos");
            }
            else
            {
                
                //convertimos a entero y double la referencia y precio
                referencia = Convert.ToInt32(refer);
                precio = Math.Round(Convert.ToSingle(p),2);
                cantidad = Convert.ToInt16(txtCantidad.Text);
                //añadir en datagridview, idArticulo -> 0 puesto que es el generico en nuestra BBDD
                dgvArticulosFactura.Rows.Add(idfactura, idpedido, idpedidoarticulo, 0, cantidad, nombre
                    , precio, (precio*cantidad));

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*Debemos guardar en varios sitios*/
        private void btnRealizar_Click(object sender, EventArgs e)
        {
            //Añadir en las tablas por primera vez
            /*Tenemos que ver donde cargarla en la tabla facturas, porque de ahí dependerá el numFactura*/
            if (señal == 0)
            {
                int numFactura = ContadorNumFactura();
                if (caja_nombre.Text.Equals(""))
                {
                    MessageBox.Show("Introduzca el cliente");
                }
                else
                {
                    double precio = contarPrecio();
                    String insertarFacturasArticulos = "insert into facturasarticulos values(" + idfactura + "," + numFactura +","+
                        idCliente + "," + MetodosAuxiliares.devolverFechaActual() + "," + MetodosAuxiliares.devolverHora() + ",'"+
                        precio+"','N',0,"+añadirEnFacturasArticulos+")";
                    
                    conexion.setData(insertarFacturasArticulos);

                    
                    
                    /*Leer el datagridview y guardar en la tabla articulos genericos*/

                    String insertatarticulosGen="";
                    int cantidad=0;
                    double precioAG=0;
                    String nombreArticulo="";
                    for (int i = 0; i<dgvArticulosFactura.RowCount; i++)
                    {
                        cantidad=Convert.ToInt32(dgvArticulosFactura.Rows[i].Cells[4].Value.ToString());
                        precioAG=Convert.ToSingle(dgvArticulosFactura.Rows[i].Cells[6].Value.ToString());
                        nombreArticulo=dgvArticulosFactura.Rows[i].Cells[5].Value.ToString();
                        insertatarticulosGen = "insert into articulosgenericos values(" +
                        MetodosAuxiliares.ultimoID(conexion, "idarticulogenerico", "ARTICULOSGENERICOS") + "," + idfactura + ",'"
                        + nombreArticulo + "','" +
                        precioAG + "'," + cantidad + ",0)";
                        conexion.setData(insertatarticulosGen);

                        //contemplamos los articulos añadidos al historial de cambios
                        insertHistorial.insertHistorialCambio(idUsuario, 1, "Articulo generico añadido : " + nombreArticulo);

                    }
                    MessageBox.Show("Añadida factura y articulos");

                    //añadimos los cambios al historial -> factura
                    insertHistorial.insertHistorialCambio(idUsuario, 1, "Factura añadida : " + numFactura);
                    
                    this.Close();
                }
                
            }
            //Modificar en las tablas
            if (señal == 1)
            {
                /*Actualizamos el precio en la factura*/
                double precioTabla = contarPrecio();
                String updateFactura = "update facturas set importeneto=importeneto+'" + precioTabla + "' where idfactura=" + idfactura;
                conexion.setData(updateFactura);
                int numFacturaModificada = Convert.ToInt32(conexion.DLookUp("numfactura", "facturas", " idfactura = " + idfactura));
                /*Añadimos en la tabla facturasarticulos*/
                double precio = contarPrecio();
                int numFacturaArticulo = ContadorNumFactura();
                String insertarFacturasArticulos = "insert into facturasarticulos values(" + idfactura + "," + numFacturaArticulo + "," +
                    idCliente + "," + MetodosAuxiliares.devolverFechaActual() + "," + MetodosAuxiliares.devolverHora() + ",'" +
                    precio + "','N',0,"+añadirEnFacturasArticulos+")";
                
                conexion.setData(insertarFacturasArticulos);

                /*AÑADIMOS EL ARTICULO NUEVO EN LA TABLA DE ARTICULOS GENÉRICOS*/
                String insertatarticulosGen = "";
                int cantidad = 0;
                double precioAG = 0;
                String nombreArticulo = "";
                for (int i = 0; i < dgvArticulosFactura.RowCount; i++)
                {
                    cantidad = Convert.ToInt32(dgvArticulosFactura.Rows[i].Cells[4].Value.ToString());
                    precioAG = Convert.ToSingle(dgvArticulosFactura.Rows[i].Cells[6].Value.ToString());
                    nombreArticulo = dgvArticulosFactura.Rows[i].Cells[5].Value.ToString();
                    insertatarticulosGen = "insert into articulosgenericos values("+
                        MetodosAuxiliares.ultimoID(conexion, "idarticulogenerico", "ARTICULOSGENERICOS") + "," + idfactura+",'"
                        + nombreArticulo + "','" +
                        precioAG + "'," + cantidad + ",0)";
                   
                    conexion.setData(insertatarticulosGen);

                    //contemplamos los articulos en historial de cambios
                    insertHistorial.insertHistorialCambio(idUsuario, 1, "Articulo generico añadido :" + nombreArticulo);

                }
                MessageBox.Show("Modificada factura y los articulos");

                //actualizamos el historial de cambios
                insertHistorial.insertHistorialCambio(idUsuario, 2, "Factura modificada : " + numFacturaModificada);
                insertHistorial.insertHistorialCambio(idUsuario, 1, "Factura añadida : " + numFacturaArticulo);
                this.Close();
            }
            
        }

        //Cargamos datos del cliente
        public void cargarCliente(int idcliente)
        {
            String nombre = Convert.ToString(conexion.DLookUp("nombre", "CLIENTES", "idcliente=" + idcliente));
            String apellido1 = Convert.ToString(conexion.DLookUp("apellido1", "CLIENTES", "idcliente=" + idcliente));
            String apellido2 = Convert.ToString(conexion.DLookUp("apellido2", "CLIENTES", "idcliente=" + idcliente));
            String direccion = Convert.ToString(conexion.DLookUp("direccion", "CLIENTES", "idcliente=" + idcliente));
            caja_nombre.Text = nombre;
            caja_apellido1.Text = apellido1;
            caja_apellido2.Text = apellido2;  
        }

        //Sumamos los precios totales
        private double contarPrecio()
        {
            double precio=0;
            for (int i=0;i<dgvArticulosFactura.RowCount;i++){
                precio=precio+Convert.ToSingle(dgvArticulosFactura.Rows[i].Cells[7].Value.ToString());
            }
            precio = Math.Round(precio, 2);
            return precio;
        }
       
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            MetodosAuxiliares.cajaDecimal(txtPrecio, e, 6, 2);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar.Equals(' ') || Char.IsDigit(e.KeyChar)
                || e.KeyChar.Equals(',') || e.KeyChar.Equals('.'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
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

        private int ContadorNumFactura()
        {
            int numF = 0, contadorNFA = 0;
            int idfactura = MetodosAuxiliares.ultimoID(conexion, "idfactura", "FACTURAS");
            int hayRegistroFactura = Convert.ToInt32(conexion.DLookUp("idfactura", "FACTURAS", "idfactura=1 and eliminado = 0"));
            int hayRegistroFA = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "idfacturaarticulos=1 and eliminada = 0"));
            int contadorNFactura = 0;
            if (hayRegistroFactura == -1 && hayRegistroFA == -1)
            {
                numF = 20150001;
            }
            else
            {
                contadorNFactura = MetodosAuxiliares.ultimoNumeroFactura(conexion, "numfactura", "FACTURAS","eliminado=0");
                contadorNFA = MetodosAuxiliares.ultimoNumeroFactura(conexion, "numfactura", "FACTURASARTICULOS","eliminada=0");
                if (hayRegistroFA != -1 && hayRegistroFactura == -1)
                {
                    
                    //Si el contador en la tabla de facturasarticulos es mayor se pone ese, sino el otro
                    //de facturas
                    if (contadorNFA > contadorNFactura)
                    {
                        numF = contadorNFA;
                    }
                    else
                    {
                        numF = contadorNFactura;
                    }
                }
                else
                {
                    if (hayRegistroFA == -1 && hayRegistroFactura != -1)
                    {

                        //Si el contador en la tabla de facturasarticulos es mayor se pone ese, sino el otro
                        //de facturas
                        if (contadorNFA > contadorNFactura)
                        {
                            numF = contadorNFA;
                        }
                        else
                        {
                            numF = contadorNFactura;
                        }
                    }
                    else
                    {
                        //Si el contador en la tabla de facturasarticulos es mayor se pone ese, sino el otro
                        //de facturas
                        if (contadorNFA > contadorNFactura)
                        {
                            numF = contadorNFA;
                        }
                        else
                        {
                            numF = contadorNFactura;
                        }
                    }
                }

            }
            return numF;
        }

        public void setIdCliente(int idC)
        {
            idCliente = idC;
        }

        public void setNombre(String nombreCliente)
        {
            caja_nombre.Text = nombreCliente;
        }
        public void setApellido1(String ape1)
        {
            caja_apellido1.Text = ape1;
        }
        public void setApellido2(String ape2)
        {
            caja_apellido2.Text = ape2;
        }
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            ClientesForm cliente = ClientesForm.Instance(3,conexion,this);
            cliente.Show();
            return;
        }

        private void AddFactura_Load(object sender, EventArgs e)
        {
            btnRealizar.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            
            txtCantidad.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
        }

        private void btnBuscarArticulo_Click(object sender, EventArgs e)
        {
            ArticulosForm articulos = ArticulosForm.Instance(3,conexion,this,idUsuario);
            articulos.ShowDialog();
            cargarDatosArticulos();
        }

        public void cargarDatosArticulos()
        {
            txtCantidad.Text = cantidad;
            txtNombre.Text = nombreArticulo;
            txtPrecio.Text = precio;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
