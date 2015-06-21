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
    public partial class AddPedido : Form
    {
        ConnectDB conexion;
        InsertHistorial insert;
        PedidosForm fPedidosPrincipal;
        double precio,totalpedido;
        int id_pedido, id_articulo_añadir, idCliente, id_rol, idUsuario, señal,f_pago;
        String n_pedido, cliente, nombre_articulo_añadir, cantidad, n_pedido_modificar;
        public static int idArticulo,cantidadArticulo;
        public static double precioArticulo;
        public static String nombreArticulo;
        ////////////////////////////////////////////////////////////////////////
        ///////////////// CONSTRUCTORES /////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////

        public AddPedido(ConnectDB c, int idrol, int idUsuario, int señal)
        {
            InitializeComponent();
            conexion = c;
            id_pedido = Convert.ToInt32(MetodosAuxiliares.ultimoID(conexion, "idpedido", "pedidos"));
            this.id_rol = idrol;
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
            this.señal = señal;
        }

        private void AddPedido_Load(object sender, EventArgs e)
        {
            generarNumero();
            cargarComboFormasPago();
            if (señal == 1)
            {
                // Modificar pedido
                dpFecha.Enabled = false;
                gbCliente.Enabled = false;
                cbFormaPago.Enabled = false;
                cbPagado.Enabled = false;  //se pagará con la funcion implementada en el boton Pagar
                txtNumeroPedido.Text = n_pedido_modificar;
                cbFormaPago.SelectedIndex = f_pago;
            }

            ToolTip tTPedidosA = new ToolTip();
            tTPedidosA.SetToolTip(btnAddArticulo, "Añadir articulo en el pedido");
            tTPedidosA.SetToolTip(btnModificarArticulo, "Modificar Articulo");
            tTPedidosA.SetToolTip(btnBuscarCliente, "Buscar Cliente");
            tTPedidosA.SetToolTip(btnCancelarPedido, "Cancelar pedido");
            tTPedidosA.SetToolTip(btnRealizar, "Guardar pedido con los artículos");
            tTPedidosA.SetToolTip(btnSalir, "Salir");
        }

        ////////////////////////////////////////////////////////////////////////
        ///////////////// LISTENER BOTONES  //////////////////////////////////
        ////////////////////////////////////////////////////////////////////////

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificarArticulo_Click(object sender, EventArgs e)
        {
            //PARA MODIFICAR UN ARTICULO en el pedido
            ArticulosForm articulos = ArticulosForm.Instance(id_rol, 1, this, conexion, idUsuario);
            articulos.Show();
            //Borramos la fila actual y se añade el pedido
            try
            {
                dgvPedidos.Rows.RemoveAt(dgvPedidos.CurrentRow.Index);
                txtTotalPedido.Text = Convert.ToString(sumarPrecios());
            }
            catch (Exception ex)
            {

            }
            
        }

        /*Recorre el datagridview para ver si existe el articulo con el nombre de dicho articulo*/
        private int recorrerDatagridView(String nombreArticulo)
        {

            int seguir = -1;
            for (int i = 0; i < dgvPedidos.RowCount; i++)
            {
                //MessageBox.Show(composicion+" "+cantidad+" "+precio);

                /*SI ENCUENTRA UN ARTICULO QUE SE LLAME IGUAL DEVUELVE LA FILA EN LA
                 QUE SE ENCUENTRA DICHO ARTICULO*/
                if (nombreArticulo.Equals(dgvPedidos.Rows[i].Cells[1].Value.ToString()))
                {

                    seguir = i;
                }
            }
            return seguir;
        }

        /*Método que si existe el articulo lo añade a la fila dada por el parámetro i*/
        private void añadirArticuloRepetido(int i, String nombreArticulo, int cantidad)
        {
            int cantidadA;
            double precioA;
            
            cantidadA = Convert.ToInt16(dgvPedidos.Rows[i].Cells[2].Value.ToString());
            precioA = Convert.ToSingle(dgvPedidos.Rows[i].Cells[3].Value.ToString());
            precioA = Math.Round(precioA, 2);
            //Insertar en el datadrig
            dgvPedidos.Rows[i].Cells[0].Value = txtNombre.Text;
            dgvPedidos.Rows[i].Cells[1].Value = nombreArticulo;
            dgvPedidos.Rows[i].Cells[2].Value = cantidadA+cantidad;

            dgvPedidos.Rows[i].Cells[3].Value = precioA + precioArticulo;
            //MENSAJE DEL PRECIO ACTUALIZADO
            //MessageBox.Show(dgvPedidos.Rows[i].Cells[3].Value.ToString());
            dgvPedidos.Rows[i].Cells[4].Value = idArticulo;
            txtTotalPedido.Text = Convert.ToString(sumarPrecios());
        }

        private void btnRealizar_Click(object sender, EventArgs e)
        {
            //Creo que esto esta mal.MODIFICAR
            if (señal == 1)
            {
                guardarPedido();
                return;
            }
            if (txtNombre.Text.Equals(""))
            {
                MessageBox.Show("Seleccione un cliente");
            }
            else if( cbFormaPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una forma de pago");
            }
            else if (dgvPedidos.RowCount == 0)
            {
                MessageBox.Show("Seleccione uno o mas articulos");
            }
            else if(cbFormaPago.SelectedIndex != -1 && txtNombre.Text.Length > 0)
            {
                guardarPedido();
            }
          
        }
 
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            ClientesForm clientes = ClientesForm.Instance(id_rol, 1, conexion, this, idUsuario);
            clientes.Show();
        }

        //Cuando vamos a añadir un articulo al pedido
        private void btnAddArticulo_Click(object sender, EventArgs e)
        {
            if (señal == 1)
            {
                // Modificando
                ArticulosForm articulos = ArticulosForm.Instance(id_rol, 1, this, conexion, idUsuario);
                articulos.Show();
                txtTotalPedido.Text = Convert.ToString(sumarPrecios());

                return;
            }
            if (txtNombre.Text != "")
            {
                /*PRIMERO EN HACERSE AL AÑADIR UN PEDIDO*/
                ArticulosForm articulos = ArticulosForm.Instance(id_rol, 1, this, conexion, idUsuario);
                articulos.Show();
                
                txtTotalPedido.Text = Convert.ToString(sumarPrecios());
            }
            else
            {
                MessageBox.Show("Debes seleccionar un cliente, porfavor");
            }
        }

        private void dpFecha_ValueChanged(object sender, EventArgs e)
        {
            generarNumero();
        }

        //Eliminar un articulo de un pedido
        private void button1_Click(object sender, EventArgs e)
        {

            if (dgvPedidos.CurrentRow == null || dgvPedidos.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                //Se resta la cantidad del pedido
                int fila=dgvPedidos.CurrentRow.Index;
                double precio = Convert.ToSingle(dgvPedidos.Rows[fila].Cells[3].Value.ToString());
                txtTotalPedido.Text = Convert.ToString(Math.Round((Convert.ToSingle(txtTotalPedido.Text) - precio),2));
                dgvPedidos.Rows.RemoveAt(dgvPedidos.CurrentRow.Index);
            }

        }

        public void cargarCliente(String idcliente)
        {
            
            String nombre = Convert.ToString(conexion.DLookUp("nombre", "CLIENTES", "idcliente=" + idcliente));
            String apellido1 = Convert.ToString(conexion.DLookUp("apellido1", "CLIENTES", "idcliente=" + idcliente));
            String apellido2 = Convert.ToString(conexion.DLookUp("apellido2", "CLIENTES", "idcliente=" + idcliente));
            String direccion = Convert.ToString(conexion.DLookUp("direccion", "CLIENTES", "idcliente=" + idcliente));
            String referencia = Convert.ToString(conexion.DLookUp("refcppoblaciones", "CLIENTES", "idcliente=" + idcliente));
            String idpoblacion = Convert.ToString(conexion.DLookUp("refpoblacion", "CODIGOSPOSTALESPOBLACIONES", "idcodigopostalpob=" + referencia));
            String poblacion = Convert.ToString(conexion.DLookUp("poblacion", "POBLACIONES", "idpoblacion=" + idpoblacion));
            txtNombre.Text = nombre;
            txtDireccion.Text = direccion;
            txtApellido1.Text = apellido1;
            txtApellido2.Text = apellido2;
            txtPoblacion.Text = poblacion;
            this.idCliente = Convert.ToInt32(idcliente);
        }

        public void nuevoArticulo()
        {
            /*TERCERA COSA QUE HACE AL AÑADIR UN ARTICULO EN PEDIDO*/
            this.id_articulo_añadir = idArticulo;
            this.nombre_articulo_añadir = nombreArticulo;
            this.precio = calcularPrecio(cantidadArticulo, precioArticulo);
            aumentarTotalPedido(this.precio);
            //MessageBox.Show(precio.ToString());
            this.cantidad = Convert.ToString(cantidadArticulo);
            /*Calculamos el precio del articulo y le sumamos la cantidad al total*/
            double precioCaja=Math.Round(sumarPrecios(),2);
            txtTotalPedido.Text = Convert.ToString(Math.Round(this.precio+sumarPrecios(),2));
            
            MessageBox.Show("Articulo añadido");
            /*LLAMADA A LA CUARTA PARTE*/
            actualizarDGV();
        }

        ////////////////////////////////////////////////////////////////////////
        ///////////////// METODOS PROGRAMA /////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        public void obtenerDatosCliente()
        {
            cliente = txtNombre.Text+","+txtApellido1.Text+" "+txtApellido2.Text;
            n_pedido = txtNumeroPedido.Text;
        }
        //Actualizamos la tabla cuando añadimos un articulo
        private void actualizarDGV()
        {
            /*CUARTA PARTE DE AÑADIR UN ARTICULO A UN PEDIDO*/
            obtenerDatosCliente();
            //Combrobamos que no existe en la tabla el articulo, si existe sumamos el precio y las cantidades
            int seguir=recorrerDatagridView(nombreArticulo);
            //Si no existe en la tabla lo añadimos
            if (seguir == -1)
            {
                dgvPedidos.Rows.Add(txtNombre.Text, nombre_articulo_añadir, cantidad, precio.ToString(), id_articulo_añadir);
            //Sino actualizamos la fila
            }else
            {
                añadirArticuloRepetido(seguir, nombreArticulo, cantidadArticulo);
            }
        }

        public void cargarComboFormasPago()
        {
            DataSet data = new DataSet();
            DataTable tabla = new DataTable();
            data = conexion.getData("SELECT FORMAPAGO FROM FORMASDEPAGO ORDER BY IDPAGO", "FORMASDEPAGO");
            tabla = data.Tables["FORMASDEPAGO"];
            foreach (DataRow row in tabla.Rows)
            {
                cbFormaPago.Items.Add(Convert.ToString(row["FORMAPAGO"]));
            }
        }

        public void generarNumero()
        {
            DateTime fechaD = dpFecha.Value;
            DateTime today = DateTime.Today;
            if (today >= fechaD)
            {
                MessageBox.Show("Selecciona otra fecha, no puede ser inferior al sistema");
            }
            else
            {
                String fcientifica = "";
                fcientifica = MetodosAuxiliares.devolverFecha(dpFecha) + id_pedido;
                txtNumeroPedido.Text = fcientifica;
            }
        }

        public void asignarFPedidos(PedidosForm f)
        {
            /*esto que es lo que es....*/
            this.fPedidosPrincipal = f;
        }
 
        public double calcularPrecio(int cantidad, double precio)
        {
            double res = cantidad * precio;
            return res;
        }

        public void aumentarTotalPedido(double p)
        {
            this.totalpedido = this.totalpedido + p;
            txtTotalPedido.Text = " " + this.totalpedido;
        }


        private void guardarPedido()
        {
            if (dgvPedidos.RowCount > 0 || cbFormaPago.SelectedIndex > 0)
            {
                String n_pedido, cliente, articulos, cantidad, precio, id_articulo;
                n_pedido = txtNumeroPedido.Text;
                cliente = txtNombre.Text+" "+txtApellido1.Text+" "+txtApellido2.Text;

                //PARA AÑADIR LA PRIMERA VEZ, no son modificaciones
                if (señal == 0)
                {
                    //METODO que añade al datagridview. FUNCIONA
                    añadirTablaPedido(cliente, txtTotalPedido.Text);
                }
                
                    //Recorre el datagridview
                    for (int i = 0; i < dgvPedidos.RowCount;i++ )
                    {
                        n_pedido = txtNumeroPedido.Text;
                        cliente = dgvPedidos.Rows[i].Cells[0].Value.ToString();
                        articulos = dgvPedidos.Rows[i].Cells[1].Value.ToString();
                        cantidad = dgvPedidos.Rows[i].Cells[2].Value.ToString();
                        precio = dgvPedidos.Rows[i].Cells[3].Value.ToString();
                        id_articulo = dgvPedidos.Rows[i].Cells[4].Value.ToString();

                        
                        if (señal == 0)
                        {
                            //LO ultimo es la señal
                            añadirPedido(n_pedido, cliente, articulos, cantidad, precio, id_articulo, 0);
                            
                        }
                        if (señal == 1)
                        {
                           
                            //a la hora de modificar
                            modificarPedido(n_pedido, articulos, cantidad, precio, id_articulo, 1);   
                        }
                     
                    }//Fin de recorrer tabla

                    if (señal == 1)
                    {

                        // Añade el pedido
                        int idpedido = Convert.ToInt32(conexion.DLookUp("idpedido", "PEDIDOS", "n_pedido=" + txtNumeroPedido.Text));
                        String updatePedidos = "update pedidos set total='" + txtTotalPedido.Text + "' where idpedido=" + idpedido;
                        conexion.setData(updatePedidos);

                        MessageBox.Show("Pedido actualizado correctamente");
                        //insert en tabla historial cambios
                        insert.insertHistorialCambio(idUsuario, 1, "Pedido modificado  num_pedido->" + n_pedido);
                    }
                    if (señal == 0)
                    {
                        MessageBox.Show("Pedido realizado correctamente");
                    }
            }
            fPedidosPrincipal.filtrar();
            this.Close();
        }

       
        private void modificarPedido(string n_pedido, string articulos, string cantidad, String precio, string id_articulo, int p)
        {
            /*CUANDO MODIFICAMOS UN PEDIDO RECORRER EL DATAGRIDVIEW Y VER EL TOTAL DL PEDIDO PARA CORREGIRLO*/
            /*FIJARZEEE BIENNN*/
            int id_pedido_modificar = Convert.ToInt32(conexion.DLookUp("IDPEDIDO", "PEDIDOS", "N_PEDIDO='" + n_pedido + "'"));
            int n_añadirArticulos = Convert.ToInt32(conexion.DLookUp("IDPEDIDOARTICULO", "PEDIDOSARTICULOS", "REFPEDIDO=" + id_pedido_modificar));
            int idPedidoArticulo = Convert.ToInt16(conexion.DLookUp("idpedidoarticulo", "pedidosarticulos", "refArticulo=" + id_articulo + "and refpedido = " + id_pedido_modificar));
            if (idPedidoArticulo >= 1)
            {
                String precioA = Convert.ToString(conexion.DLookUp("precio", "ARTICULOS", "idarticulo=" + id_articulo));
                String selectArticulos = "UPDATE PEDIDOSARTICULOS SET REFPEDIDO=" + id_pedido_modificar + ",REFARTICULO=" + Convert.ToInt32(id_articulo) +
                                            ",CANTIDAD=" + Convert.ToInt32(cantidad) + ",PRECIOVENTA='" + precioA + "' where IDPEDIDOARTICULO=" + n_añadirArticulos;
                conexion.setData(selectArticulos);
                
            }
            else
            {
                //añadimos un articulo al pedido que modificamos
                //HABRIA QUE ACTUALIZAR LA TABLA PEDIDOS//
                int ultimoID = MetodosAuxiliares.ultimoID(conexion,"IDPEDIDOARTICULO", "PEDIDOSARTICULOS");
                String selectArticulos = "insert into PEDIDOSARTICULOS values(" + ultimoID + "," + id_pedido_modificar + ","
                                        + Convert.ToInt32(id_articulo) + "," + Convert.ToInt32(cantidad) + ",'" + precio + "')";
                conexion.setData(selectArticulos);
                
            }
           
            
        }
        
        
        //Método que añade un pedido un pedido por primera vez
        private void añadirPedido(string n_pedido, string cliente, string articulos, string cantidad, string preciototal, string id,int señal)
        {
            if (señal == 0)
            {

                int idPedidoArticulo = Convert.ToInt32(MetodosAuxiliares.ultimoID(conexion, "IDPEDIDOARTICULO", "PEDIDOSARTICULOS"));
                String selectArticulos = "INSERT INTO PEDIDOSARTICULOS (IDPEDIDOARTICULO,REFPEDIDO,REFARTICULO,CANTIDAD,PRECIOVENTA)" +
                                    " VALUES(" + idPedidoArticulo + "," + id_pedido + "," + Convert.ToInt32(id) + "," + Convert.ToInt32(cantidad) + ",'" + Convert.ToDouble(preciototal) + "')";

                //MessageBox.Show(selectArticulos);
                conexion.setData(selectArticulos);
                
                // Añade el pedido
                //insert en tabla historial cambios
                insert.insertHistorialCambio(idUsuario, 1, "Pedido añadido  num_pedido->" + n_pedido);
            }
        }
        public void añadirTablaPedido(String cliente,String importetotal)
        {
            String fpago = cbFormaPago.SelectedText;
            Char pagado = 'N';
            if (cbPagado.Checked)
            {
                pagado = 'S';
            }

            /*SENTENCIA A MODIFICAR PARA GUARDAR EN LA TABLA PEDIDOS*/
            
            //para añadir un pedido nuevo
            //AL AÑADIR UN NUEVO PEDIDO PONEMOS EL IDESTADO A 5 PORQUE AUN NO SE HA ASIGNADO NINGUN ESTADO
            int fecha = Convert.ToInt32(MetodosAuxiliares.devolverFecha(dpFecha));
            
            int idpedido = Convert.ToInt32(MetodosAuxiliares.ultimoID(conexion,"IDPEDIDO","PEDIDOS"));
            int idformapago=cbFormaPago.SelectedIndex+1;
            String select = " insert into pedidos values("+idpedido+","+this.idCliente+","+idUsuario+","+fecha+","+idformapago+",'"
                +importetotal+"','"+pagado+"',"+n_pedido+",0,5,0)";
            
            conexion.setData(select);
            
        }


        /*MÉTODO QUE SUMA LOS PRECIOS EN LA TABLA, PARA ACTUALIZAR PEDIDO*/
        private float sumarPrecios()
        {
            String precioT;
            float precio = 0;
            for (int i = 0; i < dgvPedidos.RowCount; i++)
            {
                precioT = dgvPedidos.Rows[i].Cells[3].Value.ToString();
                precio = precio + Convert.ToSingle(precioT);
            }
            return precio;
        }
        
        //FUNCIONA
        public void rellenar(DataGridViewRow d)
        {
            n_pedido_modificar = d.Cells[0].Value.ToString();
            f_pago = Convert.ToInt32(conexion.DLookUp("REFFORMAPAGO", "PEDIDOS", "N_PEDIDO=" + Convert.ToInt32(d.Cells[0].Value.ToString())));
            int idPedido = Convert.ToInt32(conexion.DLookUp("IDPEDIDO", "PEDIDOS", "N_PEDIDO=" + Convert.ToInt32(d.Cells[0].Value.ToString())));
            String sentencia = "SELECT * FROM PEDIDOSARTICULOS WHERE REFPEDIDO=" + idPedido;
            DataSet resultado = conexion.getData(sentencia, "PEDIDOSARTICULOS");
            DataTable tPArticulos = resultado.Tables["PEDIDOSARTICULOS"];
            int idArticulo, cantidad;
            double precio;
            String nombreArticulo;
            foreach (DataRow row in tPArticulos.Rows)
            {
                idArticulo = Convert.ToInt32(row["REFARTICULO"]);
                cantidad = Convert.ToInt32(row["CANTIDAD"]);
                nombreArticulo = Convert.ToString(conexion.DLookUp("NOMBRE", "ARTICULOS", "IDARTICULO=" + idArticulo));
                precio = Convert.ToDouble(row["PRECIOVENTA"]);
                
                dgvPedidos.Rows.Add(d.Cells[2].Value.ToString(), nombreArticulo, cantidad, precio,idArticulo);
                /*SUMAMOS EL VALOR DEL PEDIDO*/
                txtTotalPedido.Text = Convert.ToString(sumarPrecios());
                String idcliente = Convert.ToString(d.Cells[13].Value.ToString());
                cargarCliente(idcliente);

            }
        }

        
        private void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AddPedido_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void dgvPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
