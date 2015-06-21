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
    public partial class ImprimirArticulosSimples : Form
    {
        private int idArticulo;
        private ConnectDB conexion;
        public ImprimirArticulosSimples()
        {
            InitializeComponent();
        }

        public ImprimirArticulosSimples(ConnectDB con, int idA)
        {
            InitializeComponent();
            this.idArticulo = idA;
            this.conexion = con;
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            
            ReporteArticulosSimples miReporte = new ReporteArticulosSimples();
            DataTable articulosS = new DataTable();
            articulosS.Columns.Add("idarticulo", Type.GetType("System.String"));
            articulosS.Columns.Add("composicion", Type.GetType("System.String"));
            articulosS.Columns.Add("medida", Type.GetType("System.String"));
            articulosS.Columns.Add("precio", Type.GetType("System.String"));
            articulosS.Columns.Add("nombre", Type.GetType("System.String"));
            articulosS.Columns.Add("referencia", Type.GetType("System.String"));
            articulosS.Columns.Add("stockreal", Type.GetType("System.String"));
            articulosS.Columns.Add("stockideal", Type.GetType("System.String"));
            int idcomposicion=Convert.ToInt32(conexion.DLookUp("refcomposicion","ARTICULOS","idarticulo="+idArticulo));
            int idmedida=Convert.ToInt32(conexion.DLookUp("refmedida","ARTICULOS","idarticulo="+idArticulo));
            String composicion=Convert.ToString(conexion.DLookUp("composicion","COMPOSICIONES","idcomposicion="+idcomposicion));
            String medida = Convert.ToString(conexion.DLookUp("medida", "MEDIDAS", "idmedida=" + idmedida));
            String precio = Convert.ToString(conexion.DLookUp("precio", "ARTICULOS", "idarticulo=" + idArticulo));
            String referencia = Convert.ToString(conexion.DLookUp("referencia", "ARTICULOS", "idarticulo=" + idArticulo));
            String nombre = Convert.ToString(conexion.DLookUp("nombre", "ARTICULOS", "idarticulo=" + idArticulo));
            String real = Convert.ToString(conexion.DLookUp("stockreal", "ARTICULOSSTOCK", "idarticulo=" + idArticulo));
            String ideal = Convert.ToString(conexion.DLookUp("stockideal", "ARTICULOSSTOCK", "idarticulo=" + idArticulo));
            articulosS.Rows.Add(idArticulo, composicion,medida, precio,nombre,referencia,real,ideal);
            miReporte.Database.Tables["ArticulosSimples"].SetDataSource(articulosS);
            crystalReportViewer1.ReportSource = miReporte;
        }
    }
}
