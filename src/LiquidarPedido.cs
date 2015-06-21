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
    public partial class LiquidarPedido : Form
    {
        ConnectDB conexion;
        public static int idP;
        private String precioRestante;
        //HistorialForm insertar;
        public LiquidarPedido()
        {
            InitializeComponent();
        }

        public LiquidarPedido(ConnectDB conexion, int idPedido)
        {
            InitializeComponent();
            this.conexion = conexion;
            idP = idPedido;
            //insertar = new HistorialForm(conexion);
            precioRestante = Convert.ToString(conexion.DLookUp("total-importepagado", "pedidos", " idpedido = " + idP));
            caja_Restante.Text = precioRestante;
        }
        private void botonAceptar_Click(object sender, EventArgs e)
        {
            double pagado=Convert.ToSingle(txtImporte.Text);
            double importeTotalSql = Math.Round(Convert.ToSingle(conexion.DLookUp("total", "pedidos", " idpedido = " + idP)), 2);
            double importePagadoSql = Math.Round(Convert.ToSingle(conexion.DLookUp("importepagado", "pedidos", " idpedido = " + idP)), 2);
            String update = "";
            if (rbPorcentual.Checked == true && pagado > 100)
            {
                MessageBox.Show("El porcentaje no puede ser superior a 100");
            }
            else if (rbPorcentual.Checked == true && pagado <= 100)
            {
                //calculamos el porcentaje del importe total que queremos pagar
                pagado = (importeTotalSql - importePagadoSql) * (pagado / 100);
                //MessageBox.Show("importe ->"+imp);
                update = "update pedidos set importepagado='" + (pagado+importePagadoSql) + "' where idpedido=" + idP;
                conexion.setData(update);

            }
            if (pagado + importePagadoSql > importeTotalSql)
            {
                MessageBox.Show("El importe excede a la deuda que quiere liquidar");
            }
            else
            {
                String importeR = caja_Restante.Text;
                
                if (pagado == Convert.ToSingle(importeR))
                {

                    update = "update pedidos set importepagado='" + importeTotalSql + "',liquidado='S' where idpedido=" + idP;
                    conexion.setData(update);
                    MessageBox.Show("Pedido pagado totalmente");
                    //insert en tabla historial cambios -> salida
                    //insert.insertHistorialCambio(idUsuario, 1, "Salida añadida " + concepto);
                }
                else if (pagado < importeTotalSql)
                {
                    update = "update pedidos set importepagado='" + (pagado+importePagadoSql) + "' where idpedido=" + idP;
                    conexion.setData(update);
                    
                    MessageBox.Show("Pagada una parte del pedido");
                    //insert en tabla historial cambios -> salida
                    //insert.insertHistorialCambio(idUsuario, 1, "Salida añadida " + concepto);
                }
            }
            this.Close();
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbDirecta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDirecta.Checked == true)
            {
                rbPorcentual.Checked = false;
            }
        }

        private void rbPorcentual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPorcentual.Checked == true)
            {
                rbDirecta.Checked = false;
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            MetodosAuxiliares.cajaDecimal(txtImporte, e, 6, 2);
        }
    }
}
