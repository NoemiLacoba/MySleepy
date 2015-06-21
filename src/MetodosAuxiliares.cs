using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySleepy
{
    class MetodosAuxiliares
    {
        /// <summary>
        /// Metodo que controla un textBox para que contenga cifras de cimales de tamaño especificado
        /// </summary>
        /// <param name="caja">textBox a controlar</param>
        /// <param name="e">evento de tecla pulsada</param>
        /// <param name="enteros">cantidad de números enteros que contendrá</param>
        /// <param name="decimales">cantidad de números decimales que contendrá</param>
        public static void cajaDecimal(TextBox caja, KeyPressEventArgs e, int enteros, int decimales)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            Boolean IsDec = false;
            int nroDec = 0;
            int cuentapuntos = 0;
            if (caja.Text.Length > (enteros + decimales))
            {
                e.Handled = true;
                return;
            }
            for (int i = 0; i < caja.Text.Length; i++)
            {
                if (caja.Text[i] == '.' || caja.Text[i] == ',')
                {
                    IsDec = true;
                    cuentapuntos++;
                }
                if (IsDec && nroDec++ >= decimales)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == '-')
                e.Handled = false;
            else if (e.KeyChar == 46 || e.KeyChar == 44)
            {
                if (caja.Text.Length < 1)
                {
                    e.Handled = true;
                    return;
                }
                if (cuentapuntos >= 1)
                {
                    e.Handled = true;
                }
                else
                {
                    e.KeyChar = ',';
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Metodo que comprueba que el numero introducido en el textBox 
        /// no finaliza en '.' o ',' para que pueda ser introducido en la BBDD
        /// </summary>
        /// <param name="caja">Textbox a controlar</param>
        /// <returns>devuelve un boolean segun si esta bien estructurado o no</returns>
        public static Boolean cajaDecimalCorrecta(TextBox caja)
        {
            String numero = caja.Text;
            if (!numero.Equals(""))
            {
                if (numero.ElementAt(numero.Length - 1).Equals('.') || numero.ElementAt(numero.Length - 1).Equals(','))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Metodo que indica si el string pasado corresponde con un numero decimal
        /// </summary>
        /// <param name="numero">String a comparar</param>
        /// <returns></returns>
        public static Boolean DecimalCorrecto(String numero, int entero, int decimales)
        {
            Boolean resultado = true;
            try
            {
                double num = Convert.ToSingle(numero);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error cajaDecimalCorrecta(no es un numero decimal): " + ex.Message);
                return false;
            }
            if (!numero.Equals(""))
            {
                if (numero.Length <= (entero + decimales) + 1)
                {
                    if (numero.ElementAt(numero.Length - 1).Equals('.') || numero.ElementAt(numero.Length - 1).Equals(','))
                    {
                        resultado = false;
                    }
                    else
                    {
                        int cuentapuntos = 0;
                        Boolean IsDec = false;
                        int nroDec = 0;
                        for (int i = 0; i < numero.Length; i++)
                        {
                            if (numero.ElementAt(i) == '.' || numero.ElementAt(i) == ',')
                            {
                                IsDec = true;
                                cuentapuntos++;
                            }
                            if (cuentapuntos >= 2)
                            {
                                resultado = false;
                                break;
                            }
                            if (IsDec && nroDec++ > decimales)
                            {
                                resultado = false;
                                break; ;
                            }
                        }
                    }
                }
                else
                {
                    resultado = false;
                }
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que tranforma el decimal que se le pasa al formato correspondiente para la base de datos
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static String transformaDecimalADecimalBBDD(String numero)
        {
            String aux = "";
            foreach (char e in numero)
            {
                if (e == 46 || e == 44)
                {
                    aux += ",";
                }
                else
                {
                    aux += e;
                }
            }
            numero = aux;
            return numero;
        }
        /// <summary>
        /// Metodo que comprueba que la cadena pasada no contiene ningun elemento que 
        /// pueda producir un error en la base de datos
        /// </summary>
        /// <param name="cadena">cadena que requiere ser comprobada</param>
        /// <returns></returns>
        public static Boolean stringValidoBBDD(String cadena)
        {
            foreach (char e in cadena)
            {
                if (char.IsSymbol(e) || e.Equals('\'')){
                    return false;
                }
            }
            return true;
        }
        public static int ultimoID(ConnectDB conexion,String idTabla,String tabla)
        {
            
            int contador = 0;
            if (Convert.ToInt32(conexion.DLookUp("count(*)", tabla, "")) == 0)
                contador = 1;
            else
                contador = Convert.ToInt32(conexion.DLookUp("max("+idTabla+")", tabla, "")) + 1;
            //MessageBox.Show(contador.ToString());
            return contador;
        }
        public static int ultimoNumeroFactura(ConnectDB conexion, String idTabla, String tabla,String condicion)
        {

            int contador = 0;
            if (Convert.ToInt32(conexion.DLookUp("count(*)", tabla, condicion)) == 0)
                contador = 1;
            else
                contador = Convert.ToInt32(conexion.DLookUp("max(" + idTabla + ")", tabla, condicion)) + 1;
            //MessageBox.Show(contador.ToString());
            return contador;
        }

        //Método que devuelve en formato cadena la hora
        public static String devolverHora()
        {
            DateTime fechaActual = DateTime.Now;
            String minutosS = "", horaS = "", segundosS = "";

            int hora = fechaActual.Hour;
            if (hora < 10)
            {
                horaS = 0 + "" + hora;
            }
            else
            {
                horaS = Convert.ToString(hora);
            }
            int minutos = fechaActual.Minute;
            if (minutos < 10)
            {
                minutosS = 0 + "" + minutos;
            }
            else
            {
                minutosS = Convert.ToString(minutos);
            }
            int segundos = fechaActual.Second;
            if (segundos < 10)
            {
                segundosS = 0 + "" + segundos;
            }
            else
            {
                segundosS = Convert.ToString(segundos);
            }
            String HORA_FECHA = horaS + "" + minutosS + "" + segundosS;
            return HORA_FECHA;
        }

        //Método que devuelve en formato cadena la fecha actual
        public static  String devolverFechaActual()
        {
            DateTime fechaActual = DateTime.Now;

            int año = fechaActual.Year;
            int mes = fechaActual.Month;
            String mesS = "", diaS = "";
            if (mes < 10)
            {
                mesS = 0 + "" + mes;
            }
            else
            {
                mesS = Convert.ToString(mes);
            }
            int dia = fechaActual.Day;
            if (dia < 10)
            {
                diaS = 0 + "" + dia;
            }
            else
            {
                diaS = Convert.ToString(dia);
            }
            String FECHA = año + "" + mesS + "" + diaS;
            return FECHA;
        }

        //Método que devuelve la fecha en formato cadena, pasando un entero
        public static String pasarFecha(int fechaNumerica)
        {
            String fecha = "", año = "", mes = "", dia = "";
            String numero = Convert.ToString(fechaNumerica);
            char[] caracter = numero.ToCharArray();
            for (int i = 0; i < 4; i++)
            {
                año = año + caracter[i];
            }
            for (int i = 4; i < 6; i++)
            {
                mes = mes + caracter[i];
            }
            for (int i = 6; i < 8; i++)
            {
                dia = dia + caracter[i];
            }
            fecha = dia + "/" + mes + "/" + año;
            return fecha;
        }


        public static String devolverFecha(DateTimePicker dpfecha)
        {
            DateTime fecha = dpfecha.Value;

            int año = fecha.Year;
            int mes = fecha.Month;
            String mesS = "", diaS = "";
            if (mes < 10)
            {
                mesS = 0 + "" + mes;
            }
            else
            {
                mesS = Convert.ToString(mes);
            }
            int dia = fecha.Day;
            if (dia < 10)
            {
                diaS = 0 + "" + dia;
            }
            else
            {
                diaS = Convert.ToString(dia);
            }
            String FECHA = año + "" + mesS + "" + diaS;
            return FECHA;
        }

        public static Boolean validarPass(String pass,ConnectDB conexion,int idUsuario)
        {


            String passSql = Convert.ToString(conexion.DLookUp("password", "usuarios", " idusuario = " + idUsuario));

            if (pass != "")
            {
                if (BCrypt.Net.BCrypt.Verify(pass, passSql))
                {
                    return true;
                }
            }

            return false;
        }

        public static String pasarHora(int horaNumerica)
        {
            String horaCadena = "", hora = "", minutos = "", segundos = "";
            String numero;
            if (horaNumerica < 100000)
            {
                numero = Convert.ToString(horaNumerica);
                numero = "0" + numero;
            }
            else
            {
                numero = Convert.ToString(horaNumerica);
            }

            char[] caracter = numero.ToCharArray();
            if (numero.Length < 6) numero = "0" + numero;
            for (int i = 0; i < 2; i++)
            {
                hora = hora + caracter[i];
            }
            for (int i = 2; i < 4; i++)
            {
                minutos = minutos + caracter[i];
            }
            for (int i = 4; i < 6; i++)
            {
                segundos = segundos + caracter[i];
            }
            horaCadena = hora + ":" + minutos + ":" + segundos;
            return horaCadena;
        }
        //Metodo que comprueba si el string pasado se corresponde con la estructura de un email
        public static Boolean emailCorrecto(String email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Metodo que comprueba si el dni pasado es correcto
        /// <summary>
        /// Valida un NIF
        /// </summary>
        /// <param name="valor">NIF a validar</param>
        /// <returns>Resultado de la validacion</returns>
        public static Boolean VerificarNIF(String valor)
        {
            String aux = null;
            valor = valor.ToUpper();
            // ponemos la letra en mayúscula
            aux = valor.Substring(0, valor.Length - 1);
            // quitamos la letra del NIF
            if (aux.Length >= 7 && CadenaEsNumero(aux))
            {
                aux = CalculaNIF(aux); // calculamos la letra del NIF para comparar con la que tenemos
            }
            else
            {
                return false;
            }
            // comparamos las letras
            return (valor.Equals(aux));
        }

        /// <summary>
        /// Dado un DNI obtiene la letra que le corresponde al NIF
        /// </summary>
        /// <param name="strA">DNI</param>
        /// <returns>Letra del NIF</returns>
        public static String CalculaNIF(String strA)
        {
            const String cCADENA = "TRWAGMYFPDXBNJZSQVHLCKE";
            const String cNUMEROS = "0123456789";

            int a = 0;
            int b = 0;
            int c = 0;
            int NIF = 0;
            StringBuilder sb = new StringBuilder();

            strA = strA.Trim();
            if (strA.Length == 0) return "";

            // Dejar sólo los números
            for (int i = 0; i <= strA.Length - 1; i++)
                if (cNUMEROS.IndexOf(strA[i]) > -1) sb.Append(strA[i]);

            strA = sb.ToString();
            a = 0;
            NIF = Convert.ToInt32(strA);
            do
            {
                b = Convert.ToInt32((NIF / 24));
                c = NIF - (24 * b);
                a = a + c;
                NIF = b;
            } while (b != 0);

            b = Convert.ToInt32((a / 23));
            c = a - (23 * b);
            return strA.ToString() + cCADENA.Substring(c, 1);
        }
        /// <summary>
        /// Comprueba si una cadena introducida esta compuesta unicamente por numeros
        /// </summary>
        /// <param name="cadena">cadena a comprobar</param>
        /// <returns>True- si se compone solo de numeros. False-si no solo hay numeros o ninguno</returns>
        private static Boolean CadenaEsNumero(String cadena)
        {
            foreach (char c in cadena)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Funcion que valida un CIF
        /// </summary>
        /// <param name="Numero">El numero del CIF a validar</param>
        /// <returns>True si el CIF es correcto</returns>
        public static bool Valida_CIF(string Numero)
        {
            //Valida el cif actual
            string[] letrasCodigo = { "J", "A", "B", "C", "D", "E", "F", "G", "H", "I" };

            string LetraInicial = Numero[0].ToString();
            string DigitoControl = Numero[Numero.Length - 1].ToString();
            string n = Numero.ToString().Substring(1, Numero.Length - 2);
            int sumaPares = 0;
            int sumaImpares = 0;
            int sumaTotal = 0;
            int i = 0;
            bool retVal = false;

            // Recorrido por todos los dígitos del número
            // Recorrido por todos los dígitos del número
            for (i = 0; i < n.Length; i++)
            {
                int aux;
                Int32.TryParse(n[i].ToString(), out aux);

                if ((i + 1) % 2 == 0)
                {
                    // Si es una posición par, se suman los dígitos
                    sumaPares += aux;
                }
                else
                {
                    // Si es una posición impar, se multiplican los dígitos por 2
                    aux = aux * 2;

                    // se suman los dígitos de la suma
                    sumaImpares += SumaDigitos(aux);
                }
            }

            // Se suman los resultados de los números pares e impares
            sumaTotal += sumaPares + sumaImpares;

            // Se obtiene el dígito de las unidades
            Int32 unidades = sumaTotal % 10;

            // Si las unidades son distintas de 0, se restan de 10
            if (unidades != 0) unidades = 10 - unidades;

            switch (LetraInicial)
            {
                // Sólo números
                case "A":
                case "B":
                case "E":
                case "H":
                    retVal = DigitoControl == unidades.ToString();
                    break;

                // Sólo letras
                case "K":
                case "P":
                case "Q":
                case "S":
                    retVal = DigitoControl == letrasCodigo[unidades];
                    break;

                default:
                    retVal = (DigitoControl == unidades.ToString()) || (DigitoControl == letrasCodigo[unidades]);
                    break;
            }

            return retVal;

        }

        private static Int32 SumaDigitos(Int32 digitos)
        {
            string sNumero = digitos.ToString();
            Int32 suma = 0;

            for (Int32 i = 0; i < sNumero.Length; i++)
            {
                Int32 aux;
                Int32.TryParse(sNumero[i].ToString(), out aux);
                suma += aux;
            }
            return suma;
        }

        public static int devolverFechaInt(String fechaE){
       
            char[] caracter=fechaE.ToCharArray();
            
            String texto="";
            texto=caracter[6]+""+caracter[7]+""+caracter[8]+""+caracter[9]+""+
                    caracter[3]+""+caracter[4]+""+caracter[0]+""+caracter[1];
            int fecha=Convert.ToInt32(texto);
            return fecha;
        }

        public static int devolverHoraInt(String horaE)
        {

            char[] caracter = horaE.ToCharArray();

            String texto = "";
            texto = caracter[0] + "" + caracter[1] + "" + caracter[3] + "" + caracter[4] + "" +
                    caracter[6] + "" + caracter[7];
            int hora = Convert.ToInt32(texto);
            return hora;
        }

    }
}
