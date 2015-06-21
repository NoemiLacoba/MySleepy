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
    public partial class PedidosForm : Form
    {
        // Atributos de la clase
        ConnectDB conexion;
        int rolUsuario, idUsuario,refPedido,refCliente;

        //patron singleton
        private static PedidosForm instance;
        InsertHistorial insertHistorial;

        ////////////////////////////////////////////////////////////////////////
        ///////////////// CONSTRUCTORES /////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////

        public static PedidosForm Instance(int idRol, ConnectDB c, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new PedidosForm(idRol, c, idUsuario);
            }
            return instance;
        }
        private PedidosForm(int idRol, ConnectDB c, int idUsuario)
        {
            InitializeComponent();
            conexion = c;
            rolUsuario = idRol;
            refPedido = -1;
            refCliente = -1;
            this.idUsuario = idUsuario;
            cargarInicio();
            insertHistorial = new InsertHistorial(conexion);
        }

        public void cargarInicio()
        {
            // Muestra los pedidos en la fecha actual que no estan pagados
            String sentencia = " Select * from PEDIDOS where LIQUIDADO='N' and ELIMINADO=0 order by fecha desc, n_pedido desc";
            //sentencia = sentencia + " AND FECHA='" + dateTimePicker1.Value.ToShortDateString() + "'";
            actualizarDGV(sentencia);
        }
        ////////////////////////////////////////////////////////////////////////
        ///////////////// LISTENERS BOTONES /////////////////////////////////
        ///////////////////////////////////////////////////////////////////////
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            AddPedido añadir = new AddPedido(conexion, rolUsuario, idUsuario, 0);
            añadir.asignarFPedidos(this);
            añadir.Show();
            contarPedidos();
            if (añadir.IsDisposed)
            {
                filtrar();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\'')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtReferencia_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

       

       

        private void rbPagados_Click(object sender, EventArgs e)
        {
            if(rbPagados.Checked==true){
                
                btnPagar.Enabled = false;
                actualizarDGV("Select * from PEDIDOS where LIQUIDADO='S' and ELIMINADO=0 order by fecha desc");
                contarPedidos();
                filtrar();
            }
            
           
        }

        private void rbNoPagados_Click(object sender, EventArgs e)
        {
            if (rbNoPagados.Checked == true)
            {
                btnPagar.Enabled = true;
                
                actualizarDGV("Select * from PEDIDOS where LIQUIDADO='N' and ELIMINADO=0 order by fecha desc");
                contarPedidos();
                filtrar();
            }
           
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != '\'')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != '\'')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\'')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvPedidosRealizados.CurrentRow == null || dgvPedidosRealizados.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                /*Los pedidos que ya están enviados no se pueden modificar*/
                int idpedido=Convert.ToInt32(dgvPedidosRealizados.CurrentRow.Cells[14].Value.ToString());
                int idestado=Convert.ToInt32(conexion.DLookUp("idestado","PEDIDOS","idpedido="+idpedido));
               
                if(idestado<3 || idestado==5){
                    AddPedido modificar = new AddPedido(conexion, rolUsuario, idUsuario, 1);
                    modificar.asignarFPedidos(this);
                    modificar.rellenar(dgvPedidosRealizados.CurrentRow);
                    modificar.Show();
                }
                else
                {
                    MessageBox.Show("No se pueden modificar pedidos enviados");
                }
            }
        }

       
        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (dgvPedidosRealizados.CurrentRow == null || dgvPedidosRealizados.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                //ABRIMOS EL  FORM LIQUIDAR PEDIDO AUTOMATICAMENTE
                
                    //Se liquida el pedido por la parte que se quiera, incluso totalmente
                    int idpedido=Convert.ToInt32(dgvPedidosRealizados.CurrentRow.Cells[14].Value.ToString());
                    LiquidarPedido lp = new LiquidarPedido(conexion,idpedido);
                    lp.ShowDialog();
                    actualizarDGV("Select * from PEDIDOS where LIQUIDADO='N' and ELIMINADO=0 order by fecha desc,n_pedido desc");
             
            }
        }

        private void pagarPedido(DataGridViewRow fila)
        {
            
            int id_p_pagar = Convert.ToInt32(conexion.DLookUp("IDPEDIDO", "PEDIDOS", "N_PEDIDO=" + Convert.ToInt32(fila.Cells[0].Value.ToString())));
            String sentencia = "UPDATE PEDIDOS SET LIQUIDADO='S',importepagado=total  WHERE IDPEDIDO=" + id_p_pagar;
            conexion.setData(sentencia);
        }

        ////////////////////////////////////////////////////////////////////////
        ///////////////// CARGA FORMULARIO /////////////////////////////////
        ///////////////////////////////////////////////////////////////////////
        private void Pedidos_Load(object sender, EventArgs e)
        {
            dgvPedidosRealizados.ClearSelection();
            dgvPedidosRealizados.Update();
            cargarInicio();
            contarPedidos();
            ToolTip tTPedidos = new ToolTip();
            tTPedidos.SetToolTip(btnAñadir, "Añadir Pedido");
            tTPedidos.SetToolTip(btnModificar, "Modificar Pedido");
            tTPedidos.SetToolTip(btnPagar, "Pagar Pedido");
            tTPedidos.SetToolTip(btnSalir, "Salir");
           
        }

        ////////////////////////////////////////////////////////////////////////
        ///////////////// METODOS QUE USA EL FORMULARIO ///////////////////
        ///////////////////////////////////////////////////////////////////////

        // Limpia los campos de los filtros
        private void limpiarCampos()
        {
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtReferencia.Text = "";
            fechaMin.Value = DateTime.Now;
            fechaMaxima.Value = DateTime.Now;
        }


        ///////////////////////////////////////////////////////
        // Metodo que filtra//////////////////////////////////////
        ///////////////////////////////////////////////////////
        public void filtrar()
        {
            String sentencia="";
            if (rbNoPagados.Checked == true)
            {
                sentencia = "Select * from PEDIDOS where LIQUIDADO='N' and ELIMINADO=0";
            }
            if (rbPagados.Checked == true)
            {
                sentencia = "Select * from PEDIDOS where LIQUIDADO='S' and ELIMINADO=0";
            }
            if (txtReferencia.Text != "")
            {
                sentencia = sentencia + " AND N_PEDIDO LIKE '%" + Convert.ToInt32(txtReferencia.Text) + "%'";
            }
            if (txtNombre.Text != "")
            {
                int rCliente = Convert.ToInt32(conexion.DLookUp("IDCLIENTE", "CLIENTES", "upper(NOMBRE) "+
                "LIKE upper('%" + txtNombre.Text + "%') or upper(apellido1) like upper('%" + txtNombre.Text + "%') or "+
                "upper(apellido2) like upper('%" + txtNombre.Text + "%')"));
                sentencia = sentencia + " AND REFCLIENTE=" + rCliente;
            }
            if (txtPrecio.Text != "")
            {
                sentencia = sentencia + " AND total >=" + Convert.ToSingle(txtPrecio.Text);
            }
            sentencia = sentencia + " AND FECHA>=" + MetodosAuxiliares.devolverFecha(fechaMin) + " and fecha<=" + MetodosAuxiliares.devolverFecha(fechaMaxima);

            sentencia = sentencia + " order by fecha desc, n_pedido desc";
            actualizarDGV(sentencia);
            contarPedidos();
        }

        public void contarPedidos()
        {
            numeroPedidos.Text = "Pedidos encontrados: "+dgvPedidosRealizados.RowCount.ToString();

        }

        // Metodo que actualiza el DATAGRIDVIEW
        public void actualizarDGV(string sentencia)
        {
            limpiarTabla();
            DataSet resultado = conexion.getData(sentencia, "PEDIDOS");
            DataTable tPedidos = resultado.Tables["PEDIDOS"];
            int fila=0;
            foreach (DataRow row in tPedidos.Rows)
            {
                try
                {
                    int idpedido = Convert.ToInt32(row["idpedido"]);
                    int n_pedido = Convert.ToInt32(row["N_PEDIDO"]);
                    int fecha = Convert.ToInt32(row["FECHA"]);
                    String fechaConvert = MetodosAuxiliares.pasarFecha(fecha);
                    String nombreape1 = Convert.ToString(conexion.DLookUp("NOMBRE||','||apellido1", "CLIENTES", "IDCLIENTE =" + row["REFCLIENTE"]));
                    String ape2 = Convert.ToString(conexion.DLookUp("apellido2", "CLIENTES", "IDCLIENTE =" + row["REFCLIENTE"]));
                    String cliente = nombreape1 + " " + ape2;
                    float importetotal = Convert.ToSingle(row["TOTAL"]);
                    Char liquidado = Convert.ToChar(row["LIQUIDADO"]);
                    float importepagado= Convert.ToSingle(row["importepagado"]);
                    float restante=(importetotal-importepagado);
                    String nombreUsuario = Convert.ToString(conexion.DLookUp("nombre", "USUARIOS", "idusuario="+idUsuario));
                    String formapago=Convert.ToString(conexion.DLookUp("formapago", "FORMASDEPAGO", "IDPAGO =" + row["REFFORMAPAGO"]));
                    String nombreEstado = Convert.ToString(conexion.DLookUp("NOMBRE", "ESTADOS", "IDESTADO =" + row["IDESTADO"]));
                    String idcliente = Convert.ToString(conexion.DLookUp("REFCLIENTE", "PEDIDOS", "IDPEDIDO ="+ idpedido));
                    dgvPedidosRealizados.Rows.Add(n_pedido, fechaConvert, cliente, liquidado.ToString(),importetotal,importepagado,Math.Round(restante,2),formapago," "," "," "," ",nombreUsuario,idcliente,idpedido );
                    //AHORA LLAMAMOS AL METODO PINTAR CELDAS  
                    pintarCeldasEstado(nombreEstado, fila);
                    fila++;
                    /*ESTAMOS TONTOS O QUEEE LA M CON LA A MA*/
                    //MessageBox.Show(nombreEstado);
                }
                catch (Exception e)
                {
                    Console.WriteLine("excepcion pedidos");
                }
                dgvPedidosRealizados.ClearSelection();
                contarPedidos();
            } // Fin del bucle for each
        }

        private void pintarCeldasEstado(String nombreEstado,int fila)
        {
            if (nombreEstado.Equals("NINGUNO"))
            {
                dgvPedidosRealizados.Rows[fila].Cells[8].Style.BackColor=Color.Red;
                dgvPedidosRealizados.Rows[fila].Cells[9].Style.BackColor = Color.Red;
                dgvPedidosRealizados.Rows[fila].Cells[10].Style.BackColor = Color.Red;
                dgvPedidosRealizados.Rows[fila].Cells[11].Style.BackColor = Color.Red;
            }
            if (nombreEstado.Equals("CONFIRMADO"))
            {
                dgvPedidosRealizados.Rows[fila].Cells[8].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[9].Style.BackColor = Color.Red;
                dgvPedidosRealizados.Rows[fila].Cells[10].Style.BackColor = Color.Red;
                dgvPedidosRealizados.Rows[fila].Cells[11].Style.BackColor = Color.Red;
            }
            if (nombreEstado.Equals("ETIQUETADO"))
            {
                dgvPedidosRealizados.Rows[fila].Cells[8].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[9].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[10].Style.BackColor = Color.Red;
                dgvPedidosRealizados.Rows[fila].Cells[11].Style.BackColor = Color.Red;
            }
            if (nombreEstado.Equals("ENVIADO"))
            {
                dgvPedidosRealizados.Rows[fila].Cells[8].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[9].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[10].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[11].Style.BackColor = Color.Red;
            }
            if (nombreEstado.Equals("FACTURADO"))
            {
                dgvPedidosRealizados.Rows[fila].Cells[8].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[9].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[10].Style.BackColor = Color.LightGreen;
                dgvPedidosRealizados.Rows[fila].Cells[11].Style.BackColor = Color.LightGreen;
            }
        }

        // Limpia la tabla
        private void limpiarTabla()
        {
            // Limpiamos el datagridView
            while (dgvPedidosRealizados.RowCount > 0)
            {
                dgvPedidosRealizados.Rows.RemoveAt(0);
            }
        }

        private void fechaMinima(object sender, EventArgs e)
        {
            String fechamin = fechaMin.Value.ToString();
            String fechamax =fechaMaxima.Value.ToString();
            int diaP = Convert.ToInt16(fechaMin.Value.Day.ToString());
            int mesP = Convert.ToInt16(fechaMin.Value.Month.ToString());
            int añoP = Convert.ToInt16(fechaMin.Value.Year.ToString());
            int diaE = Convert.ToInt16(fechaMaxima.Value.Day.ToString());
            int mesE = Convert.ToInt16(fechaMaxima.Value.Month.ToString());
            int añoE = Convert.ToInt16(fechaMaxima.Value.Year.ToString());
            //MessageBox.Show(diaP+" "+diaE+" "+mesP+" "+mesE);
            if (diaP > diaE && mesP >= mesE && añoP >= añoE)
            {
                MessageBox.Show("La fecha máxima no debe ser menor que la minima");
                fechaMin.Value = DateTime.Today;
            }
            filtrar();
        }

        private void fechaMaxima_ValueChanged(object sender, EventArgs e)
        {
            String fechaminima = fechaMin.Value.ToString();
            String fechamaxima = fechaMaxima.Value.ToString();
            int diaP = Convert.ToInt16(fechaMin.Value.Day.ToString());
            int mesP = Convert.ToInt16(fechaMin.Value.Month.ToString());
            int añoP = Convert.ToInt16(fechaMin.Value.Year.ToString());
            int diaE = Convert.ToInt16(fechaMaxima.Value.Day.ToString());
            int mesE = Convert.ToInt16(fechaMaxima.Value.Month.ToString());
            int añoE = Convert.ToInt16(fechaMaxima.Value.Year.ToString());
            //MessageBox.Show(diaP+" "+diaE+" "+mesP+" "+mesE);
            if (diaP > diaE && mesP >= mesE && añoP >= añoE)
            {
                MessageBox.Show("La fecha máxima no debe ser menor que la minima");
                fechaMaxima.Value = DateTime.Today;
            }
            filtrar();
        }

       /*Evento que realiza los cambios en los estados del pedido*/
        private void dgvPedidosRealizados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = dgvPedidosRealizados.CurrentRow.Index;

            int n_pedido = Convert.ToInt32(dgvPedidosRealizados.Rows[fila].Cells[0].Value.ToString());
            int idpedido = Convert.ToInt32(dgvPedidosRealizados.CurrentRow.Cells[14].Value.ToString());
            int idestado = Convert.ToInt32(conexion.DLookUp("idestado", "PEDIDOS", "idpedido=" + idpedido));
            if (e.RowIndex == null || e.RowIndex < 0)
            {

            }
            else
            {
                double importetotal = Convert.ToSingle(dgvPedidosRealizados.Rows[e.RowIndex].Cells[4].Value.ToString());
                if (e.ColumnIndex == 8)
                {

                    if (rolUsuario == 2 || rolUsuario == 1)
                    {
                        if (idestado == 5)
                        {
                            DialogResult opcion = MessageBox.Show("¿Desea confirmar el pedido?", "Confirmación",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (opcion == DialogResult.OK)
                            {
                                String actualizar = "update pedidos set idestado=1 where idpedido=" + idpedido;
                                conexion.setData(actualizar);


                                /*se debe comparar los campos de importes pagadas, totales y realizar la correspondiente
                                 inserción en las tablas de operación y/o pendientes*/
                                int idoperacion = MetodosAuxiliares.ultimoID(conexion, "idoperacion", "operaciones");
                                String tipopago = dgvPedidosRealizados.Rows[e.RowIndex].Cells[7].Value.ToString();

                                double importepagado = Convert.ToSingle(dgvPedidosRealizados.Rows[e.RowIndex].Cells[5].Value.ToString());
                                double importerestante = Math.Round(Convert.ToSingle(dgvPedidosRealizados.Rows[e.RowIndex].Cells[6].Value.ToString()), 2);
                                importetotal = Math.Round(importetotal, 2);
                                importepagado = Math.Round(importepagado, 2);
                                int fecha = Convert.ToInt32(MetodosAuxiliares.devolverFechaActual());
                                int hora = Convert.ToInt32(MetodosAuxiliares.devolverHora());
                                String cliente = dgvPedidosRealizados.Rows[e.RowIndex].Cells[2].Value.ToString();
                                //int idcliente = Convert.ToInt32(conexion.DLookUp("idcliente", "CLIENTES", "nombre like '%" + cliente + "%'"));
                                int idcliente = Convert.ToInt32(dgvPedidosRealizados.Rows[e.RowIndex].Cells[13].Value.ToString());
                                int idpendientes = Convert.ToInt32(MetodosAuxiliares.ultimoID(conexion, "idpendiente", "PENDIENTES"));
                                String procedencia;
                                /*Comprobar tb la forma de pago, si es contrareembolso(Agencia) o el resto(varios) en 
                                 el caso que haya que meter datos en la tabla pendientes*/
                                //FIJARZEEEE BIENNNN contrarreeeeeeembolso
                                if (tipopago.ToLower().Equals("contrareembolso")) procedencia = "Agencias";
                                else procedencia = "Varios";

                                /*SI SE HA PAGADO EL PEDIDO TOTALMENTE VA A LA TABLA OPERACIONES COMO UN INGRESO*/
                                if ((importetotal == importepagado) && importerestante == 0)
                                {
                                    String insertOperaciones = "insert into operaciones values(" + idoperacion + ",1,'Efectivo'," +
                                    "'Entrada añadida: Pedido Confirmado y Pagado :" + n_pedido + "','" + importetotal + "'," + fecha + "," + hora + "," + idUsuario + ",'E')";

                                    conexion.setData(insertOperaciones);
                                    MessageBox.Show("Entrada realizada");
                                }
                                /*CUANDO NO SE HA PAGADO NADA, SE INSERTA EN LA TABLA PENDIENTES DE COBRO*/
                                if (importepagado == 0 && importerestante == importetotal)
                                {

                                    /*No le asignamos ninguna agencia porque no sabemos a quien se va a "agenciar" la agencia*/
                                    if (procedencia.Equals("Varios"))
                                    {
                                        String insertPendientes = "insert into pendientes values (" + idpendientes + "," + idUsuario + "," +
                                        idcliente + ",0," + hora + "," + fecha + ",'" + (importetotal - importepagado) + "',1,'Efectivo','Pendiente añadido, Pedido: " + n_pedido + "','N')";

                                        conexion.setData(insertPendientes);
                                    }
                                    else
                                    {
                                        String insertPendientes = "insert into pendientes values (" + idpendientes + "," + idUsuario + "," +
                                        idcliente + ",0," + hora + "," + fecha + ",'" + (importetotal - importepagado) + "',0,'Efectivo','Pendiente añadido, Pedido: " + n_pedido + "','N')";

                                        conexion.setData(insertPendientes);
                                    }
                                    MessageBox.Show("Pendiente insertado");
                                }
                                /*PARTE DEL PEDIDO HA SIDO PAGADO, LUEGO HACEMOS LOS INSERT CORRESPONDIENTES EN LAS TABLAS
                                 OPERACIONES Y PENDIENTES*/
                                if (importetotal > importepagado && importepagado != 0)
                                {

                                    String insertOperaciones = "insert into operaciones values(" + idoperacion + ",1,'Efectivo'," +
                                    "'Entrada añadida: Pedido Confirmado y Pagado parcialmente :" + n_pedido + "','" + importepagado + "'," + fecha + "," + hora + "," + idUsuario + ",'E')";

                                    conexion.setData(insertOperaciones);
                                    if (procedencia.Equals("Varios"))
                                    {
                                        String insertPendientes = "insert into pendientes values (" + idpendientes + "," + idUsuario + "," +
                                        idcliente + ",0," + hora + "," + fecha + ",'" + (importetotal - importepagado) + "',1,'Efectivo','Pendiente añadido, Pedido : " + n_pedido + "','N')";

                                        conexion.setData(insertPendientes);

                                    }
                                    else
                                    {
                                        String insertPendientes = "insert into pendientes values (" + idpendientes + "," + idUsuario + "," +
                                        idcliente + ",0," + hora + "," + fecha + ",'" + (importetotal - importepagado) + "',0,'Efectivo','Pendiente añadido, Pedido: " + n_pedido + "','N')";

                                        conexion.setData(insertPendientes);

                                    }
                                    MessageBox.Show("Apuntes correspondientes insertados");
                                }
                                //rbNoPagados.Checked = true;
                                MessageBox.Show("Pedido confirmado");
                            }
                        }
                        /*Aqui termina la primera parte del quijote. PEAZOOOO EVENTO*/
                        /*Segunda parte del quijote. Desconfirmamos el pedido por devolución*/
                        else
                        {
                            DialogResult opcion = MessageBox.Show("¿Desea eliminar/desconfirmar el pedido?", "Confirmación",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (opcion == DialogResult.OK)
                            {
                                /*TAMBIEN CONTROLAR QUE SE PUEDA VOLVER A PULSAR Y DESACTIVAR*/
                                /*AQUÍ SE SUMARIAN LOS ARTICULOS AL STOCK*/
                                if(idestado==4)
                                    sumarStockArticulos(idpedido);

                                //Factura en negativo
                                int idfactura = MetodosAuxiliares.ultimoID(conexion, "idfactura", "FACTURAS");
                                int idcliente = Convert.ToInt32(dgvPedidosRealizados.Rows[e.RowIndex].Cells[13].Value.ToString());
                                int hayRegistroFactura = Convert.ToInt32(conexion.DLookUp("idfactura", "FACTURAS", "idfactura=1"));
                                int contadorNFactura;
                                if (hayRegistroFactura == -1)
                                {
                                    contadorNFactura = 20150001;
                                }
                                else
                                {
                                    contadorNFactura = MetodosAuxiliares.ultimoID(conexion, "numfactura", "FACTURAS");
                                }
                                String insertFacturas = "insert into facturas values(" + idfactura + "," + contadorNFactura +
                                        "," + idcliente + "," + idpedido + "," + MetodosAuxiliares.devolverFechaActual() + "," +
                                        MetodosAuxiliares.devolverHora() + ",'-" + importetotal + "',0,'N')";
                                conexion.setData(insertFacturas);
                                MessageBox.Show("Factura abonada en negativo");
                                //Eliminar pedido
                                String borrarPedido = "update pedidos set eliminado=1 where idpedido=" + idpedido;
                                conexion.setData(borrarPedido);
                                MessageBox.Show("Pedido eliminado");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No tiene permisos para confirmar");
                    }

                } if (e.ColumnIndex == 9)
                {
                    if (rolUsuario == 3)
                    {
                        //Se comprueba que está confirmado antes de etiquetar
                        if (idestado == 1)
                        {
                            DialogResult opcion = MessageBox.Show("¿Desea etiquetar el pedido?", "Confirmación",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (opcion == DialogResult.OK)
                            {
                                String actualizar = "update pedidos set idestado=2 where idpedido=" + idpedido;
                                conexion.setData(actualizar);
                                //actualizarDGV("Select * from PEDIDOS where LIQUIDADO='N' and ELIMINADO=0");
                                //rbNoPagados.Checked = true;
                                MessageBox.Show("Pedido etiquetado");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El pedido no está confirmado");
                        }

                    }
                    else
                    {
                        MessageBox.Show("No tiene permisos para etiquetar");
                    }

                }
                if (e.ColumnIndex == 10)
                {
                    if (rolUsuario == 3)
                    {
                        //Se comprueba que está etiquetado antes de enviar
                        if (idestado == 2)
                        {
                            DialogResult opcion = MessageBox.Show("¿Desea enviar el pedido?", "Confirmación",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (opcion == DialogResult.OK)
                            {
                                String actualizar = "update pedidos set idestado=3 where idpedido=" + idpedido;
                                conexion.setData(actualizar);
                                //actualizarDGV("Select * from PEDIDOS where LIQUIDADO='N' and ELIMINADO=0");
                                //rbNoPagados.Checked = true;
                                MessageBox.Show("Pedido enviado");
                                /*AQUÍ DEBEMOS RESTAR DEL STOCK LOS ARTICULOS DEL PEDIDO*/

                                /*LLAMADA A UN MÉTODO PARA QUE RECORRA LOS REGISTROS QUE TIENEN QUE VER CON ESTE PEDIDO
                                 VER LA CANTIDAD EN EL PEDIDO DE CADA ARTICULO Y RESTARSELO*/
                                restarStockArticulos(idpedido);

                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("No tiene permisos para poder enviar");
                    }

                }
                if (e.ColumnIndex == 11)
                {
                    if (rolUsuario == 1 || rolUsuario == 2)
                    {
                       
                        if (idestado == 3 && idestado!=1 && idestado!=2 && idestado!=5)
                        {
                            double pagado = Convert.ToSingle(dgvPedidosRealizados.Rows[fila].Cells[4].Value.ToString());
                            double total = Convert.ToSingle(dgvPedidosRealizados.Rows[fila].Cells[5].Value.ToString());
                            if ((pagado - total) == 0)
                            {
                                DialogResult opcion = MessageBox.Show("¿Desea facturar el pedido?", "Confirmación",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                if (opcion == DialogResult.OK)
                                {
                                    String actualizar = "update pedidos set idestado=4 where idpedido=" + idpedido;
                                    conexion.setData(actualizar);

                                    //ABRIR FORMULARIO FACTURAS
                                    //AÑADIR EN LA TABLA FACTURAS
                                    /*Comprobar los numeros de facturas en las tablas facturas y facturasarticulos, y 
                                     * comprobar cual es más grande */
                                    
                                    int idfactura = MetodosAuxiliares.ultimoID(conexion, "idfactura", "FACTURAS");
                                    int idcliente = Convert.ToInt32(dgvPedidosRealizados.Rows[e.RowIndex].Cells[13].Value.ToString());
                                    
                                    int numF = ContadorNumFactura();
                                    String insertFacturas = "insert into facturas values(" + idfactura + "," + numF +
                                        "," + idcliente + "," + idpedido + "," + MetodosAuxiliares.devolverFechaActual() + "," +
                                        MetodosAuxiliares.devolverHora() + ",'" + importetotal + "',0,'N')";
                                    conexion.setData(insertFacturas);
                                    //rbNoPagados.Checked = true;

                                    //IMPRIMIR FACTURA//
                                    DialogResult opcion1 = MessageBox.Show("¿Desea imprimir el pedido facturado?", "Confirmación",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                    if (opcion1 == DialogResult.OK)
                                    {
                                        ImprimirFactura imprimir = new ImprimirFactura(conexion, idfactura, idcliente, idpedido);
                                        imprimir.ShowDialog();

                                    }
                                    else
                                    {
                                        MessageBox.Show("Pedido listo para facturar. Mire en facturas");
                                    }
                                    //añadimos el cambio en el historial de cambios
                                    insertHistorial.insertHistorialCambio(idUsuario, 1, "Factura añadida :" + numF);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Pedido no pagado en su totalidad. No se puede facturar");
                            }

                        }
                        else
                        {
                            if (idestado == 1 || idestado==2 || idestado==5)
                                MessageBox.Show("Confirme los estados anteriores, para poder facturar");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("No tiene permisos para poder confirmar");
                    }
                }
                actualizarDGV("Select * from PEDIDOS where LIQUIDADO='N' and ELIMINADO=0 order by fecha desc,n_pedido desc");
            }
        }//Fin listener

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
                contadorNFactura = MetodosAuxiliares.ultimoNumeroFactura(conexion, "numfactura", "FACTURAS", "eliminado=0");
                contadorNFA = MetodosAuxiliares.ultimoNumeroFactura(conexion, "numfactura", "FACTURASARTICULOS", "eliminada=0");
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

        public void restarStockArticulos(int idpedido)
        {
            DataSet resultado = conexion.getData("select * from pedidosarticulos where refpedido="+idpedido, "PEDIDOSARTICULOS");
            DataTable tPedidos = resultado.Tables["PEDIDOSARTICULOS"];
            int cantidad=0;
            int idarticulo;
            String resta;
            foreach (DataRow row in tPedidos.Rows)
            {
                cantidad = Convert.ToInt32(row["cantidad"]);
                idarticulo =Convert.ToInt32(row["refarticulo"]);
                //Ahora restamos la cantidad al articulo
                resta = "update articulosstock set stockreal=(stockreal-" + cantidad+") where idarticulo="+idarticulo;
                conexion.setData(resta);
                
            }
        }

        public void sumarStockArticulos(int idpedido)
        {
            DataSet resultado = conexion.getData("select * from pedidosarticulos where refpedido=" + idpedido, "PEDIDOSARTICULOS");
            DataTable tPedidos = resultado.Tables["PEDIDOSARTICULOS"];
            int cantidad = 0;
            int idarticulo;
            String resta;
            foreach (DataRow row in tPedidos.Rows)
            {
                cantidad = Convert.ToInt32(row["cantidad"]);
                idarticulo = Convert.ToInt32(row["refarticulo"]);
                //Ahora sumamoss la cantidad al articulo
                resta = "update articulosstock set stockreal=(stockreal+" + cantidad + ") where idarticulo=" + idarticulo;
                conexion.setData(resta);
                
            }
        }


        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtReferencia_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

    }
}
