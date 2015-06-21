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
    public partial class RestaurarStock : Form
    {
        public ConnectDB conexion;
        public RestaurarStock()
        {
            InitializeComponent();
        }

        public RestaurarStock(ConnectDB con)
        {
            InitializeComponent();
            this.conexion = con;
            cargarDia();
            ToolTip tool = new ToolTip();
            tool.SetToolTip(botonSalir, "Salir");
            tool.SetToolTip(botonImprimir, "Imprimir el el stock en un informe según fecha y hora seleccionadas");
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            /*Llamar al informe pasandole el dia y la hora
             Cambiar los ceros por los datos de los combos
             */
            if (comboHora.SelectedIndex > 0 && comboDia.SelectedIndex > 0)
            {
                String fecha = comboDia.SelectedItem.ToString();
                int fechaNumerica = MetodosAuxiliares.devolverFechaInt(fecha);
                String hora = comboHora.SelectedItem.ToString();
                int horaNumerica = MetodosAuxiliares.devolverHoraInt(hora);
                //int cargarHora=;
                ImprimirStock stock = new ImprimirStock(conexion, fechaNumerica, horaNumerica);
                stock.ShowDialog();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fecha y la hora para ver el informe");
            }
            
            
        }
        public void cargarDia()
        {
            DataSet data = new DataSet();
            DataTable tabla = new DataTable();
            int fechaNum;
            String fecha;
            data = conexion.getData("SELECT distinct fecha FROM HISTORIALSTOCK order by fecha ", "HISTORIALSTOCK");
            tabla = data.Tables["HISTORIALSTOCK"];
            comboDia.Items.Add("Seleccione fecha");
            foreach (DataRow row in tabla.Rows)
            {
                fechaNum = Convert.ToInt32(row["fecha"]);
                fecha = MetodosAuxiliares.pasarFecha(fechaNum);
                comboDia.Items.Add(fecha);
            }
            comboDia.SelectedIndex = 0;
        }

        public void cargarHora(int fecha)
        {
            DataSet data = new DataSet();
            DataTable tabla = new DataTable();
            int horaNum;
            String hora;

            data = conexion.getData("SELECT distinct hora FROM HISTORIALSTOCK where fecha=" + fecha + " order by hora ", "HISTORIALSTOCK");
            tabla = data.Tables["HISTORIALSTOCK"];
            comboHora.Items.Add("Selecciona la hora");
            foreach (DataRow row in tabla.Rows)
            {
                horaNum = Convert.ToInt32(row["hora"]);
               
                hora = MetodosAuxiliares.pasarHora(horaNum);
                
                comboHora.Items.Add(hora);
            }
            comboHora.SelectedIndex = 0;
        }

        private void comboDia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDia.SelectedIndex > 0)
            {
                comboHora.Items.Clear();
                String fecha = comboDia.SelectedItem.ToString();
                int fechaNumerica=MetodosAuxiliares.devolverFechaInt(fecha);
                cargarHora(fechaNumerica);
            }
        }


        private void RestaurarStock_Load(object sender, EventArgs e)
        {

        }

        private void botonSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botonImprimir_Click(object sender, EventArgs e)
        {
            /*Llamar al informe pasandole el dia y la hora
             Cambiar los ceros por los datos de los combos
             */
            if (comboHora.SelectedIndex > 0 && comboDia.SelectedIndex > 0)
            {
                String fecha = comboDia.SelectedItem.ToString();
                int fechaNumerica = MetodosAuxiliares.devolverFechaInt(fecha);
                String hora = comboHora.SelectedItem.ToString();
                int horaNumerica = MetodosAuxiliares.devolverHoraInt(hora);
                //int cargarHora=;
                ImprimirStock stock = new ImprimirStock(conexion, fechaNumerica, horaNumerica);
                stock.ShowDialog();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fecha y la hora para ver el informe");
            }
        }

    }
}
