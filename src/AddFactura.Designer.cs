namespace MySleepy
{
    partial class AddFactura
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
            this.dgvArticulosFactura = new System.Windows.Forms.DataGridView();
            this.IDFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdPed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDArtic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDpediArt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARTICULO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRECIOUNITARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRECIOTOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAñadir = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRealizar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.gbCliente = new System.Windows.Forms.GroupBox();
            this.caja_nombre = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.caja_apellido1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.caja_apellido2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscarArticulo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulosFactura)).BeginInit();
            this.gbCliente.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvArticulosFactura
            // 
            this.dgvArticulosFactura.AllowUserToAddRows = false;
            this.dgvArticulosFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvArticulosFactura.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDFact,
            this.IdPed,
            this.IDArtic,
            this.IDpediArt,
            this.CANTIDAD,
            this.ARTICULO,
            this.PRECIOUNITARIO,
            this.PRECIOTOTAL});
            this.dgvArticulosFactura.Location = new System.Drawing.Point(22, 258);
            this.dgvArticulosFactura.MultiSelect = false;
            this.dgvArticulosFactura.Name = "dgvArticulosFactura";
            this.dgvArticulosFactura.RowHeadersVisible = false;
            this.dgvArticulosFactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArticulosFactura.Size = new System.Drawing.Size(804, 139);
            this.dgvArticulosFactura.TabIndex = 10;
            // 
            // IDFact
            // 
            this.IDFact.HeaderText = "IDFactura";
            this.IDFact.Name = "IDFact";
            this.IDFact.Visible = false;
            // 
            // IdPed
            // 
            this.IdPed.HeaderText = "IdPedido";
            this.IdPed.Name = "IdPed";
            this.IdPed.Visible = false;
            // 
            // IDArtic
            // 
            this.IDArtic.HeaderText = "IDArticulo";
            this.IDArtic.Name = "IDArtic";
            this.IDArtic.Visible = false;
            // 
            // IDpediArt
            // 
            this.IDpediArt.HeaderText = "IDpedArt";
            this.IDpediArt.Name = "IDpediArt";
            this.IDpediArt.Visible = false;
            // 
            // CANTIDAD
            // 
            this.CANTIDAD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CANTIDAD.HeaderText = "CANTIDAD";
            this.CANTIDAD.Name = "CANTIDAD";
            // 
            // ARTICULO
            // 
            this.ARTICULO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ARTICULO.HeaderText = "ARTICULO";
            this.ARTICULO.Name = "ARTICULO";
            // 
            // PRECIOUNITARIO
            // 
            this.PRECIOUNITARIO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PRECIOUNITARIO.HeaderText = "PRECIO UD";
            this.PRECIOUNITARIO.Name = "PRECIOUNITARIO";
            // 
            // PRECIOTOTAL
            // 
            this.PRECIOTOTAL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PRECIOTOTAL.HeaderText = "PRECIO TOTAL";
            this.PRECIOTOTAL.Name = "PRECIOTOTAL";
            // 
            // btnAñadir
            // 
            this.btnAñadir.Image = global::MySleepy.Properties.Resources.mas;
            this.btnAñadir.Location = new System.Drawing.Point(22, 191);
            this.btnAñadir.Name = "btnAñadir";
            this.btnAñadir.Size = new System.Drawing.Size(44, 45);
            this.btnAñadir.TabIndex = 60;
            this.btnAñadir.UseVisualStyleBackColor = true;
            this.btnAñadir.Click += new System.EventHandler(this.btnAñadir_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.btnSalir.Location = new System.Drawing.Point(754, 189);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 47);
            this.btnSalir.TabIndex = 59;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnRealizar
            // 
            this.btnRealizar.Image = global::MySleepy.Properties.Resources.guardar;
            this.btnRealizar.Location = new System.Drawing.Point(640, 191);
            this.btnRealizar.Name = "btnRealizar";
            this.btnRealizar.Size = new System.Drawing.Size(46, 45);
            this.btnRealizar.TabIndex = 61;
            this.btnRealizar.UseVisualStyleBackColor = true;
            this.btnRealizar.Click += new System.EventHandler(this.btnRealizar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 67;
            this.label3.Text = "Precio unitario";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(113, 101);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(105, 20);
            this.txtPrecio.TabIndex = 66;
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(113, 65);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(105, 20);
            this.txtCantidad.TabIndex = 64;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Nombre Articulo";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(114, 30);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(231, 20);
            this.txtNombre.TabIndex = 62;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // gbCliente
            // 
            this.gbCliente.Controls.Add(this.caja_nombre);
            this.gbCliente.Controls.Add(this.btnBuscarCliente);
            this.gbCliente.Controls.Add(this.label6);
            this.gbCliente.Controls.Add(this.label8);
            this.gbCliente.Controls.Add(this.caja_apellido1);
            this.gbCliente.Controls.Add(this.label9);
            this.gbCliente.Controls.Add(this.caja_apellido2);
            this.gbCliente.Location = new System.Drawing.Point(420, 25);
            this.gbCliente.Name = "gbCliente";
            this.gbCliente.Size = new System.Drawing.Size(406, 146);
            this.gbCliente.TabIndex = 69;
            this.gbCliente.TabStop = false;
            this.gbCliente.Text = "CLIENTE";
            // 
            // caja_nombre
            // 
            this.caja_nombre.Location = new System.Drawing.Point(24, 32);
            this.caja_nombre.Name = "caja_nombre";
            this.caja_nombre.ReadOnly = true;
            this.caja_nombre.Size = new System.Drawing.Size(130, 20);
            this.caja_nombre.TabIndex = 13;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Image = global::MySleepy.Properties.Resources.clientes;
            this.btnBuscarCliente.Location = new System.Drawing.Point(329, 48);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(50, 49);
            this.btnBuscarCliente.TabIndex = 14;
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Nombre";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Primer Apellido";
            // 
            // caja_apellido1
            // 
            this.caja_apellido1.Location = new System.Drawing.Point(24, 77);
            this.caja_apellido1.Name = "caja_apellido1";
            this.caja_apellido1.ReadOnly = true;
            this.caja_apellido1.Size = new System.Drawing.Size(130, 20);
            this.caja_apellido1.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(168, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Segundo Apellido";
            // 
            // caja_apellido2
            // 
            this.caja_apellido2.Location = new System.Drawing.Point(171, 77);
            this.caja_apellido2.Name = "caja_apellido2";
            this.caja_apellido2.ReadOnly = true;
            this.caja_apellido2.Size = new System.Drawing.Size(117, 20);
            this.caja_apellido2.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscarArticulo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.txtCantidad);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPrecio);
            this.groupBox1.Location = new System.Drawing.Point(22, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 144);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS PARA AÑADIR A LA FACTURA";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::MySleepy.Properties.Resources.prestamos;
            this.btnLimpiar.Location = new System.Drawing.Point(529, 191);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(45, 45);
            this.btnLimpiar.TabIndex = 71;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscarArticulo
            // 
            this.btnBuscarArticulo.Image = global::MySleepy.Properties.Resources.lupa;
            this.btnBuscarArticulo.Location = new System.Drawing.Point(289, 77);
            this.btnBuscarArticulo.Name = "btnBuscarArticulo";
            this.btnBuscarArticulo.Size = new System.Drawing.Size(56, 45);
            this.btnBuscarArticulo.TabIndex = 72;
            this.btnBuscarArticulo.UseVisualStyleBackColor = true;
            this.btnBuscarArticulo.Click += new System.EventHandler(this.btnBuscarArticulo_Click);
            // 
            // AddFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(856, 421);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbCliente);
            this.Controls.Add(this.btnRealizar);
            this.Controls.Add(this.btnAñadir);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.dgvArticulosFactura);
            this.Name = "AddFactura";
            this.Text = "Añadir/Modificar a Factura";
            this.Load += new System.EventHandler(this.AddFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulosFactura)).EndInit();
            this.gbCliente.ResumeLayout(false);
            this.gbCliente.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvArticulosFactura;
        private System.Windows.Forms.Button btnAñadir;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnRealizar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.GroupBox gbCliente;
        private System.Windows.Forms.TextBox caja_nombre;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox caja_apellido1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox caja_apellido2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdPed;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDArtic;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDpediArt;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANTIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARTICULO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRECIOUNITARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRECIOTOTAL;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscarArticulo;
    }
}