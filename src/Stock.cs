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
    public partial class Stock : Form
    {

        private static Stock instance;
        ConnectDB conexion;
        private int idRol;
        private int idUsuario;

        public static Stock Instance(int idRol, ConnectDB c, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Stock(idRol, c, idUsuario);
            }
            return instance;
        }

        public Stock()
        {
            InitializeComponent();
        }

        public Stock(int idR, ConnectDB con, int idU)
        {
            InitializeComponent();
            this.conexion = con;
            this.idRol = idR;
            this.idUsuario = idU;
            CargarTablas.cargarTablaStocks("SELECT * FROM ARTICULOSSTOCK", dgvStock);
            ToolTip tool = new ToolTip();
            tool.SetToolTip(botonSalir, "Salir del menú");
            tool.SetToolTip(btnAnadir, "Guardar los datos en el historial de stock");
            tool.SetToolTip(btnModificar, "Modificar el stock de un articulo seleccionado de la tabla");
            tool.SetToolTip(btnLimpiarFiltros, "Limpiar los datos del formulario");
            tool.SetToolTip(botonImprimir, "Imprimir el contenido de la tabla en un informe");
            tool.SetToolTip(botonRecuperar, "Recuperar el stock inicial y mostrarlo en un informe");
        }
        private void filtrar()
        {
            dgvStock.Rows.Clear();
            String sentencia = "SELECT * FROM ARTICULOSSTOCK";
            if (!caja_nombre.Text.Equals(""))
            {
   
                sentencia = sentencia + " WHERE IDARTICULO IN (SELECT IDARTICULO FROM ARTICULOS WHERE UPPER(NOMBRE) LIKE '%" + caja_nombre.Text.ToUpper() + "%')";
            }
            if (!caja_stockReal.Text.Equals(""))
            {
                if (!caja_nombre.Text.Equals(""))
                {
                    sentencia = sentencia + " AND STOCKREAL LIKE '%" + caja_stockReal.Text + "%'";
                }
                else
                {
                    sentencia = sentencia + " WHERE STOCKREAL LIKE '%" + caja_stockReal.Text + "%'";
                }
            }
            if (!caja_StockIdeal.Text.Equals(""))
            {
                if (!caja_nombre.Text.Equals("") || !caja_stockReal.Text.Equals(""))
                {
                    sentencia = sentencia + " AND STOCKIDEAL LIKE '%" + caja_StockIdeal.Text + "%'";
                }
                else { sentencia = sentencia + " WHERE STOCKIDEAL LIKE '%" + caja_StockIdeal.Text + "%'"; }
                
            }
            CargarTablas.cargarTablaStocks(sentencia, dgvStock);
        }
        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvStock.CurrentRow == null || dgvStock.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna fila seleccionada");
            }
            else
            {
            
               int fila = dgvStock.CurrentRow.Index;
                String nombre = caja_usuario.Text.Trim();
                String pass = caja_contraseña.Text.Trim();
                int rol = Convert.ToInt32(conexion.DLookUp("idrol", "USUARIOS", "nombre like '" + nombre + "'"));
                if (rol == 5)//comprueba si el rol es correcto
                {

                    int idUser = Convert.ToInt32(conexion.DLookUp("idusuario", "USUARIOS", "nombre like '" + nombre + "'"));
                    Boolean seguir = MetodosAuxiliares.validarPass(pass, conexion, idUser);
                    if (seguir == true)//compruebo si la contraseña es correcta
                    {
                       //Abro el dialog y actualizo la tabla al cerrarlo
                        int idArt = Convert.ToInt32(dgvStock.Rows[fila].Cells[0].Value.ToString());
                        ModificarStock modificar = new ModificarStock(idArt, conexion, idUsuario);
                        modificar.ShowDialog();
                        CargarTablas.cargarTablaStocks("SELECT * FROM ARTICULOSSTOCK", dgvStock);
                        
                    }
                    else//si la contraseña es incorrecta
                    {
                        MessageBox.Show(this,"Usuario y/o contraseña incorrectos","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        //caja_usuario.Text = "";
                        caja_contraseña.Text = "";
                    }
                }
                else//si el rol es incorrecto
                {
                    MessageBox.Show(this,"Sólo Jefes de almacen","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    caja_usuario.Text = "";
                    caja_contraseña.Text = "";
                }              
            }
            
        }

        
        
        
        private void Stock_Load(object sender, EventArgs e)
        {

        }

        private void caja_nombre_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void caja_stockReal_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void caja_StockIdeal_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void btnLimpiarFiltros_Click(object sender, MouseEventArgs e)
        {
            caja_nombre.Text = "";
            caja_StockIdeal.Text = "";
            caja_stockReal.Text = "";
            caja_usuario.Text = "";
            caja_contraseña.Text = "";
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            String nombre = caja_usuario.Text.Trim();
            String pass = caja_contraseña.Text.Trim();
            int rol = Convert.ToInt32(conexion.DLookUp("idrol", "USUARIOS", "nombre like '" + nombre + "'"));
            if (rol == 5 || rol == 2 ||rol == 1)//comprueba si el rol es correcto, con el ROOT se hace la vista gorda
            {
                int idUser = Convert.ToInt32(conexion.DLookUp("idusuario", "USUARIOS", "nombre like '" + nombre + "'"));
                    Boolean seguir = MetodosAuxiliares.validarPass(pass, conexion, idUser);
                    if (seguir == true)//compruebo si la contraseña es correcta
                    {
                        try//el try es una medida de seguridad debido a la conversion de multiples variables de importancia
                        {
                            String fecha = MetodosAuxiliares.devolverFechaActual();
                            String hora = MetodosAuxiliares.devolverHora();
                            int horaGuardado = Convert.ToInt32(hora);
                            int fechaguardado = Convert.ToInt32(fecha);
                            int idHistorialStock;
                            int idArticulo, stockReal, stockIdial;//valores que se cogen del dgv
                            String insert;
                            for (int i = 0; i < dgvStock.Rows.Count; i++)
                            {
                                idHistorialStock = MetodosAuxiliares.ultimoID(conexion, "IDHISTORIAL", "HISTORIALSTOCK");
                                idArticulo = Convert.ToInt32(dgvStock.Rows[i].Cells[0].Value);
                                stockReal = Convert.ToInt32(dgvStock.Rows[i].Cells[6].Value);
                                stockIdial = Convert.ToInt32(dgvStock.Rows[i].Cells[5].Value);
                                insert = "INSERT INTO HISTORIALSTOCK VALUES(" + idHistorialStock + "," + fechaguardado + "," + idArticulo + "," + stockReal + "," + stockIdial + "," + horaGuardado + ")";
                                conexion.setData(insert);
                            }
                            MessageBox.Show(this, "Stock almacenado", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, "Se produjo un error al guardar el stock.\nContacte con el servicio de mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine("Error al guardar el seguimiento del stock:\n" + ex.Message);
                        }
                    }
                    else//si la contraseña es incorrecta
                    {
                        MessageBox.Show(this, "Usuario y/o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        caja_contraseña.Text = "";
                    }
            }
            else//si el rol es incorrecto
            {
                MessageBox.Show(this, "Solo el personal administrativo y el Jefe de Almacen están autorizados \na realizar el seguimiento del stock", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {

        }

        private void botonImprimir_Click(object sender, EventArgs e)
        {
            String fecha = MetodosAuxiliares.devolverFechaActual();
            String hora = MetodosAuxiliares.devolverHora();
            int horaGuardado = Convert.ToInt32(hora);
            int fechaguardado = Convert.ToInt32(fecha);
            int idHistorialStock;
            int idArticulo, stockReal, stockIdial;//valores que se cogen del dgv
            String insert;
            
            for (int i = 0; i < dgvStock.Rows.Count; i++)
            {
                idHistorialStock = MetodosAuxiliares.ultimoID(conexion, "IDHISTORIAL", "HISTORIALSTOCK");
                idArticulo = Convert.ToInt32(dgvStock.Rows[i].Cells[0].Value);
                stockReal = Convert.ToInt32(dgvStock.Rows[i].Cells[6].Value);
                stockIdial = Convert.ToInt32(dgvStock.Rows[i].Cells[5].Value);
                insert = "INSERT INTO HISTORIALSTOCK VALUES(" + idHistorialStock + "," + fechaguardado + "," + idArticulo + "," + stockReal + "," + stockIdial + "," + horaGuardado + ")";
                conexion.setData(insert);
            }
            
            ImprimirStock imprimir = new ImprimirStock(conexion,fechaguardado,horaGuardado);
            imprimir.ShowDialog();
        }

        private void botonRecuperar_Click(object sender, EventArgs e)
        {
            RestaurarStock restaurar = new RestaurarStock(conexion);
            restaurar.ShowDialog();
        }

    }
}
