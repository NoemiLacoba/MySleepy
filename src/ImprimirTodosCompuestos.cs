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
    public partial class ImprimirTodosCompuestos : Form
    {
        private ConnectDB conexion;

        public ImprimirTodosCompuestos()
        {
            InitializeComponent();
        }

        public ImprimirTodosCompuestos(ConnectDB con)
        {
            InitializeComponent();
            this.conexion=con;
        }


        private void CrystalReportViewer1_Load(object sender, EventArgs e)
        {
            ReporteTodosCompuestos miReporte = new ReporteTodosCompuestos();

            String sqlC = "select distinct art.* from articulos art, articulospartes ap where art.idarticulo=ap.idcompuesto and art.idarticulo<>ap.idarticulo and eliminado=0";
            DataSet dataC = conexion.getData(sqlC, "ARTICULOS");
            DataTable dtTableC = dataC.Tables["ARTICULOS"];
            int idCompuesto;
           
            DataTable articulosS = new DataTable();

           
            //DATOS DE LOS ARTICULOS QUE COMPONEN EL COMPUESTO //

            articulosS.Columns.Add("idarticulo", Type.GetType("System.String"));
            articulosS.Columns.Add("nombresimple", Type.GetType("System.String"));
            articulosS.Columns.Add("nombrecompuesto", Type.GetType("System.String"));
            
            int idA;
            String composicion, medida, nombreA, referencia;//,articulo;
            int cantidad, idcomposicion, idmedida;
            double precioUnitario;//, precioTotal;
            String nombresimplecompleto, nombreCompuesto;
            int i = 1;
            foreach (DataRow rowC in dtTableC.Rows)
            {
                nombreCompuesto ="Nº: "+i+" NOMBRE PACK : "+ Convert.ToString(rowC["nombre"])+"     PRECIO PACK :"+Convert.ToString(rowC["precio"]);
                  
               
                idCompuesto = Convert.ToInt32(rowC["idarticulo"]);
                
                String sql = "select * from articulos where idarticulo in (select idarticulo from articulospartes where idcompuesto="+idCompuesto+") and eliminado =0";
                DataSet data = conexion.getData(sql, "ARTICULOS");
                DataTable dtTable = data.Tables["ARTICULOS"];
                //MessageBox.Show(sql);
                
                foreach (DataRow row in dtTable.Rows)
                {
                    
                    idA = Convert.ToInt32(row["idarticulo"]); 
                    nombreA = Convert.ToString(row["nombre"]);
                    referencia = Convert.ToString(row["referencia"]);
                    idcomposicion = Convert.ToInt32(row["refcomposicion"]);
                    
                    composicion = Convert.ToString(conexion.DLookUp("composicion", "COMPOSICIONES", "idcomposicion=" + idcomposicion));
                    idmedida = Convert.ToInt32(row["refmedida"]);
                    medida = Convert.ToString(conexion.DLookUp("medida", "MEDIDAS", "idmedida=" + idmedida));
                    precioUnitario = Convert.ToSingle(row["precio"]);
                    cantidad = Convert.ToInt32(conexion.DLookUp("cantidad","ARTICULOSPARTES","idarticulo="+idA));
                    precioUnitario = Math.Round(precioUnitario, 2);
                    nombresimplecompleto = referencia + "  " + nombreA + "  " + composicion + "  " + medida + "  " + precioUnitario + "  " + cantidad;
                    //MessageBox.Show(nombresimplecompleto);
                    
                    articulosS.Rows.Add(idA, nombresimplecompleto,nombreCompuesto);
                }
                i++;
            }
            miReporte.Database.Tables["PackArticulos"].SetDataSource(articulosS);
            crystalReportViewer1.ReportSource = miReporte;

        }
        
    }
}
