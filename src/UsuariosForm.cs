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
using Microsoft.VisualBasic;
     

namespace MySleepy
{
    public partial class UsuariosForm : Form
    {
        private static int idUsuario;
        private static int rolUsuario = -1;
        private static ConnectDB conexion;
        int tipoCambio;
        InsertHistorial insert;

        //patron singleton
        public static UsuariosForm instance;
        private UsuariosForm(int idRol, ConnectDB c,int id)
        {
            InitializeComponent();
            conexion = c;
            rolUsuario = idRol;
            dgvUsuarios.ClearSelection();
            cargarTablaInicio();
            rellenarCombo();
            btnRestaurar.Enabled = false;
            btnBorrar.Enabled = true;
            idUsuario = id;
            insert = new InsertHistorial(conexion);
        }

        public static UsuariosForm Instance(int idRol, ConnectDB c, int idUsu)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new UsuariosForm(idRol,c,idUsu);
            }
            return instance;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void limpiarTabla()
        {
            // Limpiamos el datagridView
            while (dgvUsuarios.RowCount > 0)
            {
                dgvUsuarios.Rows.Remove(dgvUsuarios.CurrentRow);
            }
        }
        public void cargarTablaInicio()
        {
            //solo mostraremos los no eliminados inicialmente
            String select =  "SELECT * from USUARIOS where ELIMINADO = "+0 +" and IDUSUARIO > 1 order by IDUSUARIO";
            cargarTabla(select);
        }
        public void cargarTabla(String sentencia)
        {
            //MessageBox.Show(sentencia);
            limpiarTabla();
            int idUsu;
            String nombre, passEncriptada;

            
            DataSet data;
            data = conexion.getData(sentencia, "USUARIOS");

            DataTable tUsuarios = data.Tables["USUARIOS"];


            foreach (DataRow row in tUsuarios.Rows)
            {
                idUsu = Convert.ToInt32(row["IDUSUARIO"]);
                //MessageBox.Show("id tabla->"+idUsu);
                nombre = Convert.ToString(row["NOMBRE"]);
                passEncriptada = Convert.ToString(row["PASSWORD"]);

                dgvUsuarios.Rows.Add(idUsu, nombre, passEncriptada);
                
            } // Fin del bucle for each
            dgvUsuarios.ClearSelection();
            dgvUsuarios.Update();
        }
        
        
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            tipoCambio = 1; //tipo de cambio -> añadir
            AddUsuario add = new AddUsuario(this,conexion,0,idUsuario);
            //comprobar que no este abierto los formularios addClientes etc
            add.ShowDialog();
            
            if (add.IsDisposed)
            {
                cargarTablaInicio();
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            tipoCambio = 2;

            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
               
                    AddUsuario modificar = new AddUsuario(this, conexion, 1, idUsuario);
                    modificar.ShowDialog();   
            }
        }

        public int extraerIDTabla()
        {
            int id = 0;
          
                DataGridViewRow fila = dgvUsuarios.CurrentRow;
                id = Convert.ToInt32(fila.Cells[0].Value); //columna referente al idUsuario, invisible para el usuario
                
                //MessageBox.Show(""+idUsuario);
            
            return id;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            tipoCambio = 3;
            
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                //pedimos confirmacion
                DialogResult opcion = MessageBox.Show("¿Desea borrar el usuario?", "Confirmación",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (opcion == DialogResult.OK)
                {
                   
                    int idUsu = extraerIDTabla();
                    //MessageBox.Show("id usuario " + idUsu);
                    
                            String update = " update USUARIOS  set ELIMINADO = " + 1 + " where IDUSUARIO=" + idUsu;
                            conexion.setData(update);

                            //Actualizar los usuarios visualizados en el data grid view
                            limpiar();    
                            cargarTablaInicio(); 
                            

                            //Al borrar un elemento pedimos el motivo del borrado
                            
                            //insert historial cambios
                            String nombreUsuario = Convert.ToString
                                    (conexion.DLookUp("NOMBRE", "USUARIOS", " IDUSUARIO = " + idUsu));
                            String mensaje = Interaction.InputBox("¿Motivo por el cual se elimina?", "Motivo", "");
                            mensaje = "Usuario borrado ->" + nombreUsuario+" Motivo ->"+mensaje;
                            //MessageBox.Show("id usuario que modifica -> " + idUsuario);
                            insert.insertHistorialCambio(idUsuario, tipoCambio,mensaje );

                            //MessageBox.Show("Usuario eliminado");
                        
                    
                }
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            tipoCambio = 4;
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
                //pedimos confirmacion
                DialogResult opcion = MessageBox.Show("¿Desea restaurar el usuario?", "Confirmación",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (opcion == DialogResult.OK)
                {
                    if (dgvUsuarios.CurrentRow == null)
                    {
                        MessageBox.Show("Debe seleccionar una fila");
                    }
                    else
                    {
                        int idUsu = extraerIDTabla();
                        //MessageBox.Show("id usuario " + idUsu);
                        
                            String update = " update USUARIOS  set ELIMINADO = " + 0 + " where IDUSUARIO=" + idUsu;
                            conexion.setData(update);
                            //MessageBox.Show("Usuario restaurado");

                            //Actualizar los usuarios visualizados en el data grid view
                            cargarTablaInicio();
                            limpiar();
                            //insert en la tabla historial de cambios
                            String nombreUsuario = Convert.ToString
                                        (conexion.DLookUp("NOMBRE", "USUARIOS", " IDUSUARIO = " + idUsu));
                            //MessageBox.Show("id usuario que modifica -> " + idUsuario);
                            insert.insertHistorialCambio(idUsuario, tipoCambio, "Usuario restaurado ->" + nombreUsuario);
                        
                   }
                }
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            filtrar();
        }
        public void filtrar()
        {
            String select = "Select * from USUARIOS where idUsuario > 1";
            if (rbEliminados.Checked == true)
            {
                select += " and ELIMINADO = "+ 1;
            }
            else
            {
                select += " and ELIMINADO = " + 0;
            }

            if (txtNombre.Text != "")
            {
                select += " and upper(NOMBRE) like '%" + txtNombre.Text.ToUpper() + "%'";
            }
            if (cbRoles.SelectedIndex != -1)
            {
                select += " and IDROL like '%" + rolUsuario+"%'";
            }
            select += " order by IDUSUARIO";

            cargarTabla(select);
        }
        public void rellenarCombo()
        {
            //Select tabla roles -> Nombre roles y cada elemento al comboBox, el usuario ROOT no aparecera
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

        private void cbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRoles.SelectedIndex != -1)
            {
                //extraer idRol
                String rol = cbRoles.SelectedItem.ToString();
                String select = "Select idROL from ROLES where NOMBREROL = '" + rol + "'";
                rolUsuario = extraerID(select, "IDROL", "ROLES");
                filtrar();
            }
            
        }
        public int extraerID(String select, String idTabla,String tabla)
        {
            DataSet data = conexion.getData(select, tabla);

            DataTable tRoles = data.Tables[tabla];

            int id = 0;
            foreach (DataRow row in tRoles.Rows)
            {
                id = Convert.ToInt32(row[idTabla]);

            } // Fin del bucle for each
            return id;
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar() ;
        }
        public void limpiar()
        {
            txtNombre.Text = "";
            rolUsuario = -1;
            cargarTablaInicio();
            cbRoles.SelectedIndex = -1;
            rbNoEliminados.Checked = true;
            rbEliminados.Checked = false;
            btnBorrar.Enabled = true;
            btnRestaurar.Enabled = false;
        }
        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            dgvUsuarios.ClearSelection();
            dgvUsuarios.Update();
            ToolTip tTUsuarios = new ToolTip();
            tTUsuarios.SetToolTip(btnAñadir, "Añadir Usuario");
            tTUsuarios.SetToolTip(btnModificar, "Modificar Usuario");
            tTUsuarios.SetToolTip(btnRestaurar, "Restaurar Usuario");
            tTUsuarios.SetToolTip(btnLimpiar, "Limpiar Campos");
            tTUsuarios.SetToolTip(btnBorrar, "Borrar Usuario");
            tTUsuarios.SetToolTip(btnSalir, "Salir");
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        
        private void rbNoEliminados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoEliminados.Checked == true)
            {
                rbEliminados.Checked = false;
                btnBorrar.Enabled = true;
                btnRestaurar.Enabled = false;
                filtrar();
            }
           
        }

        private void rbEliminados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEliminados.Checked == true)
            {
                rbNoEliminados.Checked = false;
                btnBorrar.Enabled = false;
                btnRestaurar.Enabled = true;
                filtrar();
            }
        }
    }
}
