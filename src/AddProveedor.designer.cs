namespace MySleepy
{
    partial class AddProveedor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.caja_pais = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbCP = new System.Windows.Forms.ComboBox();
            this.cbCAutonoma = new System.Windows.Forms.ComboBox();
            this.cbProvincia = new System.Windows.Forms.ComboBox();
            this.cbPoblacion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.caja_observaciones = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.caja_contacto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.caja_movil = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.caja_email = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ltelefono = new System.Windows.Forms.Label();
            this.lNombre = new System.Windows.Forms.Label();
            this.cbEA = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCIF = new System.Windows.Forms.TextBox();
            this.lCIF = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lDNI = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvAddProveedor = new System.Windows.Forms.DataGridView();
            this.idarticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Articulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.composicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Medida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtArticulo = new System.Windows.Forms.TextBox();
            this.cbComposicion = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddProveedor)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::MySleepy.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(794, 338);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(42, 48);
            this.btnCancelar.TabIndex = 27;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::MySleepy.Properties.Resources.guardar;
            this.btnGuardar.Location = new System.Drawing.Point(707, 338);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(51, 48);
            this.btnGuardar.TabIndex = 26;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.caja_pais);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cbCP);
            this.groupBox2.Controls.Add(this.cbCAutonoma);
            this.groupBox2.Controls.Add(this.cbProvincia);
            this.groupBox2.Controls.Add(this.cbPoblacion);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtDireccion);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(11, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(825, 146);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dirección";
            // 
            // caja_pais
            // 
            this.caja_pais.Location = new System.Drawing.Point(435, 99);
            this.caja_pais.Name = "caja_pais";
            this.caja_pais.Size = new System.Drawing.Size(124, 20);
            this.caja_pais.TabIndex = 32;
            this.caja_pais.TextChanged += new System.EventHandler(this.caja_pais_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(392, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Pais:";
            // 
            // cbCP
            // 
            this.cbCP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCP.FormattingEnabled = true;
            this.cbCP.Location = new System.Drawing.Point(433, 56);
            this.cbCP.Name = "cbCP";
            this.cbCP.Size = new System.Drawing.Size(147, 21);
            this.cbCP.TabIndex = 10;
            this.cbCP.SelectedIndexChanged += new System.EventHandler(this.cbCP_SelectedIndexChanged);
            // 
            // cbCAutonoma
            // 
            this.cbCAutonoma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCAutonoma.FormattingEnabled = true;
            this.cbCAutonoma.Location = new System.Drawing.Point(140, 27);
            this.cbCAutonoma.Name = "cbCAutonoma";
            this.cbCAutonoma.Size = new System.Drawing.Size(147, 21);
            this.cbCAutonoma.TabIndex = 7;
            this.cbCAutonoma.SelectedIndexChanged += new System.EventHandler(this.cbCAutonoma_SelectedIndexChanged);
            // 
            // cbProvincia
            // 
            this.cbProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProvincia.FormattingEnabled = true;
            this.cbProvincia.Location = new System.Drawing.Point(140, 55);
            this.cbProvincia.Name = "cbProvincia";
            this.cbProvincia.Size = new System.Drawing.Size(147, 21);
            this.cbProvincia.TabIndex = 8;
            this.cbProvincia.Tag = "";
            this.cbProvincia.SelectedIndexChanged += new System.EventHandler(this.cbProvincia_SelectedIndexChanged);
            // 
            // cbPoblacion
            // 
            this.cbPoblacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPoblacion.FormattingEnabled = true;
            this.cbPoblacion.Location = new System.Drawing.Point(433, 27);
            this.cbPoblacion.Name = "cbPoblacion";
            this.cbPoblacion.Size = new System.Drawing.Size(147, 21);
            this.cbPoblacion.TabIndex = 9;
            this.cbPoblacion.SelectedIndexChanged += new System.EventHandler(this.cbPoblacion_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Comunidad Autónoma:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(140, 99);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(177, 20);
            this.txtDireccion.TabIndex = 11;
            this.txtDireccion.TextChanged += new System.EventHandler(this.txtDireccion_TextChanged);
            this.txtDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccion_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(371, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Poblacion:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Provincia:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Avenida/Calle:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(401, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "C.P:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.caja_observaciones);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.caja_contacto);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.caja_movil);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.caja_email);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ltelefono);
            this.groupBox1.Controls.Add(this.lNombre);
            this.groupBox1.Controls.Add(this.cbEA);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCIF);
            this.groupBox1.Controls.Add(this.lCIF);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.lDNI);
            this.groupBox1.Controls.Add(this.txtTelefono);
            this.groupBox1.Controls.Add(this.txtDNI);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(824, 168);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Personales";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(286, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "Observaciones: ";
            // 
            // caja_observaciones
            // 
            this.caja_observaciones.Location = new System.Drawing.Point(373, 113);
            this.caja_observaciones.Multiline = true;
            this.caja_observaciones.Name = "caja_observaciones";
            this.caja_observaciones.Size = new System.Drawing.Size(422, 49);
            this.caja_observaciones.TabIndex = 35;
            this.caja_observaciones.TextChanged += new System.EventHandler(this.caja_observaciones_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(314, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "Contacto: ";
            // 
            // caja_contacto
            // 
            this.caja_contacto.Location = new System.Drawing.Point(373, 85);
            this.caja_contacto.Name = "caja_contacto";
            this.caja_contacto.Size = new System.Drawing.Size(197, 20);
            this.caja_contacto.TabIndex = 33;
            this.caja_contacto.TextChanged += new System.EventHandler(this.caja_contacto_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(266, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 32;
            // 
            // caja_movil
            // 
            this.caja_movil.Location = new System.Drawing.Point(621, 57);
            this.caja_movil.Name = "caja_movil";
            this.caja_movil.Size = new System.Drawing.Size(124, 20);
            this.caja_movil.TabIndex = 30;
            this.caja_movil.TextChanged += new System.EventHandler(this.caja_movil_TextChanged);
            this.caja_movil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.caja_movil_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(583, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Móvil:";
            // 
            // caja_email
            // 
            this.caja_email.Location = new System.Drawing.Point(80, 85);
            this.caja_email.Name = "caja_email";
            this.caja_email.Size = new System.Drawing.Size(197, 20);
            this.caja_email.TabIndex = 28;
            this.caja_email.TextChanged += new System.EventHandler(this.caja_email_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Email:";
            // 
            // ltelefono
            // 
            this.ltelefono.AutoSize = true;
            this.ltelefono.ForeColor = System.Drawing.Color.Red;
            this.ltelefono.Location = new System.Drawing.Point(73, 116);
            this.ltelefono.Name = "ltelefono";
            this.ltelefono.Size = new System.Drawing.Size(0, 13);
            this.ltelefono.TabIndex = 27;
            // 
            // lNombre
            // 
            this.lNombre.AutoSize = true;
            this.lNombre.ForeColor = System.Drawing.Color.Red;
            this.lNombre.Location = new System.Drawing.Point(77, 120);
            this.lNombre.Name = "lNombre";
            this.lNombre.Size = new System.Drawing.Size(0, 13);
            this.lNombre.TabIndex = 26;
            // 
            // cbEA
            // 
            this.cbEA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEA.FormattingEnabled = true;
            this.cbEA.Items.AddRange(new object[] {
            "Empresa",
            "Autonomo"});
            this.cbEA.Location = new System.Drawing.Point(145, 24);
            this.cbEA.Name = "cbEA";
            this.cbEA.Size = new System.Drawing.Size(160, 21);
            this.cbEA.TabIndex = 25;
            this.cbEA.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "EMPRESA/AUTONOMO:";
            // 
            // txtCIF
            // 
            this.txtCIF.Location = new System.Drawing.Point(373, 25);
            this.txtCIF.Name = "txtCIF";
            this.txtCIF.Size = new System.Drawing.Size(124, 20);
            this.txtCIF.TabIndex = 1;
            this.txtCIF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCIF_KeyPress);
            // 
            // lCIF
            // 
            this.lCIF.AutoSize = true;
            this.lCIF.Location = new System.Drawing.Point(340, 28);
            this.lCIF.Name = "lCIF";
            this.lCIF.Size = new System.Drawing.Size(26, 13);
            this.lCIF.TabIndex = 22;
            this.lCIF.Text = "CIF:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(79, 56);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(197, 20);
            this.txtNombre.TabIndex = 2;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // lDNI
            // 
            this.lDNI.AutoSize = true;
            this.lDNI.Location = new System.Drawing.Point(591, 27);
            this.lDNI.Name = "lDNI";
            this.lDNI.Size = new System.Drawing.Size(29, 13);
            this.lDNI.TabIndex = 14;
            this.lDNI.Text = "DNI:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(373, 58);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(124, 20);
            this.txtTelefono.TabIndex = 5;
            this.txtTelefono.TextChanged += new System.EventHandler(this.txtTelefono_TextChanged);
            this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefono_KeyPress);
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(621, 24);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(124, 20);
            this.txtDNI.TabIndex = 3;
            this.txtDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDNI_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Teléfono:";
            // 
            // dgvAddProveedor
            // 
            this.dgvAddProveedor.AllowUserToAddRows = false;
            this.dgvAddProveedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAddProveedor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idarticulo,
            this.Articulo,
            this.composicion,
            this.Medida,
            this.Precio,
            this.NombreProveedor});
            this.dgvAddProveedor.Location = new System.Drawing.Point(11, 392);
            this.dgvAddProveedor.MultiSelect = false;
            this.dgvAddProveedor.Name = "dgvAddProveedor";
            this.dgvAddProveedor.RowHeadersVisible = false;
            this.dgvAddProveedor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvAddProveedor.Size = new System.Drawing.Size(825, 225);
            this.dgvAddProveedor.TabIndex = 57;
            // 
            // idarticulo
            // 
            this.idarticulo.HeaderText = "ID";
            this.idarticulo.Name = "idarticulo";
            this.idarticulo.ReadOnly = true;
            this.idarticulo.Visible = false;
            // 
            // Articulo
            // 
            this.Articulo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Articulo.HeaderText = "Articulo";
            this.Articulo.Name = "Articulo";
            this.Articulo.ReadOnly = true;
            // 
            // composicion
            // 
            this.composicion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.composicion.HeaderText = "Composición";
            this.composicion.Name = "composicion";
            this.composicion.ReadOnly = true;
            // 
            // Medida
            // 
            this.Medida.HeaderText = "Medida";
            this.Medida.Name = "Medida";
            this.Medida.ReadOnly = true;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            // 
            // NombreProveedor
            // 
            this.NombreProveedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NombreProveedor.HeaderText = "Nombre Proveedor";
            this.NombreProveedor.Name = "NombreProveedor";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(29, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(88, 13);
            this.label15.TabIndex = 58;
            this.label15.Text = "Nombre Articulo :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(348, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 13);
            this.label16.TabIndex = 59;
            this.label16.Text = "Composición :";
            // 
            // txtArticulo
            // 
            this.txtArticulo.Location = new System.Drawing.Point(139, 17);
            this.txtArticulo.Name = "txtArticulo";
            this.txtArticulo.Size = new System.Drawing.Size(141, 20);
            this.txtArticulo.TabIndex = 60;
            this.txtArticulo.TextChanged += new System.EventHandler(this.txtArticulo_TextChanged);
            // 
            // cbComposicion
            // 
            this.cbComposicion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComposicion.FormattingEnabled = true;
            this.cbComposicion.Location = new System.Drawing.Point(427, 17);
            this.cbComposicion.Name = "cbComposicion";
            this.cbComposicion.Size = new System.Drawing.Size(152, 21);
            this.cbComposicion.TabIndex = 61;
            this.cbComposicion.SelectedIndexChanged += new System.EventHandler(this.cbComposicion_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbComposicion);
            this.groupBox3.Controls.Add(this.txtArticulo);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Location = new System.Drawing.Point(19, 338);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(595, 48);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtros";
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.Image = global::MySleepy.Properties.Resources.prestamos;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(633, 338);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(47, 48);
            this.btnLimpiarFiltros.TabIndex = 62;
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // AddProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(862, 629);
            this.ControlBox = false;
            this.Controls.Add(this.btnLimpiarFiltros);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvAddProveedor);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddProveedor";
            this.Text = "Añadir/Modificar Proveedor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddProveedor_FormClosed);
            this.Load += new System.EventHandler(this.AddProveedor_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddProveedor)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbCP;
        private System.Windows.Forms.ComboBox cbCAutonoma;
        private System.Windows.Forms.ComboBox cbProvincia;
        private System.Windows.Forms.ComboBox cbPoblacion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbEA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCIF;
        private System.Windows.Forms.Label lCIF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lDNI;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ltelefono;
        private System.Windows.Forms.Label lNombre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox caja_movil;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox caja_email;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox caja_pais;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvAddProveedor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox caja_contacto;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox caja_observaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn idarticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn composicion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Medida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreProveedor;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtArticulo;
        private System.Windows.Forms.ComboBox cbComposicion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLimpiarFiltros;


    }
}