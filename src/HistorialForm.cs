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
    public partial class HistorialForm : Form
    {
        ConnectDB conexion;
        private int idUsuario;
        //patron singleton
        private static HistorialForm instance;

        public static HistorialForm Instance(int idUsuario, ConnectDB c)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new HistorialForm(idUsuario, c);
            }
            return instance;
        }
        private HistorialForm(int idUsuario, ConnectDB c)
        {
            InitializeComponent();
            cbLiquidar.Visible = false;
            this.conexion = c;
            this.idUsuario = idUsuario;
            cargarTablaInicio();
            rellenarCombo(cbUsuarios, "Select NOMBRE from USUARIOS", "NOMBRE", "USUARIOS");
            rellenarCombo(cbTipoCambio, "Select DESCRIPCION FROM TIPOCAMBIO", "DESCRIPCION", "TIPOCAMBIO");
        }

        public HistorialForm(ConnectDB conexion)
        {
            // TODO: Complete member initialization
            this.conexion = conexion;
            
        }
        private void HistorialForm_Load_1(object sender, EventArgs e)
        {
            dgvHistorial.ClearSelection();
            dgvHistorial.Update();
        }
        private void limpiarTabla()
        {
            // Limpiamos el datagridView
            while (dgvHistorial.RowCount > 0)
            {
                dgvHistorial.Rows.Remove(dgvHistorial.CurrentRow);
            }
        }
        public void cargarTablaInicio()
        {
            //solo mostraremos los no eliminados inicialmente
            String select = "Select distinct h.IDUSUARIO,h.OBSERVACION,u.NOMBRE,h.FECHA,tc.DESCRIPCION " +
                              "from HISTORIALCAMBIOS h,USUARIOS u, TIPOCAMBIO tc" +
                              " where tc.IDTIPOCAMBIO = h.IDTIPOCAMBIO"+
                              " and u.IDUSUARIO = h.IDUSUARIO"+
                              " order by h.fecha desc";
            cargarTabla(select);
        }

        public void cargarTabla(String sentencia)
        {
            limpiarTabla();     

            DataSet data;
            data = conexion.getData(sentencia, "HISTORIALCAMBIOS");

            DataTable tHistorial = data.Tables["HISTORIALCAMBIOS"];

            //int idHistorial = 0;
            int fecha = 0;
            String observacion  ="",tipoCambio="",nombre ="",fechaConvert ="";

            foreach (DataRow row in tHistorial.Rows)
            {
                //idHistorial = Convert.ToInt32(row["IDHISTOCAMBIO"]);
                idUsuario = Convert.ToInt32(row["IDUSUARIO"]);
                nombre = Convert.ToString(conexion.DLookUp("NOMBRE", "USUARIOS", "IDUSUARIO = " + idUsuario));
                fecha = Convert.ToInt32(row["FECHA"]);
                fechaConvert = MetodosAuxiliares.pasarFecha(fecha);
                tipoCambio = Convert.ToString(row["DESCRIPCION"]);
                observacion = Convert.ToString(row["OBSERVACION"]);

                dgvHistorial.Rows.Add(nombre,fechaConvert, tipoCambio,observacion);

            } // Fin del bucle for each
            dgvHistorial.ClearSelection();
            dgvHistorial.Update();
        }
        
        public void rellenarCombo(ComboBox combo,String select,String idTabla,String tabla)
        {
 
            //realizar el select 
            DataSet data = conexion.getData(select, tabla);

            DataTable tTabla = data.Tables[tabla];

            String nombre;
            foreach (DataRow row in tTabla.Rows)
            {
                nombre = Convert.ToString(row[idTabla]);

                //añadir a comboBox
                combo.Items.Add(nombre);

            } // Fin del bucle for each
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //pedimos confirmacion
            DialogResult opcion = MessageBox.Show("¿Desea guardar el historial de cambios?", "Confirmación",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (opcion == DialogResult.OK)
            {
                InsertHistorial insert = new InsertHistorial(conexion);
                insert.guardarEnFichero(dgvHistorial);
                MessageBox.Show("Cambios guardados");
            }
        }


        private void cbParteModificada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbParteModificada.SelectedIndex == 2)
            {
                cbPagado.Visible = true;
            }
            else
            {
                cbPagado.Visible = false;
            }
            filtrar();
        }

        public void filtrar()
        {
            String select = "Select distinct h.IDUSUARIO,h.OBSERVACION,u.NOMBRE,h.FECHA,tc.IDTIPOCAMBIO,tc.DESCRIPCION " +
                              "from HISTORIALCAMBIOS h,USUARIOS u, TIPOCAMBIO tc" +
                              " where tc.IDTIPOCAMBIO = h.IDTIPOCAMBIO"+
                              " and u.IDUSUARIO = h.IDUSUARIO"
                              ;
            if (txtObservacion.Text != "")
            {
                select += " and upper(h.OBSERVACION) like '%" + txtObservacion.Text.ToUpper() + "%'";
            }
            if (cbUsuarios.SelectedIndex != -1)
            {
                select += " and upper(u.nombre) like '%"+ cbUsuarios.SelectedItem.ToString().ToUpper()+"%'";
            }
            if (cbTipoCambio.SelectedIndex != -1)
            {
                select += " and upper(tc.DESCRIPCION) LIKE '%"+cbTipoCambio.SelectedItem.ToString().ToUpper()+"%'";
            }
            if (cbParteModificada.SelectedIndex != -1)
            {
                select += " and upper(h.OBSERVACION) LIKE '%" + cbParteModificada.SelectedItem.ToString().ToUpper() + "%'";
            }
            if (cbCaja.SelectedIndex != -1)
            {
                select += " and upper(h.OBSERVACION) LIKE '%" + cbCaja.SelectedItem.ToString().ToUpper() + "%'";
            }
            if (dateTimePickerFechaInicio.Value <= dateTimePickerFechaFin.Value)
            {
                int fechaInicio = Convert.ToInt32(MetodosAuxiliares.devolverFecha(dateTimePickerFechaInicio));
                int fechaFin = Convert.ToInt32(MetodosAuxiliares.devolverFecha(dateTimePickerFechaFin));

                select += " and FECHA between " + fechaInicio +
                          " and " + fechaFin;
            }
            if (cbPagado.Visible == true && cbPagado.Checked)
            {
                select += " and upper(h.Observacion) like '%PAGADO%'";
            }
            if (cbLiquidar.Visible == true && cbLiquidar.Checked && cbLiquidar.Text.ToLower().Equals("liquidada"))
            {
                select += " and upper(h.Observacion) like '%LIQUIDAD%'";
            }

            select += " order by h.FECHA";
            
            cargarTabla(select);
        }

        private void cbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void cbTipoCambio_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            filtrar();
        }

        private void txtObservacion_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void dateTimePickerFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerFechaInicio.Value > System.DateTime.Now && dateTimePickerFechaFin.Value > System.DateTime.Now)
            {
                lblFechaErronea.Text = "Orden de fecha incorrecto";
            }
            else
            {
                lblFechaErronea.Text = "";
                filtrar();
            }
            
        }
      
        private void dateTimePickerFechaFin_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerFechaInicio.Value > System.DateTime.Now && dateTimePickerFechaFin.Value > System.DateTime.Now)
            {
                lblFechaErronea.Text="Fecha posterior a la actual";
            }
            else
            {
                lblFechaErronea.Text = "";
                filtrar();
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtObservacion.Text = "";
            cbParteModificada.SelectedIndex = -1;
            cbTipoCambio.SelectedIndex = -1;
            cbCaja.SelectedIndex = -1;
            cbUsuarios.SelectedIndex = -1;
            dateTimePickerFechaFin.Value = System.DateTime.Today;
            dateTimePickerFechaInicio.Value = System.DateTime.Today;
            cbLiquidar.Visible = false;
        }

        private void cbPagado_CheckedChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void cbCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbLiquidar.Visible = true;
            filtrar();
        }

        private void cbLiquidar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLiquidar.Checked == true)
            {
                cbLiquidar.Text = "Liquidada";
            }
            else
            {
                cbLiquidar.Text = "No Liquidada";
            }
            filtrar();
        }
        
    }
}
