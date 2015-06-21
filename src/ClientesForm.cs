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
    public partial class ClientesForm : Form
    {
        ConnectDB conexion;
        private int rolUsuario;
        private int ckEliminado;
        private AddPedido addPedido;
        private AddEntradas addEntrada;
        private AddFactura addFacturas;
        private int señalCliente; // Almacena si lo llama el formulario Add pedido o el formulario Add entrada
        //Atributo que almacena la sentencia BASE sin filtros
        private const String SQL = "SELECT distinct * FROM CLIENTES C, POBLACIONES P, CODIGOSPOSTALESPOBLACIONES X,PROVINCIAS R WHERE C.REFCPPOBLACIONES=X.IDCODIGOPOSTALPOB AND X.REFPOBLACION = P.IDPOBLACION AND X.REFPROVINCIA = R.IDPROVINCIA AND C.ELIMINADO = 0";
        private int idUsuario;
        InsertHistorial insert;

        //patron Singleton
        private static ClientesForm instance;

        public static ClientesForm Instance(int idRol, int señal, ConnectDB c, AddPedido a, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ClientesForm(idRol, señal, c, a, idUsuario);
            }
            return instance;
        }

        public static ClientesForm Instance(int idRol, int señal, ConnectDB c, AddEntradas b, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ClientesForm(idRol, señal, c, b, idUsuario);
            }
            return instance;
        }

        public static ClientesForm Instance(int señal,ConnectDB c, AddFactura f)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ClientesForm(señal,c, f);
            }
            return instance;
        }

        public static ClientesForm Instance(int idRol, ConnectDB conexion, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ClientesForm(idRol, conexion, idUsuario);
            }
            return instance;
        }
        private ClientesForm(int idRol, int señal, ConnectDB c, AddPedido a, int idUsuario)
        {
            InitializeComponent();
            conexion = c;
            rolUsuario = idRol;
            ckEliminado = 0;
            cargarTabla(SQL);
            señalCliente = señal;
            addPedido = a;
            btnRestaurar.Enabled = false;
            btnBorrar.Enabled = true;
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(c);
        }

        //Contructor para facturas
        private ClientesForm(int señalF,ConnectDB c, AddFactura a)
        {
            InitializeComponent();
            conexion = c;
            señalCliente = señalF;
            ckEliminado = 0;
            cargarTabla(SQL);
            addFacturas = a;
            btnRestaurar.Enabled = false;
            btnBorrar.Enabled = true;
            
        }

        private ClientesForm(int idRol, int señal, ConnectDB c, AddEntradas a, int idUsuario)
        {
            InitializeComponent();
            conexion = c;
            rolUsuario = idRol;
            ckEliminado = 0;
            cargarTabla(SQL);
            señalCliente = señal;
            addEntrada = a;
            btnRestaurar.Enabled = false;
            btnBorrar.Enabled = true;
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(c);
        }



        private ClientesForm(int idRol, ConnectDB conexion, int idUsuario)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.rolUsuario = idRol;
            this.conexion = conexion;
            this.idUsuario = idUsuario;
            btnRestaurar.Enabled = false;
            btnBorrar.Enabled = true;
            ckEliminado = 0;
            cargarTabla(SQL);
            insert = new InsertHistorial(conexion);
        }

        //Con este modo se limpia la tabla
        public void limpiarTabla()
        {
            // Limpiamos el datagridView
            while (dgvClientes.RowCount > 0)
            {
                dgvClientes.Rows.Remove(dgvClientes.CurrentRow);
            }
        }
        //Con este metodo cargo la tabla Clientes
        public void cargarTabla(String sql)
        {
            limpiarTabla();
            int idCliente, telefono;
            String nombre, apellido1,apellido2, direccion, poblacion, email;

            sql = sql + " AND ELIMINADO = " + this.ckEliminado + " order by C.IDCLIENTE";
            DataSet data;
            data = conexion.getData(sql, "CLIENTES");

            DataTable tClientes = data.Tables["CLIENTES"];


            foreach (DataRow row in tClientes.Rows)
            {
                idCliente = Convert.ToInt32(row["IDCLIENTE"]);
                nombre = Convert.ToString(row["NOMBRE"]);
                apellido1 = Convert.ToString(row["APELLIDO1"]);
                apellido2 = Convert.ToString(row["APELLIDO2"]);
                telefono = Convert.ToInt32(row["TELEFONO"]);
                direccion = Convert.ToString(row["DIRECCION"]);
                poblacion = Convert.ToString(row["POBLACION"]);
                email = Convert.ToString(row["EMAIL"]);

                dgvClientes.Rows.Add(idCliente, nombre, apellido1,apellido2, telefono, direccion, poblacion, email);
            } // Fin del bucle for each
            dgvClientes.ClearSelection();
            dgvClientes.Update();
        }
        //Boton salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Abro la ventana añadir clientes
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            AddCliente add = new AddCliente(conexion, this, idUsuario);
            add.ShowDialog();
        }
        //Metodo que es llamado cuando se carga la interfaz
        private void ClientesForm_Load(object sender, EventArgs e)
        {
            dgvClientes.ClearSelection();
            dgvClientes.Update();
            ToolTip tTClientes = new ToolTip();
            tTClientes.SetToolTip(btnAñadir, "Añadir Cliente");
            tTClientes.SetToolTip(btnModificar, "Modificar Cliente");
            tTClientes.SetToolTip(btnRestaurar, "Restaurar Cliente");
            tTClientes.SetToolTip(btnLimpiar, "Limpiar Campos");
            tTClientes.SetToolTip(btnBorrar, "Borrar Cliente");
            tTClientes.SetToolTip(btnSalir, "Salir");
        }

        //Metodo para extraer id
        public int extraerIDTabla()
        {
            int id = 0;
            DataGridViewRow fila = dgvClientes.CurrentRow;
            id = Convert.ToInt32(fila.Cells[0].Value);
            return id;
        }
        //Boton modificar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null || dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna fila");
            }
            else
            {
                int idClienteSel = extraerIDTabla();
                AddCliente add = new AddCliente(conexion, this, idClienteSel, idUsuario);
                add.ShowDialog();
            }
        }
        /// <summary>
        /// Boton Resaturar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null || dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna fila");
            }
            else
            {
                String mensaje, mensajeConf;
                int eliminar_rest;
                mensaje = "¿Desea restaurar al cliente?";
                mensajeConf = "Cliente restaurado correctamente";
                eliminar_rest = 0;
                int idCliente;
                dgvClientes.ClearSelection();
                //pedimos confirmacion
                DialogResult opcion = MessageBox.Show(mensaje, "Confirmación",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (opcion == DialogResult.OK)
                {
                    idCliente = extraerIDTabla();

                    String update = " UPDATE CLIENTES  set ELIMINADO = " + eliminar_rest + " where IDCLIENTE=" + idCliente;
                    conexion.setData(update);

                    //Actualizar los usuarios visualizados en el data grid view
                    MessageBox.Show(mensajeConf, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();

                    //insert historial cambios
                    String nombreCliente = Convert.ToString
                               (conexion.DLookUp("NOMBRE", "CLIENTES", " IDCLIENTE = " + idCliente));
                                        
                    String   me = "Cliente restaurado ->" + nombreCliente ;
                    
                    insert.insertHistorialCambio(idUsuario, 4, me);
                }
            }
        }
        /// <summary>
        /// Boton borrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null || dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna fila");
            }
            else
            {
                String mensaje, mensajeConf;
                int eliminar_rest;
                mensaje = "¿Desea borrar al cliente?";
                mensajeConf = "Cliente borrado correctamente";
                eliminar_rest = 1;
                int idCliente;
                dgvClientes.ClearSelection();
                //pedimos confirmacion
                DialogResult opcion = MessageBox.Show(mensaje, "Confirmación",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (opcion == DialogResult.OK)
                {
                    idCliente = extraerIDTabla();

                    String update = " UPDATE CLIENTES  set ELIMINADO = " + eliminar_rest + " where IDCLIENTE=" + idCliente;
                    conexion.setData(update);

                    //Actualizar los usuarios visualizados en el data grid view
                    MessageBox.Show(mensajeConf, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();

                    //insert historial cambios
                    //pedimos el motivo por el cual se borra el cliente
                    String nombreCliente = Convert.ToString
                               (conexion.DLookUp("NOMBRE", "CLIENTES", " IDCLIENTE = " + idCliente));
                    String me = Interaction.InputBox("¿Motivo por el cual se elimina?", "Motivo", "");
                    me = "Cliente borrado ->" + nombreCliente + " Motivo ->" + me;
                    insert.insertHistorialCambio(idUsuario, 3, me);
                }
            }
        }
        //Metodo que limpia la interfaz
        public void limpiar()
        {
            txtNombre.Text = "";
            txtPoblacion.Text = "";
            txtApellido.Text = "";
            
            cargarTabla(SQL);
            rbNoEliminados.Checked = true;
            rbEliminados.Checked = false;
            btnBorrar.Enabled = true;
            btnRestaurar.Enabled = false;
        }
        //Metodo usado para filtrar la tabla
        private void filtrar()
        {
            String sentencia = "SELECT distinct * FROM CLIENTES C, POBLACIONES P, COMUNIDADES M, CODIGOSPOSTALESPOBLACIONES X,PROVINCIAS R" +
                " WHERE C.REFCPPOBLACIONES=X.IDCODIGOPOSTALPOB AND X.REFPOBLACION = P.IDPOBLACION AND X.REFPROVINCIA = R.IDPROVINCIA" +
                " AND M.IDCOMUNIDAD = R.REFCOMUNIDAD";
            if (txtNombre.Text.Length > 0)
            {
                sentencia = sentencia + " AND UPPER(C.NOMBRE) LIKE '%" + txtNombre.Text.ToUpper() + "%'";
            }
            if (txtApellido.Text.Length > 0)
            {
                sentencia = sentencia + " AND UPPER(C.APELLIDO1) LIKE '%" + txtApellido.Text.ToUpper() + "%'";
            }
            if (txtPoblacion.Text.Length > 0)
            {
                sentencia = sentencia + " AND UPPER(P.POBLACION) LIKE '%" + txtPoblacion.Text.ToUpper() + "%'";
            }
            
            cargarTabla(sentencia);
        }
        //Metodo que controlo el textBox de Nombre
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtNombre.Text.Length >= 30)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
               
            }
        }
        //Metodo que controla el textBox de Apellido
        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtApellido.Text.Length >= 20)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
                
            }
        }
        //Metodo que controla el textBox de Poblacion
        private void txtPoblacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\''))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
                
            }
        }
        
        //Boton limpiar filtros
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (señalCliente == 1)
            {
                addPedido.cargarCliente(dgvClientes.CurrentRow.Cells[0].Value.ToString());
                MessageBox.Show("Cliente añadido al pedido");
                this.Close();
            }
            if (señalCliente == 2)
            {
                addEntrada.setIdCliente(Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value.ToString()));
                addEntrada.setNombre(Convert.ToString(dgvClientes.CurrentRow.Cells[1].Value.ToString())+","+
                                        Convert.ToString(dgvClientes.CurrentRow.Cells[2].Value.ToString())+
                                        Convert.ToString(dgvClientes.CurrentRow.Cells[3].Value.ToString()));
                MessageBox.Show("Cliente añadido a la entrada");
                this.Close();
            }
            //Cuando lo llama facturas
            if (señalCliente == 3)
            {
                int idCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value.ToString());
                String nombre=dgvClientes.CurrentRow.Cells[1].Value.ToString();
                String apellido1 = dgvClientes.CurrentRow.Cells[2].Value.ToString();
                String apellido2 = dgvClientes.CurrentRow.Cells[3].Value.ToString();
                addFacturas.setApellido1(apellido1);
                addFacturas.setApellido2(apellido2);
                addFacturas.setIdCliente(idCliente);
                addFacturas.setNombre(nombre);
                MessageBox.Show("Cliente añadido a la factura");
                this.Close();
            }
        }

        private void rbNoEliminados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoEliminados.Checked == true)
            {
                btnBorrar.Enabled = true;
                btnRestaurar.Enabled = false;
                this.ckEliminado = 0;
                filtrar();
            }
            
        }

        private void rbEliminados_Click(object sender, EventArgs e)
        {
            rbEliminados_CheckedChanged(sender, e);
        }

        private void rbNoEliminados_Click(object sender, EventArgs e)
        {
            rbNoEliminados_CheckedChanged(sender, e);
        }

        private void rbEliminados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEliminados.Checked == true)
            {
                btnBorrar.Enabled = false;
                btnRestaurar.Enabled = true;
                this.ckEliminado = 1;
                filtrar();
            }
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtPoblacion_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

            
        }
    }

