using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySleepy;

namespace MySleepy
{
    public partial class AddUsuario : Form
    {
        ConnectDB conexion;
        String pass;
        UsuariosForm usuario;
        int señal,idUsuario;
        String mensajeError = "Por favor rellene los campos vacíos : ";
        InsertHistorial insert;
        public AddUsuario(UsuariosForm u,ConnectDB conexion,int señal, int idUsuario)
        {
            InitializeComponent();
            txtRepetirPassword.LostFocus += new EventHandler(txtRepetirPassword_lostFocus);
            txtNombre.LostFocus += new EventHandler(txtNombre_lostFocus);
            this.conexion = conexion;
            this.señal = señal;
            this.usuario = u;
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
            rellenarCombo();

            insert = new InsertHistorial(conexion);
            if (señal == 1)
            {
                //partimos del boton modificar del formulario anterior
                asignarAForm();
            }
        }


        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            //Actualizamos la tabla
            usuario.cargarTablaInicio();            
        }
        
        public void txtRepetirPassword_lostFocus(object sender, EventArgs e)
        {
            if (coincidenciaPassword() == false) //si no coinciden, no se perderá el foco
            {
                lblError.Text = "No coincide";
            }else{
                lblError.Text = "";
            }

        }
        public void txtNombre_lostFocus(object sender, EventArgs e)
        {
            //Comprobamos que no haya un usuario con ese nombre en la base de datos
            if (usuarioRepetido(txtNombre.Text) && señal == 0)
            {
                lblNombre.Text = " Usuario repetido";
            }
            else
            {
                lblNombre.Text = "";
            }
        }
                
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (!txtPassword.Text.Equals(txtRepetirPassword.Text) )
            {
                MessageBox.Show("AQUI");
                lblError.Text = "No coinciden";
            }
            if (usuarioRepetido(txtNombre.Text))
            {
                lblNombre.Text = "Usuario repetido";
            }
            else if (txtPassword.Text.Equals(txtRepetirPassword.Text) && !usuarioRepetido(txtNombre.Text))
            {
                lblNombre.Text = "";
                lblError.Text = "";
                if (señal == 0)
                {
                    //añadir
                    añadirRegistro();
                }
                else
                {

                    //modificar
                    modificarRegistro();
                    usuario.cargarTablaInicio();
                    this.Close(); //cerramos el form

                }
            }
        }
        public void asignarAForm()
        {
            //extraemos los datos de la fila seleccionada
            int idUsuario = usuario.extraerIDTabla();

            String select = "Select NOMBRE,IDROL from USUARIOS where IDUSUARIO = " + idUsuario;

            DataSet data = conexion.getData(select, "USUARIOS");

            DataTable tTabla = data.Tables["USUARIOS"];

            int idRol = 0;
            String nombre = "";//,pass="";

            foreach (DataRow row in tTabla.Rows)
            {
                idRol = Convert.ToInt32(row["IDROL"]);
                nombre = Convert.ToString(row["NOMBRE"]);
                //pass = Convert.ToString(row["PASSWORD"]);

            } // Fin del bucle for each
           
            //asignamos
            txtNombre.Text = nombre;
            //txtPassword.Text = encriptarPassword(pass,false); no pondremos la contraseña, solo se podrá cambiar, no obtener
            cbRoles.SelectedItem = getNombreRol(idRol);
            
        }
        public void limpiar()
        {
            txtNombre.Text = "";
            txtPassword.Text = "";
            txtRepetirPassword.Text = "";
            cbRoles.SelectedIndex = -1;
        }
        public String getNombreRol(int idRol)
        {
            String nombreRol = conexion.DLookUp("NOMBREROL", "ROLES", " IDROL = " + idRol).ToString();  
            return nombreRol;
        }
        public void modificarRegistro()
        {
                //Pedimos confirmacion
                DialogResult opcion = MessageBox.Show("¿Desea modificar el usuario?", "Confirmación",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                String nombre = txtNombre.Text;
                int idRol = Convert.ToInt32(conexion.DLookUp("IDROL", "ROLES", " NOMBREROL = '" + cbRoles.SelectedItem.ToString() + "'"));
                int idUsu = usuario.extraerIDTabla();
                String update = "";
                if (opcion == DialogResult.OK)
                {
                    if (txtPassword.Text.Equals("") && txtRepetirPassword.Text.Equals(""))
                    {
                         update = " update USUARIOS set NOMBRE = '" + nombre + "' " +
                            " ,IDROL = " + idRol + " where IDUSUARIO = " + idUsu
                                                               + " and ELIMINADO = " + 0;
                    }
                    else
                    {
                        String passEncriptada = encriptarPassword(txtPassword.Text);    //en este metodo veo si coinciden ambas contraseñas
                        
                        //update
                        update = " update USUARIOS set NOMBRE = '" + nombre + "' ," + " PASSWORD = '"
                                        + passEncriptada + "' ,IDROL = " + idRol + " where IDUSUARIO = " + idUsu
                                                                   + " and ELIMINADO = " + 0;
                       
                    }
                    //MessageBox.Show(update);
                    conexion.setData(update);
                    //MessageBox.Show("Usuario modificado");

                    //insert en la tabla historial cambios
                    String nombreUsuario = Convert.ToString
                                            (conexion.DLookUp("NOMBRE", "USUARIOS", " IDUSUARIO = " + idUsu));
                    insert.insertHistorialCambio(idUsuario, 2, "Usuario modificado ->" + nombreUsuario);
                }
              
           
        }

        public void añadirRegistro()
        {
            if (vacio())
            {
                MessageBox.Show(mensajeError);
            }
            else
            {
                //Pedimos confirmacion
                DialogResult opcion = MessageBox.Show("¿Desea crear el usuario?", "Confirmación",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (opcion == DialogResult.OK)
                {
                    int idRol = extraerIDRol();
                    int idUsu = ultimoIDUser() + 1;
                    //Creamos el usuario, por defecto a no eliminado
                    String ins = "Insert into USUARIOS values ( " + idUsu + ", '"
                                       + txtNombre.Text + "' , '" + encriptarPassword(txtPassword.Text) + "' ," + 0 + "," + idRol + ")";
                    //Console.WriteLine(insert);
                    //realizamos el insert
                    conexion.setData(ins);
                    //MessageBox.Show("Usuario creado");

                    //insert en la tabla historial de cambios
                    String nombreUsuario = Convert.ToString
                                            (conexion.DLookUp("NOMBRE", "USUARIOS", " IDUSUARIO = " + idUsu));
                    insert.insertHistorialCambio(idUsuario, 1, "Usuario añadido ->" + nombreUsuario);
                    opcion = MessageBox.Show("¿Desea añadir más usuarios?", "Confirmación",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (opcion == DialogResult.OK)
                    {
                        //Actualizamos la tabla
                        usuario.cargarTablaInicio();
                        limpiar();
                    }
                    else
                    {
                        //Actualizamos la tabla
                        usuario.cargarTablaInicio();
                        this.Close();
                    }
                }
            }
        }
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            char letra = e.KeyChar;

            if (Char.IsDigit(letra))
            {
                lblNombre.Text = "No se permiten numeros";
            }
            else
            {
                lblNombre.Text = "";
            }
        }
        public Boolean vacio()
        {
            Boolean vacio = false;
            if (txtNombre.Text.Equals(""))
            {
                mensajeError += " nombre,";
                vacio = true;
            }
            if (txtPassword.Text.Equals(""))
            {
                mensajeError += "contraseña,";
                vacio = true;
            }
            if (txtRepetirPassword.Equals(""))
            {
                mensajeError += "confirmación contraseña,";
                vacio = true;
            }
            if (cbRoles.SelectedIndex == -1)
            {
                mensajeError += " rol";
                vacio = true;
            }
            return vacio;
        }
        //comprueba si en la base de datos ya exite un usuario con el mismo nombre
        public Boolean usuarioRepetido(String user)
        {
            Boolean repetido = false;
            String select = "Select idUsuario from USUARIOS where upper(nombre) = '"+user+"'";
            DataSet data = conexion.getData(select, "USUARIOS");
            DataTable tUsuarios = data.Tables["USUARIOS"];
            int idUsu;
            foreach (DataRow row in tUsuarios.Rows)
            {
                //Si no devuelve -1 existe ese usuario
                idUsu = Convert.ToInt32(row["IDUSUARIO"]);
                if (idUsu != -1)
                {
                    repetido = true;
                }

            } // Fin del bucle for each
            return repetido;
        }
        public Boolean coincidenciaPassword()
        {
            pass = txtPassword.Text;
            if (!pass.Equals(txtRepetirPassword.Text))
            {
                lblError.Text = "No coincide";
                return false;
            }
            else
            {
                lblError.Text = "";
                return true;
            }
        }
        public String encriptarPassword(String pass)
        {
            String password = "";
            string codificacion = BCrypt.Net.BCrypt.GenerateSalt();
            //encriptamos
            password = BCrypt.Net.BCrypt.HashPassword(pass, codificacion);
      
            return password;

        }

     
        //metodo rellenar comboBox
        public void rellenarCombo()
        {
            //Select tabla roles -> Nombre roles y cada elemento al comboBox, el rol ROOT no aparecera
            String select = "Select NOMBREROL from ROLES where idrol != 1";

            //realizar el select 
            DataSet data = conexion.getData(select, "ROLES");

            DataTable tRoles = data.Tables["ROLES"];

            String nombre;
            foreach (DataRow row in tRoles.Rows)
            {
                nombre = Convert.ToString(row["NOMBREROL"]);

                //añadir a comboBox
                cbRoles.Items.Add(nombre);

            } // Fin del bucle for each
        }
        public int extraerIDRol()
        {
            int idRol = Convert.ToInt32(conexion.DLookUp("IDROL", "ROLES", "NOMBREROL = '" + cbRoles.SelectedItem + "'"));
            return idRol;
        }

        public int ultimoIDUser()
        {
            //Extraemos el id del rol seleccionado en el comboBox
            String extraerID = "Select IDUSUARIO from USUARIOS";
            DataSet data = conexion.getData(extraerID, "USUARIOS");

            DataTable tUsuarios = data.Tables["USUARIOS"];

            int idUser = 0;
            foreach (DataRow row in tUsuarios.Rows)
            {
                idUser = Convert.ToInt16(row["IDUSUARIO"]);

            } // Fin del bucle for each

            return idUser;
        }
    }
}
