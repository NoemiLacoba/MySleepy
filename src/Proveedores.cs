using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MySleepy
{
    public partial class Proveedores : Form
    {
        ConnectDB conexion;
        private int rolUsuario;
        private int ckEliminado;
        private AddPedido addPedido;
        private const String RUTAXML = "proveedor.xml";
        private int numero; // Almacena si lo llama el formulario Add pedido
        //Atributo que almacena la sentencia BASE sin filtros
        private const String SQL = "SELECT * FROM PROVEEDORES C, POBLACIONES P, CODIGOSPOSTALESPOBLACIONES X,PROVINCIAS R WHERE C.REFCPPOBLACIONES=X.IDCODIGOPOSTALPOB AND X.REFPOBLACION = P.IDPOBLACION AND X.REFPROVINCIA = R.IDPROVINCIA AND C.ELIMINADO = 0";
        private ToolTip toolTip1;
        int idUsuario;
        private int empresaAutonomo = -1;
        private static Proveedores instance;
        //Atributo que almacena el dataTable usado(sacado del xml)
        private DataSet ds;
        private InsertHistorial insert;
        private AddSalidas salida;
        private int señalControl; //controlará que el evento de doubleclick que usada junto con addSalida
        private int idRol;
        private GestionProveedor gestion;
        private GestionProveedor gestionProveedor;
        private int p;

        public static Proveedores Instance(int idRol, int señal, ConnectDB c, AddPedido a, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Proveedores(idRol, señal, c, a, idUsuario);
            }
            return instance;
        }
        public static Proveedores Instance(int idRol, ConnectDB conexion, int idUsuario, DataSet ds)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Proveedores(idRol, conexion, idUsuario, ds);
            }
            return instance;
        }
        public static Proveedores Instance(DataSet ds,ConnectDB conexion, int idUsuario,AddSalidas s, int señalControl)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Proveedores(ds,conexion, idUsuario,s,señalControl);
            }
            return instance;
        }

        

        public static Proveedores Instance(DataSet ds, ConnectDB conexion, int idUsuario, GestionProveedor gestionProveedor, int p)
        {
           
            if (instance == null || instance.IsDisposed)
            {
                instance = new Proveedores(ds,conexion,idUsuario,gestionProveedor,p);
            }
            return instance;
        }
        private Proveedores(int idRol, int señal, ConnectDB c, AddPedido a, int idUsuario)
        {
            ds = XML_proveedor.leerXMLDataSet(RUTAXML);
            toolTip1 = new ToolTip();
            InitializeComponent();
            conexion = c;
            rolUsuario = idRol;
            ckEliminado = 0;
            numero = señal;
            addPedido = a;
            this.idUsuario = idUsuario;
            this.cbEA.SelectedIndex = 0;
            this.empresaAutonomo = 1;
            btnRestaurar.Enabled = false;
            cargarTablaInicio();
            insert = new InsertHistorial(c);
        }

        private Proveedores(int idRol, ConnectDB conexion, int idUsuario, DataSet ds)
        {
            this.ds = ds;
            toolTip1 = new ToolTip();
            InitializeComponent();
            this.rolUsuario = idRol;
            this.conexion = conexion;
            this.idUsuario = idUsuario;
            ckEliminado = 0;
            this.cbEA.SelectedIndex = 0;
            this.empresaAutonomo = 1;
            btnRestaurar.Enabled = false;
            cargarTablaInicio();
            insert = new InsertHistorial(conexion);
            
        }
        private Proveedores(DataSet ds, ConnectDB conexion, int idUsuario,AddSalidas s,int señalControl)
        {
            this.ds = ds;
            toolTip1 = new ToolTip();
            InitializeComponent();
            this.conexion = conexion;
            this.idUsuario = idUsuario;
            ckEliminado = 0;
            this.cbEA.SelectedIndex = 0;
            this.empresaAutonomo = 1;
            btnRestaurar.Enabled = false;
            cargarTablaInicio();
            insert = new InsertHistorial(conexion);
            salida = s;
            this.señalControl = señalControl;
        }

        

        public Proveedores(DataSet ds, ConnectDB conexion, int idUsuario, GestionProveedor gestionProveedor, int p)
        {
            this.ds = ds;
            toolTip1 = new ToolTip();
            InitializeComponent();
            this.conexion = conexion;
            this.idUsuario = idUsuario;
            ckEliminado = 0;
            this.cbEA.SelectedIndex = 0;
            this.empresaAutonomo = 1;
            btnRestaurar.Enabled = false;
            cargarTablaInicio();
            insert = new InsertHistorial(conexion);
            gestion = gestionProveedor;
            this.señalControl = p;
        }
        //Con este modo se limpia la tabla
        public void limpiarTabla()
        {
            dgvProveedores.Rows.Clear();
        }

        public void cargarTablaInicio()
        {
            //solo mostraremos los no eliminados inicialmente
            DataTable dt = ds.Tables[0];
            limpiarTabla();
            ds.Tables.Clear();
            ds.Tables.Add(dt);
            int idProveedor, telefono, eliminado,movil;
            String cif, nombre, direccion, nif,email,pais,poblacion,contacto,observaciones;
            foreach (DataRow row in dt.Rows)
            {
                idProveedor = Convert.ToInt32(row["IDPROVEEDOR"]);
                cif = Convert.ToString(row["CIF"]);
                nombre = Convert.ToString(row["NOMBRE"]);
                telefono = Convert.ToInt32(row["TELEFONO"]);
                direccion = Convert.ToString(row["DIRECCION"]);
                eliminado = Convert.ToInt32(row["ELIMINADO"]);
                nif = Convert.ToString(row["NIF"]);
                movil = Convert.ToInt32(row["MOVIL"]);
                poblacion=Convert.ToString(row["refcppoblaciones"]);
                String pobl = Convert.ToString(conexion.DLookUp("poblacion", "poblaciones", "idpoblacion in(select refpoblacion from codigospostalespoblaciones where idcodigopostalpob="+Convert.ToInt32(poblacion)+")"));
                email = Convert.ToString(row["EMAIL"]);
                pais = Convert.ToString(row["PAIS"]);
                contacto = Convert.ToString(row["CONTACTO"]);
                observaciones = Convert.ToString(row["OBSERVACIONES"]);
                if (eliminado == ckEliminado)
                {
                    if (this.empresaAutonomo == 0)
                    {
                        if (cif.Equals("-"))
                        {
                            dgvProveedores.Rows.Add(idProveedor, nif, nombre, direccion, telefono,movil,pobl,pais,email,contacto,observaciones);
                        }
                    }
                    else
                    {
                        if (this.empresaAutonomo == 1)
                        {
                            if (nif.Equals("-"))
                            {
                                dgvProveedores.Rows.Add(idProveedor, cif, nombre, direccion, telefono, movil, pobl, pais, email,contacto,observaciones);
                            }
                        }
                    }
                }
            }

            dgvProveedores.ClearSelection();
            dgvProveedores.Update();
        }
        public void cargarTabla(DataRow row)
        {
            limpiarTabla();
            int idProveedor, telefono,movil;
            String cif, nombre, direccion, nif, email, pais, poblacion,contacto,observaciones;

            if (row != null)
            {
                
                idProveedor = Convert.ToInt32(row["IDPROVEEDOR"]);
                cif = Convert.ToString(row["CIF"]);
                nombre = Convert.ToString(row["NOMBRE"]);
                telefono = Convert.ToInt32(row["TELEFONO"]);
                direccion = Convert.ToString(row["DIRECCION"]);
                poblacion = Convert.ToString(row["refcppoblaciones"]);
                String pobl = Convert.ToString(conexion.DLookUp("poblacion", "poblaciones", "idpoblacion in(select refpoblacion from codigospostalespoblaciones where idcodigopostalpob=" + Convert.ToInt32(poblacion) + ")"));
                nif = Convert.ToString(row["NIF"]);
                movil = Convert.ToInt32(row["MOVIL"]);
                email = Convert.ToString(row["EMAIL"]);
                pais = Convert.ToString(row["PAIS"]);
                contacto = Convert.ToString(row["CONTACTO"]);
                observaciones = Convert.ToString(row["OBSERVACIONES"]);
                if (this.empresaAutonomo == 0)
                    {
                        if (cif.Equals("-"))
                        {
                            dgvProveedores.Rows.Add(idProveedor, nif, nombre, direccion, telefono, movil, pobl, pais, email,contacto,observaciones);
                        }
                    }
                    else
                    {
                        if (this.empresaAutonomo == 1)
                        {
                            if (nif.Equals("-"))
                            {
                                dgvProveedores.Rows.Add(idProveedor, cif, nombre, direccion, telefono,movil,pobl,pais,email,contacto,observaciones);
                            }
                        }
                    }
                    dgvProveedores.ClearSelection();
                    dgvProveedores.Update();
                }
            else
            {
                cargarTablaInicio();
            }
        }
        //Boton salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                ds.WriteXml(RUTAXML);
            }
            catch (IOException ex)
            {
                Console.WriteLine("ERROR escritura XML: "+ex.Message);
            }
           
            this.Dispose();
        }
        //Abro la ventana añadir proveedor
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            AddProveedor add = AddProveedor.Instance(conexion, ds,this, idUsuario);
            add.ShowDialog(this);
            filtro();
        }
        //Abre la ventana modificar proveedor
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.ckEliminado == 0)
            {
                if (dgvProveedores.CurrentRow == null || dgvProveedores.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No ha seleccionado ninguna fila");
                }
                else
                {
                    int id = extraerIDTabla();
                    AddProveedor add = AddProveedor.Instance(conexion, id, ds, this,idUsuario);
                    add.ShowDialog(this);
                    filtro();
                }
            }
        }
        //Metodo que es llamado cuando se carga la interfaz
        private void Proveedores_Load(object sender, EventArgs e)
        {
            dgvProveedores.ClearSelection();
            dgvProveedores.Update();
            cargarTablaInicio();
            tooltip();
        }
        //Metodo que crea los tooltip de los botones
        private void tooltip()
        {
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.btnAñadir, "Añadir proveedor");
            toolTip1.SetToolTip(this.btnLimpiar, "Borrar filtros");
            toolTip1.SetToolTip(this.btnModificar, "Modificar Proveedor");
            toolTip1.SetToolTip(this.btnSalir, "Cerrar ventana");
            toolTip1.SetToolTip(this.btnBorrar, "Borrar Proveedor");
            toolTip1.SetToolTip(this.btnRestaurar, "Restaurar Proveedor");
        }
        //Metodo para extraer id
        public int extraerIDTabla()
        {
            DataGridViewRow fila = dgvProveedores.CurrentRow;
            int id = Convert.ToInt32(fila.Cells[0].Value);
            return id;
        }//Metodo que limpia la interfaz
        public void limpiar()
        {
            txtNombre.Text = "";
            txtCIFNIF.Text = "";
            txtTelefono.Text = "";
            rbNoEliminados.Checked = true;
            rbEliminados.Checked = false;
            btnBorrar.Enabled = true;
            btnRestaurar.Enabled = false;
            cbEA.SelectedItem = -1;
            cargarTablaInicio();
        }

        //Metodo que controlo el textBox de Nombre
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if (Char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') )
            {
                e.Handled = true;
                lNombre.Text = "SOLO SE PERMITEN LETRAS";
            }
            else
            {
                e.Handled = false;
                lNombre.Text = "";
                filtro();
            }
            if (txtNombre.Text.Length >= 30 && (codigo != 8))
            {
                e.Handled = true;
            }
        }
        //Metodo que controla el textBox de Apellido
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if (Char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\''))
            {
                
                lTelefono.Text = "SOLO SE PERMITEN NUMEROS";
            }
            else
            {
                e.Handled = false;
                lTelefono.Text = "";
                filtro();
            }
            if (txtTelefono.Text.Length >= 20 && (codigo != 8))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Metodo que controla el textbox de NIF/CIF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCIFNIF_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if (char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtTelefono.Text.Length >= 9 && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
                filtro();
            }
        }

        //Boton limpiar filtros
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (numero == 1)
            {
                addPedido.cargarCliente(dgvProveedores.CurrentRow.Cells[0].Value.ToString());
                MessageBox.Show("Proveedor añadido al pedido");
                this.Close();
            }
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.CurrentRow == null || dgvProveedores .SelectedRows.Count == 0)
            {
                MessageBox.Show("No ha seleccionado ninguna fila");
            }
            else
            {
                int id = this.extraerIDTabla();
                DataView dv = new DataView(ds.Tables[0]);
                dv.Sort = "IDPROVEEDOR";
                int index = dv.Find(id);
                dv[index]["ELIMINADO"] = 1;
                MessageBox.Show(this, "Proveedor eliminado", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarTablaInicio();
                //pedimos la confirmacion del borrado
                String nombreProveedor = Convert.ToString(conexion.DLookUp("NOMBRE", "PROVEEDORES", " IdProveedor = " + id));
                String mensaje = Interaction.InputBox("¿Motivo por el cual se elimina?", " Motivo", "");
                mensaje = "Proveedor borrado ->" + nombreProveedor + " Motivo ->" + mensaje;
                insert.insertHistorialCambio(idUsuario, 3, mensaje);

                cargarTablaInicio();
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.CurrentRow == null || dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna fila");
            }
            else
            {

                int id = this.extraerIDTabla();
                DataView dv = new DataView(ds.Tables[0]);
                dv.Sort = "IDPROVEEDOR";
                int index = dv.Find(id);
                dv[index]["ELIMINADO"] = 0;
                MessageBox.Show(this, "Proveedor restaurado", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarTablaInicio();
                //insert en historial de cambios
                String nombreProveedor = Convert.ToString(conexion.DLookUp("NOMBRE", "PROVEEDORES", " IdProveedor = " + id));
                insert.insertHistorialCambio(idUsuario, 4, "Proveedor restaurado ->" + nombreProveedor);
            }
        }

        private void rbNoEliminados_Click(object sender, EventArgs e)
        {
            if (rbNoEliminados.Checked == true)
            {
                rbNoEliminados.Checked = true;
                this.ckEliminado = 0;
                btnRestaurar.Enabled = false;
                btnBorrar.Enabled = true;
                filtro();
            }
            
        }
        private void rbEliminados_Click(object sender, EventArgs e)
        {
            if (rbEliminados.Checked == true)
            {
                rbEliminados.Checked = true;
                this.ckEliminado = 1;
                btnRestaurar.Enabled = true;
                btnBorrar.Enabled = false;
                filtro();
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEA.SelectedIndex != -1)
            {
                txtCIFNIF.Enabled = true;
                if (cbEA.SelectedIndex == 0)
                {
                    //Es una empresa
                    empresaAutonomo = 1;
                }
                else
                {
                    //Es autonomo
                    empresaAutonomo = 0;
                }
            }
            else
            {
                txtCIFNIF.Enabled = false;
            }
            filtro();
        }
        /// <summary>
        /// Metodo que filtra la interfaz con la informacion buscada según los campos rellenados
        /// </summary>
        private void filtro()
        {
            DataTable dt = ds.Tables[0];
            int idProveedor, telefono, eliminado;
            String direccion, nombre, cif, nif;
            DataRow fila = null;
            foreach (DataRow row in dt.Rows)
            {
                idProveedor = Convert.ToInt32(row["IDPROVEEDOR"]);
                cif = Convert.ToString(row["CIF"]);
                nombre = Convert.ToString(row["NOMBRE"]);
                telefono = Convert.ToInt32(row["TELEFONO"]);
                direccion = Convert.ToString(row["DIRECCION"]);
                eliminado = Convert.ToInt32(row["ELIMINADO"]);
                nif = Convert.ToString(row["NIF"]);

                if (eliminado == ckEliminado)
                {
                    if (txtNombre.Text.Length > 0)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(nombre, txtNombre.Text, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            fila = row;
                        }
                    }
                    if (txtCIFNIF.Text.Length > 0)
                    {
                        if (this.empresaAutonomo == 1)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(cif, txtCIFNIF.Text, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                fila = row;
                            }
                        }
                        else
                        {
                            if (this.empresaAutonomo == 0)
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(nif, txtCIFNIF.Text, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                {
                                    fila = row;
                                }
                            }

                        }
                    }//fin txtCIFNIF
                    if (txtTelefono.Text.Length > 0)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(telefono.ToString(), txtTelefono.Text, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            fila = row;
                        }
                    }
                    if (fila != null)
                    {
                         break;
                    }
                }
            }
            cargarTabla(fila);
        }
        /// <summary>
        /// Metodo que cambia el atributo DataSet ds
        /// </summary>
        /// <param name="ds">DataSet que sustituira al  valor del atributo</param>
        public void setDS(DataSet ds)
        {
            this.ds = ds;
            filtro();
        }

        private void Proveedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ds.WriteXml(RUTAXML);
        }

        private void dgvProveedores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Extraemos el idProveedor y el nombre y lo devolvemos al form addSalida
            if (señalControl == 1)
            {
                salida.cargarProveedor(dgvProveedores.CurrentRow.Cells[2].Value.ToString());
                MessageBox.Show("Proveedor añadido");
                this.Close();
            }

            if (señalControl == 2)
            {
                String id = dgvProveedores.CurrentRow.Cells[0].Value.ToString();
                String cifdni = dgvProveedores.CurrentRow.Cells[1].Value.ToString();
                String nombre = dgvProveedores.CurrentRow.Cells[2].Value.ToString();
                String direccion = dgvProveedores.CurrentRow.Cells[3].Value.ToString();
                String telefono=dgvProveedores.CurrentRow.Cells[4].Value.ToString();
                String poblacion=dgvProveedores.CurrentRow.Cells[6].Value.ToString();
                String condicion = "refpoblacion in (select idpoblacion from poblaciones where poblacion like '"+poblacion+"')";
                int idcodPobl = Convert.ToInt32(conexion.DLookUp("idcodigopostalpob", "codigospostalespoblaciones", condicion));
                String email=dgvProveedores.CurrentRow.Cells[8].Value.ToString();
                String pais = dgvProveedores.CurrentRow.Cells[7].Value.ToString();
                String movil = dgvProveedores.CurrentRow.Cells[5].Value.ToString();
                String contacto = dgvProveedores.CurrentRow.Cells[9].Value.ToString();
                String observaciones=dgvProveedores.CurrentRow.Cells[10].Value.ToString();
                gestion.cargarProveedor(id,cifdni,nombre,direccion,telefono,movil,idcodPobl,email,pais,contacto,observaciones);
                MessageBox.Show("Proveedor añadido");
                this.Close();
            }
            
        }


    }
}
