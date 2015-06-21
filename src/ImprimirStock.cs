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
    public partial class ImprimirStock : Form
    {
        public ConnectDB conexion;
        public int fecha;
        public int hora;
        
        public ImprimirStock(ConnectDB con, int fech,int h)
        {
            
            InitializeComponent();
            conexion = con;
            fecha = fech;
            hora = h;

        }

        private void ImprimirStock_Load(object sender, EventArgs e)
        {
            ReporteStock informe = new ReporteStock();
           
            //CREO LOS CAMPOS PARA ARTICULOS
            DataTable articulos = new DataTable();
            
            articulos.Columns.Add("Stock Real", Type.GetType("System.String"));
            articulos.Columns.Add("Stock Ideal", Type.GetType("System.String"));
            articulos.Columns.Add("Fecha",Type.GetType("System.String"));
            articulos.Columns.Add("Hora",Type.GetType("System.String"));
            articulos.Columns.Add("Referencia", Type.GetType("System.String"));
            articulos.Columns.Add("Nombre", Type.GetType("System.String"));

            
            //DATOS DE FECHA Y HORA
            int referencia,stockR, stockI,idArticulo;
            String nombreArt, fechaI, horaI;

            

            String sql = "Select * from HISTORIALSTOCK where fecha=" + fecha + " and hora=" + hora + "";
            DataSet data = conexion.getData(sql, "HISTORIALSTOCK");
            DataTable dtTable = data.Tables["HISTORIALSTOCK"];
            

            foreach (DataRow row in dtTable.Rows)
            {
                idArticulo = Convert.ToInt32(row["IDARTICULO"]);
                referencia = Convert.ToInt32(conexion.DLookUp("REFERENCIA", "ARTICULOS", "IDARTICULO=" + idArticulo));
                nombreArt = Convert.ToString(conexion.DLookUp("NOMBRE", "ARTICULOS", "IDARTICULO=" + idArticulo));
                stockR = Convert.ToInt32(row["STOCKREAL"]);
                stockI = Convert.ToInt32(row["STOCKIDEAL"]);
                fechaI = MetodosAuxiliares.pasarFecha(Convert.ToInt32(row["FECHA"]));
                horaI = MetodosAuxiliares.pasarHora(Convert.ToInt32(row["HORA"]));
                articulos.Rows.Add(stockR, stockI, fechaI, horaI, referencia, nombreArt);     
            }
            
            informe.Database.Tables["HistorialStock"].SetDataSource(articulos);
            crystalReportViewer1.ReportSource = informe;

        }


    }
}
