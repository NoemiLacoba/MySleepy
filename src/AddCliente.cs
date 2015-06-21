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
    public partial class AddCliente : Form
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
        //Atributo que almacena el id a controlar
        private int idCliente;
        //Atributo que almacena la ventana padre
        private ClientesForm padre;
        private ToolTip toolTip1;
        //Atributo que almacena el id del usuario que se conecta a la aplicación
        private int idUsuario;
        //Atributo que representa el objeto con el que realizaremos los insert en la tabla historial de cambios
        InsertHistorial insert;
        public AddCliente(ConnectDB conexion, ClientesForm padre, int idUsuario)
        {
            toolTip1 = new ToolTip();
            InitializeComponent();
            this.mod = false;
            this.confirmacion = "¿Desea añadir el cliente?";
            //iniciamos la conexion 
            this.conexion = conexion;
            DataSet data = new DataSet();
            DataTable tabla = new DataTable();
            //Cargo el combo box de comunidades autonomas
            data = conexion.getData("SELECT COMUNIDAD FROM COMUNIDADES ORDER BY ORDEN", "COMUNIDADES");
            tabla = data.Tables["COMUNIDADES"];
            this.idUsuario = idUsuario;
            foreach (DataRow row in tabla.Rows)
            {
                cbCAutonoma.Items.Add(Convert.ToString(row["COMUNIDAD"]));
            }
            this.padre = padre;
            //txtDNI.LostFocus += new EventHandler(txtDNI_lostFocus);
            txtEmail.LostFocus += new EventHandler(txtEmail_lostFocus);
            txtTelefono.LostFocus += new EventHandler(txtTelefono_lostFocus);
            insert = new InsertHistorial(conexion);
        }
        public AddCliente(ConnectDB conexion, ClientesForm padre, int id, int idUsuario)
        {
            toolTip1 = new ToolTip();
            InitializeComponent();
            this.mod = true;
            this.confirmacion = "¿Desea modificar al cliente?";
            //iniciamos la conexion 
            this.conexion = conexion;
            //txtDNI.LostFocus += new EventHandler(txtDNI_lostFocus);
            txtEmail.LostFocus += new EventHandler(txtEmail_lostFocus);
            txtTelefono.LostFocus += new EventHandler(txtTelefono_lostFocus);
            idCliente = id;
            this.padre = padre;
            rellenaDatos();
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
        }
        //Metodo que rellena los campos segun el id pasado
        private void rellenaDatos()
        {
            String sentencia = "SELECT distinct C.IDCLIENTE,C.NOMBRE,C.APELLIDO1,C.APELLIDO2,C.TELEFONO,C.EMAIL,C.DIRECCION,C.DNI," +
                "P.POBLACION,R.PROVINCIA,M.COMUNIDAD,L.CODIGOPOSTAL" +
                " FROM CLIENTES C, POBLACIONES P, PROVINCIAS R, COMUNIDADES M,CODIGOSPOSTALES L,CODIGOSPOSTALESPOBLACIONES X" +
                " WHERE C.REFCPPOBLACIONES=X.IDCODIGOPOSTALPOB AND X.REFPROVINCIA = R.IDPROVINCIA AND R.REFCOMUNIDAD = M.IDCOMUNIDAD" +
                " AND X.REFPOBLACION = P.IDPOBLACION AND X.REFCODIGOPOSTAL = L.IDCODIGOPOSTAL AND C.IDCLIENTE = " + idCliente;
            String dni = "", nombre = "", apellido1 = "", apellido2 = "", telefono = "", email = "", direccion = "", comunidad = "", provincia = "", poblacion = "", cp = "";
            DataSet data = new DataSet();
            DataTable tabla = new DataTable();
            data = conexion.getData(sentencia, "CLIENTES");
            tabla = data.Tables["CLIENTES"];
            foreach (DataRow row in tabla.Rows)
            {
                dni = Convert.ToString(row["DNI"]);
                nombre = Convert.ToString(row["NOMBRE"]);
                apellido1 = Convert.ToString(row["APELLIDO1"]);
                apellido2 = Convert.ToString(row["APELLIDO2"]);
                telefono = Convert.ToString(row["TELEFONO"]);
                email = Convert.ToString(row["EMAIL"]);
                direccion = Convert.ToString(row["DIRECCION"]);
                comunidad = Convert.ToString(row["COMUNIDAD"]);
                provincia = Convert.ToString(row["PROVINCIA"]);
                poblacion = Convert.ToString(row["POBLACION"]);
                cp = Convert.ToString(row["CODIGOPOSTAL"]);
            }
            txtDNI.Text = dni;
            txtNombre.Text = nombre;
            txtApellido1.Text = apellido1;
            txtApellido2.Text = apellido2;
            txtTelefono.Text = telefono;
            txtEmail.Text = email;
            txtDireccion.Text = direccion;
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
        }
        //Boton guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (compruebaCampos() == false)
            {
                confirmacion = confirmacion + "\n-" + txtNombre.Text + "  " + txtApellido1.Text + " " + txtApellido2.Text;
                //Se pide la confirmacion al usuario para realizar dicho cambio en la BBDD
                DialogResult option = MessageBox.Show(this, this.confirmacion, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (option == DialogResult.Yes)
                {
                    String nombre = txtNombre.Text, apellido1 = txtApellido1.Text, apellido2 = txtApellido2.Text, email = txtEmail.Text,
                            direccion = txtDireccion.Text, dni = txtDNI.Text;
                    int telefono = Convert.ToInt32(txtTelefono.Text);
                    //Busco el id correspondientede la tabla conjunta CODIGOSPOSTALESPOBLACIONES
                    int refCpProPo = Convert.ToInt32(conexion.DLookUp("IDCODIGOPOSTALPOB", "CODIGOSPOSTALESPOBLACIONES",
                        "REFCODIGOPOSTAL =" + this.idCodigoPostal + " AND REFPOBLACION =" + this.idPoblacion + " AND REFPROVINCIA =" + this.idProvincia));
                    //Console.WriteLine(refCpProPo);
                    if (this.mod == false)
                    {
                        if (isExist() == false)
                        {
                            //Busco el ultimo id existente en la tabla clientes y lo aumento en 1
                            idCliente = MetodosAuxiliares.ultimoID(conexion,"IDCLIENTE","CLIENTES");
                            
                            //Almaceno los datos en la base de datos
                            String ins = "INSERT INTO CLIENTES (IDCLIENTE,NOMBRE,APELLIDO1,APELLIDO2,DIRECCION,REFCPPOBLACIONES,TELEFONO,EMAIL,ELIMINADO,DNI)" +
                                " VALUES(" + idCliente + ",'" + nombre + "','" + apellido1 + "','" + apellido2 + "','" + direccion +
                                "'," + refCpProPo + "," + telefono + ",'" + email + "'," + 0 + ",'" + dni + "')";

                           
                            conexion.setData(ins);
                            //reseteamos el mensaje para que no se acumulen los nombres
                            confirmacion = "¿Desea añadir el cliente?";

                            //Insert en la tabla historial de cambios
                            insert.insertHistorialCambio(idUsuario, 1, "Cliente añadido ->" + nombre);

                            DialogResult opcion = MessageBox.Show("¿Desea añadir más Clientes?", "Question",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            String sentencia = "SELECT * FROM CLIENTES C, POBLACIONES P, CODIGOSPOSTALESPOBLACIONES X,PROVINCIAS R WHERE C.REFCPPOBLACIONES=X.IDCODIGOPOSTALPOB AND X.REFPOBLACION = P.IDPOBLACION AND X.REFPROVINCIA = R.IDPROVINCIA";
                            if (opcion == DialogResult.Yes)
                            {
                                
                                padre.cargarTabla(sentencia);
                                limpiar();
                            }
                            else
                            {
                                
                                padre.cargarTabla(sentencia);
                                this.Dispose();
                            }
                        }//fin if existe cliente
                    }//fin if añadir
                    else
                    {
                        //Actualizo los datos del cliente
                        String update = "UPDATE CLIENTES set NOMBRE = '" + nombre + "', APELLIDO1 = '" + apellido1 +
                            "',APELLIDO2 = '" + apellido2 + "', DIRECCION = '" + direccion + "', REFCPPOBLACIONES = " + refCpProPo +
                            ",TELEFONO = " + telefono + ", EMAIL = '" + email + "', DNI ='" + dni + "' WHERE IDCLIENTE=" + idCliente;
                        conexion.setData(update);
                        String sentencia = "SELECT * FROM CLIENTES C, POBLACIONES P, CODIGOSPOSTALESPOBLACIONES X,PROVINCIAS R WHERE C.REFCPPOBLACIONES=X.IDCODIGOPOSTALPOB AND X.REFPOBLACION = P.IDPOBLACION AND X.REFPROVINCIA = R.IDPROVINCIA";
                        padre.cargarTabla(sentencia);
                        //reseteamos el mensaje para que no se acumulen los nombres
                        confirmacion = "¿Desea modificar al cliente?";
                       //insert en la tabla historial de cambios
                        insert.insertHistorialCambio(idUsuario, 2, "Cliente modificado ->" + nombre);
                        this.Dispose();
                    }//fin else modificar

                }//fin if permiso
            }//fin if comprobar campos
            else { MessageBox.Show(this, mensaje, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        //Metodo que si alguno de los campos estan vacios
        private Boolean compruebaCampos()
        {
            Boolean vacio = false;
            this.mensaje = "Faltan por rellenar los siguientes campos: \n";
            if (txtDNI.Text.Equals("")) { mensaje = mensaje + "-DNI \n"; vacio = true; }
            if (txtNombre.Text.Equals("")) { mensaje = mensaje + "-Nombre \n"; vacio = true; }
            if (txtApellido1.Text.Equals("")) { mensaje = mensaje + "-Primer Apellido \n"; vacio = true; }
            if (txtApellido2.Text.Equals("")) { mensaje = mensaje + "-Segundo Apellido \n"; vacio = true; }
            if (txtTelefono.Text.Equals("")) { mensaje = mensaje + "-Teléfono \n"; vacio = true; }
            if (txtDireccion.Text.Equals("")) { mensaje = mensaje + "-Direccion \n"; vacio = true; }
            if (cbCAutonoma.SelectedIndex == -1) { mensaje = mensaje + "-Comunidad Autonoma \n"; vacio = true; }
            if (cbProvincia.SelectedIndex == -1) { mensaje = mensaje + "-Provincia \n"; vacio = true; }
            if (cbPoblacion.SelectedIndex == -1) { mensaje = mensaje + "-Poblacion \n"; vacio = true; }
            if (cbCP.SelectedIndex == -1) { mensaje = mensaje + "-CP \n"; vacio = true; }
            return vacio;
        }
        //Metodo que controla el combobox de DNI
        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtDNI.Text.Length >= 9) && (codigo != 8))
            {
                e.Handled = true;
            }
            else if (txtDNI.Text.Length == 8)
            {
                txtDNI.Text = MetodosAuxiliares.CalculaNIF(txtDNI.Text);
            }
            else
            {
                e.Handled = false;
            }
        }
        //Metodo para controlar que solo se escriban caracteres alfabeticos en  en campo nombre
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((Char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtNombre.Text.Length >= 30) && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        //Metodo para controlar que solo se escriban caracteres alfabeticos en  en campo Apellido1
        private void txtApellido1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((Char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtApellido1.Text.Length >= 20) && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        //Metodo para controlar que solo se escriban caracteres alfabeticos en  en campo Apellido2
        private void txtApellido2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((Char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtApellido2.Text.Length >= 20) && (codigo != 8))
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
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((Char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || txtTelefono.Text.Length >= 9) && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        //Metodo que controla el textbox del email
        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((e.KeyChar.Equals('\'') || txtEmail.Text.Length >= 30) && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        //Metodo que controla el textbox de direccion
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
        //Metodo que comprueba si el email introducido es el correcto
        public void txtEmail_lostFocus(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                if (MetodosAuxiliares.emailCorrecto(txtEmail.Text) == false)
                {
                    //label indicando que esta mal
                    lblEmailErroneo.Text = "Formato erroneo";
                    txtEmail.Focus();
                   
                }
                else
                {
                    //label en blanco
                    lblEmailErroneo.Text = "";
                }
            }
        }
        //Metodo que Controla que se ha introducido el numero correcto(longitud)
        public void txtTelefono_lostFocus(object sender, EventArgs e)
        {
            if (txtTelefono.Text != "")
            {
                if (txtTelefono.Text.Length < 9)
                {
                    
                    //label indicando que es erroneo
                    lblTelErroneo.Text = "Longitud erronea";
                    txtTelefono.Focus();
                }
                else
                {
                    //label en blanco
                    lblTelErroneo.Text = "";
                }
            }
        }
        //Metodo que Controla que se ha introducido el numero correcto(longitud)
        /*public void txtDNI_lostFocus(object sender, EventArgs e)
        {
            if (txtDNI.Text != "")
            {
                if (MetodosAuxiliares.VerificarNIF(txtDNI.Text) == false)
                {
                    //rellenar letra
                    txtDNI.Focus();
                }
            }
        }*/
        //Metodo que carga el combobox de provincias dependiendo de la comunidad autonoma seleccionada
        private void cbCAutonoma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProvincia.Items.Clear();
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
            this.idCodigoPostal = Convert.ToInt32(conexion.DLookUp("IDCODIGOPOSTAL", "CODIGOSPOSTALES", "CODIGOPOSTAL ='" + cbCP.SelectedItem.ToString() + "'"));
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            String sentencia = "SELECT * FROM CLIENTES C, POBLACIONES P, CODIGOSPOSTALESPOBLACIONES X,PROVINCIAS R WHERE C.REFCPPOBLACIONES=X.IDCODIGOPOSTALPOB AND X.REFPOBLACION = P.IDPOBLACION AND X.REFPROVINCIA = R.IDPROVINCIA";
            padre.cargarTabla(sentencia);
            this.Close();
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
        //Metodo que limpia los campos
        private void limpiar()
        {
            txtNombre.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            cbCAutonoma.SelectedIndex = 0;
            cbCP.Items.Clear();
            cbPoblacion.Items.Clear();
            this.confirmacion = "¿Desea añadir el cliente?";
        }
        //Metodo que comprueba si ese cliente ya existe
        private Boolean isExist()
        {
            String sentencia = "SELECT DNI FROM CLIENTES WHERE UPPER(DNI) = '" + txtDNI.Text.ToUpper()+"'";
            String coincidencia = Convert.ToString(conexion.DLookUp("DNI", "CLIENTES", " upper(dni) = '" + txtDNI.Text.ToUpper() + "'"));
            

            // -1 si no encuentra ninguna coincidencia
            if (!coincidencia.Equals("-1"))
            {
                MessageBox.Show(this, "El cliente introducido ya existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
        //Metodo que durante la carga de la interfaz genera los tooltips
        private void AddCliente_Load(object sender, EventArgs e)
        {
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
            toolTip1.SetToolTip(this.btnGuardar, "Guardar cliente");
            toolTip1.SetToolTip(this.btnCancelar, "Cerrar ventana");
            toolTip1.SetToolTip(this.cbProvincia, "Ha de seleccionar primero la Comunidad Autonoma");
            toolTip1.SetToolTip(this.cbPoblacion, "Ha de seleccionar primero la Provincia");
            toolTip1.SetToolTip(this.cbCP, "Ha de seleccionar primero la Poblacion");
        }

        private void AddCliente_Load_1(object sender, EventArgs e)
        {

        }
    }
}
