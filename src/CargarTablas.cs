using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySleepy
{
    class CargarTablas
    {
        //ATRIBUTOS
        public static DataTable dtTable;
        public static ConnectDB conexion = new ConnectDB();
        public static double IVA = 0.21; //1,21 para multiplicar directamente y obtener el importe correspondiente al iva
        //Cargar tabla de Entradas=Ingresos
        public static void cargarEntradas(String sql,DataGridView tabla)
        {
            borrarTabla(tabla);

            DataSet data;
            data = conexion.getData(sql, "OPERACIONES");
            dtTable = data.Tables["OPERACIONES"];
            int id, fecha, hora, idusuario, idprocedencia;
            string procedencia = "", tipo = "", concepto = "", importe = "", fechaS = "", horaS = "", usuarioS = "";

            foreach (DataRow row in dtTable.Rows)
            {

                id = Convert.ToInt32(row["IDOPERACION"]);
                idprocedencia = Convert.ToInt32(row["IDprocdest"]);
                procedencia = Convert.ToString(conexion.DLookUp("procedencia", "procedencias", "idprocedencia=" + idprocedencia));
                tipo = Convert.ToString(row["tipo"]);
                concepto = Convert.ToString(row["concepto"]);
                importe = Convert.ToString(row["importe"]);
                fecha = Convert.ToInt32(row["fecha"]);
                fechaS = MetodosAuxiliares.pasarFecha(fecha);
                hora = Convert.ToInt32(row["hora"]);
                horaS = MetodosAuxiliares.pasarHora(hora);
                idusuario = Convert.ToInt16(row["idusuario"]);
                usuarioS = Convert.ToString(conexion.DLookUp("nombre", "USUARIOS", "idusuario=" + idusuario));

                tabla.Rows.Add(id, procedencia, tipo, concepto, importe, fechaS, horaS, usuarioS);
                tabla.ClearSelection();
            } // Fin del bucle for each
        }

        public static void borrarTabla(DataGridView tabla)
        {
            // Limpiamos el datagridView
            while (tabla.RowCount > 0)
            {
                tabla.Rows.Remove(tabla.CurrentRow);
            }
        }


        //CARGAR TABLA VALIDACIÓN
        public static void cargarDGVValidacion(DataGridView tabla)
        {

            borrarTabla(tabla);


            String sql = "select * FROM VALIDACIONES ORDER BY IDVALIDACION desc";
            int id;
            int fechaE;
            int horaE;
            int usuarioE;

            String fechaS = "", horaS = "", usuarioS = "", saldoS = "";

            DataSet data;
            data = conexion.getData(sql, "VALIDACIONES");
            dtTable = data.Tables["VALIDACIONES"];


            foreach (DataRow row in dtTable.Rows)
            {
                id = Convert.ToInt32(row["IDVALIDACION"]);
                fechaE = Convert.ToInt32(row["fecha"]);
                fechaS = MetodosAuxiliares.pasarFecha(fechaE);

                horaE = Convert.ToInt32(row["hora"]);
                horaS = MetodosAuxiliares.pasarHora(horaE);

                saldoS = Convert.ToString(row["saldototal"]);
                usuarioE = Convert.ToInt16(row["usuario"]);
                usuarioS = Convert.ToString(conexion.DLookUp("nombre", "USUARIOS", "idusuario=" + usuarioE));

                tabla.Rows.Add(id, fechaS, horaS, saldoS, usuarioS);
            } // Fin del bucle for each
            tabla.ClearSelection();
        }

        //CARGAR TABLA DEUDAS
        public static void cargarTablaDeudas(String sql,DataGridView tabla)
        {
            borrarTabla(tabla);
            int idDeuda;
            double importepagado, importetotal;
            String concepto, fechaConvert, hora, usuario, tipo;
            int fecha = 0;
            sql = sql + " order by fecha desc,hora desc";
            DataSet data;
            data = conexion.getData(sql, "DEUDAS");

            dtTable = data.Tables["DEUDAS"];
            foreach (DataRow row in dtTable.Rows)
            {
                idDeuda = Convert.ToInt32(row["IDDEUDA"]);
                concepto = Convert.ToString(row["CONCEPTO"]);
                importetotal = Convert.ToSingle(row["IMPORTETOTAL"]);
                importetotal = Math.Round(importetotal, 2);
                importepagado = Convert.ToSingle(row["IMPORTEPAGADO"]);
                importepagado = Math.Round(importepagado, 2);
                fecha = Convert.ToInt32(row["FECHA"]);
                fechaConvert = MetodosAuxiliares.pasarFecha(fecha);
                hora = MetodosAuxiliares.pasarHora(Convert.ToInt32(row["HORA"]));
                usuario = averiguarUsuario(Convert.ToInt32(row["IDUSUARIO"]));
                tipo = Convert.ToString(row["TIPO"]);
                tabla.Rows.Add(idDeuda, fechaConvert, hora, usuario, tipo, importetotal - importepagado, concepto);
            } // Fin del bucle for each
            tabla.ClearSelection();
            tabla.Update();
        }

        public static String averiguarUsuario(int id)
        {
            return Convert.ToString(conexion.DLookUp("NOMBRE", "USUARIOS", "IDUSUARIO=" + id));
        }

        //CARGAR TABLA SALIDAS=GASTOS
        public static void cargarTablaSalida(String sql,DataGridView tabla)
        {
            borrarTabla(tabla);
            int idOperacion;
            double importe;
            String destino, tipo, concepto, fecha, hora, usuario;

            sql = sql + " order by fecha desc,hora desc";

            DataSet data;
            data = conexion.getData(sql, "OPERACIONES");

            dtTable = data.Tables["OPERACIONES"];


            foreach (DataRow row in dtTable.Rows)
            {
                idOperacion = Convert.ToInt32(row["IDOPERACION"]);
                destino = averiguarDestino(Convert.ToInt32(row["IDPROCDEST"]));
                tipo = Convert.ToString(row["TIPO"]);
                concepto = Convert.ToString(row["CONCEPTO"]);
                importe = Convert.ToSingle(row["IMPORTE"]);
                importe = Math.Round(importe, 2);
                fecha = FCientificaToFecha(Convert.ToInt32(row["FECHA"]));
                hora = MetodosAuxiliares.pasarHora(Convert.ToInt32(row["HORA"]));
                usuario = averiguarUsuario(Convert.ToInt32(row["IDUSUARIO"]));

                tabla.Rows.Add(idOperacion, destino, tipo, concepto, importe, fecha, hora, usuario);
            } // Fin del bucle for each
            tabla.ClearSelection();
            tabla.Update();
        }

        public static String FCientificaToFecha(int fechaCientifica)
        {
            String fecha = Convert.ToString(fechaCientifica);
            String anno = "", mes = "", dia = "";
            int contaAnno = 4, contames = 6, x = 0;
            while (fecha.Length > x)
            {
                if (x < contaAnno)
                {
                    anno = anno + fecha.ElementAt(x);
                }
                else
                {
                    if (x < contames)
                    {
                        mes = mes + fecha.ElementAt(x);
                    }
                    else
                    {
                        dia = dia + fecha.ElementAt(x);
                    }
                }
                x++;
            }
            fecha = dia + "/" + mes + "/" + anno;
            return fecha;
        }

        public static String averiguarDestino(int id)
        {
            return Convert.ToString(conexion.DLookUp("DESTINO", "DESTINOS", "IDDESTINO=" + id));
        }


        //CARGAR TABLA PENDIENTES
        public static void cargarTablaPendientes(String sql,DataGridView tabla)
        {
            borrarTabla(tabla);
            int idPendiente, idagencia, idcliente;
            double importetotal, importepagado;
            String concepto, fechaConvert, hora, usuario, tipo, agencia, cliente;
            int fecha = 0;
            sql = sql + " order by fecha desc, hora desc";
            DataSet data;
           
            data = conexion.getData(sql, "PENDIENTES");
            
            dtTable = data.Tables["PENDIENTES"];
            foreach (DataRow row in dtTable.Rows)
            {
                idPendiente = Convert.ToInt32(row["IDPENDIENTE"]);
                idagencia = Convert.ToInt32(row["IDAGENCIA"]);
                agencia = Convert.ToString(conexion.DLookUp("NOMBRE", "AGENCIAS", "idagencia=" + idagencia));
                idcliente = Convert.ToInt32(row["IDCLIENTE"]);
                cliente = Convert.ToString(conexion.DLookUp("NOMBRE", "CLIENTES", "idcliente=" + idcliente));
                concepto = Convert.ToString(row["CONCEPTO"]);
                importetotal = Convert.ToSingle(row["IMPORTETOTAL"]);
                importetotal = Math.Round(importetotal, 2);
                importepagado = Convert.ToSingle(row["IMPORTEPAGADO"]);
                importepagado = Math.Round(importepagado, 2);
                fecha = Convert.ToInt32(row["FECHA"]);
                fechaConvert = MetodosAuxiliares.pasarFecha(fecha);
                hora = MetodosAuxiliares.pasarHora(Convert.ToInt32(row["HORA"]));
                usuario = averiguarUsuario(Convert.ToInt32(row["IDUSUARIO"]));
                tipo = Convert.ToString(row["tipo"]);
                
                tabla.Rows.Add(idPendiente, cliente, agencia, fechaConvert, hora, usuario, (importetotal - importepagado), tipo, concepto);
            } // Fin del bucle for each
            tabla.ClearSelection();
            tabla.Update();
        }


        public static void cargarTablaFacturas(String sql,DataGridView tabla)
        {

            borrarTabla(tabla);
            int idFactura, idCliente, idPedido,numFactura;
            double importeneto,importeTotal; //importe total -> con iva -> 21%
            String cliente, fechaConvert, hora,contabilizada;
            sql = sql + " order by fecha desc,hora desc";
            DataSet data;
            data = conexion.getData(sql, "FACTURAS");
            int i = 0;
            dtTable = data.Tables["FACTURAS"];
            foreach (DataRow row in dtTable.Rows)
            {
                idFactura = Convert.ToInt32(row["IDFACTURA"]);
                idCliente = Convert.ToInt32(row["IDCLIENTE"]);

                idPedido = Convert.ToInt32(row["IDPEDIDO"]);
                numFactura = Convert.ToInt32(row["NUMFACTURA"]);
                cliente = Convert.ToString(conexion.DLookUp("NOMBRE||','||APELLIDO1","CLIENTES","idcliente = "+idCliente));
                cliente = cliente + " "+Convert.ToString(conexion.DLookUp("APELLIDO2", "CLIENTES", "idcliente = " + idCliente));
                importeneto = Convert.ToSingle(row["IMPORTENETO"]);
                importeneto = Math.Round(importeneto, 2);
                importeTotal = Math.Round(importeneto * (1 + IVA), 2); //importeneto*1.21
                fechaConvert = MetodosAuxiliares.pasarFecha(Convert.ToInt32(row["FECHA"]));
                hora = MetodosAuxiliares.pasarHora(Convert.ToInt32(row["HORA"]));
                contabilizada = Convert.ToString(row["contabilizada"]);
                tabla.Rows.Add(idFactura,idCliente,idPedido,numFactura, fechaConvert+" // "+ hora, cliente, importeneto, importeTotal);
                if (contabilizada.Equals("S"))
                {
                    tabla.Rows[i].Cells[3].Style.BackColor = Color.LightGreen;
                }
                i++;
            } // Fin del bucle for each
            DataSet data1;
            data1 = conexion.getData("select * from facturasarticulos where eliminada=0 order by fecha,hora desc", "FACTURASARTICULOS");
            
            DataTable dtTable1 = data1.Tables["FACTURASARTICULOS"];
            foreach (DataRow row in dtTable1.Rows)
            {
                
                idFactura = Convert.ToInt32(row["IDFACTURAARTICULOS"]);
                idCliente = Convert.ToInt32(row["IDCLIENTE"]);
                numFactura = Convert.ToInt32(row["NUMFACTURA"]);
                cliente = Convert.ToString(conexion.DLookUp("NOMBRE||','||APELLIDO1", "CLIENTES", "idcliente = " + idCliente));
                cliente = cliente + " " + Convert.ToString(conexion.DLookUp("APELLIDO2", "CLIENTES", "idcliente = " + idCliente));
                importeneto = Convert.ToSingle(row["IMPORTENETO"]);
                importeneto = Math.Round(importeneto, 2);
                importeTotal = Math.Round(importeneto * (1 + IVA), 2); //importeneto*1.21
                fechaConvert = MetodosAuxiliares.pasarFecha(Convert.ToInt32(row["FECHA"]));
                hora = MetodosAuxiliares.pasarHora(Convert.ToInt32(row["HORA"]));
                contabilizada = Convert.ToString(row["contabilizada"]);
                tabla.Rows.Add(idFactura, idCliente, "0", numFactura, fechaConvert + " // " + hora, cliente, importeneto, importeTotal);
                if (contabilizada.Equals("S"))
                {
                    tabla.Rows[i].Cells[3].Style.BackColor = Color.LightGreen;
                }
                i++;
            }
            
            tabla.Sort(tabla.Columns["NUMFACTURA"], System.ComponentModel.ListSortDirection.Descending);
            tabla.ClearSelection();
            tabla.Update();
        }

        public static void cargarTablaStocks(String sql, DataGridView tabla)
        {
            borrarTabla(tabla);
            int stockR, stockD, stockI, pedir, idarticulo;
            double existencias;
            String nombre;
            sql = sql + " order by idstock";
            DataSet data;
            int i=0;
            data = conexion.getData(sql, "ARTICULOSSTOCK");
            dtTable = data.Tables["ARTICULOSSTOCK"];
            foreach (DataRow row in dtTable.Rows)
            {
                stockR = Convert.ToInt32(row["STOCKREAL"]);
                stockI = Convert.ToInt32(row["STOCKIDEAL"]);
                idarticulo = Convert.ToInt32(row["IDARTICULO"]);
                nombre =Convert.ToString( conexion.DLookUp("NOMBRE", "ARTICULOS", "IDARTICULO=" + idarticulo));

                //Estos valores se utilizaran si existe el articulo en pedidos
                stockD = Convert.ToInt32(conexion.DLookUp("STOCKDISPONIBLE", "STOCKDISPONIBLE", "IDARTICULO=" + idarticulo));
                pedir = Convert.ToInt32(conexion.DLookUp("PEDIR", "PEDIR", "IDARTICULO=" + idarticulo));
                existencias = Convert.ToDouble(conexion.DLookUp("EXISTENCIAS", "PORCENTAJE", "IDARTICULO=" + idarticulo));
               
                String pedirT = "";

                int suma = Convert.ToInt32(conexion.DLookUp("SUMA", "SUMAPEDIDOS", "REFARTICULO=" + idarticulo));
                //Esto se hará en caso de que no exista el articulo en pedidos articulos, por lo que las vistas no valdrian
                if (suma == -1)
                {
                    stockD = stockR;
                    pedir = stockI - stockD;
                    existencias = (stockD * 100) / stockI;
                }
                existencias = Math.Round(existencias, 2);
                if (pedir <= 0) pedirT = "No";
                else pedirT = "Si";

                tabla.Rows.Add(idarticulo, nombre, existencias, pedirT, stockD, stockI, stockR);
                if (pedirT.Equals("No"))
                {
                    tabla.Rows[i].Cells[3].Style.BackColor = Color.LightGreen;
                }
                else
                {
                    tabla.Rows[i].Cells[3].Style.BackColor = Color.LightCoral;
                }
                i++;
            }

            tabla.Sort(tabla.Columns["existencias"], System.ComponentModel.ListSortDirection.Ascending);
            tabla.ClearSelection();
            tabla.Update();

        }

    }
}
