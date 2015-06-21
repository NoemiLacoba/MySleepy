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
    public partial class ImprimirTodosSimples : Form
    {
        private ConnectDB conexion;
        public ImprimirTodosSimples()
        {
            InitializeComponent();
        }

        public ImprimirTodosSimples(ConnectDB con)
        {
            InitializeComponent();
            this.conexion = con;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            ReporteTodosSimples miReporte = new ReporteTodosSimples();

            //CREAMOS LOS CAMPOS 
            DataTable articulosS = new DataTable();
            articulosS.Columns.Add("idarticulo", Type.GetType("System.String"));
            articulosS.Columns.Add("composicion", Type.GetType("System.String"));
            articulosS.Columns.Add("medida", Type.GetType("System.String"));
            articulosS.Columns.Add("precio", Type.GetType("System.String"));
            articulosS.Columns.Add("nombre", Type.GetType("System.String"));
            articulosS.Columns.Add("referencia", Type.GetType("System.String"));
            articulosS.Columns.Add("stockreal", Type.GetType("System.String"));
            articulosS.Columns.Add("stockideal", Type.GetType("System.String"));

            //SELECCIONAMOS TODOS LOS DATOS DE LOS ARTICULOS SIMPLES
            int idArticulo;
            String composicion, medida, nombre, real,ideal,referencia,precio;
            int idcomposicion, idmedida;
            String sql = "select * from articulos where refcomposicion<>12 and eliminado = 0 order by nombre";
            DataSet data = conexion.getData(sql, "ARTICULOSPARTES");
            DataTable dtTable = data.Tables["ARTICULOSPARTES"];

            foreach (DataRow row in dtTable.Rows)
            {
                idArticulo = Convert.ToInt32(row["idarticulo"]);
                idcomposicion = Convert.ToInt32(row["refcomposicion"]);
                idmedida = Convert.ToInt32(row["refmedida"]);
                composicion = Convert.ToString(conexion.DLookUp("composicion", "COMPOSICIONES", "idcomposicion=" + idcomposicion));
                medida = Convert.ToString(conexion.DLookUp("medida", "MEDIDAS", "idmedida=" + idmedida));
                precio = Convert.ToString(row["precio"]);
                referencia = Convert.ToString(row["referencia"]);
                nombre = Convert.ToString(row["nombre"]);
                real = Convert.ToString(conexion.DLookUp("stockreal", "ARTICULOSSTOCK", "idarticulo=" + idArticulo));
                ideal = Convert.ToString(conexion.DLookUp("stockideal", "ARTICULOSSTOCK", "idarticulo=" + idArticulo));
                articulosS.Rows.Add(idArticulo, composicion, medida, precio, nombre, referencia, real, ideal);
            }
            miReporte.Database.Tables["ArticulosSimples"].SetDataSource(articulosS);
            crystalReportViewer1.ReportSource = miReporte;
        }
    }
}
