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
    public partial class ImprimirCompuestos : Form
    {
        private int idCompuesto;
        private ConnectDB conexion;
        public ImprimirCompuestos()
        {
            InitializeComponent();
        }

        public ImprimirCompuestos(ConnectDB con,int idC)
        {
            InitializeComponent();
            this.conexion=con;
            this.idCompuesto = idC;
        }

        private void CrystalReportViewer1_Load(object sender, EventArgs e)
        {
           
            ReporteCompuestos miReporte = new ReporteCompuestos();

            // DATOS DEL ARTICULO COMPUESTO //
            DataTable articuloC = new DataTable();
            articuloC.Columns.Add("nombre", Type.GetType("System.String"));
            articuloC.Columns.Add("precio", Type.GetType("System.String"));
            String nombre = Convert.ToString(conexion.DLookUp("nombre", "ARTICULOS", "idarticulo=" + idCompuesto));
            String precio = Convert.ToString(conexion.DLookUp("precio", "ARTICULOS", "idarticulo=" + idCompuesto));
            articuloC.Rows.Add(nombre,precio);
            miReporte.Database.Tables["Compuestos"].SetDataSource(articuloC);
            crystalReportViewer1.ReportSource = miReporte;

            
            //DATOS DE LOS ARTICULOS QUE COMPONEN EL COMPUESTO //
            DataTable articulosS = new DataTable();
            articulosS.Columns.Add("idarticulo", Type.GetType("System.String"));
            articulosS.Columns.Add("nombre", Type.GetType("System.String"));
            articulosS.Columns.Add("composicion", Type.GetType("System.String"));
            articulosS.Columns.Add("medida", Type.GetType("System.String"));
            articulosS.Columns.Add("precio", Type.GetType("System.String"));
            articulosS.Columns.Add("referencia", Type.GetType("System.String"));
            articulosS.Columns.Add("cantidad", Type.GetType("System.String"));
            articulosS.Columns.Add("preciototal", Type.GetType("System.String"));


            int idA;
            String composicion, medida, nombreA, articulo,referencia;
            int cantidad, idcomposicion, idmedida;
            double precioUnitario, precioTotal;
            String sql = "select * from articulospartes where idcompuesto=" + idCompuesto;
            DataSet data = conexion.getData(sql, "ARTICULOSPARTES");
            DataTable dtTable = data.Tables["ARTICULOSPARTES"];

            foreach (DataRow row in dtTable.Rows)
            {
                idA = Convert.ToInt32(row["idarticulo"]);
                nombreA = Convert.ToString(conexion.DLookUp("nombre", "ARTICULOS", "idarticulo=" + idA));
                referencia = Convert.ToString(conexion.DLookUp("referencia", "ARTICULOS", "idarticulo=" + idA));
                idcomposicion = Convert.ToInt32(conexion.DLookUp("refcomposicion", "ARTICULOS", "idarticulo=" + idA));
                composicion = Convert.ToString(conexion.DLookUp("composicion", "COMPOSICIONES", "idcomposicion=" + idcomposicion));
                idmedida = Convert.ToInt32(conexion.DLookUp("refmedida", "ARTICULOS", "idarticulo=" + idA));
                medida = Convert.ToString(conexion.DLookUp("medida", "MEDIDAS", "idmedida=" + idmedida));
                precioUnitario = Convert.ToSingle(conexion.DLookUp("precio", "ARTICULOS", "idarticulo=" + idA));
                cantidad = Convert.ToInt32(row["cantidad"]);
                precioUnitario = Math.Round(precioUnitario, 2);
                precioTotal = precioUnitario * cantidad;
                precioTotal = Math.Round(precioTotal, 2);
                articulo = nombreA + " " + composicion + " " + medida;
                articulosS.Rows.Add(idA, nombreA,composicion,medida,precioUnitario,referencia,cantidad,precioTotal);

            }
            miReporte.Database.Tables["ArticulosCompuestos"].SetDataSource(articulosS);
            crystalReportViewer1.ReportSource = miReporte;

        }
    }
}
