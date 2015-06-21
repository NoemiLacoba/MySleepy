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
    public partial class AddProveedor : Form
    {
        //Atributo que almacena la conexion utilizada
        private ConnectDB conexion;
        //Atributo que almacena el id de la comunidad autonoma
        private int idCAutonoma;
        //Atributo que almacena el id de la provincia
        private int idProvincia;
        //Atributo que almacena el id de la poblacion
        private int idPoblacion;
        //Atributo que almacena el id del codigo postal
        private int idCodigoPostal;
        //Atributo que almacena el mensaje de error al guardar
        private String mensaje;
        //Atributo que almacena el mensaje de confirmación
        private String confirmacion;
        //Atributo que indica si se ha de modificar o insertar en la BBDD
        private Boolean mod;
        //Atributo que indica si se han producido cambios en la interfaz
        private Boolean cambios;
        //Atributo que almacena el id a controlar
        private int idProveedor;
        //Atributo que hace el patron singlenton
        private static AddProveedor instance;
        //Atributo que contiene la ruta fija del XML
        private const String RUTAXML = "proveedor.xml";
        private DataSet ds;
        private ToolTip toolTip1;
        private int empresaAutonomo;
        private Proveedores daddy;
        private InsertHistorial insert;
        private int idUsuario;
        public static AddProveedor Instance(ConnectDB c, DataSet ds, Proveedores daddy, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new AddProveedor(c, ds, daddy, idUsuario);
            }
            return instance;
        }
        public static AddProveedor Instance(ConnectDB c, int id, DataSet ds, Proveedores daddy, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new AddProveedor(c, id, ds, daddy, idUsuario);
            }
            return instance;
        }
        public AddProveedor(ConnectDB conexion, DataSet ds, Proveedores daddy, int idUsuario)
        {
            this.daddy = daddy;
            toolTip1 = new ToolTip();
            InitializeComponent();
            txtCIF.Enabled = false;
            txtDNI.Enabled = false;
            txtCIF.Visible = false;
            txtDNI.Visible = false;
            cbEA.SelectedIndex = 0;
            this.mod = false;
            this.cambios = false;
            
            this.confirmacion = "¿Desea añadir al proveedor?";
            //iniciamos la conexion 
            this.conexion = conexion;
            DataSet data = new DataSet();
            DataTable tabla = new DataTable();
            this.idUsuario = idUsuario;
            //Cargo el combo box de comunidades autonomas
            data = conexion.getData("SELECT COMUNIDAD FROM COMUNIDADES ORDER BY ORDEN", "COMUNIDADES");
            tabla = data.Tables["COMUNIDADES"];
            foreach (DataRow row in tabla.Rows)
            {
                cbCAutonoma.Items.Add(Convert.ToString(row["COMUNIDAD"]));
            }
            txtCIF.LostFocus += new EventHandler(txtCIF_lostFocus);
            txtTelefono.LostFocus += new EventHandler(txtTelefono_lostFocus);
            caja_email.LostFocus += new EventHandler(caja_email_lostFocus);
            this.ds = ds;
            insert = new InsertHistorial(conexion);
            cargarTablaArticulos();
            cargaCbFiltro();
        }

        private void caja_email_lostFocus(object sender, EventArgs e)
        {
            String email = caja_email.Text;
            Boolean correcto=MetodosAuxiliares.emailCorrecto(email);
            if (correcto == false)
            {
                MessageBox.Show("Email incorrecto");
            }
        }
        public AddProveedor(ConnectDB conexion, int id, DataSet ds, Proveedores daddy, int idUsuario)
        {
            this.daddy = daddy;
            toolTip1 = new ToolTip();
            InitializeComponent();
            txtCIF.Enabled = false;
            txtDNI.Enabled = false;
            txtCIF.Visible = false;
            txtDNI.Visible = false;
            this.mod = true;
            
            this.confirmacion = "¿Desea modificar al proveedor?";
            //iniciamos la conexion 
            this.conexion = conexion;
            txtCIF.LostFocus += new EventHandler(txtCIF_lostFocus);
            txtTelefono.LostFocus += new EventHandler(txtTelefono_lostFocus);
            idProveedor = id;
            this.ds = ds;
            this.idUsuario = idUsuario;
            rellenaDatos();
            cargaCbFiltro();
            insert = new InsertHistorial(conexion);

        }
        private void rellenaDatos()
        {
            DataView dv = new DataView(ds.Tables[0]);
            dv.Sort = "IDPROVEEDOR";
            int posicion = dv.Find(idProveedor);
            this.txtCIF.Text = dv[posicion][1].ToString();
            this.txtNombre.Text = dv[posicion][2].ToString();
             this.txtDireccion.Text = dv[posicion][3].ToString();
            this.txtTelefono.Text = dv[posicion][5].ToString();
            this.txtDNI.Text = dv[posicion][7].ToString();
            this.caja_movil.Text = dv[posicion][8].ToString();
            if (txtDNI.Text.Equals("-")) { cbEA.SelectedIndex = 0; }
            else { cbEA.SelectedIndex = 1; }
            this.caja_pais.Text = dv[posicion][9].ToString();
            this.caja_email.Text = dv[posicion][10].ToString();
            this.caja_contacto.Text = dv[posicion][11].ToString();
            this.caja_observaciones.Text = dv[posicion][12].ToString();
           
            String sentencia = "SELECT P.POBLACION,R.PROVINCIA,M.COMUNIDAD,L.CODIGOPOSTAL" +
                 " FROM POBLACIONES P, PROVINCIAS R, COMUNIDADES M,CODIGOSPOSTALES L,CODIGOSPOSTALESPOBLACIONES X" +
                 " WHERE X.IDCODIGOPOSTALPOB = " + dv[posicion][4].ToString() + "AND X.REFPROVINCIA = R.IDPROVINCIA AND R.REFCOMUNIDAD = M.IDCOMUNIDAD" +
                 " AND X.REFPOBLACION = P.IDPOBLACION AND X.REFCODIGOPOSTAL = L.IDCODIGOPOSTAL";
            String comunidad = "", provincia = "", poblacion = "", cp = "";
            DataSet data = new DataSet();
            DataTable tabla = new DataTable();
            data = conexion.getData(sentencia, "CODIGOSPOSTALESPOBLACIONES");
            tabla = data.Tables["CODIGOSPOSTALESPOBLACIONES"];
            foreach (DataRow row in tabla.Rows)
            {
                comunidad = Convert.ToString(row["COMUNIDAD"]);
                provincia = Convert.ToString(row["PROVINCIA"]);
                poblacion = Convert.ToString(row["POBLACION"]);
                cp = Convert.ToString(row["CODIGOPOSTAL"]);
            }
            //Cargo el combo box de comunidades autonomas
            data = conexion.getData("SELECT COMUNIDAD FROM COMUNIDADES ORDER BY ORDEN", "COMUNIDADES");
            tabla = data.Tables["COMUNIDADES"];
            foreach (DataRow row in tabla.Rows)
            {
                cbCAutonoma.Items.Add(Convert.ToString(row["COMUNIDAD"]));
            }
            cbCAutonoma.SelectedItem = comunidad;
            cbProvincia.SelectedItem = provincia;
            cbPoblacion.SelectedItem = poblacion;
            cbCP.SelectedItem = cp;
            cargarTablaArticulosRellenar("SELECT * FROM ARTICULOS WHERE ELIMINADO=0");
            cambios = false;
        }
        //Metodo que si alguno de los campos estan vacios
        private Boolean compruebaCampos()
        {
            Boolean vacio = false;
            this.mensaje = "Faltan por rellenar los siguientes campos: \n";
            if (this.empresaAutonomo == 1)
            {
                if (txtCIF.Text.Equals("")) { mensaje = mensaje + "-CIF \n"; vacio = true; }
            }
            else
            {
                if (this.empresaAutonomo == 0)
                {
                    if (txtDNI.Text.Equals("")) { mensaje = mensaje + "-DNI \n"; vacio = true; }
                }
                else
                {
                    mensaje = mensaje + "-CIF/DNI \n"; vacio = true;
                }
            }
            if (txtNombre.Text.Equals("")) { mensaje = mensaje + "-Nombre \n"; vacio = true; }
            if (txtTelefono.Text.Equals("")) { mensaje = mensaje + "-Teléfono \n"; vacio = true; }
            if (txtDireccion.Text.Equals("")) { mensaje = mensaje + "-Direccion \n"; vacio = true; }
            if (cbCAutonoma.SelectedIndex == -1) { mensaje = mensaje + "-Comunidad Autonoma \n"; vacio = true; }
            if (cbProvincia.SelectedIndex == -1) { mensaje = mensaje + "-Provincia \n"; vacio = true; }
            if (cbPoblacion.SelectedIndex == -1) { mensaje = mensaje + "-Poblacion \n"; vacio = true; }
            if (cbCP.SelectedIndex == -1) { mensaje = mensaje + "-CP \n"; vacio = true; }
            return vacio;
        }
        //Metodo que controla el combobox de DNI
        private void txtCIF_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtCIF.Text.Length >= 9) && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        //Metodo para controlar que solo se escriban caracteres alfabeticos en  en campo nomb
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((Char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtNombre.Text.Length >= 30) && (codigo != 8))
            {
                e.Handled = true;
                lNombre.Text = "SOLO SE PERMITEN LETRAS";
            }
            else
            {
                e.Handled = false;
                lNombre.Text ="";
            }
        }
        //Metodo para controlar que solo se escriban caracteres alfabeticos en  en campo Apellido1
        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtDNI.Text.Length >= 9) && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        //Metodo para controlar que solo se escriban caracteres numericos en  en campo telefono
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTelefono.TextLength >= 9 && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    ltelefono.Text = "";
                }
                else
                {
                    ltelefono.Text = "Solo se pueden introducir numeros";
                    e.Handled = true;
                }
            }
        }


        //Metodo que controla el textbox direccion
        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtDireccion.Text.Length >= 30) && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        //Metodo que Controla que se ha introducido el numero correcto(longitud)
        public void txtTelefono_lostFocus(object sender, EventArgs e)
        {
            if (txtTelefono.Text != "")
            {
                if (txtTelefono.Text.Length < 9)
                {
                    txtTelefono.Text = "";
                    MessageBox.Show(this, "El número de telefono introducido es incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTelefono.Focus();
                }
            }
        }

        
        //Metodo que Controla que se ha introducido el numero correcto(longitud)
        public void txtCIF_lostFocus(object sender, EventArgs e)
        {
            if (txtCIF.Text != "")
            {
                if (MetodosAuxiliares.Valida_CIF(txtCIF.Text) == false)
                {
                    MessageBox.Show(this, "El CIF introducido es incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCIF.Focus();
                }
            }
        }
        //Metodo que durante la carga de la interfaz genera los tooltips
        private void AddProveedor_Load(object sender, EventArgs e)
        {
            tooltip();
            this.dgvAddProveedor.CellValidating += new
        DataGridViewCellValidatingEventHandler(dgvAddProveedor_CellValidating);
            this.dgvAddProveedor.CellEndEdit += new
                DataGridViewCellEventHandler(dgvAddProveedor_CellEndEdit);
        }
        //Metodo que servira para validar el contenido de las celdas
        private void dgvAddProveedor_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int posX = e.ColumnIndex;//Posicion de la columna en el dgv
            int posY = e.RowIndex;//Posicion de la fila en el dgv
            string headerText = dgvAddProveedor.Columns[posX].HeaderText;
            string valor = e.FormattedValue.ToString();
            cambios = true;

            if (posX == 4)//comprobamos la columna correspondiente a Precio, si no está vacia
            {
                if (MetodosAuxiliares.DecimalCorrecto(valor,6,2) == false && !valor.Equals(""))
                {
                    dgvAddProveedor.Rows[posY].Cells[posX].Style.BackColor = Color.Coral;
                }
                else
                {
                    dgvAddProveedor.Rows[posY].Cells[posX].Style.BackColor = Color.White;
                }
            }
            else
            {
                if (posX == 5 )//comprobamos la columna correspondiente a NombreProveedor
                {
                    if (MetodosAuxiliares.stringValidoBBDD(valor) == false && !valor.Equals(""))
                    {
                        dgvAddProveedor.Rows[posY].Cells[posX].Style.BackColor = Color.Coral;
                    }
                    else
                    {
                        dgvAddProveedor.Rows[posY].Cells[posX].Style.BackColor = Color.White;
                    }
                }
                else//si no corresponde a ninguna de las dos anteriores columnas no hace nada y finaliza
                {
                    return;
                }                
            }
        }

        void dgvAddProveedor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.   
            dgvAddProveedor.Rows[e.RowIndex].ErrorText = String.Empty;
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
            toolTip1.SetToolTip(this.btnGuardar, "Guardar proveedor");
            toolTip1.SetToolTip(this.btnCancelar, "Cerrar ventana");
            toolTip1.SetToolTip(this.cbProvincia, "Ha de seleccionar primero la Comunidad Autonoma");
            toolTip1.SetToolTip(this.cbPoblacion, "Ha de seleccionar primero la Provincia");
            toolTip1.SetToolTip(this.cbCP, "Ha de seleccionar primero la Poblacion");
        }
        
        
        //Metodo que carga el combobox de provincias dependiendo de la comunidad autonoma seleccionada
        private void cbCAutonoma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProvincia.Items.Clear();
            cambios = true;
            if (cbCAutonoma.SelectedIndex > 0)
            {
                String cautonoma = cbCAutonoma.SelectedItem.ToString();
                this.idCAutonoma = Convert.ToInt32(conexion.DLookUp("IDCOMUNIDAD", "COMUNIDADES", "COMUNIDAD = '" + cautonoma + "'"));
                rellenarComboBox(cbProvincia, "PROVINCIAS", "PROVINCIA", "REFCOMUNIDAD =" + idCAutonoma + " ORDER BY ORDEN");
                cbProvincia.SelectedIndex = 0;
            }
        }
        //Metodo que carga el combobox de poblaciones dependiendo de la provincia seleccionada
        private void cbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPoblacion.Items.Clear();
            cambios = true;
            if (cbProvincia.SelectedIndex >= 0)
            {
                String provincia = cbProvincia.SelectedItem.ToString();
                this.idProvincia = Convert.ToInt32(conexion.DLookUp("IDPROVINCIA", "PROVINCIAS", "PROVINCIA ='" + provincia + "'"));
                rellenarComboBox(cbPoblacion, "POBLACIONES", "POBLACION", "IDPOBLACION IN (SELECT REFPOBLACION FROM CODIGOSPOSTALESPOBLACIONES WHERE " +
                "REFPROVINCIA =" + idProvincia + ") ORDER BY POBLACION");
                cbPoblacion.SelectedIndex = 0;
            }

        }
        //Metodo que carga el combobox de codigos postales dependiendo de la poblacion seleccionada
        private void cbPoblacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCP.Items.Clear();
            cambios = true;
            if (cbPoblacion.SelectedIndex >= 0)
            {
                String poblacion = cbPoblacion.SelectedItem.ToString();
                this.idPoblacion = Convert.ToInt32(conexion.DLookUp("IDPOBLACION", "POBLACIONES", "POBLACION ='" + poblacion + "'"));
                rellenarComboBox(cbCP, "CODIGOSPOSTALES", "CODIGOPOSTAL", "IDCODIGOPOSTAL IN (SELECT REFCODIGOPOSTAL FROM CODIGOSPOSTALESPOBLACIONES WHERE " +
                "REFPOBLACION = " + idPoblacion + ") ORDER BY CODIGOPOSTAL");
                cbCP.SelectedIndex = 0;
            }

        }
        //Metodo que recoge el id del codigo postal seleccionado
        private void cbCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambios = true;
            this.idCodigoPostal = Convert.ToInt32(conexion.DLookUp("IDCODIGOPOSTAL", "CODIGOSPOSTALES", "CODIGOPOSTAL ='" + cbCP.SelectedItem.ToString() + "'"));
        }
        //Metodo para rellenar los combobox en base al anterior
        private void rellenarComboBox(ComboBox combo, String tabla, String columna, String condicion)
        {
            DataSet data = new DataSet();
            DataTable table = new DataTable();
            //Cargo el combo box de comunidades autonomas
            data = conexion.getData("SELECT * FROM " + tabla + " WHERE " + condicion, tabla);
            table = data.Tables[tabla];
            foreach (DataRow row in table.Rows)
            {
                combo.Items.Add(row[columna]);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Boolean correcto;
            if (cambios)
            {
                DialogResult opcion = MessageBox.Show(this, "Todos los cambios no guardados se perderán, ¿desea guardarlos?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (opcion == DialogResult.Yes)
                {
                    correcto=cargarDataSetProveedores();

                    if (correcto)
                    {
                        daddy.setDS(this.ds);
                        this.Dispose();
                    }            
                }
                else
                {
                    daddy.setDS(this.ds);
                    this.Dispose();
                }
            }
            else
            {
                daddy.setDS(this.ds);
                this.Dispose();
            }

           
        }

        private Boolean cargarDataSetProveedores()
        {
            Boolean guardado = false;
            if (compruebaCampos() == false)
            {
                //Busco el id correspondientede la tabla conjunta CODIGOSPOSTALESPOBLACIONES
                int refCpProPo = Convert.ToInt32(conexion.DLookUp("IDCODIGOPOSTALPOB", "CODIGOSPOSTALESPOBLACIONES",
                    "REFCODIGOPOSTAL =" + this.idCodigoPostal + " AND REFPOBLACION =" + this.idPoblacion + " AND REFPROVINCIA =" + this.idProvincia));
                String cif = "-", nif = "-", direccion = txtDireccion.Text, nombre = txtNombre.Text;
                String movilC = caja_movil.Text;
                int movil;
                if (movilC.Equals("")) movil = 0;
                else movil = Convert.ToInt32(movilC);
                int telefono = Convert.ToInt32(txtTelefono.Text);
                String pais = "-", email = "-";
                String observaciones = caja_observaciones.Text;
                String contacto = caja_contacto.Text;
                if (contacto.Equals("")) contacto = "-";
                if (observaciones.Equals("")) observaciones = "-";
                if (!txtCIF.Text.Equals("")) { cif = txtCIF.Text; }
                if (!txtDNI.Text.Equals("")) { nif = txtDNI.Text; }
                if (!caja_pais.Text.Equals("")) pais = caja_pais.Text;
                if (!caja_email.Text.Equals("")) email = caja_email.Text;
                if (mod == false)
                {
                    this.idProveedor = MetodosAuxiliares.ultimoID(conexion, "IDPROVEEDOR", "PROVEEDORES");

                    DataTable dt = ds.Tables[0];
                    String[] columnas = { "IDPROVEEDOR", "CIF", "NOMBRE", "DIRECCION", "REFCPPOBLACIONES", "TELEFONO", "ELIMINADO", "NIF", "MOVIL", "PAIS", "EMAIL", "CONTACTO", "OBSERVACIONES" };
                    Object[] valores = { idProveedor, cif, nombre, direccion, refCpProPo, telefono, 0, nif, movil, pais, email, contacto, observaciones };

                    Boolean correcto = guardarCambiosProveedores();
                    if (correcto)
                    {
                        XML_proveedor.rellenaFilas(dt, columnas, valores);
                        MessageBox.Show(movilC + " " + pais);
                        ds.Tables.Clear();
                        ds.Tables.Add(dt);
                        //insert en historial de cambios
                        insert.insertHistorialCambio(idUsuario, 1, "Proveedor añadido ->" + nombre);

                        guardado = true;
                    }
                }
                else
                {

                    Boolean correcto = guardarCambiosProveedores();

                    if (correcto)
                    {
                        DataView dv = new DataView(ds.Tables[0]);
                        dv.Sort = "IDPROVEEDOR";
                        int index = dv.Find(idProveedor);
                        dv[index][0] = idProveedor;
                        dv[index][1] = cif;
                        dv[index][2] = nombre;
                        dv[index][3] = direccion;
                        dv[index][4] = refCpProPo;
                        dv[index][5] = telefono;
                        dv[index][6] = 0;
                        dv[index][7] = nif;
                        dv[index][8] = movil;
                        dv[index][9] = pais;
                        dv[index][10] = email;
                        dv[index][11] = contacto;
                        dv[index][12] = observaciones;
                        //MessageBox.Show(this, "Proveedor modificado", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Se realiza el insert en la tabla Historial
                        insert.insertHistorialCambio(idUsuario, 1, "Proveedor modificado ->" + nombre);
                        guardado = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(this, mensaje,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return guardado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (cargarDataSetProveedores() == true)
            {
                if (mod == false)
                {

                    DialogResult opcion = MessageBox.Show("¿Desea añadir más Proveedores?", "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (opcion == DialogResult.Yes)
                    {
                        limpiar();
                        CargarTablas.borrarTabla(dgvAddProveedor);
                        cargarTablaArticulos();

                    }
                    else
                    {
                        this.Dispose();
                    }

                }
                else
                {
                    this.Dispose();
                }

            }

        }
        /// <summary>
        /// Metodo que comprueba el dgv y almacena los datos
        /// </summary>
        /// <returns></returns>
        private Boolean guardarCambiosProveedores()
        {
            Boolean correcto = true;
            DataTable tabla = new DataTable();
            tabla.Columns.Add("IDARTICULO");
            tabla.Columns.Add("PRECIO");
            tabla.Columns.Add("NOMBRE");
            tabla.Columns.Add("ELIMINADO");
            String[] campos = { "IDARTICULO", "PRECIO", "NOMBRE", "ELIMINADO"};
            for (int i = 0; i < dgvAddProveedor.Rows.Count; i++)
            {
                if (dgvAddProveedor.Rows[i].Cells[4].Style.BackColor == Color.White || dgvAddProveedor.Rows[i].Cells[5].Style.BackColor == Color.White)
                {
                    if ((dgvAddProveedor.Rows[i].Cells[4].Value == null && !(dgvAddProveedor.Rows[i].Cells[5].Value == null)) || (!(dgvAddProveedor.Rows[i].Cells[4].Value == null) && dgvAddProveedor.Rows[i].Cells[5].Value == null))
                    {
                        correcto = false;
                        MessageBox.Show("ERROR: No puedes rellenar solo uno de los campos editables","WARNING",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return correcto;
                    }
                    else
                    {
                        if (!(dgvAddProveedor.Rows[i].Cells[4].Value == null) && !(dgvAddProveedor.Rows[i].Cells[5].Value == null))
                        {
                            Object[] valores = { dgvAddProveedor.Rows[i].Cells[0].Value, MetodosAuxiliares.transformaDecimalADecimalBBDD(dgvAddProveedor.Rows[i].Cells[4].Value.ToString()), dgvAddProveedor.Rows[i].Cells[5].Value, 0 };
                            XML_proveedor.rellenaFilas(tabla, campos, valores);
                        }
                        else//si ambos estan en blanco comprobamos si el articulo pertenecia ya al proveedor, en caso de ser cierto lo eliminamos (ELIMINADO = 1)
                        {
                            int idArt = Convert.ToInt32(dgvAddProveedor.Rows[i].Cells[0].Value);
                            int comprobarId = Convert.ToInt32(conexion.DLookUp("IDPROVEEDORARTICULO", "PROVEEDORARTICULOS", " IDARTICULO= " + idArt + " AND IDPROVEEDOR= " + idProveedor));
                            if (comprobarId != -1)
                            {
                                Object[] valores = { dgvAddProveedor.Rows[i].Cells[0].Value, 0, "-", 1 };
                                XML_proveedor.rellenaFilas(tabla, campos, valores);
                            }
                        }
                    }
                }
                if (dgvAddProveedor.CurrentRow.Cells[4].Style.BackColor == Color.Coral || dgvAddProveedor.CurrentRow.Cells[5].Style.BackColor == Color.Coral)
                {
                    MessageBox.Show("Existen campos mal escritos en la tabla", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            foreach (DataRow row in tabla.Rows)
            {
                int idProvArt;
                int idArt = Convert.ToInt32(row["IDARTICULO"]);
                double precio = Convert.ToSingle(row["PRECIO"]);
                precio = Math.Round(precio, 2);
                String nombre = Convert.ToString(row["NOMBRE"]);
                int eliminado = Convert.ToInt32(row["ELIMINADO"]);
                int comprobarId = Convert.ToInt32(conexion.DLookUp("IDPROVEEDORARTICULO", "PROVEEDORARTICULOS", " IDARTICULO= " + idArt + " AND IDPROVEEDOR= " + idProveedor));
                String sql;
                if (comprobarId == -1)
                {
                    idProvArt = MetodosAuxiliares.ultimoID(conexion, "IDPROVEEDORARTICULO", "PROVEEDORARTICULOS");
                    sql = "INSERT INTO PROVEEDORARTICULOS VALUES(" + idProvArt + ", " + idProveedor + ", " + idArt + ",'" + nombre + "', '" + precio + "', "+eliminado+")";
                }
                else
                {
                    idProvArt = comprobarId;
                    sql = "UPDATE PROVEEDORARTICULOS SET NOMBREPROVEEDOR= '" + nombre + "', PRECIOPROVEEDOR= '" + precio + "',ELIMINADO = "+eliminado+" WHERE IDPROVEEDORARTICULO= " + idProvArt;
                }
                conexion.setData(sql);
            }
            return correcto;
        }

        //MÉTODO PARA GUARDAR EN LA BBDD
        private void cargarProveedorBBDD(DataSet ds)
        {
            try
            {
                String[,] datos = XML_proveedor.convertirDsArrayBi(ds);
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
                                ",ELIMINADO= " + datos[i, 6] + ", NIF ='" + datos[i, 7] + "', MOVIL =" + datos[i, 8] + ", PAIS ='" + datos[i, 9] +
                                "', EMAIL = '" + datos[i, 10] + "', CONTACTO='" + datos[i, 11] + "', OBSERVACIONES='" + datos[i, 12] + "' WHERE IDPROVEEDOR=" + datos[i, 0];
                        }
                        else
                        {
                            Console.WriteLine("ENTRO EN INSERT");
                            Console.Write(" " + datos[i, 0]);
                            sentencia = "INSERT INTO PROVEEDORES (IDPROVEEDOR,CIF,NOMBRE,DIRECCION,REFCPPOBLACIONES,TELEFONO,ELIMINADO,NIF,MOVIL,PAIS,EMAIL,CONTACTO,OBSERVACIONES)" +
                                        " VALUES(" + datos[i, 0] + ",'" + datos[i, 1] + "','" + datos[i, 2] + "','" + datos[i, 3] + "'," + datos[i, 4] +
                                        "," + datos[i, 5] + "," + datos[i, 6] + ",'" + datos[i, 7] + "'," + datos[i, 8] + ",'" + datos[i, 9] + "','" + datos[i, 10] + "','" + datos[i, 11] + "','" + datos[i, 12] + "')";
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

        //METODO CARGAR TABLA ARTICULOS
        private void cargarTablaArticulos()
        {
            
            //limpiarTabla();
            DataSet resultado = conexion.getData("SELECT * FROM ARTICULOS WHERE ELIMINADO=0", "ARTICULOS");
            DataTable tArticulos = resultado.Tables["ARTICULOS"];
            foreach (DataRow row in tArticulos.Rows)
            {
                int id = Convert.ToInt32(row["IDARTICULO"]);
                String nombre = Convert.ToString(row["NOMBRE"]);
                String composicion = Convert.ToString(conexion.DLookUp("COMPOSICION", "COMPOSICIONES", "IDCOMPOSICION=" + row["REFCOMPOSICION"]));
                String medida = Convert.ToString(conexion.DLookUp("MEDIDA", "MEDIDAS", "IDMEDIDA=" + row["REFMEDIDA"]));
                dgvAddProveedor.Rows.Add(id, nombre, composicion, medida);

            } // Fin del bucle for each
        }
        private void cargarTablaArticulosRellenar(String sql)
        {

            //limpiarTabla();
            DataSet resultado = conexion.getData(sql, "ARTICULOS");
            DataTable tArticulos = resultado.Tables["ARTICULOS"];
            foreach (DataRow row in tArticulos.Rows)
            {
                int id = Convert.ToInt32(row["IDARTICULO"]);
                String nombre = Convert.ToString(row["NOMBRE"]);
                String composicion = Convert.ToString(conexion.DLookUp("COMPOSICION", "COMPOSICIONES", "IDCOMPOSICION=" + row["REFCOMPOSICION"]));
                String medida = Convert.ToString(conexion.DLookUp("MEDIDA", "MEDIDAS", "IDMEDIDA=" + row["REFMEDIDA"]));
                int idProvArt = Convert.ToInt32(conexion.DLookUp("IDPROVEEDORARTICULO", "PROVEEDORARTICULOS", "IDARTICULO=" + id + " AND IDPROVEEDOR=" + idProveedor+" AND ELIMINADO = 0"));
                if (idProvArt == -1)
                {
                    dgvAddProveedor.Rows.Add(id, nombre, composicion, medida);
                }
                else
                {
                    double precio = Math.Round(Convert.ToSingle(conexion.DLookUp("PRECIOPROVEEDOR", "PROVEEDORARTICULOS", "IDPROVEEDORARTICULO=" + idProvArt)), 2);
                    String nombreArtP = Convert.ToString(conexion.DLookUp("NOMBREPROVEEDOR", "PROVEEDORARTICULOS", "IDPROVEEDORARTICULO=" + idProvArt));
                    dgvAddProveedor.Rows.Add(id, nombre, composicion, medida,precio,nombreArtP);
                }
            } // Fin del bucle for each
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEA.SelectedIndex != -1)
            {
                if (cbEA.SelectedIndex == 0)
                {
                    empresaAutonomo = 1;
                    lCIF.Visible = true;
                    lDNI.Visible = false;
                    txtCIF.Enabled = true;
                    txtDNI.Enabled = false;
                    txtCIF.Visible = true;
                    txtDNI.Visible = false;
                }
                else
                {
                    //Es autonomo
                    empresaAutonomo = 0;
                    lCIF.Visible = false;
                    lDNI.Visible = true;
                    txtCIF.Enabled = false;
                    txtDNI.Enabled = true;
                    txtCIF.Visible = false;
                    txtDNI.Visible = true;
                }
            }
        }
        //Metodo que limpia los campos
        private void limpiar()
        {
            txtNombre.Text = "";
            txtDNI.Text = "";
            txtCIF.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            caja_movil.Text = "";
            caja_email.Text = "";
            caja_pais.Text = "";
            cbCAutonoma.SelectedIndex = 0;
            cbCP.Items.Clear();
            cbPoblacion.Items.Clear();
            lCIF.Visible = false;
            lDNI.Visible = false;
            txtCIF.Enabled = false;
            txtDNI.Enabled = false;
            txtCIF.Visible = false;
            txtDNI.Visible = false;
            cbEA.SelectedIndex = 0;
        }

        private void caja_movil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (caja_movil.TextLength >= 9 && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    ltelefono.Text = "";
                }
                else
                {
                    ltelefono.Text = "Solo se pueden introducir numeros";
                    e.Handled = true;
                }
            }
        }

        private void AddProveedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargarProveedorBBDD(ds);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            cambios = true;
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            cambios = true;
        }

        private void caja_movil_TextChanged(object sender, EventArgs e)
        {
            cambios = true;
        }

        private void caja_email_TextChanged(object sender, EventArgs e)
        {
            cambios = true;
        }

        private void caja_contacto_TextChanged(object sender, EventArgs e)
        {
            cambios = true;
        }

        private void caja_observaciones_TextChanged(object sender, EventArgs e)
        {
            cambios = true;
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            cambios = true;
        }

        private void caja_pais_TextChanged(object sender, EventArgs e)
        {
            cambios = true;
        }

        private void cargaCbFiltro()
        {
            DataSet data;
            DataTable tabla = null;
            String sentencia;
            
                sentencia = "SELECT COMPOSICION FROM COMPOSICIONES order by idcomposicion";
                data = conexion.getData(sentencia, "COMPOSICIONES");
                tabla = data.Tables["COMPOSICIONES"];
                cbComposicion.Items.Add("--Seleccione composicion--");
                foreach (DataRow row in tabla.Rows)
                {
                    cbComposicion.Items.Add(Convert.ToString(row["COMPOSICION"]));
                }
                cbComposicion.SelectedIndex = 0;
        }

        private void filtrar()
        {
            dgvAddProveedor.Rows.Clear();
            String sentencia = "SELECT * FROM ARTICULOS WHERE ELIMINADO=0";
            if (!txtArticulo.Equals(""))
            {
                sentencia = sentencia + " AND UPPER(NOMBRE) LIKE '%" + txtArticulo.Text.ToUpper() + "%'";
            }
            if (cbComposicion.SelectedIndex > 0)
            {
                int refComp =Convert.ToInt32(conexion.DLookUp("IDCOMPOSICION", "COMPOSICIONES", "COMPOSICION= '" + cbComposicion.SelectedItem.ToString()+"'"));
                sentencia = sentencia + " AND REFCOMPOSICION= "+refComp;
            }
            cargarTablaArticulosRellenar(sentencia);
        }

        private void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void cbComposicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtArticulo.Text = "";
            cbComposicion.SelectedIndex = 0;
            filtrar();
        }
    }
}
