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
    public partial class Facturas : Form
    {
        //patron singleton
        private static Facturas instance;
        private int idRol;
        private ConnectDB conexion;
        private int idUsuario;
        private InsertHistorial insert;

        public Facturas()
        {
            InitializeComponent();
        }

        public Facturas(int idRol, ConnectDB conexion, int idUsuario)
        {
            InitializeComponent();
            this.idRol = idRol;
            this.conexion = conexion;
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
            ToolTip tool = new ToolTip();
            tool.SetToolTip(botonImprimir, "Imprimir Factura");
            tool.SetToolTip(btnAñadir, "Añadir Factura");
            tool.SetToolTip(btnModificar, "Modificar Factura");
            tool.SetToolTip(btnSalir, "Salir al menu");
            tool.SetToolTip(btnEliminar, "Eliminar Factura");
            tool.SetToolTip(btnLimpiar, "Limpiar campos de usuario");

            //cargamos el datagrid view -> en clase CargarTablas
            CargarTablas.cargarTablaFacturas("Select * from facturas where eliminado = 0", dgvFactura);
        }

        internal static Facturas Instance(int idRol,ConnectDB conexion, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Facturas(idRol, conexion, idUsuario);
            }
            return instance;
        }

        private void dgvFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botonImprimir_Click(object sender, EventArgs e)
        {
            if(dgvFactura.RowCount>=0){
                int fila=dgvFactura.CurrentRow.Index;
                int idfactura=Convert.ToInt32(dgvFactura.Rows[fila].Cells[0].Value.ToString());
                int idcliente = Convert.ToInt32(dgvFactura.Rows[fila].Cells[1].Value.ToString());
                int idpedido = Convert.ToInt32(dgvFactura.Rows[fila].Cells[2].Value.ToString());
                
                ImprimirFactura imprimir = new ImprimirFactura(conexion,idfactura,idcliente,idpedido);
                imprimir.ShowDialog();
            }
        }

        //Para añadir facturas debes ser administrador o root, e introducir correctamente los datos en las cajas
        private void btnAñadir_Click(object sender, EventArgs e)
        {   
            if (idRol < 3)
            {
                int idUltimaFacturaArticulo = Convert.ToInt32(MetodosAuxiliares.ultimoID(conexion,"idfacturaarticulos","FACTURASARTICULOS"));
                //abrimos formulario addFactura
                //Para añadir le ponemos la señal a 0
                AddFactura add = new AddFactura(conexion,idUltimaFacturaArticulo,0,0,idUsuario);
                add.ShowDialog(this);
                CargarTablas.cargarTablaFacturas("select * from facturas where eliminado=0", dgvFactura);
                caja_usuario.Text = "";
                caja_contraseña.Text = "";
       
             }else{ 
                MessageBox.Show("No tiene permisos para añadir facturas");
                caja_usuario.Text = "";
                caja_contraseña.Text = "";
            } 
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //abrimos formulario addFactura -> le pasariamos el idFactura, idPedido, y otros id etc en comun con ambos dataGrid
            int fila = dgvFactura.CurrentRow.Index;
            if (dgvFactura.CurrentRow == null || dgvFactura.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila");
            }
            else
            {
                int idFactura = Convert.ToInt32(dgvFactura.CurrentRow.Cells[0].Value.ToString());
                int idPedido = Convert.ToInt32(dgvFactura.CurrentRow.Cells[2].Value.ToString());
                String contabilizadaF = Convert.ToString(conexion.DLookUp("contabilizada", "FACTURAS", "idfactura=" + idFactura));
                String contabilizadaFA = Convert.ToString(conexion.DLookUp("contabilizada", "FACTURASARTICULOS", "idfacturaarticulos=" + idFactura));
                if (contabilizadaF.Equals("S") || contabilizadaFA.Equals("S"))
                {
                    MessageBox.Show("No se puede modificar facturas contabilizadas");
                }
                else
                {
                    //Para modificar factura
                    AddFactura add = new AddFactura(conexion, idFactura,idPedido,1,idUsuario);
                    add.ShowDialog(this);
                    CargarTablas.cargarTablaFacturas("select * from facturas where eliminado=0", dgvFactura);
                
                }   
            }  
        }

        /*Evento que tras introducir el nombre y contraseña del administrativo haciendo doble click en la fila
         le permite marcar las facturas ya contabilizadas poniendo en verde el número de factura*/
        private void dgvFactura_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                String nombre=caja_usuario.Text.Trim();
                String pass=caja_contraseña.Text.Trim();
                if (nombre.Equals("") || pass.Equals(""))
                {
                    MessageBox.Show("Por favor rellene los campos Usuario y Contraseña");
                }
                else
                {
                    int rol = Convert.ToInt32(conexion.DLookUp("idrol", "USUARIOS", "nombre like '" + nombre + "'"));
                    if (rol == 4)
                    {
                        int idUser = Convert.ToInt32(conexion.DLookUp("idusuario", "USUARIOS", "nombre like '" + nombre + "'"));
                        Boolean seguir = MetodosAuxiliares.validarPass(pass, conexion, idUser);
                        if (seguir == true)
                        {

                            int numFactura = Convert.ToInt32(dgvFactura.CurrentRow.Cells[3].Value.ToString());
                            String contabilizadaF = Convert.ToString(conexion.DLookUp("contabilizada", "FACTURAS", "numfactura=" + (numFactura - 1)));
                            String contabilizadaFA = Convert.ToString(conexion.DLookUp("contabilizada", "FACTURASARTICULOS", "numfactura=" + (numFactura - 1)));
                            if (contabilizadaF.Equals("N") || contabilizadaFA.Equals("N"))
                            {
                                MessageBox.Show("Contabilice la factura anterior");
                            }
                            else
                            {
                                int fila = dgvFactura.CurrentRow.Index;

                                int idpedido = Convert.ToInt32(dgvFactura.Rows[fila].Cells[2].Value.ToString());
                                if (idpedido != 0)
                                {
                                    String update = "update facturas set contabilizada='S' where numfactura=" + numFactura;
                                    conexion.setData(update);

                                    dgvFactura.Rows[fila].Cells[3].Style.BackColor = Color.LightGreen;
                                }
                                else
                                {
                                    String update = "update facturasarticulos set contabilizada='S' where numfactura=" + numFactura;
                                    conexion.setData(update);

                                    dgvFactura.Rows[fila].Cells[3].Style.BackColor = Color.LightGreen;
                                }

                                MessageBox.Show("Factura contabilizada");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Usuario y/o Contraseña incorrecta");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Sólo personal de contabilidad");
                        caja_usuario.Text = "";
                        caja_contraseña.Text = "";
                    }
                }
        
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvFactura.CurrentRow == null || dgvFactura.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila");
            }
            else
            {
                int idFactura = Convert.ToInt32(dgvFactura.CurrentRow.Cells[0].Value.ToString());
                int idPedido = Convert.ToInt32(dgvFactura.CurrentRow.Cells[2].Value.ToString());
                String contabilizadaF = Convert.ToString(conexion.DLookUp("contabilizada", "FACTURAS", "idfactura=" + idFactura));
                String contabilizadaFA = Convert.ToString(conexion.DLookUp("contabilizada", "FACTURASARTICULOS", "idfacturaarticulos=" + idFactura));
                if (contabilizadaF.Equals("S") || contabilizadaFA.Equals("S"))
                {
                    MessageBox.Show("No se pueden eliminar facturas contabilizadas");
                }
                else
                {

                    if (idRol != 3)
                    {
                        //Borramos la factura
                        //EXISTE LA FACTURA EN la tabla facturas o en facturasarticulos
                        //int idFactura = Convert.ToInt32(dgvFactura.CurrentRow.Cells[0].Value.ToString());
                        //Número de factura de la fila seleccionada
                        int fila = dgvFactura.CurrentRow.Index;
                        String numeroF = Convert.ToString(dgvFactura.Rows[fila].Cells[3].Value.ToString());
                        int existeF = Convert.ToInt32(conexion.DLookUp("idfactura", "FACTURAS", "numFactura=" + numeroF));
                        int existeFA = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "numFactura=" + numeroF));
                        
                        
                        String maximoFA = Convert.ToString(conexion.DLookUp("max(numFactura)", "facturasarticulos", "eliminada=0"));
                        String maximoF = Convert.ToString(conexion.DLookUp("max(numFactura)", "facturas", "eliminado=0"));
                        //MessageBox.Show(numeroF + " FA -> " + maximoFA + " F ->" + maximoF +" exFA "+existeFA+ " exF "+existeF);
                        /*AHORA SI QUE SI , ESTAMOS TONTACASSSS*/
                        if (existeF==-1 && existeFA!=-1 && maximoFA.Equals(numeroF))
                        {
                            String borrarFactura = "update facturasarticulos set eliminada=1 where idfacturaarticulos=" + idFactura;
                            conexion.setData(borrarFactura);
                            //MessageBox.Show(borrarFactura);
                            MessageBox.Show("Factura eliminada");

                            int numFactura = Convert.ToInt32(conexion.DLookUp("numfactura", "facturasarticulos", " idfacturaarticulos = " + idFactura));
                            //actualizamos el historial de cambios
                            insert.insertHistorialCambio(idUsuario, 3, "Factura eliminada : " + numFactura + 1); //ponemos el numFactura original
                            CargarTablas.cargarTablaFacturas("select * from facturas where eliminado=0", dgvFactura);
                        
                        }
                        else 
                        {
                            if (existeF != -1 && existeFA == -1 && maximoF.Equals(numeroF))
                            {
                                String borrarFactura = "update facturas set eliminado=1 where idfactura=" + idFactura;
                                conexion.setData(borrarFactura);
                                //MessageBox.Show(borrarFactura);

                                CargarTablas.cargarTablaFacturas("select * from facturas where eliminado=0", dgvFactura);
                                MessageBox.Show("Factura eliminada");

                                int numFactura = Convert.ToInt32(conexion.DLookUp("numfactura", "facturas", " idfactura = " + idFactura));
                                //actualizamos el historial de cambios
                                insert.insertHistorialCambio(idUsuario, 3, "Factura eliminada : " + numFactura + 1); //ponemos el numFactura original

                            }
                            else
                            {
                                MessageBox.Show("Debe borrar las facturas posteriores");
                            }
                        }

                        
                       
                    }
                    else
                    {
                        MessageBox.Show("No tiene permisos para eliminar");
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            caja_usuario.Text = "";
            caja_contraseña.Text = "";
        } 
    }
}
