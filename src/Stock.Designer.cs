namespace MySleepy
{
    partial class Stock
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.caja_StockIdeal = new System.Windows.Forms.TextBox();
            this.caja_stockReal = new System.Windows.Forms.TextBox();
            this.caja_nombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.idarticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pedir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockdisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockideal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAnadir = new System.Windows.Forms.Button();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.botonSalir = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.botonImprimir = new System.Windows.Forms.Button();
            this.caja_contraseña = new System.Windows.Forms.TextBox();
            this.caja_usuario = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.botonRecuperar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.caja_StockIdeal);
            this.groupBox1.Controls.Add(this.caja_stockReal);
            this.groupBox1.Controls.Add(this.caja_nombre);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(25, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            // 
            // caja_StockIdeal
            // 
            this.caja_StockIdeal.Location = new System.Drawing.Point(127, 114);
            this.caja_StockIdeal.Name = "caja_StockIdeal";
            this.caja_StockIdeal.Size = new System.Drawing.Size(53, 20);
            this.caja_StockIdeal.TabIndex = 7;
            this.caja_StockIdeal.TextChanged += new System.EventHandler(this.caja_StockIdeal_TextChanged);
            // 
            // caja_stockReal
            // 
            this.caja_stockReal.Location = new System.Drawing.Point(126, 74);
            this.caja_stockReal.Name = "caja_stockReal";
            this.caja_stockReal.Size = new System.Drawing.Size(63, 20);
            this.caja_stockReal.TabIndex = 5;
            this.caja_stockReal.TextChanged += new System.EventHandler(this.caja_stockReal_TextChanged);
            // 
            // caja_nombre
            // 
            this.caja_nombre.Location = new System.Drawing.Point(124, 34);
            this.caja_nombre.Name = "caja_nombre";
            this.caja_nombre.Size = new System.Drawing.Size(159, 20);
            this.caja_nombre.TabIndex = 3;
            this.caja_nombre.TextChanged += new System.EventHandler(this.caja_nombre_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stock Ideal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Stock Real";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Articulo";
            // 
            // dgvStock
            // 
            this.dgvStock.AllowUserToAddRows = false;
            this.dgvStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idarticulo,
            this.nombre,
            this.existencias,
            this.pedir,
            this.stockdisponible,
            this.stockideal,
            this.stockreal});
            this.dgvStock.Location = new System.Drawing.Point(24, 243);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.ReadOnly = true;
            this.dgvStock.RowHeadersVisible = false;
            this.dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStock.Size = new System.Drawing.Size(711, 240);
            this.dgvStock.TabIndex = 1;
            // 
            // idarticulo
            // 
            this.idarticulo.HeaderText = "idarticulo";
            this.idarticulo.Name = "idarticulo";
            this.idarticulo.ReadOnly = true;
            this.idarticulo.Visible = false;
            // 
            // nombre
            // 
            this.nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombre.HeaderText = "Nombre Articulo";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // existencias
            // 
            this.existencias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.existencias.HeaderText = "% Existencias";
            this.existencias.Name = "existencias";
            this.existencias.ReadOnly = true;
            this.existencias.Width = 88;
            // 
            // pedir
            // 
            this.pedir.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.pedir.HeaderText = "Pedir";
            this.pedir.Name = "pedir";
            this.pedir.ReadOnly = true;
            this.pedir.Width = 56;
            // 
            // stockdisponible
            // 
            this.stockdisponible.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.stockdisponible.HeaderText = "Stock Disponible";
            this.stockdisponible.Name = "stockdisponible";
            this.stockdisponible.ReadOnly = true;
            this.stockdisponible.Width = 103;
            // 
            // stockideal
            // 
            this.stockideal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.stockideal.HeaderText = "Stock Ideal";
            this.stockideal.Name = "stockideal";
            this.stockideal.ReadOnly = true;
            this.stockideal.Width = 79;
            // 
            // stockreal
            // 
            this.stockreal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.stockreal.HeaderText = "Stock Real";
            this.stockreal.Name = "stockreal";
            this.stockreal.ReadOnly = true;
            this.stockreal.Width = 78;
            // 
            // btnAnadir
            // 
            this.btnAnadir.Image = global::MySleepy.Properties.Resources.guardar;
            this.btnAnadir.Location = new System.Drawing.Point(591, 184);
            this.btnAnadir.Name = "btnAnadir";
            this.btnAnadir.Size = new System.Drawing.Size(52, 49);
            this.btnAnadir.TabIndex = 26;
            this.btnAnadir.UseVisualStyleBackColor = true;
            this.btnAnadir.Click += new System.EventHandler(this.btnAnadir_Click);
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.Image = global::MySleepy.Properties.Resources.prestamos;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(443, 188);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(47, 48);
            this.btnLimpiarFiltros.TabIndex = 65;
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            this.btnLimpiarFiltros.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnLimpiarFiltros_Click);
            // 
            // botonSalir
            // 
            this.botonSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.botonSalir.Location = new System.Drawing.Point(687, 184);
            this.botonSalir.Name = "botonSalir";
            this.botonSalir.Size = new System.Drawing.Size(48, 49);
            this.botonSalir.TabIndex = 64;
            this.botonSalir.UseVisualStyleBackColor = true;
            this.botonSalir.Click += new System.EventHandler(this.botonSalir_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::MySleepy.Properties.Resources.adwords_editor_128;
            this.btnModificar.Location = new System.Drawing.Point(25, 190);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(47, 47);
            this.btnModificar.TabIndex = 66;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // botonImprimir
            // 
            this.botonImprimir.Image = global::MySleepy.Properties.Resources.print1;
            this.botonImprimir.Location = new System.Drawing.Point(126, 190);
            this.botonImprimir.Name = "botonImprimir";
            this.botonImprimir.Size = new System.Drawing.Size(50, 42);
            this.botonImprimir.TabIndex = 67;
            this.botonImprimir.UseVisualStyleBackColor = true;
            this.botonImprimir.Click += new System.EventHandler(this.botonImprimir_Click);
            // 
            // caja_contraseña
            // 
            this.caja_contraseña.Location = new System.Drawing.Point(122, 91);
            this.caja_contraseña.Name = "caja_contraseña";
            this.caja_contraseña.PasswordChar = '*';
            this.caja_contraseña.Size = new System.Drawing.Size(100, 20);
            this.caja_contraseña.TabIndex = 71;
            // 
            // caja_usuario
            // 
            this.caja_usuario.Location = new System.Drawing.Point(122, 54);
            this.caja_usuario.Name = "caja_usuario";
            this.caja_usuario.Size = new System.Drawing.Size(100, 20);
            this.caja_usuario.TabIndex = 70;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 69;
            this.label5.Text = "Contraseña";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 68;
            this.label6.Text = "Usuario";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.caja_contraseña);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.caja_usuario);
            this.groupBox2.Location = new System.Drawing.Point(443, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 153);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Usuario Almacen";
            // 
            // botonRecuperar
            // 
            this.botonRecuperar.Location = new System.Drawing.Point(201, 193);
            this.botonRecuperar.Name = "botonRecuperar";
            this.botonRecuperar.Size = new System.Drawing.Size(75, 40);
            this.botonRecuperar.TabIndex = 74;
            this.botonRecuperar.Text = "Recuperar Stock";
            this.botonRecuperar.UseVisualStyleBackColor = true;
            this.botonRecuperar.Click += new System.EventHandler(this.botonRecuperar_Click);
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(766, 503);
            this.Controls.Add(this.botonRecuperar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.botonImprimir);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnLimpiarFiltros);
            this.Controls.Add(this.botonSalir);
            this.Controls.Add(this.btnAnadir);
            this.Controls.Add(this.dgvStock);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Stock";
            this.Text = "Stock";
            this.Load += new System.EventHandler(this.Stock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.Button btnAnadir;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Button botonSalir;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button botonImprimir;
        private System.Windows.Forms.TextBox caja_StockIdeal;
        private System.Windows.Forms.TextBox caja_stockReal;
        private System.Windows.Forms.TextBox caja_nombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idarticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencias;
        private System.Windows.Forms.DataGridViewTextBoxColumn pedir;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockdisponible;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockideal;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockreal;
        private System.Windows.Forms.TextBox caja_contraseña;
        private System.Windows.Forms.TextBox caja_usuario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button botonRecuperar;
    }
}