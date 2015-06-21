using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Data;

namespace MySleepy
{
    class XML_proveedor
    {
        //-----------------------------------------------------
        /// <summary>
        /// Atributo que representa al tipo int
        /// </summary>
        public const int INT = 1;
        /// <summary>
        /// Atributo que representa al tipo string
        /// </summary>
        public const int STRING = 2;
        /// <summary>
        /// Atributo que representa al tipo boolean
        /// </summary>
        public const int BOOLEAN = 3;
        /// <summary>
        /// Atributo que representa al tipo double
        /// </summary>
        public const int DOUBLE = 4;
        /// <summary>
        /// Atributo que representa al tipo char
        /// </summary>
        public const int CHAR = 5;
        /// <summary>
        /// Atributo que representa al tipo float
        /// </summary>
        public const int FLOAT = 6;
        //-------------------------------------------------

        /// <summary>
        /// Metodo que transforma un dataset en un array bidimensional
        /// en el que cada fila es un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static String[,] convertirDsArrayBi(DataSet ds)
        {
            String[,] resultado;
            resultado = new String[ds.Tables[0].Rows.Count, ds.Tables[0].Columns.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Object[] row = ds.Tables[0].Rows[i].ItemArray;
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    resultado[i, j] = Convert.ToString(row[j]);
                }
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que lee el archivo indicado y devuelve un
        /// array de String con los valores de los campos
        /// </summary>
        /// <param name="direccionXml">ruta del archivo a leer</param>
        /// <param name="tabla">tabla a leer de la BBDD</param>
        /// <param name="campos">cantidad de campos que contiene la tabla a leer</param>
        /// <returns>un array de String con todos los nodos</returns>
        public static String[,] Leerxml(String direccionXml, String tabla, int campos)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i;
            String[,] resultado;
            FileStream fs = new FileStream(direccionXml, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName(tabla);
            resultado = new String[xmlnode.Count, campos];
            for (i = 0; i < xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                for (int j = 0; j < campos; j++)
                {
                    resultado[i, j] = xmlnode[i].ChildNodes.Item(j).InnerText.Trim();
                }
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que escribira en el archivo XML especificado
        /// </summary>
        /// <param name="rutaXml">ruta en la que se encuentra o se creara el archivo</param>
        /// <param name="nombreElemento">Nombre usado para los nodos</param>
        /// <param name="nombreColumnas">Nombres usados para los nodos hijos</param>
        /// <param name="valorCampos">Valores a almacenar en cada nodo hijo</param>
        public static void escribirXml(String rutaXml, String nombreElemento, String[] nombreColumnas, String[,] valorCampos)
        {
            XmlTextWriter writer = new XmlTextWriter(rutaXml, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Table");
            for (int i = 0; i < 2; i++)
            {
                String[] registro = new String[nombreColumnas.Length];
                for (int j = 0; j < registro.Length; j++)
                {
                    registro[j] = valorCampos[i, j];
                }
                crearNodo(writer, nombreElemento, nombreColumnas, registro);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
        /// <summary>
        /// Metodo que genera los nodos para almacenar los 
        /// datos en el archivo xml
        /// </summary>
        /// <param name="writer">Clase usada para escribir en el archivo XML</param>
        /// <param name="nombreElemento">Nombre usado para los nodos</param>
        /// <param name="nombreColumnas">Nombres para los nodos hijo (ChildNode)</param>
        /// <param name="valorCampos">Valores a almacenar en cada nodo hijo</param>
        private static void crearNodo(XmlTextWriter writer, String nombreElemento, String[] nombreColumnas, String[] valorCampos)
        {
            writer.WriteStartElement(nombreElemento);
            for (int i = 0; i < nombreColumnas.Length; i++)
            {
                writer.WriteStartElement(nombreColumnas[i]);
                writer.WriteString(valorCampos[i]);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        /// <summary>
        /// Metodo que devuelve un nodo creado con los valores pasados
        /// </summary>
        /// <param name="documento">XmlDocument que se este usando</param>
        /// <param name="nombreElemento">nombre que recibira el elemento(Normalmente se llama igual que la tabla de la que procede)</param>
        /// <param name="nombreColumnas">nombre de las columnas/hijos de los que se compondra el elemento principal</param>
        /// <param name="valorCampos">valor que contendran las columnas/hijos del elemento padre</param>
        /// <returns></returns>
        private static XmlNode crearNodo(XmlDocument documento, String nombreElemento, String[] nombreColumnas, String[] valorCampos)
        {
            XmlElement elemento = documento.CreateElement(nombreElemento);
            int j = 0, valorLength = valorCampos.Length;
            foreach (String i in nombreColumnas)
            {
                if (j < valorLength)
                {
                    XmlElement columna = documento.CreateElement(i);
                    columna.InnerText = valorCampos[j];
                    elemento.AppendChild(columna);
                    j++;
                }
            }
            return elemento;
        }
        /// <summary>
        /// Metodo que genera un XMLDataSet 
        /// </summary>
        /// <param name="nombreDataSet">nombre que se le asignara al dataset</param>
        /// <param name="rutaXml">ruta al archivo en el que se guardara el dataset</param>
        /// <param name="nombreColumnas">array con el nombre de las columnas</param>
        /// <param name="tipoColumnas">array con los tipos que soportara cada columna</param>
        /// <param name="valoresColumnas">array con los valores que tendra cada columna en su respectiva fila</param>
        public static void crearXMLDataSet(String nombreDataSet, String rutaXml, String[] nombreColumnas, Int32[] tipoColumnas, Object[,] valoresColumnas)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //Genera las columnas del datatable
            for (int i = 0; i < nombreColumnas.Length; i++)
            {
                dt.Columns.Add(new DataColumn(nombreColumnas[i], Type.GetType(tipoDato(tipoColumnas[i]))));
            }
            //Genero un array con los valores de un registro y los añado a una fila y así sucesivamente
            int x = 0;
            Object[] row = new Object[nombreColumnas.Length];
            for (int j = 0; j < valoresColumnas.GetLength(0); j++)
            {

                if (x == row.Length)
                {
                    rellenaFilas(dt, nombreColumnas, row);
                    x = 0;
                }
                else
                {
                    row[x] = valoresColumnas[j, x];
                    j = j - 1;
                    x++;
                }

            }
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = nombreDataSet;
            ds.WriteXml(rutaXml);
            Console.WriteLine("XML creado");
        }
        /// <summary>
        /// Metodo que escribe en el fichero Xml un dataSet en base a un dataTable pasado
        /// </summary>
        /// <param name="nombreDataSet">Nombre que se le asignara al dataSet</param>
        /// <param name="rutaXml">Ruta en la que se encuentra el archivo</param>
        /// <param name="dt">dataTable a pasar al xml</param>
        public static void crearXmlDataSet(String nombreDataSet, String rutaXml, DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = nombreDataSet;
            ds.WriteXml(rutaXml);
        }
        /// <summary>
        /// Metodo que rellena filas para un dataTable
        /// </summary>
        /// <param name="dt">dataTable al cual se le añade la fila</param>
        /// <param name="nombreColumnas">array con los nombres de las columnas</param>
        /// <param name="valoresColumnas">array con los valores de cada columna</param>
        public static void rellenaFilas(DataTable dt, String[] nombreColumnas, Object[] valoresColumnas)
        {
            DataRow dr = dt.NewRow();
            for (int i = 0; i < nombreColumnas.Length; i++)
            {
                dr[nombreColumnas[i]] = valoresColumnas[i];
            }
            dt.Rows.Add(dr);
        }
        /// <summary>
        /// Metodo que devuelve el tipo de dato a usar
        /// </summary>
        /// <param name="tipo">numero que representa el tipo de dato que se quiere usar</param>
        /// <returns></returns>
        private static String tipoDato(int tipo)
        {
            String cadena = "";
            switch (tipo)
            {
                case 1:
                    cadena = "System.Int32";
                    break;
                case 2:
                    cadena = "System.String";
                    break;
                case 3:
                    cadena = "System.Boolean";
                    break;
                case 4:
                    cadena = "System.Double";
                    break;
                case 5:
                    cadena = "System.Char";
                    break;
                case 6:
                    cadena = "System.Single";
                    break;
            }
            return cadena;
        }
        /// <summary>
        /// Metodo que lee un XMLDataSet
        /// </summary>
        /// <param name="rutaXml">ruta del archivo XML</param>
        /// <returns>Devuelve los valores leidos</returns>
        public static String[,] leerXMLDataSet(String rutaXml, Boolean si)
        {
            XmlReader xmlFile = XmlReader.Create(rutaXml, new XmlReaderSettings());
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);
            String[,] resultado;
            try
            {
                resultado = new String[ds.Tables[0].Rows.Count, ds.Tables[0].Columns.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Object[] row = ds.Tables[0].Rows[i].ItemArray;
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        resultado[i, j] = Convert.ToString(row[j]);
                    }
                }
            }
            catch (XmlException ex)
            {
                resultado = null;
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que lee un XMLDataSet
        /// </summary>
        /// <param name="rutaXml">ruta del archivo XML</param>
        /// <returns>Devuelve el dataTable del xml</returns>
        public static DataSet leerXMLDataSet(String rutaXml)
        {
            XmlReader xmlFile = XmlReader.Create(rutaXml, new XmlReaderSettings());
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);
            return ds;
        }
        /// <summary>
        /// Metodo que busca un elemento en el archivo Xml
        /// </summary>
        /// <param name="pdv">dataView que se esta utilizando</param>
        /// <param name="rutaXml">ruta en la cual se encuentra el archivo</param>
        /// <param name="columna">columna que se buscara(Elemento que almacena el valor a buscar)</param>
        /// <param name="valor">valor que se busca</param>
        /// <returns>-1 si no se encuentra el elemento, sino se devuelve la posicion de este</returns>
        private static int buscarElemento(DataView pdv, String valor)
        {
            DataView dv = pdv;
            int index = dv.Find(valor);
            return index;
        }
        /// <summary>
        /// Metodo que cogera los datos del elemento que se desea buscar
        /// </summary>
        /// <param name="rutaXml">ruta del archivo xml</param>
        /// <param name="nombreColumnas">nombre de las columnas/elementos</param>
        /// <param name="columna">nombre de la columna/elemento a buscar</param>
        /// <param name="valor">valor que se buscara, es el que contiene la columna/elemento</param>
        /// <returns></returns>
        public static String[] cogerDatos(String rutaXml, String[] nombreColumnas, String columna, String valor)
        {
            XmlReader xmlFile = XmlReader.Create(rutaXml, new XmlReaderSettings());
            DataSet ds = new DataSet();
            DataView dv;
            ds.ReadXml(xmlFile);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = columna;
            int posicion = buscarElemento(dv, valor);
            if (posicion != -1)
            {
                String[] datos = new String[nombreColumnas.Length];
                int y = 0;
                foreach (String i in nombreColumnas)
                {
                    datos[y] = dv[posicion][i].ToString();
                }
                return datos;
            }
            return null;
        }
        /// <summary>
        /// Metodo que sirve para modificar los datos de un elemento 
        /// </summary>
        /// <param name="rutaXml">ruta del xml</param>
        /// <param name="nombreColumnas">nombre de las columnas/elementos que se modificaran</param>
        /// <param name="valorColumnas">valor que contendran las columnas/elementos</param>
        /// <param name="columna">nombre de la columna por la cual se busca el elemento a modificar</param>
        /// <param name="valor">valor que contiene la columna para la busqueda del elemento</param>
        public static void modificarDatos(String rutaXml, String[] nombreColumnas, String[] valorColumnas, String columna, String valor)
        {
            XmlReader xmlFile = XmlReader.Create(rutaXml, new XmlReaderSettings());
            DataSet ds = new DataSet();
            DataView dv;
            ds.ReadXml(xmlFile);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = columna;
            int posicion = buscarElemento(dv, valor);
            if (posicion != -1)
            {
                String[] datos = new String[nombreColumnas.Length];
                int y = 0;
                foreach (String i in nombreColumnas)
                {
                    dv[posicion][i] = valorColumnas[y];
                }
            }
        }
        /// <summary>
        /// Metodo que insertara un nuevo nodo al archivo xml
        /// </summary>
        /// <param name="rutaxml">ruta del archivo xml</param>
        /// <param name="nombreElemento">nombre del elemento a crear(normalmente es el mismo de la tabla)</param>
        /// <param name="nombreColumnas">nombre de los hijos/columnas</param>
        /// <param name="valorCampos">valor que contienen las columnas/hijos</param>
        public void InsertarXml(String rutaxml, String nombreElemento, String[] nombreColumnas, String[] valorCampos)
        {
            //Cargamos el documento XML.
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(rutaxml);

            //Creamos el nodo que deseamos insertar.
            XmlNode empleado = crearNodo(xmldoc, nombreElemento, nombreColumnas, valorCampos);

            //Obtenemos el nodo raiz del documento.
            XmlNode nodoRaiz = xmldoc.DocumentElement;

            //Insertamos el nodo empleado al final del archivo
            nodoRaiz.InsertAfter(empleado, nodoRaiz.LastChild);

            xmldoc.Save(rutaxml);
        }
        /// <summary>
        /// Metodo que carga el fichero xml seleccionado con la tabla de la 
        /// BBDD escogida
        /// </summary>
        /// <param name="rutaXml">Ruta en la que se encuentra el fichero xml</param>
        /// <param name="tabla">Tabla de la BBDD que se desea cargar</param>
        /// <param name="conection">Conexion a la BBDD</param>
        public static void cargarBBDDXML(String rutaXml, String tabla, ConnectDB conection)
        {
            DataSet ds = conection.getData("SELECT * FROM " + tabla, tabla);
            ds.WriteXml(rutaXml);
        }
    }
}
