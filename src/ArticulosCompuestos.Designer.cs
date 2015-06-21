namespace MySleepy
{
    partial class ArticulosCompuestos
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
            this.caja_StockReal = new System.Windows.Forms.NumericUpDown();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.lblComposicion = new System.Windows.Forms.Label();
            this.lblMedida = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.caja_nombreCompuesto = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.caja_referencia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.caja_precioCompuesto = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.caja_medida = new System.Windows.Forms.TextBox();
            this.caja_composicion = new System.Windows.Forms.TextBox();
            this.caja_cantidad = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscarArticulo = new System.Windows.Forms.Button();
            this.caja_StockIdeal = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAñadir = new System.Windows.Forms.Button();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.botonSalir = new System.Windows.Forms.Button();
            this.dgvCompuestos = new System.Windows.Forms.DataGridView();
            this.Identificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REFERENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMBRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMPOSICION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEDIDA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCKREAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOCKIDEAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caja_precioTotal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockReal)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.caja_cantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockIdeal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompuestos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // caja_StockReal
            // 
            this.caja_StockReal.Enabled = false;
            this.caja_StockReal.Location = new System.Drawing.Point(74, 158);
            this.caja_StockReal.Name = "caja_StockReal";
            this.caja_StockReal.Size = new System.Drawing.Size(73, 20);
            this.caja_StockReal.TabIndex = 45;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(71, 136);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(32, 13);
            this.lblStock.TabIndex = 44;
            this.lblStock.Text = "Real:";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Enabled = false;
            this.txtReferencia.Location = new System.Drawing.Point(167, 36);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(73, 20);
            this.txtReferencia.TabIndex = 34;
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(101, 39);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(62, 13);
            this.lblReferencia.TabIndex = 41;
            this.lblReferencia.Text = "Referencia:";
            // 
            // lblComposicion
            // 
            this.lblComposicion.AutoSize = true;
            this.lblComposicion.Location = new System.Drawing.Point(275, 39);
            this.lblComposicion.Name = "lblComposicion";
            this.lblComposicion.Size = new System.Drawing.Size(70, 13);
            this.lblComposicion.TabIndex = 40;
            this.lblComposicion.Text = "Composicion:";
            // 
            // lblMedida
            // 
            this.lblMedida.AutoSize = true;
            this.lblMedida.Location = new System.Drawing.Point(300, 83);
            this.lblMedida.Name = "lblMedida";
            this.lblMedida.Size = new System.Drawing.Size(45, 13);
            this.lblMedida.TabIndex = 39;
            this.lblMedida.Text = "Medida:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Enabled = false;
            this.txtPrecio.Location = new System.Drawing.Point(348, 119);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(100, 20);
            this.txtPrecio.TabIndex = 36;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(305, 122);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(40, 13);
            this.lblPrecio.TabIndex = 37;
            this.lblPrecio.Text = "Precio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "Nombre Del Articulo Compuesto";
            // 
            // caja_nombreCompuesto
            // 
            this.caja_nombreCompuesto.Location = new System.Drawing.Point(22, 58);
            this.caja_nombreCompuesto.Name = "caja_nombreCompuesto";
            this.caja_nombreCompuesto.Size = new System.Drawing.Size(179, 20);
            this.caja_nombreCompuesto.TabIndex = 48;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.caja_referencia);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.caja_precioCompuesto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.caja_nombreCompuesto);
            this.groupBox1.Location = new System.Drawing.Point(33, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 189);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Art. Compuesto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 84;
            this.label3.Text = "Referencia: ";
            // 
            // caja_referencia
            // 
            this.caja_referencia.Location = new System.Drawing.Point(118, 133);
            this.caja_referencia.Name = "caja_referencia";
            this.caja_referencia.Size = new System.Drawing.Size(82, 20);
            this.caja_referencia.TabIndex = 83;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 78;
            this.label5.Text = "Precio Compuesto:";
            // 
            // caja_precioCompuesto
            // 
            this.caja_precioCompuesto.Location = new System.Drawing.Point(119, 93);
            this.caja_precioCompuesto.Name = "caja_precioCompuesto";
            this.caja_precioCompuesto.Size = new System.Drawing.Size(82, 20);
            this.caja_precioCompuesto.TabIndex = 77;
            this.caja_precioCompuesto.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(38, 83);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(45, 13);
            this.lblNombre.TabIndex = 82;
            this.lblNombre.Text = "Articulo:";
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Location = new System.Drawing.Point(89, 80);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(151, 20);
            this.txtNombre.TabIndex = 81;
            // 
            // caja_medida
            // 
            this.caja_medida.Enabled = false;
            this.caja_medida.Location = new System.Drawing.Point(351, 80);
            this.caja_medida.Name = "caja_medida";
            this.caja_medida.Size = new System.Drawing.Size(149, 20);
            this.caja_medida.TabIndex = 80;
            // 
            // caja_composicion
            // 
            this.caja_composicion.Enabled = false;
            this.caja_composicion.Location = new System.Drawing.Point(351, 36);
            this.caja_composicion.Name = "caja_composicion";
            this.caja_composicion.Size = new System.Drawing.Size(149, 20);
            this.caja_composicion.TabIndex = 79;
            // 
            // caja_cantidad
            // 
            this.caja_cantidad.Location = new System.Drawing.Point(348, 153);
            this.caja_cantidad.Name = "caja_cantidad";
            this.caja_cantidad.Size = new System.Drawing.Size(81, 20);
            this.caja_cantidad.TabIndex = 76;
            this.caja_cantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(293, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Cantidad:";
            // 
            // btnBuscarArticulo
            // 
            this.btnBuscarArticulo.Image = global::MySleepy.Properties.Resources.lupa;
            this.btnBuscarArticulo.Location = new System.Drawing.Point(9, 19);
            this.btnBuscarArticulo.Name = "btnBuscarArticulo";
            this.btnBuscarArticulo.Size = new System.Drawing.Size(56, 45);
            this.btnBuscarArticulo.TabIndex = 73;
            this.btnBuscarArticulo.Text = " ";
            this.btnBuscarArticulo.UseVisualStyleBackColor = true;
            this.btnBuscarArticulo.Click += new System.EventHandler(this.btnBuscarArticulo_Click);
            // 
            // caja_StockIdeal
            // 
            this.caja_StockIdeal.Enabled = false;
            this.caja_StockIdeal.Location = new System.Drawing.Point(167, 158);
            this.caja_StockIdeal.Name = "caja_StockIdeal";
            this.caja_StockIdeal.Size = new System.Drawing.Size(73, 20);
            this.caja_StockIdeal.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Ideal:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::MySleepy.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(109, 223);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(39, 37);
            this.btnCancelar.TabIndex = 65;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAñadir
            // 
            this.btnAñadir.Image = global::MySleepy.Properties.Resources.mas;
            this.btnAñadir.Location = new System.Drawing.Point(34, 224);
            this.btnAñadir.Name = "btnAñadir";
            this.btnAñadir.Size = new System.Drawing.Size(41, 36);
            this.btnAñadir.TabIndex = 64;
            this.btnAñadir.UseVisualStyleBackColor = true;
            this.btnAñadir.Click += new System.EventHandler(this.btnAñadir_Click);
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.Image = global::MySleepy.Properties.Resources.prestamos;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(567, 218);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(47, 48);
            this.btnLimpiarFiltros.TabIndex = 63;
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::MySleepy.Properties.Resources.guardar;
            this.btnGuardar.Location = new System.Drawing.Point(662, 217);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(52, 49);
            this.btnGuardar.TabIndex = 50;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // botonSalir
            // 
            this.botonSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.botonSalir.Location = new System.Drawing.Point(754, 216);
            this.botonSalir.Name = "botonSalir";
            this.botonSalir.Size = new System.Drawing.Size(48, 49);
            this.botonSalir.TabIndex = 49;
            this.botonSalir.UseVisualStyleBackColor = true;
            this.botonSalir.Click += new System.EventHandler(this.botonSalir_Click);
            // 
            // dgvCompuestos
            // 
            this.dgvCompuestos.AllowUserToAddRows = false;
            this.dgvCompuestos.AllowUserToDeleteRows = false;
            this.dgvCompuestos.AllowUserToResizeColumns = false;
            this.dgvCompuestos.AllowUserToResizeRows = false;
            this.dgvCompuestos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCompuestos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvCompuestos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompuestos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Identificador,
            this.REFERENCIA,
            this.NOMBRE,
            this.COMPOSICION,
            this.MEDIDA,
            this.STOCKREAL,
            this.STOCKIDEAL,
            this.CANTIDAD,
            this.precio});
            this.dgvCompuestos.Location = new System.Drawing.Point(33, 272);
            this.dgvCompuestos.Name = "dgvCompuestos";
            this.dgvCompuestos.RowHeadersVisible = false;
            this.dgvCompuestos.RowTemplate.ReadOnly = true;
            this.dgvCompuestos.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCompuestos.Size = new System.Drawing.Size(769, 207);
            this.dgvCompuestos.TabIndex = 67;
            // 
            // Identificador
            // 
            this.Identificador.HeaderText = "Identificador";
            this.Identificador.Name = "Identificador";
            this.Identificador.Visible = false;
            // 
            // REFERENCIA
            // 
            this.REFERENCIA.HeaderText = "REFERENCIA";
            this.REFERENCIA.Name = "REFERENCIA";
            // 
            // NOMBRE
            // 
            this.NOMBRE.HeaderText = "NOMBRE";
            this.NOMBRE.Name = "NOMBRE";
            // 
            // COMPOSICION
            // 
            this.COMPOSICION.HeaderText = "COMPOSICON";
            this.COMPOSICION.Name = "COMPOSICION";
            // 
            // MEDIDA
            // 
            this.MEDIDA.HeaderText = "MEDIDA";
            this.MEDIDA.Name = "MEDIDA";
            // 
            // STOCKREAL
            // 
            this.STOCKREAL.HeaderText = "STOCKREAL";
            this.STOCKREAL.Name = "STOCKREAL";
            // 
            // STOCKIDEAL
            // 
            this.STOCKIDEAL.HeaderText = "STOCKIDEAL";
            this.STOCKIDEAL.Name = "STOCKIDEAL";
            // 
            // CANTIDAD
            // 
            this.CANTIDAD.HeaderText = "CANTIDAD";
            this.CANTIDAD.Name = "CANTIDAD";
            // 
            // precio
            // 
            this.precio.HeaderText = "PRECIO";
            this.precio.Name = "precio";
            // 
            // caja_precioTotal
            // 
            this.caja_precioTotal.Enabled = false;
            this.caja_precioTotal.Location = new System.Drawing.Point(700, 484);
            this.caja_precioTotal.Name = "caja_precioTotal";
            this.caja_precioTotal.Size = new System.Drawing.Size(100, 20);
            this.caja_precioTotal.TabIndex = 68;
            this.caja_precioTotal.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(595, 487);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "Precio Total";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblNombre);
            this.groupBox2.Controls.Add(this.lblComposicion);
            this.groupBox2.Controls.Add(this.lblStock);
            this.groupBox2.Controls.Add(this.caja_StockReal);
            this.groupBox2.Controls.Add(this.txtNombre);
            this.groupBox2.Controls.Add(this.caja_medida);
            this.groupBox2.Controls.Add(this.txtReferencia);
            this.groupBox2.Controls.Add(this.lblReferencia);
            this.groupBox2.Controls.Add(this.caja_composicion);
            this.groupBox2.Controls.Add(this.btnBuscarArticulo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.caja_StockIdeal);
            this.groupBox2.Controls.Add(this.caja_cantidad);
            this.groupBox2.Controls.Add(this.txtPrecio);
            this.groupBox2.Controls.Add(this.lblPrecio);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblMedida);
            this.groupBox2.Location = new System.Drawing.Point(280, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(522, 189);
            this.groupBox2.TabIndex = 83;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos del Articulo Simple";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 83;
            this.label7.Text = "STOCKS";
            // 
            // ArticulosCompuestos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(840, 516);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.caja_precioTotal);
            this.Controls.Add(this.dgvCompuestos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAñadir);
            this.Controls.Add(this.btnLimpiarFiltros);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.botonSalir);
            this.MaximizeBox = false;
            this.Name = "ArticulosCompuestos";
            this.Text = "Articulos Compuestos";
            this.Load += new System.EventHandler(this.ArticulosCompuestos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockReal)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.caja_cantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockIdeal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompuestos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown caja_StockReal;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.Label lblComposicion;
        private System.Windows.Forms.Label lblMedida;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox caja_nombreCompuesto;
        private System.Windows.Forms.Button botonSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Button btnAñadir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown caja_StockIdeal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscarArticulo;
        private System.Windows.Forms.NumericUpDown caja_cantidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox caja_precioCompuesto;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox caja_medida;
        private System.Windows.Forms.TextBox caja_composicion;
        private System.Windows.Forms.DataGridView dgvCompuestos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Identificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn REFERENCIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMBRE;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMPOSICION;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEDIDA;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCKREAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOCKIDEAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANTIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio;
        private System.Windows.Forms.TextBox caja_precioTotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox caja_referencia;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
    }
}