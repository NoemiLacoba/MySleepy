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
    public partial class Caja : Form
    {
        private static Caja instance;
        private static ConnectDB conexion;    
        private int idUsuario;
       
        private char primero = ' ';
        InsertHistorial insert;


        internal static Caja Instance(ConnectDB conexion, int idUsuario)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Caja(conexion, idUsuario);
            }
            return instance;
        }

        public Caja(ConnectDB conex,int idUsuario)
        {
            InitializeComponent();
            conexion = conex;
            this.idUsuario = idUsuario;
            insert = new InsertHistorial(conexion);
            ToolTip caja = new ToolTip();
            caja.SetToolTip(botonAñadir, "Insertar Apunte");
            caja.SetToolTip(botonAnular, "Anular Apunte");
            caja.SetToolTip(btnLiquidar, "Liquidar apunte");
            caja.SetToolTip(botonValidar, "Validación de caja");
            caja.SetToolTip(botonLimpiar, "Limpiar Campos");
            caja.SetToolTip(botonSalir, "Salir");
            
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pestañas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Seleccionado entradas
            //Entradas (saldra seleccionada por defecto al abrir el form)
            if (pestañas.SelectedIndex == 0)
            {
                botonLimpiar.Visible = true;
                botonAñadir.Visible = true;
                botonAnular.Visible = true;
                botonValidar.Visible = false;
                btnLiquidar.Visible = false;
                
                ToolTip tTEntradas = new ToolTip();
                tTEntradas.SetToolTip(botonAñadir, "Añadir Apunte");
                tTEntradas.SetToolTip(botonAnular, "Anular Apunte");
                tTEntradas.SetToolTip(botonLimpiar, "Limpiar Campos");
                tTEntradas.SetToolTip(botonSalir, "Salir");
                cargarComboProcedencia();
                cargarComboTipo();
                procdest.Text = "Procedencia";
                String sql = "Select * from operaciones where tipooperacion like 'E'order by fecha desc, hora desc";
                CargarTablas.cargarEntradas(sql,dgvEntradas);
            }
            //Seleccionado salidas
            if (pestañas.SelectedIndex == 1)
            {
                
                botonLimpiar.Visible = true;
                botonAñadir.Visible = true;
                botonAnular.Visible = true;
                botonValidar.Visible = false;
                btnLiquidar.Visible = false;
                ToolTip tTEntradas = new ToolTip();
                tTEntradas.SetToolTip(botonAñadir, "Añadir Apunte");
                tTEntradas.SetToolTip(botonAnular, "Anular Apunte");
                tTEntradas.SetToolTip(botonLimpiar, "Limpiar Campos");
                tTEntradas.SetToolTip(botonSalir, "Salir");
                procdest.Text = "Destino";
                cargarComboDestino();
                String sql = "SELECT * FROM OPERACIONES WHERE TIPOOPERACION='S'";
                CargarTablas.cargarTablaSalida(sql,dgvSalidas);
            }
            //Seleccionado pendiente de pago
            if (pestañas.SelectedIndex == 2)
            {
                
                botonLimpiar.Visible = true;
                botonAñadir.Visible = true;
                botonAnular.Visible = false;
                botonValidar.Visible = false;
                btnLiquidar.Visible = true;
                cargarComboProcedenciaPend();
                String sql = "Select *  from pendientes where upper(liquidada) = 'N'";
                CargarTablas.cargarTablaPendientes(sql,dgvPendientes);
            }
            //Seleccionado deudas
            if (pestañas.SelectedIndex == 3)
            {
                
                botonLimpiar.Visible = true;
                botonAñadir.Visible = true;
                botonAnular.Visible = false;
                botonValidar.Visible = false;
                btnLiquidar.Visible = true;
                
                CargarTablas.cargarTablaDeudas("Select *  from deudas where upper(liquidada) = 'N'",dgvDeudas);
            }
            //Seleccionado validación
            if (pestañas.SelectedIndex == 4)
            {
                
                botonLimpiar.Visible = false;
                botonAñadir.Visible = false;
                botonAnular.Visible = false;
                botonValidar.Visible = true;
                btnLiquidar.Visible = false;
                CargarTablas.cargarDGVValidacion(dgvValidar);
                gbEntradasSalidas.Visible = false;
                gbDeudasPendientes.Visible = false;
                gbPendientes.Visible = false;
            }
            else
            {
                
                gbEntradasSalidas.Visible = true;
                gbDeudasPendientes.Visible = true;
                gbPendientes.Visible = true;
                if (pestañas.SelectedIndex == 0)
                {
                    gbEntradasSalidas.Visible = true;
                    gbDeudasPendientes.Visible = false;
                    gbPendientes.Visible = false;
                }
                if (pestañas.SelectedIndex == 1)
                {
                    gbEntradasSalidas.Visible = true;
                    gbDeudasPendientes.Visible = false;
                    gbPendientes.Visible = false;
                }
                if (pestañas.SelectedIndex == 2)
                {
                    gbEntradasSalidas.Visible = false;
                    gbDeudasPendientes.Visible = false;
                    gbPendientes.Visible = true;
                }
                if (pestañas.SelectedIndex == 3)
                {
                    //gbEntradasSalidas.Visible = false;
                    gbDeudasPendientes.Visible = true;
                    gbPendientes.Visible = false;
                }
                
            }

        }

        

        
        public void calcularTotales()
        {
            try
            {
                
                String recibo1, recibo2, efectivo1, efectivo2, cheque1, cheque2;
                //double recibo1, recibo2, efectivo1, efectivo2, cheque1, cheque2;
                recibo1 = Convert.ToString(conexion.DLookUp("sum(importe)", "operaciones", "tipo like 'Recibo' and tipooperacion like 'E'"));
                recibo2 = Convert.ToString(conexion.DLookUp("sum(importe)", "operaciones", "tipo like 'Recibo' and tipooperacion like 'S'"));
                efectivo1 = Convert.ToString(conexion.DLookUp("sum(importe)", "operaciones", "tipo like 'Efectivo' and tipooperacion like 'E'"));
                efectivo2 = Convert.ToString(conexion.DLookUp("sum(importe)", "operaciones", "tipo like 'Efectivo' and tipooperacion like 'S'"));
                cheque1 = Convert.ToString(conexion.DLookUp("sum(importe)", "operaciones", "tipo like 'Cheque' and tipooperacion like 'E'"));
                cheque2 = Convert.ToString(conexion.DLookUp("sum(importe)", "operaciones", "tipo like 'Cheque' and tipooperacion like 'S'"));

                if (recibo1.Equals("")) recibo1 = "0";
                if (recibo2.Equals("")) recibo2 = "0";
                if (!recibo1.Equals("") || !recibo2.Equals(""))
                    cajaRecibos.Text = Convert.ToString(Convert.ToSingle(recibo1) - Convert.ToSingle(recibo2));
                if (Convert.ToSingle(cajaRecibos.Text) >= 0)
                {
                    cajaRecibos.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    cajaRecibos.BackColor = System.Drawing.Color.LightCoral;
                }

                if (efectivo1.Equals("")) efectivo1 = "0";
                if (efectivo2.Equals("")) efectivo2 = "0";
                if (!efectivo1.Equals("") || !efectivo2.Equals(""))
                    cajaEfectivo.Text = Convert.ToString(Convert.ToSingle(efectivo1) - Convert.ToSingle(efectivo2));

                if (Convert.ToSingle(cajaEfectivo.Text) >= 0)
                {
                    cajaEfectivo.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    cajaEfectivo.BackColor = System.Drawing.Color.LightCoral;
                }

                if (cheque1.Equals("")) cheque1 = "0";
                if (cheque2.Equals("")) cheque2 = "0";
                if (!cheque1.Equals("") || !cheque2.Equals(""))
                    cajaCheques.Text = Convert.ToString(Convert.ToSingle(cheque1) - Convert.ToSingle(cheque2));

                if (Convert.ToSingle(cajaCheques.Text) >= 0)
                {
                    cajaCheques.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    cajaCheques.BackColor = System.Drawing.Color.LightCoral;
                }
                cajaSaldoTotal.Text = Convert.ToString(Convert.ToSingle(cajaEfectivo.Text) + Convert.ToSingle(cajaRecibos.Text) + Convert.ToSingle(cajaCheques.Text));
                
                if (Convert.ToSingle(cajaSaldoTotal.Text) <= 0)
                {
                    botonValidar.Enabled = false;
                }
                if (Convert.ToSingle(cajaSaldoTotal.Text) >= 0)
                {
                    cajaSaldoTotal.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    cajaSaldoTotal.BackColor = System.Drawing.Color.LightCoral;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        
        public void limpiarTabla(DataGridView dgvTabla)
        {
            // Limpiamos el datagridView
            while (dgvTabla.RowCount > 0)
            {
                dgvTabla.Rows.Remove(dgvTabla.CurrentRow);
            }
        }
        public void borrarTabla()
        {
            // Limpiamos el datagridView
            while (dgvEntradas.RowCount > 0)
            {
                dgvEntradas.Rows.Remove(dgvEntradas.CurrentRow);
            }
        }

        private String averiguarDestino(int id)
        {
            return Convert.ToString(conexion.DLookUp("DESTINO", "DESTINOS", "IDDESTINO=" + id));
        }
        
        private int averiguarIdDestino(String destino)
        {
            return Convert.ToInt32(conexion.DLookUp("IDDESTINO", "DESTINOS", "DESTINO like '" + destino + "'"));
        }
       
        private String averiguarUsuario(int id)
        {
            return Convert.ToString(conexion.DLookUp("NOMBRE", "USUARIOS", "IDUSUARIO=" + id));
        }


        private void cargarComboDestino()
        {
            borrarCombo(comboProcedencia);
            String destino = "-Seleccione destino-";
            comboProcedencia.Items.Add(destino);
            DataSet data = conexion.getData("SELECT DESTINO FROM DESTINOS ORDER BY IDDESTINO", "DESTINOS");
            DataTable tClientes = data.Tables["DESTINOS"];
            foreach (DataRow row in tClientes.Rows)
            {
                comboProcedencia.Items.Add(row["DESTINO"]);
            } // Fin del bucle for each
            comboProcedencia.SelectedIndex = 0;
        }

        private void cargarComboProcedencia()
        {
            borrarCombo(comboProcedencia);
            String[] procedencia = { "-Seleccione procedencia-", "Venta de productos", "Ingreso directo administrativo", "Variación de saldo", "Varios" };
            for (int i = 0; i < procedencia.Length; i++)
            {
                comboProcedencia.Items.Add(procedencia[i]);
            }
            comboProcedencia.SelectedIndex = 0;
        }

        private void cargarComboProcedenciaPend()
        {
            borrarCombo(cbProcedenciaPend);
            String agencia = "-Seleccione agencia-";
            cbProcedenciaPend.Items.Add(agencia);
            DataSet data = conexion.getData("SELECT upper(nombre) FROM agencias ORDER BY idagencia", "AGENCIAS");
            DataTable tAgencia = data.Tables["AGENCIAS"];
            foreach (DataRow row in tAgencia.Rows)
            {
                cbProcedenciaPend.Items.Add(row["upper(NOMBRE)"]);
            } // Fin del bucle for each
            cbProcedenciaPend.SelectedIndex = 0;

        }
        private void cargarComboTipo()
        {
            borrarCombo(comboTipo);
            String[] tipo = { "-Seleccione tipo de ingreso", "Efectivo", "Cheque", "Recibo" };
            for (int i = 0; i < tipo.Length; i++)
            {
                comboTipo.Items.Add(tipo[i]);
            }
            comboTipo.SelectedIndex = 0;
        }

        private void borrarCombo(ComboBox combo)
        {
            combo.Items.Clear();
        }

        
        private void Caja_Load(object sender, EventArgs e)
        {
            
                cargarComboProcedencia();
                cargarComboTipo();
                String sql = "Select * from operaciones where tipooperacion like 'E'order by fecha desc, hora desc";
                CargarTablas.cargarEntradas(sql,dgvEntradas);
                calcularTotales();
                
                botonValidar.Visible = false;
                btnLiquidar.Visible = false;
                if (pestañas.SelectedIndex == 0)
                {
                    gbEntradasSalidas.Visible = true;
                    gbDeudasPendientes.Visible = false;
                    gbPendientes.Visible = false;
                }
                
        }

        private void botonAñadir_Click(object sender, EventArgs e)
        {
            if (caja_contraseña.Text.Equals(""))
            {
                MessageBox.Show("Introduzca contraseña");
            }
            else
            {
                Boolean seguir = MetodosAuxiliares.validarPass(caja_contraseña.Text.Trim(), conexion, idUsuario);
                if (seguir == true)
                {
                    if (pestañas.SelectedIndex == 0)
                    {
                        caja_contraseña.Text = "";
                        AddEntradas entradas = new AddEntradas(conexion, idUsuario);
                        entradas.ShowDialog();
                        String sql = "Select * from operaciones where tipooperacion like 'E' order by fecha desc, hora desc";
                        CargarTablas.cargarEntradas(sql, dgvEntradas);
                        calcularTotales();
                    }
                    if (pestañas.SelectedIndex == 1)
                    {
                        caja_contraseña.Text = "";
                        AddSalidas salida = new AddSalidas(conexion, idUsuario);
                        salida.ShowDialog();
                        String sql = "SELECT * FROM OPERACIONES WHERE TIPOOPERACION='S'";
                        CargarTablas.cargarTablaSalida(sql, dgvSalidas);
                        calcularTotales();
                    }
                    if (pestañas.SelectedIndex == 2)
                    {
                        caja_contraseña.Text = "";
                        AddEntradas entradas = new AddEntradas(conexion, idUsuario);
                        entradas.ShowDialog();
                        String sql = "Select *  from pendientes where upper(liquidada) = 'N'";
                        CargarTablas.cargarTablaPendientes(sql, dgvPendientes);
                        calcularTotales();
                    }
                    if (pestañas.SelectedIndex == 3)
                    {
                        caja_contraseña.Text = "";
                        AddSalidas salida = new AddSalidas(conexion, idUsuario, 2);
                        salida.ShowDialog();
                        String sql = "Select *  from deudas where upper(liquidada) = 'N'";
                        CargarTablas.cargarTablaDeudas(sql, dgvDeudas);
                        calcularTotales();
                    }

                }
                else
                {
                    MessageBox.Show("Contraseña No Válida");
                    caja_contraseña.Text = "";
                }
            }
        }

        private void botonValidar_Click(object sender, EventArgs e)
        {
            if (caja_contraseña.Text.Equals(""))
            {
                MessageBox.Show("Introduzca contraseña");
            }
            else
            {
                Boolean seguir = MetodosAuxiliares.validarPass(caja_contraseña.Text.Trim(), conexion, idUsuario);
                if (seguir == true)
                {
                    caja_contraseña.Text = "";
                    DialogResult opcion = MessageBox.Show("¿Desea hacer la validación?", "Validación", MessageBoxButtons.YesNo);
                    if (opcion == DialogResult.Yes)
                    {
                        double saldototal = Convert.ToSingle(cajaSaldoTotal.Text);
                        DateTime fechaActual = DateTime.Now;

                        int año = fechaActual.Year;
                        int mes = fechaActual.Month;

                        String FECHA = MetodosAuxiliares.devolverFechaActual();
                        String HORA_FECHA = MetodosAuxiliares.devolverHora();
                        int idValidacion = MetodosAuxiliares.ultimoID(conexion, "idvalidacion", "validaciones");
                        String sql = "insert into validaciones values(" + idValidacion + "," + Convert.ToInt32(FECHA) + "," + Convert.ToInt32(HORA_FECHA) + ",'" + Math.Round(saldototal, 3) + "'," + idUsuario + ")";
                        //MessageBox.Show(sql);
                        conexion.setData(sql);
                        MessageBox.Show("Validación correcta");
                        CargarTablas.cargarDGVValidacion(dgvValidar);
                    }
                }else
                 {
                        MessageBox.Show("Contraseña No Válida");
                        caja_contraseña.Text = "";
               }
            }

        }


        private int devolverProcedencia(String procedencia)
        {
            int id = 0;
            id = Convert.ToInt32(conexion.DLookUp("idprocedencia", "procedencias", "procedencia like '" + procedencia + "'"));
            return id;
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 0)
            {
                limpiarEntradas();
            }
            if (pestañas.SelectedIndex == 1)
            {
                limpiarEntradas();
            }
            if (pestañas.SelectedIndex== 2)
            {
                limpiarPendientes();
            }
            if (pestañas.SelectedIndex == 3)
            {
                limpiarDeudas();
            }
        }

        private void limpiarDeudas()
        {
            txtImportePendiente.Text = "";
            
            dtFin.Value = DateTime.Today;
            dtInicio.Value = DateTime.Today;
            rbNoLiquidadas.Checked = true;
            rbLiquidadas.Checked = false;
            if (pestañas.SelectedIndex == 3)
            {
                String sql = "Select *  from deudas where upper(liquidada) = 'N'";
                CargarTablas.cargarTablaDeudas(sql,dgvDeudas);
            }
           
        }
        private void limpiarPendientes()
        {
            dtPendIni.Value = DateTime.Today;
            dtPendFin.Value = DateTime.Today;
            cbProcedenciaPend.SelectedIndex = 0;
            txtConceptoPend.Text = "";
            txtImpPend.Text = "";
            rbNoLiquidadaPend.Checked = true;
            rbLiquidadaPend.Checked = false;
            String sql = "Select *  from pendientes where upper(liquidada) = 'N'";
            CargarTablas.cargarTablaPendientes(sql,dgvPendientes);

        }
        private void limpiarEntradas()
        {
            comboProcedencia.SelectedIndex = 0;
            comboTipo.SelectedIndex = 0;
            cajaImporte.Text = "";
            dtFin.Value = DateTime.Today;
            dtInicio.Value = DateTime.Today;
            String sql = "Select * from operaciones where tipooperacion like 'E' order by fecha desc, hora desc";
            CargarTablas.cargarEntradas(sql,dgvEntradas);
        }

        private void botonAnular_Click(object sender, EventArgs e)
        {
            if (caja_contraseña.Text.Equals(""))
            {
                MessageBox.Show("Introduzca contraseña");
            }
            else
            {
                Boolean seguir = MetodosAuxiliares.validarPass(caja_contraseña.Text.Trim(), conexion, idUsuario);
                if (seguir == true)
                {
                    if (pestañas.SelectedIndex == 0)
                    {
                        anularApunteEntrada();
                        calcularTotales();
                        caja_contraseña.Text = "";
                    }
                    if (pestañas.SelectedIndex == 1)
                    {
                        anularApunteSalida();
                        calcularTotales();
                        caja_contraseña.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Contraseña No Válida");
                    caja_contraseña.Text = "";
                }

            }
        }

        private void anularApunteEntrada()
        {
            if (dgvEntradas.CurrentRow == null || dgvEntradas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila");
            }
            else
            {
                String importe = Convert.ToString(dgvEntradas.SelectedCells[4].Value);
                if (Convert.ToSingle(importe) <= 0)
                {
                    MessageBox.Show("Es un ingreso anulado, no se puede anular");
                }
                else
                {
                    String conceptoP = Convert.ToString(dgvEntradas.SelectedCells[3].Value);
                    if (conceptoP.Substring(0, 9).Equals("Pendiente"))
                    {
                        MessageBox.Show("No se puede anular ingresos que son pendientes de pago");
                    }
                    else
                    {
                        DialogResult opcion = MessageBox.Show("¿Realmente desea anular el apunte?", "Anulación", MessageBoxButtons.YesNo);
                        if (opcion == DialogResult.Yes)
                        {
                            int fila = dgvEntradas.CurrentRow.Index;
                            String procedencia = Convert.ToString(dgvEntradas.SelectedCells[1].Value);
                            int idproc = devolverProcedencia(procedencia);
                            String tipo = Convert.ToString(dgvEntradas.SelectedCells[2].Value);
                            String concepto = Convert.ToString(dgvEntradas.SelectedCells[3].Value);

                            double importeD = Convert.ToSingle(importe);
                            String fecha = MetodosAuxiliares.devolverFechaActual();
                            String hora = MetodosAuxiliares.devolverHora();
                            String usuario = Convert.ToString(dgvEntradas.SelectedCells[7].Value);
                            int idOperacion = MetodosAuxiliares.ultimoID(conexion, "idoperacion", "operaciones");
                            String sql = "insert into operaciones values(" + idOperacion + "," + idproc + ",'" + tipo + "','Anulado: " + concepto + "','" + (-importeD) + "'," + fecha + "," + hora + "," + idUsuario + ",'E')";
                            //MessageBox.Show(sql);
                            conexion.setData(sql);
                            insert.insertHistorialCambio(idUsuario, 7, "Entrada anulada " + concepto);
                            MessageBox.Show("Apunte anulado");
                            sql = "Select * from operaciones where tipooperacion like 'E' order by fecha desc, hora desc";
                            CargarTablas.cargarEntradas(sql,dgvEntradas);
                        }
                        botonAnular.Enabled = true;
                    }
                }
            }
        }
        /// <summary>
        /// Metodo que sirve para crear un apunte que anule otro seleccionado en el dgv
        /// </summary>
        private void anularApunteSalida()
        {
            if (dgvSalidas.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una fila");
            }
            else
            {
                String importe = Convert.ToString(dgvSalidas.SelectedCells[4].Value);

                if (Convert.ToSingle(importe) <= 0)
                {
                    MessageBox.Show("Es un gasto anulado, no se puede anular");
                }
                else
                {
                    int fila = dgvSalidas.CurrentRow.Index;
                    String destino = Convert.ToString(dgvSalidas.SelectedCells[1].Value);
                    String concepto = Convert.ToString(dgvSalidas.SelectedCells[3].Value);
                    //MessageBox.Show(destino+" "+deudaLiquidada+"   "+concepto);
                    if (destino.ToLower().Equals("compras de mercaderías"))
                    {
                        if (concepto.ToLower().StartsWith("deuda"))
                        {
                            MessageBox.Show("No se pueden anular deudas");
                        }
                    }
                    else
                    {
                        DialogResult opcion = MessageBox.Show("¿Realmente desea anular el apunte?", "Anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (opcion == DialogResult.Yes)
                        {


                            int iddestino = averiguarIdDestino(destino);
                            String tipo = Convert.ToString(dgvSalidas.SelectedCells[2].Value);
                            concepto = Convert.ToString(dgvSalidas.SelectedCells[3].Value);
                            int idOperacion = MetodosAuxiliares.ultimoID(conexion, "idoperacion", "operaciones");
                            double importeD = Convert.ToSingle(importe);
                            String fecha = MetodosAuxiliares.devolverFechaActual();
                            String hora = MetodosAuxiliares.devolverHora();
                            String usuario = Convert.ToString(dgvSalidas.SelectedCells[7].Value);
                            String sql1 = "insert into operaciones values(" +idOperacion + ",'" + iddestino + "','" + tipo + "','Anulado: " + concepto + "','" + (-importeD) + "'," + fecha + "," + hora + "," + idUsuario + ",'S')";

                            conexion.setData(sql1);

                            //anulamos la deuda en caso de que sea una compra de mercaderías
                            //anularDeuda(fila);

                            insert.insertHistorialCambio(idUsuario, 7, "Salida anulada " + concepto);
                            MessageBox.Show("Apunte anulado");
                            String sql = "SELECT * FROM OPERACIONES WHERE TIPOOPERACION='S' ";
                            CargarTablas.cargarTablaSalida(sql,dgvSalidas);
                        }
                    }
                }
            }
        }
        public int extraerIDTabla(DataGridView dgvtabla)
        {
            int id = 0;
            DataGridViewRow fila = dgvtabla.CurrentRow;
            id = Convert.ToInt32(fila.Cells[0].Value);
            return id;
        }

        private void filtrarPendientes()
        {
            String sql = "SELECT * FROM PENDIENTES ";
            if (rbLiquidadaPend.Checked == true)
            {
                sql = sql + " WHERE upper(LIQUIDADA) = '" + "S" + "' ";
            }
            if(rbNoLiquidadaPend.Checked==true)
            {
                sql = sql + " WHERE upper(LIQUIDADA) = '" + "N" + "' ";
            }
            String importependiente = txtImpPend.Text;
            if (txtImpPend.Text != "")
            {
                sql = sql + "and (importetotal-importepagado) >= " + Convert.ToSingle(txtImpPend.Text);
            }
            if (cbProcedenciaPend.SelectedIndex > 0)
            {
                sql = sql + "and IDAGENCIA= " + cbProcedenciaPend.SelectedIndex + " ";
            }

            if (txtConceptoPend.Text != "")
            {
                sql += " and upper(concepto) like '%" + txtConceptoPend.Text.ToUpper() + "%' ";
            }
            if (dtPendIni.Value <= dtPendFin.Value)
            {
                String mesMin = Convert.ToString(dtPendIni.Value.Month);
                String mesMax = Convert.ToString(dtPendFin.Value.Month);
                if (Convert.ToInt16(mesMin) < 10) mesMin = "0" + mesMin;
                if (Convert.ToInt16(mesMax) < 10) mesMax = "0" + mesMax;
                String diaMin = Convert.ToString(dtPendIni.Value.Day);
                String diaMax = Convert.ToString(dtPendFin.Value.Day);
                if (Convert.ToInt16(diaMin) < 10) diaMin = "0" + diaMin;
                if (Convert.ToInt16(diaMax) < 10) diaMax = "0" + diaMax;
                String fechaMin = dtPendIni.Value.Year + "" + mesMin + "" + diaMin;
                String fechaMax = dtPendFin.Value.Year + "" + mesMax + "" + diaMax;

                sql = sql + "and fecha between " + fechaMin + " and " + fechaMax;

            }
            //MessageBox.Show(sql);
            CargarTablas.cargarTablaPendientes(sql,dgvPendientes);

        }
        private void filtrarDeudas()
        {

            String sql = "SELECT * FROM DEUDAS ";
            if (rbLiquidadas.Checked == true)
            {
                sql = sql + " WHERE upper(LIQUIDADA) = '" + "S" + "' ";
            }
            else
            {
                sql = sql + " WHERE upper(LIQUIDADA) = '" + "N" + "' ";
            }
            String importePendiente = txtImportePendiente.Text;
            if (importePendiente.Equals("-"))
            {
                //sql = sql + " importe>=-0 and "; 
                primero = '-';
            }
            else
            {
                if (importePendiente.Length > 1 && primero != ' ')
                {
                    sql = sql + " AND (importetotal-importepagado)>='" + Convert.ToSingle(importePendiente) + "' ";
                }
                else
                {
                    if (!importePendiente.Equals(""))
                        sql = sql + " AND (importetotal-importepagado)>='" + Convert.ToSingle(importePendiente) + "' ";

                }
            }
            if (txtConcepto.Text != "")
            {
                sql += " and upper(concepto) like '%" + txtConcepto.Text.ToUpper() + "%' ";
            }
            if (dtInicio.Value <= dtFin.Value)
            {
                String mesMin = Convert.ToString(dtInicio.Value.Month);
                String mesMax = Convert.ToString(dtFin.Value.Month);
                if (Convert.ToInt16(mesMin) < 10) mesMin = "0" + mesMin;
                if (Convert.ToInt16(mesMax) < 10) mesMax = "0" + mesMax;
                String diaMin = Convert.ToString(dtInicio.Value.Day);
                String diaMax = Convert.ToString(dtFin.Value.Day);
                if (Convert.ToInt16(diaMin) < 10) diaMin = "0" + diaMin;
                if (Convert.ToInt16(diaMax) < 10) diaMax = "0" + diaMax;
                String fechaMin = dtInicio.Value.Year + "" + mesMin + "" + diaMin;
                String fechaMax = dtFin.Value.Year + "" + mesMax + "" + diaMax;

                sql = sql + "and fecha between " + fechaMin + " and " + fechaMax;

            }
            //MessageBox.Show(sql);
            CargarTablas.cargarTablaDeudas(sql,dgvDeudas);
        }
        public void liquidarDeuda()
        {
            if (dgvDeudas.CurrentRow == null || dgvDeudas.SelectedRows.Count == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna fila");
            }
            else
            {
                String concepto = "Deuda " + Convert.ToString(dgvDeudas.SelectedCells[6].Value);
                double importe = Convert.ToSingle(dgvDeudas.SelectedCells[5].Value);
                String tipo = Convert.ToString(dgvDeudas.SelectedCells[4].Value);
                if (concepto.ToLower().StartsWith("deuda anulada "))
                {
                    MessageBox.Show(this, "No se puede liquidar una deuda anulada", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult opcion = MessageBox.Show("¿Desea liquidar la deuda?"
               , "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (opcion == DialogResult.Yes)
                    {
                        int idDeuda = extraerIDTabla(dgvDeudas);
                        DialogResult opc = MessageBox.Show("¿Desea liquidar la deuda por el total del importe?"
                        , "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (opc == DialogResult.Yes)
                        {
                            //update -> ponemos la deuda a Liquidada = 'S' a la fecha en que se ha realizado la operacion
                            String update = "Update deudas set Liquidada = 'S' ,fecha = " + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + ",hora=" + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + ", importepagado = importetotal where iddeuda = " + idDeuda;
                            conexion.setData(update);

                            int idOperacion = MetodosAuxiliares.ultimoID(conexion, "IDOPERACION", "OPERACIONES");

                            //realizamos la salida de capital, compra de mercaderias -> idDestino  = 2
                            String sql = "insert into operaciones values(" + idOperacion
                            + ",'" + 2 + "','" + tipo + "','" + concepto + "','" + importe +
                            "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + idUsuario + ",'S')";
                            conexion.setData(sql);

                            MessageBox.Show("Apunte Insertado");

                            //calculamos totales
                            calcularTotales();

                            //insert en tabla historial cambios -> salida
                            insert.insertHistorialCambio(idUsuario, 1, "Salida añadida " + concepto);
                            //extraemos el concepto
                            concepto = Convert.ToString(conexion.DLookUp("CONCEPTO", "DEUDAS", " IDDEUDA = " + idDeuda));
                            //insert en tabla historial
                            insert.insertHistorialCambio(idUsuario, 5, "Deuda liquidada " + concepto);

                            rbNoLiquidadas.Checked = true;
                            CargarTablas.cargarTablaDeudas("Select *  from deudas where upper(liquidada) = 'N'",dgvDeudas);
                        }
                        else
                        {
                            //Abrimos form LiquidarDeuda
                            LiquidarDeuda liquidarForm = new LiquidarDeuda(conexion, idUsuario, idDeuda);
                            liquidarForm.ShowDialog();
                            CargarTablas.cargarTablaDeudas("Select *  from deudas where upper(liquidada) = 'N'",dgvDeudas);
                        }
                    }
                }
            }
        }
        public void liquidarPendiente()
        {
            if (dgvPendientes.CurrentRow == null || dgvPendientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna fila");
            }
            else
            {
                String concepto = "Pendiente " + Convert.ToString(dgvPendientes.SelectedCells[8].Value);
                //double importeT = Convert.ToSingle(dgvPendientes.SelectedCells[6].Value);
                //double importeP = Convert.ToSingle(dgvPendientes.SelectedCells[5].Value);
                //double importe = Math.Round(importeT - importeP, 2);
                double importe = Convert.ToSingle(dgvPendientes.SelectedCells[6].Value);
                importe = Math.Round(importe, 2);
                String tipo = Convert.ToString(dgvPendientes.SelectedCells[7].Value);
                if (concepto.ToLower().StartsWith("Pendiente anulado "))
                {
                    MessageBox.Show(this, "No se puede liquidar un pendiente anulada", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult opcion = MessageBox.Show("¿Desea liquidar el pendiente?"
               , "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (opcion == DialogResult.Yes)
                    {
                        int idPendiente = extraerIDTabla(dgvPendientes);
                        DialogResult opc = MessageBox.Show("¿Desea liquidar el pendiente por el total del importe?"
                        , "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (opc == DialogResult.Yes)
                        {
                            //ponemos el campo Liquidada = 'S' de la tabla pendientes a la fecha en que se ha realizado la operacion
                            String update = "Update pendientes set Liquidada = 'S' ,fecha = " + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + ",hora=" + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + ", importepagado = importetotal where idpendiente = " + idPendiente;
                            conexion.setData(update);

                            int idOperacion = MetodosAuxiliares.ultimoID(conexion, "IDOPERACION", "OPERACIONES");
                            //Hacemos el insert en la tabla operaciones de la entrada                           
                            String insertsql = "Insert into operaciones values(" + idOperacion + ",1,'" + tipo + "','" + concepto + "','" + importe + "'," + Convert.ToInt32(MetodosAuxiliares.devolverFechaActual()) + "," + Convert.ToInt32(MetodosAuxiliares.devolverHora()) + "," + idUsuario + ",'E')";                           
                            conexion.setData(insertsql);
                           
                            MessageBox.Show("Apunte Insertado");

                            //calculamos totales
                            calcularTotales();

                            //extraemos el concepto de la tabla pendientes
                            concepto = Convert.ToString(conexion.DLookUp("CONCEPTO", "PENDIENTES", " IDPENDIENTE = " + idPendiente));
                            //insert en tabla historial cambios -> salida
                            MessageBox.Show(concepto);
                            insert.insertHistorialCambio(idUsuario, 5, "Entrada añadida " + concepto);
                            
                            //insertamos el dato en tabla historial
                            insert.insertHistorialCambio(idUsuario, 5, "Pendiente liquidado " + concepto);

                            rbNoLiquidadas.Checked = true;
                            CargarTablas.cargarTablaPendientes("Select *  from pendientes where upper(liquidada) = 'N'",dgvPendientes);
                        }
                        else
                        {
                            //Abrimos el form LiquidarPendiente
                            LiquidarPendiente liquidarForm = new LiquidarPendiente(conexion, idUsuario, idPendiente);
                            liquidarForm.ShowDialog();
                            CargarTablas.cargarTablaPendientes("Select *  from pendientes where upper(liquidada) = 'N'",dgvPendientes);
                        }
                    }
                }
            }
        }
        private void btnLiquidar_Click(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 3)
            {
                //liquidamos deuda
                liquidarDeuda();
            }
            if(pestañas.SelectedIndex == 2){
                //liquidamos pendiente
                liquidarPendiente();
                
            }
        }

        

        
        private void txtConcepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar.Equals(' ') || Char.IsDigit(e.KeyChar)
                 || e.KeyChar.Equals(',') || e.KeyChar.Equals('.'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtConceptoPend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar.Equals(' ') || Char.IsDigit(e.KeyChar) 
                || e.KeyChar.Equals(',') ||e.KeyChar.Equals('.'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 3)
            {
                //filtramos deudas
                filtrarDeudas();
            }
        }

        private void txtConceptoPend_TextChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 2)
            {
                //filtramos pendientes
                filtrarPendientes();
            }
        }

        
       
        private void dtInicio_ValueChanged(object sender, EventArgs e)
        {
            String fechaminima = dtInicio.Value.ToString();
            String fechamaxima = dtFin.Value.ToString();
            int diaP = Convert.ToInt16(dtInicio.Value.Day.ToString());
            int mesP = Convert.ToInt16(dtInicio.Value.Month.ToString());
            int añoP = Convert.ToInt16(dtInicio.Value.Year.ToString());
            int diaE = Convert.ToInt16(dtFin.Value.Day.ToString());
            int mesE = Convert.ToInt16(dtFin.Value.Month.ToString());
            int añoE = Convert.ToInt16(dtFin.Value.Year.ToString());
            //MessageBox.Show(diaP+" "+diaE+" "+mesP+" "+mesE);
            if (diaP > diaE && mesP >= mesE && añoP >= añoE)
            {
                MessageBox.Show("La fecha máxima no debe ser menor que la minima");
                dtInicio.Value = DateTime.Now;
            }
            else
            {
                if (pestañas.SelectedIndex == 3)
                {
                    filtrarDeudas();
                }
            }
        }

        private void dtFin_ValueChanged(object sender, EventArgs e)
        {
            String fechaminima = dtInicio.Value.ToString();
            String fechamaxima = dtFin.Value.ToString();
            int diaP = Convert.ToInt16(dtInicio.Value.Day.ToString());
            int mesP = Convert.ToInt16(dtInicio.Value.Month.ToString());
            int añoP = Convert.ToInt16(dtInicio.Value.Year.ToString());
            int diaE = Convert.ToInt16(dtFin.Value.Day.ToString());
            int mesE = Convert.ToInt16(dtFin.Value.Month.ToString());
            int añoE = Convert.ToInt16(dtFin.Value.Year.ToString());
            //MessageBox.Show(diaP+" "+diaE+" "+mesP+" "+mesE);
            if (diaP > diaE && mesP >= mesE && añoP >= añoE)
            {
                MessageBox.Show("La fecha máxima no debe ser menor que la minima");
                dtFin.Value = DateTime.Now;
            }
            else
            {
                if (pestañas.SelectedIndex == 3)
                {
                    filtrarDeudas();
                }
            }
        }
        private void dtPendIni_ValueChanged(object sender, EventArgs e)
        {
            String fechaminima = dtPendIni.Value.ToString();
            String fechamaxima = dtPendFin.Value.ToString();
            int diaP = Convert.ToInt16(dtPendIni.Value.Day.ToString());
            int mesP = Convert.ToInt16(dtPendIni.Value.Month.ToString());
            int añoP = Convert.ToInt16(dtPendIni.Value.Year.ToString());
            int diaE = Convert.ToInt16(dtPendFin.Value.Day.ToString());
            int mesE = Convert.ToInt16(dtPendFin.Value.Month.ToString());
            int añoE = Convert.ToInt16(dtPendFin.Value.Year.ToString());
            //MessageBox.Show(diaP+" "+diaE+" "+mesP+" "+mesE);
            if (diaP > diaE && mesP >= mesE && añoP >= añoE)
            {
                MessageBox.Show("La fecha máxima no debe ser menor que la minima");
                dtPendIni.Value = DateTime.Now;
            }
            else
            {
                if (pestañas.SelectedIndex == 2)
                {
                    filtrarPendientes();
                }
            }
        }

        private void dtPendFin_ValueChanged(object sender, EventArgs e)
        {
            String fechaminima = dtPendIni.Value.ToString();
            String fechamaxima = dtPendFin.Value.ToString();
            int diaP = Convert.ToInt16(dtPendIni.Value.Day.ToString());
            int mesP = Convert.ToInt16(dtPendIni.Value.Month.ToString());
            int añoP = Convert.ToInt16(dtPendIni.Value.Year.ToString());
            int diaE = Convert.ToInt16(dtPendFin.Value.Day.ToString());
            int mesE = Convert.ToInt16(dtPendFin.Value.Month.ToString());
            int añoE = Convert.ToInt16(dtPendFin.Value.Year.ToString());
            //MessageBox.Show(diaP+" "+diaE+" "+mesP+" "+mesE);
            if (diaP > diaE && mesP >= mesE && añoP >= añoE)
            {
                MessageBox.Show("La fecha máxima no debe ser menor que la minima");
                dtPendFin.Value = DateTime.Now;
            }
            else
            {
                if (pestañas.SelectedIndex == 2)
                {
                    filtrarPendientes();
                }
            }
        }

        private void rbNoLiquidadas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoLiquidadas.Checked == true)
            {
                rbLiquidadas.Checked = false;
                if (pestañas.SelectedIndex == 3)
                {
                    btnLiquidar.Enabled = true;
                    filtrarDeudas();
                }
            }
        }

        private void rbLiquidadas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLiquidadas.Checked == true)
            {
                rbNoLiquidadas.Checked = false;
                if (pestañas.SelectedIndex == 3)
                {
                    btnLiquidar.Enabled = false;
                    filtrarDeudas();
                }
            }
        }
        private void rbLiquidadaPend_CheckedChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 2)
            {
                btnLiquidar.Enabled = false;
                filtrarPendientes();
            }
        }

        private void rbNoLiquidadaPend_CheckedChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 2)
            {
                btnLiquidar.Enabled = true;
                filtrarPendientes();
            }
        }

        private void comboProcedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 0)
            {
                if (comboProcedencia.SelectedIndex >= 1)
                    filtrarEntradas();
            }
        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 0)
            {
                if (comboTipo.SelectedIndex >= 1)
                    filtrarEntradas();
            }
        }

        private void cajaImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            Boolean IsDec = false;
            int nroDec = 0;
            int cuentapuntos = 0;
            if (cajaImporte.Text.Length > 6)
            {
                e.Handled = true;
                return;
            }
            for (int i = 0; i < cajaImporte.Text.Length; i++)
            {
                if (cajaImporte.Text[i] == '.' || cajaImporte.Text[i] == ',')
                {
                    IsDec = true;
                    cuentapuntos++;
                }
                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == '-')
                e.Handled = false;
            else if (e.KeyChar == 46 || e.KeyChar == 44)
            {
                if (cajaImporte.Text.Length < 1)
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
                e.Handled = true;
            
            
        }

        private void filtrarEntradas()
        {
                    
            String sql = "select * from operaciones where ";
            if (comboProcedencia.SelectedIndex >=1)
            {
                sql = sql + "idprocdest=" + comboProcedencia.SelectedIndex+" and";
            }
            if (comboTipo.SelectedIndex >= 1)
            {
                sql = sql + " tipo like '%" + comboTipo.SelectedItem.ToString() + "%' and";
            }
            String importe = cajaImporte.Text;

            if (importe.Equals("-"))
            {
                //sql = sql + " importe>=-0 and "; 
                primero= '-';
            }
            else
            {
                if (importe.Length > 1 && primero!=' ')
                {
                    sql = sql + " importe>='" + Convert.ToSingle(importe) + "' and ";
                }
                else
                {
                    if (!importe.Equals(""))
                        sql = sql + " importe>='" + Convert.ToSingle(importe) + "' and ";

                }
            }
            
            String mesMin = Convert.ToString(fechaMinima.Value.Month);
            String mesMax = Convert.ToString(fechaMaxima.Value.Month);
            if (Convert.ToInt16(mesMin) < 10) mesMin = "0" + mesMin;
            if (Convert.ToInt16(mesMax) < 10) mesMax = "0" + mesMax;
            String diaMin = Convert.ToString(fechaMinima.Value.Day);
            String diaMax = Convert.ToString(fechaMaxima.Value.Day);
            if (Convert.ToInt16(diaMin) < 10) diaMin = "0" + diaMin;
            if (Convert.ToInt16(diaMax) < 10) diaMax = "0" + diaMax;
            String fechaMin = fechaMinima.Value.Year+""+mesMin+""+diaMin;
            String fechaMax = fechaMaxima.Value.Year + "" + mesMax + "" + diaMax;

            sql =sql + "(fecha>="+fechaMin+" and fecha<="+fechaMax+")";
           
            CargarTablas.cargarEntradas(sql,dgvEntradas);
        
        }

        private void cajaImporte_TextChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 0)
            {
                filtrarEntradas();
            }
            if (pestañas.SelectedIndex == 1)
            {
                filtrarSalida();
            }            
        }

        private void txtImpPend_TextChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex==2)
            {
                filtrarPendientes();
            }
        }

        private void fechaMinima_ValueChanged(object sender, EventArgs e)
        {
            String fechaminima = fechaMinima.Value.ToString();
            String fechamaxima = fechaMaxima.Value.ToString();
            int diaP = Convert.ToInt16(fechaMinima.Value.Day.ToString());
            int mesP = Convert.ToInt16(fechaMinima.Value.Month.ToString());
            int añoP = Convert.ToInt16(fechaMinima.Value.Year.ToString());
            int diaE = Convert.ToInt16(fechaMaxima.Value.Day.ToString());
            int mesE = Convert.ToInt16(fechaMaxima.Value.Month.ToString());
            int añoE = Convert.ToInt16(fechaMaxima.Value.Year.ToString());
            //MessageBox.Show(diaP+" "+diaE+" "+mesP+" "+mesE);
            if (diaP > diaE && mesP >= mesE && añoP >= añoE)
            {
                MessageBox.Show("La fecha mínima no debe ser mayor que la máxima");
                fechaMinima.Value = DateTime.Now;
            }
            else
            {
                if (pestañas.SelectedIndex == 0)
                {
                    filtrarEntradas();
                }
                if (pestañas.SelectedIndex == 1)
                {
                    filtrarSalida();
                }              
            }
        }

        private void fechaMaxima_ValueChanged(object sender, EventArgs e)
        {
            String fechaminima = fechaMinima.Value.ToString();
            String fechamaxima = fechaMaxima.Value.ToString();
            int diaP = Convert.ToInt16(fechaMinima.Value.Day.ToString());
            int mesP = Convert.ToInt16(fechaMinima.Value.Month.ToString());
            int añoP = Convert.ToInt16(fechaMinima.Value.Year.ToString());
            int diaE = Convert.ToInt16(fechaMaxima.Value.Day.ToString());
            int mesE = Convert.ToInt16(fechaMaxima.Value.Month.ToString());
            int añoE = Convert.ToInt16(fechaMaxima.Value.Year.ToString());
            //MessageBox.Show(diaP+" "+diaE+" "+mesP+" "+mesE);
            if (diaP > diaE && mesP >= mesE && añoP >= añoE)
            {
                MessageBox.Show("La fecha máxima no debe ser menor que la minima");
                fechaMaxima.Value = DateTime.Now;
            }
            else
            {
                if (pestañas.SelectedIndex == 0)
                {
                    filtrarEntradas();
                }
                if (pestañas.SelectedIndex == 1)
                {
                    filtrarSalida();
                }        
            }
        }
        /// <summary>
        /// Metodo que filtra el dgv segun los campos rellenados 
        /// </summary>
        private void filtrarSalida()
        {
            String sql = "SELECT * FROM OPERACIONES WHERE TIPOOPERACION='S'";
            if (comboProcedencia.SelectedIndex >= 1)
            {
                sql = sql + " AND idprocdest =" + averiguarIdDestino(comboProcedencia.SelectedItem.ToString());
            }
            if (comboTipo.SelectedIndex >= 1)
            {
                sql = sql + " AND tipo like '%" + comboTipo.SelectedItem.ToString() + "%'";
            }
            String importe = cajaImporte.Text;

            if (importe.Equals("-"))
            {
                //sql = sql + " importe>=-0 and "; 
                primero = '-';
            }
            else
            {
                if (importe.Length > 1 && primero != ' ')
                {
                    sql = sql + " AND importe>='" + Convert.ToSingle(importe) + "'";
                }
                else
                {
                    if (!importe.Equals(""))
                        sql = sql + " AND importe>='" + Convert.ToSingle(importe) + "'";

                }
            }

            sql = sql + " and " + condicionIntervaloFecha();

            CargarTablas.cargarTablaSalida(sql,dgvSalidas);
        }
        /// <summary>
        /// Metodo que calcula el intervalo de fecha para la condicion de la sentencia sql
        /// </summary>
        /// <returns></returns>
        private String condicionIntervaloFecha()
        {
            String mesMin = Convert.ToString(fechaMinima.Value.Month);
            String mesMax = Convert.ToString(fechaMaxima.Value.Month);
            if (Convert.ToInt16(mesMin) < 10) mesMin = "0" + mesMin;
            if (Convert.ToInt16(mesMax) < 10) mesMax = "0" + mesMax;
            String diaMin = Convert.ToString(fechaMinima.Value.Day);
            String diaMax = Convert.ToString(fechaMaxima.Value.Day);
            if (Convert.ToInt16(diaMin) < 10) diaMin = "0" + diaMin;
            if (Convert.ToInt16(diaMax) < 10) diaMax = "0" + diaMax;
            String fechaMin = fechaMinima.Value.Year + "" + mesMin + "" + diaMin;
            String fechaMax = fechaMaxima.Value.Year + "" + mesMax + "" + diaMax;

            return "(fecha>=" + fechaMin + " and fecha<=" + fechaMax + ")";
        }

        private void cbProcedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProcedenciaPend.SelectedIndex!=0)
            {
                if (cbProcedenciaPend.SelectedIndex>0)
                {
                    filtrarPendientes();
                }
            }
        }

        private void txtImpPend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            Boolean IsDec = false;
            int nroDec = 0;
            int cuentapuntos = 0;
            if (cajaImporte.Text.Length > 6)
            {
                e.Handled = true;
                return;
            }
            for (int i = 0; i < cajaImporte.Text.Length; i++)
            {
                if (cajaImporte.Text[i] == '.' || cajaImporte.Text[i] == ',')
                {
                    IsDec = true;
                    cuentapuntos++;
                }
                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == '-')
                e.Handled = false;
            else if (e.KeyChar == 46 || e.KeyChar == 44)
            {
                if (cajaImporte.Text.Length < 1)
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
                e.Handled = true;
            
            
        }

        private void txtImportePendiente_TextChanged(object sender, EventArgs e)
        {
            if (pestañas.SelectedIndex == 3)
            {
                filtrarDeudas();
            }
        }

        private void txtImportePendiente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            Boolean IsDec = false;
            int nroDec = 0;
            int cuentapuntos = 0;
            if (cajaImporte.Text.Length > 6)
            {
                e.Handled = true;
                return;
            }
            for (int i = 0; i < cajaImporte.Text.Length; i++)
            {
                if (cajaImporte.Text[i] == '.' || cajaImporte.Text[i] == ',')
                {
                    IsDec = true;
                    cuentapuntos++;
                }
                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == '-')
                e.Handled = false;
            else if (e.KeyChar == 46 || e.KeyChar == 44)
            {
                if (cajaImporte.Text.Length < 1)
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
                e.Handled = true;
            
        }  
    }
}
