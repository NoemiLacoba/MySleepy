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
    public partial class ImprimirProveedor : Form
    {
        public int idProveedor;
        public ConnectDB conexion;
        public ImprimirProveedor(ConnectDB con, int idproveedor)
        {
            InitializeComponent();
            idProveedor = idproveedor;
            conexion = con;

        }

       
        
        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {
            ReporteProveedor informe = new ReporteProveedor();

            //DATOS DEL PROVEEDOR
            DataTable proveedor = new DataTable();
            proveedor.Columns.Add("idProveedor", Type.GetType("System.String"));
            proveedor.Columns.Add("Nombre", Type.GetType("System.String"));
            proveedor.Columns.Add("Poblacion", Type.GetType("System.String"));
            proveedor.Columns.Add("Provincia", Type.GetType("System.String"));
            proveedor.Columns.Add("CP", Type.GetType("System.String"));
            proveedor.Columns.Add("nifcif", Type.GetType("System.String"));
            proveedor.Columns.Add("contacto", Type.GetType("System.String"));
            proveedor.Columns.Add("observaciones", Type.GetType("System.String"));
            proveedor.Columns.Add("telefono", Type.GetType("System.String"));
            proveedor.Columns.Add("movil", Type.GetType("System.String"));
            proveedor.Columns.Add("direccion", Type.GetType("System.String"));
            proveedor.Columns.Add("email", Type.GetType("System.String"));
            proveedor.Columns.Add("pais", Type.GetType("System.String"));


            //DATOS DE CONSULTAS PROVEEDOR
            String nombre = Convert.ToString(conexion.DLookUp("nombre", "PROVEEDORES", "idProveedor=" + idProveedor));
            String direccion = Convert.ToString(conexion.DLookUp("direccion", "PROVEEDORES", "idProveedor=" + idProveedor));
            String telefono = Convert.ToString(conexion.DLookUp("telefono", "PROVEEDORES", "idProveedor=" + idProveedor));
            String movil = Convert.ToString(conexion.DLookUp("movil", "PROVEEDORES", "idProveedor=" + idProveedor));
            String pais = Convert.ToString(conexion.DLookUp("pais", "PROVEEDORES", "idProveedor=" + idProveedor));
            String email = Convert.ToString(conexion.DLookUp("email", "PROVEEDORES", "idProveedor=" + idProveedor));
            String contacto = Convert.ToString(conexion.DLookUp("contacto", "PROVEEDORES", "idProveedor=" + idProveedor));
            String observaciones = Convert.ToString(conexion.DLookUp("observaciones", "PROVEEDORES", "idProveedor=" + idProveedor));
            String dni = Convert.ToString(conexion.DLookUp("cif", "PROVEEDORES", "idProveedor=" + idProveedor));

            if (dni.Equals("")) dni = Convert.ToString(conexion.DLookUp("dni", "PROVEEDORES", "idProveedor=" + idProveedor));
            int refcodPob = Convert.ToInt32(conexion.DLookUp("REFCPPOBLACIONES", "PROVEEDORES", "idProveedor=" + idProveedor));

            String idpoblacion = Convert.ToString(conexion.DLookUp("refpoblacion", "CODIGOSPOSTALESPOBLACIONES", "idcodigopostalpob=" + refcodPob));
            String poblacion = Convert.ToString(conexion.DLookUp("poblacion", "POBLACIONES", "idpoblacion=" + idpoblacion));
            String idprovincia = Convert.ToString(conexion.DLookUp("refprovincia", "CODIGOSPOSTALESPOBLACIONES", "idcodigopostalpob=" + refcodPob));
            String provincia = Convert.ToString(conexion.DLookUp("provincia", "PROVINCIAS", "idprovincia=" + idprovincia));
            String idcp = Convert.ToString(conexion.DLookUp("refcodigopostal", "CODIGOSPOSTALESPOBLACIONES", "idcodigopostalpob=" + refcodPob));
            String postal = Convert.ToString(conexion.DLookUp("codigopostal", "CODIGOSPOSTALES", "idcodigopostal=" + idcp));

            proveedor.Rows.Add(idProveedor, nombre, poblacion, provincia, postal, dni, contacto, observaciones, telefono, movil, direccion, email, pais);
            informe.Database.Tables["Proveedor"].SetDataSource(proveedor);
            crystalReportViewer1.ReportSource = informe;
            

            //DATOS DE LA TABLA DE LOS PROVEEDORES
            DataTable art = new DataTable();

            art.Columns.Add("idarticulo",Type.GetType("System.String"));
            art.Columns.Add("descripcion", Type.GetType("System.String"));
            art.Columns.Add("precio", Type.GetType("System.String"));
            art.Columns.Add("preciototal", Type.GetType("System.String"));
            art.Columns.Add("cantidad", Type.GetType("System.String"));

            //Datos de la consulta de la tabla
            String sql = "select * from proveedorarticulos where idproveedor="+idProveedor;
            DataSet data= conexion.getData(sql, "proveedorarticulos");
            DataTable dtTable = data.Tables["proveedorarticulos"];
            String idarticulo, descripcion, precio, preciototal,cantidad="1";
            double IVA = 1.21;
            double res=0;
            double sumaPrecio=0,sumarTotales=0;
            foreach (DataRow row in dtTable.Rows)
            {

                idarticulo = Convert.ToString(row["idarticulo"]);
                descripcion = Convert.ToString(row["nombreproveedor"]);
                precio = Convert.ToString(row["precioproveedor"]);
                res = Convert.ToSingle(precio) * 1.2652;
                sumaPrecio=sumaPrecio+Convert.ToSingle(precio);
                res = Math.Round(res, 2);
                preciototal = Convert.ToString(res);
                sumarTotales=sumarTotales+res;
                art.Rows.Add(idarticulo,descripcion,precio,preciototal,cantidad);
            }
            informe.Database.Tables["Articulos"].SetDataSource(art);
            crystalReportViewer1.ReportSource = informe;

            //DATOS DEL TOTAL DE LA FACTURA
            DataTable total = new DataTable();
            total.Columns.Add("IVA", Type.GetType("System.String"));
            total.Columns.Add("precio", Type.GetType("System.String"));
            total.Columns.Add("total", Type.GetType("System.String"));

            total.Rows.Add(Math.Round((sumaPrecio * 0.262),2), Math.Round(sumaPrecio,2), Math.Round(sumarTotales,2));
            informe.Database.Tables["Totales"].SetDataSource(total);
            crystalReportViewer1.ReportSource = informe;
        }

        private void ImprimirProveedor_Load(object sender, EventArgs e)
        {

        }

        
    }
}
