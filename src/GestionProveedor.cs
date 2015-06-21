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
    public partial class GestionProveedor : Form
    {
        private static ConnectDB conexion;
        private int idUsuario;
        private int idRol;
        private int idComunidad;
        private int idProvincia;
        private int idPoblacion;
        
        private Proveedores proveedor;
        private HistorialForm insert;
        //patron singleton
        private static GestionProveedor instance;
        public GestionProveedor()
        {
            InitializeComponent();
        }

        public GestionProveedor(int idRol,ConnectDB con, int idUsuario)
        {
            InitializeComponent();
            this.idRol = idRol;
            conexion = con;
            this.idUsuario = idUsuario;
            
            insert = new HistorialForm(conexion);
            caja_email.LostFocus += new EventHandler(caja_email_lostFocus);

        }



        internal static GestionProveedor Instance(int idRol,ConnectDB conexion, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new GestionProveedor(idRol,conexion, idUsuario);
            }
            return instance;
        }

        private void GestionProveedor_Load(object sender, EventArgs e)
        {
            
        }


        private void cargarCombosPoblacion(int idcodPobl)
        {
            String sentencia = "SELECT P.POBLACION,R.PROVINCIA,M.COMUNIDAD,L.CODIGOPOSTAL" +
                 " FROM POBLACIONES P, PROVINCIAS R, COMUNIDADES M,CODIGOSPOSTALES L,CODIGOSPOSTALESPOBLACIONES X" +
                 " WHERE X.IDCODIGOPOSTALPOB = " +idcodPobl + "AND X.REFPROVINCIA = R.IDPROVINCIA AND R.REFCOMUNIDAD = M.IDCOMUNIDAD" +
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
                comboComunidad.Items.Add(Convert.ToString(row["COMUNIDAD"]));
            }
            comboComunidad.SelectedItem = comunidad;
            comboProvincia.SelectedItem = provincia;
            comboPoblacion.SelectedItem = poblacion;
            comboCP.SelectedItem = cp;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botonImprimir_Click(object sender, EventArgs e)
        {
            if (caja_id.Text.Equals(""))
            {
                MessageBox.Show("No tiene cargado ningún proveedor");
            }
            else
            {
                ImprimirProveedor imprimir = new ImprimirProveedor(conexion, Convert.ToInt32(caja_id.Text));
                imprimir.ShowDialog();
            }
            
        }


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

        //Cargar Combo Comunidad
        private void cargarComboComunidad()
        {
            DataSet data = new DataSet();
            DataTable tabla = new DataTable();
            //Cargo el combo box de comunidades autonomas
            data = conexion.getData("SELECT COMUNIDAD FROM COMUNIDADES ORDER BY ORDEN", "COMUNIDADES");
            tabla = data.Tables["COMUNIDADES"];
            
            foreach (DataRow row in tabla.Rows)
            {
                comboComunidad.Items.Add(Convert.ToString(row["COMUNIDAD"]));
            }
            comboComunidad.SelectedIndex = 0;
        }

        private void comboComunidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboProvincia.Items.Clear();
            if (comboComunidad.SelectedIndex > 0)
            {
                String cautonoma = comboComunidad.SelectedItem.ToString();
                this.idComunidad = Convert.ToInt32(conexion.DLookUp("IDCOMUNIDAD", "COMUNIDADES", "COMUNIDAD = '" + cautonoma + "'"));
                rellenarComboBox(comboProvincia, "PROVINCIAS", "PROVINCIA", "REFCOMUNIDAD =" + idComunidad + " ORDER BY ORDEN");
                comboProvincia.SelectedIndex = 0;
            }
        }

        private void comboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboPoblacion.Items.Clear();
            if (comboProvincia.SelectedIndex >= 0)
            {
                String provincia = comboProvincia.SelectedItem.ToString();
                this.idProvincia = Convert.ToInt32(conexion.DLookUp("IDPROVINCIA", "PROVINCIAS", "PROVINCIA ='" + provincia + "'"));
                rellenarComboBox(comboPoblacion, "POBLACIONES", "POBLACION", "IDPOBLACION IN (SELECT REFPOBLACION FROM CODIGOSPOSTALESPOBLACIONES WHERE " +
                "REFPROVINCIA =" + idProvincia + ") ORDER BY POBLACION");
                comboPoblacion.SelectedIndex = 0;
            }
        }

        private void comboPoblacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboCP.Items.Clear();
            if (comboPoblacion.SelectedIndex >= 0)
            {
                String poblacion = comboPoblacion.SelectedItem.ToString();
                this.idPoblacion = Convert.ToInt32(conexion.DLookUp("IDPOBLACION", "POBLACIONES", "POBLACION ='" + poblacion + "'"));
                rellenarComboBox(comboCP, "CODIGOSPOSTALES", "CODIGOPOSTAL", "IDCODIGOPOSTAL IN (SELECT REFCODIGOPOSTAL FROM CODIGOSPOSTALESPOBLACIONES WHERE " +
                "REFPOBLACION = " + idPoblacion + ") ORDER BY CODIGOPOSTAL");
                comboCP.SelectedIndex = 0;
            }

            /***SUPERFIJARZE BIENNN esto se pone aquí para cargar la tabla para que no peeteee la tabla
             * m con la a maaa cosas de la informática***/
            int id = Convert.ToInt32(caja_id.Text);
            cargarTablaArticulosRellenar(dgvAddProveedor, id);
            
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            //SELECCIONAR PROVEEDOR
            DataSet ds = new DataSet();
            ds = conexion.getData("SELECT * FROM PROVEEDORES", "PROVEEDORES");
            //abrimos ProveedoresForm
            Proveedores prov = Proveedores.Instance(ds, conexion, idUsuario,this,2);
            prov.Show();
           return;
        }

        public static void cargarTablaArticulosRellenar(DataGridView tabla,int idpro)
        {

            tabla.Rows.Clear();
            DataSet resultado = conexion.getData("SELECT * FROM ARTICULOS WHERE ELIMINADO=0", "ARTICULOS");
            DataTable tArticulos = resultado.Tables["ARTICULOS"];
            foreach (DataRow row in tArticulos.Rows)
            {
                int id = Convert.ToInt32(row["IDARTICULO"]);
                String nombre = Convert.ToString(row["NOMBRE"]);
                String composicion = Convert.ToString(conexion.DLookUp("COMPOSICION", "COMPOSICIONES", "IDCOMPOSICION=" + row["REFCOMPOSICION"]));
                String medida = Convert.ToString(conexion.DLookUp("MEDIDA", "MEDIDAS", "IDMEDIDA=" + row["REFMEDIDA"]));
                int idProvArt = Convert.ToInt32(conexion.DLookUp("IDPROVEEDORARTICULO", "PROVEEDORARTICULOS", "IDARTICULO=" + id + " AND IDPROVEEDOR=" +idpro + " AND ELIMINADO = 0"));
                if (idProvArt > -1)
                {
                    double precio = Math.Round(Convert.ToSingle(conexion.DLookUp("PRECIOPROVEEDOR", "PROVEEDORARTICULOS", "IDPROVEEDORARTICULO=" + idProvArt)), 2);
                    String nombreArtP = Convert.ToString(conexion.DLookUp("NOMBREPROVEEDOR", "PROVEEDORARTICULOS", "IDPROVEEDORARTICULO=" + idProvArt));
                    tabla.Rows.Add(id, nombre, composicion, medida, precio, nombreArtP);
                }
            } // Fin del bucle for each
            tabla.ClearSelection();
        }

        internal void cargarProveedor(string id, string cifdni, string nombre, string direccion, string telefono,String movil, int idcodPoblacion, string email,String pais,String contacto,String observaciones)
        {
            caja_id.Text = id;
            caja_dninif.Text = cifdni;
            caja_nombre.Text = nombre;
            caja_direccion.Text = direccion;
            caja_telefono.Text = telefono;
            caja_email.Text = email;
            caja_movil.Text = movil;
            caja_pais.Text = pais;
            caja_contacto.Text = contacto;
            caja_observaciones.Text = observaciones;
            cargarCombosPoblacion(idcodPoblacion);
        }

        private void caja_nombre_KeyPress(object sender, KeyPressEventArgs e)
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

        private void caja_contacto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void caja_observaciones_KeyPress(object sender, KeyPressEventArgs e)
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

        private void caja_pais_KeyPress(object sender, KeyPressEventArgs e)
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

        private void caja_direccion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void caja_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((Char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || caja_telefono.Text.Length >= 9) && (codigo != 8))
            {
                e.Handled = true;
                labelTelefono.Text = "SOLO SE PERMITEN NUMEROS";
            }
            else
            {
                e.Handled = false;
                labelTelefono.Text = "";
            }
        }

        private void caja_movil_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((Char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || caja_telefono.Text.Length >= 9) && (codigo != 8))
            {
                e.Handled = true;
                labelTelefono.Text = "SOLO SE PERMITEN NUMEROS";
            }
            else
            {
                e.Handled = false;
                labelTelefono.Text = "";
            }
        }

        private void caja_fax_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((Char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || caja_telefono.Text.Length >= 9) && (codigo != 8))
            {
                e.Handled = true;
                labelTelefono.Text = "SOLO SE PERMITEN NUMEROS";
            }
            else
            {
                e.Handled = false;
                labelTelefono.Text = "";
            }
        }

        private void caja_dninif_KeyPress(object sender, KeyPressEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyChar);
            if ((char.IsSymbol(e.KeyChar) || e.KeyChar.Equals('\'') || caja_dninif.Text.Length >= 9) && (codigo != 8))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void caja_email_lostFocus(object sender, EventArgs e)
        {
            String email = caja_email.Text;
            Boolean correcto = MetodosAuxiliares.emailCorrecto(email);
            if (correcto == false)
            {
                MessageBox.Show("Email incorrecto");
            }
        }
    }
}
