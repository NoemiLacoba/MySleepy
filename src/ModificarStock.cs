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
    public partial class ModificarStock : Form
    {
        public int idArticulo;
        public ConnectDB conexion;
        public int idUsuario;
        private InsertHistorial insert;

        public ModificarStock()
        {
            InitializeComponent();
        }

        //Lo referencia la tabla stock para modificar el stock de un articulo
        public ModificarStock(int idArt, ConnectDB con, int idU)
        {
            InitializeComponent();
            this.idArticulo = idArt;
            this.conexion = con;
            this.idUsuario = idU;
            insert = new InsertHistorial(conexion);
            String nombre = Convert.ToString(con.DLookUp("nombre", "ARTICULOS", "idarticulo=" + idArticulo));
            int stockReal = Convert.ToInt32(con.DLookUp("stockreal", "ARTICULOSSTOCK", "idarticulo=" + idArticulo));
            int stockIdeal = Convert.ToInt32(con.DLookUp("stockideal", "ARTICULOSSTOCK", "idarticulo=" + idArticulo));
            caja_StockIdeal.Value = stockIdeal;
            caja_StockReal.Value = stockReal;
            caja_nombre.Text = nombre;
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea modificar el stock?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int stockreal = Convert.ToInt32(caja_StockReal.Value.ToString());
                int stockideal = Convert.ToInt32(caja_StockIdeal.Value.ToString());
                String causa = caja_causa.Text;
                if (stockreal > 0)
                {
                    if (stockideal > 0)
                    {
                        if (causa.Equals(""))
                        {
                            MessageBox.Show("Introduzca el motivo por el que modifica el stock");
                        }
                        else
                        {
                            String update = "update articulosstock set stockreal=" + stockreal + ", stockideal=" + stockideal + " where idarticulo=" + idArticulo;
                            conexion.setData(update);

                            //Ahora la causa se guarda en el historial
                            int referencia = Convert.ToInt32(conexion.DLookUp("referencia", "ARTICULOS", "idarticulo=" + idArticulo));
                            insert.insertHistorialCambio(idUsuario, 2, "Stock Modificado del articulo=" + referencia);
                            MessageBox.Show("Stock modificado");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Introduzca un número mayor que 0 en stock ideal");
                    }
                }
                else
                {
                    MessageBox.Show("Introduzca un número mayor que 0 en stock real");
                }
            }
            
        }

        private void ModificarStock_Load(object sender, EventArgs e)
        {

        }

        
    }
}
