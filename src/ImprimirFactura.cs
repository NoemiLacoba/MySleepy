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
    public partial class ImprimirFactura : Form
    {
        private static int idFactura;
        private static int idCliente;
        private static int idPedido;
        ConnectDB conexion;
        public ImprimirFactura()
        {
            InitializeComponent();
        }

        public ImprimirFactura(ConnectDB con, int factura,int cliente, int pedido)
        {
            InitializeComponent();
            idFactura = factura;
            idCliente = cliente;
            idPedido = pedido;
            conexion = con;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ReporteFacturas miReporte = new ReporteFacturas();


            /// DATOS DE FACTURAS ///
            
            if (idPedido != 0)
            {
                DataTable facturas = new DataTable();
                facturas.Columns.Add("NumeroFactura", Type.GetType("System.String"));
                facturas.Columns.Add("ImporteNeto", Type.GetType("System.String"));
                facturas.Columns.Add("ImporteTotal", Type.GetType("System.String"));
                facturas.Columns.Add("IVA", Type.GetType("System.String"));
                facturas.Columns.Add("idFactura", Type.GetType("System.String"));
                facturas.Columns.Add("Fecha", Type.GetType("System.String"));
                String numfactura = Convert.ToString(conexion.DLookUp("numFactura", "FACTURAS", "idfactura=" + idFactura));
                int fecha = Convert.ToInt32(conexion.DLookUp("fecha", "FACTURAS", "idfactura=" + idFactura));
                int hora = Convert.ToInt32(conexion.DLookUp("hora", "FACTURAS", "idfactura=" + idFactura));
                String fechaF = MetodosAuxiliares.pasarFecha(fecha);
                String horaF = MetodosAuxiliares.pasarHora(hora);
                String fechaHora = fechaF + " " + horaF;
                double importeNeto = Convert.ToSingle(conexion.DLookUp("importeneto", "FACTURAS", "idfactura=" + idFactura));
                double iva = importeNeto * (0.21);
                iva = Math.Round(iva, 2);
                importeNeto = Math.Round(importeNeto, 2);
                double importeTotal = importeNeto + iva;
                facturas.Rows.Add(numfactura, importeNeto, importeTotal, iva, idFactura, fechaHora);
                miReporte.Database.Tables["Facturas"].SetDataSource(facturas);
                crystalReportViewer1.ReportSource = miReporte;
            }
            else
            {
                
                DataTable facturas = new DataTable();
                facturas.Columns.Add("NumeroFactura", Type.GetType("System.String"));
                facturas.Columns.Add("ImporteNeto", Type.GetType("System.String"));
                facturas.Columns.Add("ImporteTotal", Type.GetType("System.String"));
                facturas.Columns.Add("IVA", Type.GetType("System.String"));
                facturas.Columns.Add("idFactura", Type.GetType("System.String"));
                facturas.Columns.Add("Fecha", Type.GetType("System.String"));
                String numfactura = Convert.ToString(conexion.DLookUp("numFactura", "FACTURASARTICULOS", "idfacturaarticulos=" + idFactura));
                int fecha = Convert.ToInt32(conexion.DLookUp("fecha", "FACTURASARTICULOS", "idfacturaarticulos=" + idFactura));
                int hora = Convert.ToInt32(conexion.DLookUp("hora", "FACTURASARTICULOS", "idfacturaarticulos=" + idFactura));
                String fechaF = MetodosAuxiliares.pasarFecha(fecha);
                String horaF = MetodosAuxiliares.pasarHora(hora);
                String fechaHora = fechaF + " " + horaF;
                double importeNeto = Convert.ToSingle(conexion.DLookUp("importeneto", "FACTURASARTICULOS", "idfacturaarticulos=" + idFactura));
                double iva = importeNeto * (0.21);
                iva = Math.Round(iva, 2);
                importeNeto = Math.Round(importeNeto, 2);
                double importeTotal = importeNeto + iva;
                facturas.Rows.Add(numfactura, importeNeto, importeTotal, iva, idFactura, fechaHora);
                miReporte.Database.Tables["Facturas"].SetDataSource(facturas);
                crystalReportViewer1.ReportSource = miReporte;
            }
            
            

            ///DATOS DEL CLIENTE ///
            String nombre = Convert.ToString(conexion.DLookUp("nombre","CLIENTES","idCliente="+idCliente));
            String apellido1 = Convert.ToString(conexion.DLookUp("apellido1", "CLIENTES", "idCliente=" + idCliente));
            String apellido2 = Convert.ToString(conexion.DLookUp("apellido2", "CLIENTES", "idCliente=" + idCliente));
            String apellidos = apellido1 + " " + apellido2;
            String direccion=Convert.ToString(conexion.DLookUp("direccion", "CLIENTES", "idCliente=" + idCliente));
            String telefono=Convert.ToString(conexion.DLookUp("telefono", "CLIENTES", "idCliente=" + idCliente));
            String email=Convert.ToString(conexion.DLookUp("email", "CLIENTES", "idCliente=" + idCliente));
            String dni=Convert.ToString(conexion.DLookUp("dni", "CLIENTES", "idCliente=" + idCliente));
            int refcodPob=Convert.ToInt32(conexion.DLookUp("REFCPPOBLACIONES", "CLIENTES", "idCliente=" + idCliente));
            String idpoblacion = Convert.ToString(conexion.DLookUp("refpoblacion", "CODIGOSPOSTALESPOBLACIONES", "idcodigopostalpob=" + refcodPob));
            String poblacion = Convert.ToString(conexion.DLookUp("poblacion", "POBLACIONES", "idpoblacion=" + idpoblacion));
            String idprovincia = Convert.ToString(conexion.DLookUp("refprovincia", "CODIGOSPOSTALESPOBLACIONES", "idcodigopostalpob=" + refcodPob));
            String provincia = Convert.ToString(conexion.DLookUp("provincia", "PROVINCIAS", "idprovincia=" + idprovincia));
            String idcp = Convert.ToString(conexion.DLookUp("refcodigopostal", "CODIGOSPOSTALESPOBLACIONES", "idcodigopostalpob=" + refcodPob));
            String postal = Convert.ToString(conexion.DLookUp("codigopostal", "CODIGOSPOSTALES", "idcodigopostal=" + idcp));
            
            DataTable cliente = new DataTable();
            cliente.Columns.Add("Nombrecliente", Type.GetType("System.String"));
            cliente.Columns.Add("Direccion", Type.GetType("System.String"));
            cliente.Columns.Add("Poblacion", Type.GetType("System.String"));
            cliente.Columns.Add("Provincia", Type.GetType("System.String"));
            cliente.Columns.Add("CP", Type.GetType("System.String"));
            cliente.Columns.Add("DNI", Type.GetType("System.String"));
            cliente.Columns.Add("Telefono", Type.GetType("System.String"));
            cliente.Columns.Add("email", Type.GetType("System.String"));
            cliente.Columns.Add("idCliente", Type.GetType("System.String"));
            cliente.Rows.Add(nombre + " " + apellidos, direccion, poblacion, provincia, postal,dni,telefono,email,idCliente);
            
            miReporte.Database.Tables["Clientes"].SetDataSource(cliente);
            crystalReportViewer1.ReportSource = miReporte;


            ///DATOS DEL PEDIDO///
            if (idPedido != 0)
            {
                DataTable pedido = new DataTable();
                pedido.Columns.Add("FormaPago", Type.GetType("System.String"));
                pedido.Columns.Add("idPedido", Type.GetType("System.String"));
                int formapago = Convert.ToInt32(conexion.DLookUp("refformapago", "PEDIDOS", "idpedido=" + idPedido));
                String pago = Convert.ToString(conexion.DLookUp("formapago", "FORMASDEPAGO", "idpago=" + formapago));
                pedido.Rows.Add(pago, idPedido);
                miReporte.Database.Tables["Pedidos"].SetDataSource(pedido);
                crystalReportViewer1.ReportSource = miReporte;
            }
            else
            {
                DataTable pedido = new DataTable();
                pedido.Columns.Add("FormaPago", Type.GetType("System.String"));
                pedido.Columns.Add("idPedido", Type.GetType("System.String"));
                pedido.Rows.Add("Efectivo", "0");
                miReporte.Database.Tables["Pedidos"].SetDataSource(pedido);
                crystalReportViewer1.ReportSource = miReporte;
            }

            //EN EL CASO QUE SEAN PEDIDOS QUE NO DEPENDAN DE PEDIDOS
            if (idPedido != 0)
            {
                /// DATOS DE LOS ARTICULOS SEGÚN EL PEDIDO///
                DataTable pa = new DataTable();
                pa.Columns.Add("idPedidoArticulo", Type.GetType("System.String"));
                pa.Columns.Add("Articulo", Type.GetType("System.String"));
                pa.Columns.Add("Cantidad", Type.GetType("System.String"));
                pa.Columns.Add("PrecioUnitario", Type.GetType("System.String"));
                pa.Columns.Add("PrecioTotal", Type.GetType("System.String"));
                int idPA;
                String composicion, medida, nombreA, articulo;
                int cantidad, idArticulo, idcomposicion, idmedida;
                double precioUnitario, precioTotal;
                String sql = "select * from pedidosarticulos where refpedido=" + idPedido;
                DataSet data = conexion.getData(sql, "PEDIDOSARTICULOS");
                DataTable dtTable = data.Tables["PEDIDOSARTICULOS"];

                foreach (DataRow row in dtTable.Rows)
                {
                    idPA = Convert.ToInt32(row["idpedidoarticulo"]);
                    idArticulo = Convert.ToInt32(row["refarticulo"]);
                    nombreA = Convert.ToString(conexion.DLookUp("nombre", "ARTICULOS", "idarticulo=" + idArticulo));
                    idcomposicion = Convert.ToInt32(conexion.DLookUp("refcomposicion", "ARTICULOS", "idarticulo=" + idArticulo));
                    composicion = Convert.ToString(conexion.DLookUp("composicion", "COMPOSICIONES", "idcomposicion=" + idcomposicion));
                    idmedida = Convert.ToInt32(conexion.DLookUp("refmedida", "ARTICULOS", "idarticulo=" + idArticulo));
                    medida = Convert.ToString(conexion.DLookUp("medida", "MEDIDAS", "idmedida=" + idmedida));
                    precioUnitario = Convert.ToSingle(conexion.DLookUp("precio", "ARTICULOS", "idarticulo=" + idArticulo));
                    cantidad = Convert.ToInt32(row["cantidad"]);
                    precioUnitario = Math.Round(precioUnitario, 2);
                    precioTotal = precioUnitario * cantidad;
                    precioTotal = Math.Round(precioTotal, 2);
                    articulo = nombreA + " " + composicion + " " + medida;
                    pa.Rows.Add(idPA, articulo, cantidad, precioUnitario, precioTotal);

                }
                //AÑADIMOS LOS DATOS DE LOS ARTICULOS
                int idfacturaFA = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "idfactura=" + idFactura));
                //int idfacturaAG = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "idfactura=" + idFactura));
                if (idfacturaFA != -1)
                {

                    String sql1 = "select * from articulosgenericos where idfacturaarticulos=" + idfacturaFA;
                    DataSet data1 = conexion.getData(sql1, "ARTICULOSGENERICOS");

                    DataTable dtTable1 = data1.Tables["ARTICULOSGENERICOS"];
                    String cantidadAG, precioAG, nombreAG, idAG, precioTotalAG;
                    double res;
                    foreach (DataRow row in dtTable1.Rows)
                    {

                        idAG = Convert.ToString(row["idarticulogenerico"]);
                        cantidadAG = Convert.ToString(row["cantidad"]);
                        nombreAG = Convert.ToString(row["descripcion"]);
                        precioAG = Convert.ToString(row["precio"]);
                        res = Math.Round(Convert.ToSingle(precioAG), 2);
                        precioAG = Convert.ToString(res);
                        precioTotalAG = Convert.ToString(Math.Round((res * Convert.ToInt32(cantidadAG) * 1.21), 2));

                        pa.Rows.Add(idAG, nombreAG, cantidadAG, precioAG, precioTotalAG);
                    }
                }
                miReporte.Database.Tables["PedidosArticulos"].SetDataSource(pa);
                crystalReportViewer1.ReportSource = miReporte;
                }
                else
                {
                    /// DATOS DE LOS ARTICULOS SEGÚN EL PEDIDO///
                    DataTable pa = new DataTable();
                    pa.Columns.Add("idPedidoArticulo", Type.GetType("System.String"));
                    pa.Columns.Add("Articulo", Type.GetType("System.String"));
                    pa.Columns.Add("Cantidad", Type.GetType("System.String"));
                    pa.Columns.Add("PrecioUnitario", Type.GetType("System.String"));
                    pa.Columns.Add("PrecioTotal", Type.GetType("System.String"));
                    
                    int idfacturaFA = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "idfactura=" + idFactura));
                    //int idfacturaAG = Convert.ToInt32(conexion.DLookUp("idfacturaarticulos", "FACTURASARTICULOS", "idfactura=" + idFactura));
                    if (idfacturaFA != -1)
                    {

                        String sql1 = "select * from articulosgenericos where idfacturaarticulos=" + idfacturaFA;
                        DataSet data1 = conexion.getData(sql1, "ARTICULOSGENERICOS");

                        DataTable dtTable1 = data1.Tables["ARTICULOSGENERICOS"];
                        String cantidadAG, precioAG, nombreAG, idAG, precioTotalAG;
                        double res;
                        foreach (DataRow row in dtTable1.Rows)
                        {

                            idAG = Convert.ToString(row["idarticulogenerico"]);
                            cantidadAG = Convert.ToString(row["cantidad"]);
                            nombreAG = Convert.ToString(row["descripcion"]);
                            precioAG = Convert.ToString(row["precio"]);
                            res = Math.Round(Convert.ToSingle(precioAG), 2);
                            precioAG = Convert.ToString(res);
                            precioTotalAG = Convert.ToString(Math.Round((res * Convert.ToInt32(cantidadAG) * 1.21), 2));

                            pa.Rows.Add(idAG, nombreAG, cantidadAG, precioAG, precioTotalAG);
                        }
                        
                    }
                    miReporte.Database.Tables["PedidosArticulos"].SetDataSource(pa);
                    crystalReportViewer1.ReportSource = miReporte;
                }

                
        }

        private void ImprimirFactura_Load(object sender, EventArgs e)
        {

        }
    }
}
