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
    public partial class AccesoForm : Form
    {
        int idRol;
        ConnectDB conexion;
        private static AccesoForm instance;

        public static AccesoForm Instance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new AccesoForm();
            }
            return instance;
        }
        private AccesoForm()
        {
            InitializeComponent();
            conexion = new ConnectDB();
            //txtUsuario.LostFocus += new EventHandler(txtUsuarios_lostFocus);
            //txtPassword.LostFocus += new EventHandler(txtPassword_lostFocus);
            btnAceptar.NotifyDefault(true);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarUsuario(txtUsuario.Text))
            {
                if (validarPass())
                {
                    //establecemos el rol con el que se ha conectado el usuario
                    conseguirIdRol();
                    String nombre = txtUsuario.Text;
                    int idUsuario = Convert.ToInt32
                                    (conexion.DLookUp("IDUSUARIO", "USUARIOS", " NOMBRE = '" + nombre + "'"));
                    PrincipalForm principal = new PrincipalForm(idUsuario, idRol, conexion, nombre);
                    principal.Show();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Compruebe su usuario y contraseña");
                }
            }
            else
            {
                MessageBox.Show("Compruebe su usuario y contraseña");
            }
        }

        private void conseguirIdRol()
        {
            String sentencia = "SELECT IDROL FROM USUARIOS WHERE UPPER(NOMBRE)='" + txtUsuario.Text.ToUpper() + "' AND ELIMINADO=0";
            DataSet data = conexion.getData(sentencia, "tUsuarios");
            DataTable tUsuarios = data.Tables["tUsuarios"];
            foreach (DataRow row in tUsuarios.Rows)
            {
                idRol = Convert.ToInt32(row["IDROL"]);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public Boolean validarPass()
        {
            //Extraemos los campos usuario y contraseña
            String user = txtUsuario.Text;
            String pass = txtPassword.Text;

            //Encriptamos la contraseña
            String passEncriptada = BCrypt.Net.BCrypt.HashPassword(pass, BCrypt.Net.BCrypt.GenerateSalt());


            String select = "Select PASSWORD from USUARIOS where NOMBRE = '" + user + "'" + " and ELIMINADO = " + 0;
            DataSet data = conexion.getData(select, "tUsuarios");

            DataTable tUsuarios = data.Tables["tUsuarios"];

            String p = "";
            foreach (DataRow row in tUsuarios.Rows)
            {
                p = Convert.ToString(row["PASSWORD"]);
                //Console.WriteLine(p);
            }

            if (p != "")
            {
                if (BCrypt.Net.BCrypt.Verify(pass, p))
                {
                    return true;
                }
            }

            return false;


        }

        public Boolean validarUsuario(String nombre)
        {
            String select = "SELECT NOMBRE FROM USUARIOS WHERE NOMBRE = '" + nombre + "'" + " and ELIMINADO = " + 0;
            
            DataSet data = conexion.getData(select, "tUsuarios");
            
            DataTable tUsuarios = data.Tables["tUsuarios"];

            String n = "";
            foreach (DataRow row in tUsuarios.Rows)
            {
                n = Convert.ToString(row["NOMBRE"]);
                //Console.WriteLine(p);
            }

            if (n != "")
            {
                if (nombre.Equals(n))
                {
                    //Console.WriteLine("Aqui");
                    return true;
                }
            }

            return false;
        }
    }
}
