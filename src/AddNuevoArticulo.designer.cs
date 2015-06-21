namespace MySleepy
{
    partial class AddNuevoArticulo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNuevoArticulo));
            this.caja_StockReal = new System.Windows.Forms.NumericUpDown();
            this.lbReal = new System.Windows.Forms.Label();
            this.cboMedida = new System.Windows.Forms.ComboBox();
            this.cboComposicion = new System.Windows.Forms.ComboBox();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.lblComposicion = new System.Windows.Forms.Label();
            this.lblMedida = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAnadir = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.caja_StockIdeal = new System.Windows.Forms.NumericUpDown();
            this.lbIdeal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockReal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockIdeal)).BeginInit();
            this.SuspendLayout();
            // 
            // caja_StockReal
            // 
            this.caja_StockReal.Location = new System.Drawing.Point(363, 118);
            this.caja_StockReal.Name = "caja_StockReal";
            this.caja_StockReal.Size = new System.Drawing.Size(100, 20);
            this.caja_StockReal.TabIndex = 33;
            // 
            // lbReal
            // 
            this.lbReal.AutoSize = true;
            this.lbReal.Location = new System.Drawing.Point(298, 122);
            this.lbReal.Name = "lbReal";
            this.lbReal.Size = new System.Drawing.Size(63, 13);
            this.lbReal.TabIndex = 32;
            this.lbReal.Text = "Stock Real:";
            // 
            // cboMedida
            // 
            this.cboMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMedida.FormattingEnabled = true;
            this.cboMedida.Location = new System.Drawing.Point(83, 120);
            this.cboMedida.Name = "cboMedida";
            this.cboMedida.Size = new System.Drawing.Size(164, 21);
            this.cboMedida.TabIndex = 31;
            // 
            // cboComposicion
            // 
            this.cboComposicion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComposicion.FormattingEnabled = true;
            this.cboComposicion.Location = new System.Drawing.Point(83, 75);
            this.cboComposicion.Name = "cboComposicion";
            this.cboComposicion.Size = new System.Drawing.Size(164, 21);
            this.cboComposicion.TabIndex = 30;
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(363, 27);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(69, 20);
            this.txtReferencia.TabIndex = 20;
            this.txtReferencia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReferencia_KeyDown);
            this.txtReferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReferencia_KeyPress);
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(297, 30);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(62, 13);
            this.lblReferencia.TabIndex = 29;
            this.lblReferencia.Text = "Referencia:";
            // 
            // lblComposicion
            // 
            this.lblComposicion.AutoSize = true;
            this.lblComposicion.Location = new System.Drawing.Point(10, 78);
            this.lblComposicion.Name = "lblComposicion";
            this.lblComposicion.Size = new System.Drawing.Size(70, 13);
            this.lblComposicion.TabIndex = 28;
            this.lblComposicion.Text = "Composicion:";
            // 
            // lblMedida
            // 
            this.lblMedida.AutoSize = true;
            this.lblMedida.Location = new System.Drawing.Point(32, 125);
            this.lblMedida.Name = "lblMedida";
            this.lblMedida.Size = new System.Drawing.Size(45, 13);
            this.lblMedida.TabIndex = 27;
            this.lblMedida.Text = "Medida:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::MySleepy.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(239, 243);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(46, 49);
            this.btnCancelar.TabIndex = 26;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAnadir
            // 
            this.btnAnadir.Image = global::MySleepy.Properties.Resources.guardar;
            this.btnAnadir.Location = new System.Drawing.Point(143, 243);
            this.btnAnadir.Name = "btnAnadir";
            this.btnAnadir.Size = new System.Drawing.Size(52, 49);
            this.btnAnadir.TabIndex = 25;
            this.btnAnadir.UseVisualStyleBackColor = true;
            this.btnAnadir.Click += new System.EventHandler(this.btnAnadir_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(83, 27);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(170, 20);
            this.txtNombre.TabIndex = 21;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(32, 34);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 24;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(363, 71);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(100, 20);
            this.txtPrecio.TabIndex = 22;
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(320, 77);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(40, 13);
            this.lblPrecio.TabIndex = 23;
            this.lblPrecio.Text = "Precio:";
            // 
            // caja_StockIdeal
            // 
            this.caja_StockIdeal.Location = new System.Drawing.Point(363, 158);
            this.caja_StockIdeal.Name = "caja_StockIdeal";
            this.caja_StockIdeal.Size = new System.Drawing.Size(100, 20);
            this.caja_StockIdeal.TabIndex = 35;
            // 
            // lbIdeal
            // 
            this.lbIdeal.AutoSize = true;
            this.lbIdeal.Location = new System.Drawing.Point(297, 160);
            this.lbIdeal.Name = "lbIdeal";
            this.lbIdeal.Size = new System.Drawing.Size(64, 13);
            this.lbIdeal.TabIndex = 34;
            this.lbIdeal.Text = "Stock Ideal:";
            // 
            // AddNuevoArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(515, 304);
            this.ControlBox = false;
            this.Controls.Add(this.caja_StockIdeal);
            this.Controls.Add(this.lbIdeal);
            this.Controls.Add(this.caja_StockReal);
            this.Controls.Add(this.lbReal);
            this.Controls.Add(this.cboMedida);
            this.Controls.Add(this.cboComposicion);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.lblReferencia);
            this.Controls.Add(this.lblComposicion);
            this.Controls.Add(this.lblMedida);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAnadir);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.lblPrecio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddNuevoArticulo";
            this.Text = "Nuevo Articulo";
            this.Load += new System.EventHandler(this.AddNuevoArticulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockReal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockIdeal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown caja_StockReal;
        private System.Windows.Forms.Label lbReal;
        private System.Windows.Forms.ComboBox cboMedida;
        private System.Windows.Forms.ComboBox cboComposicion;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.Label lblComposicion;
        private System.Windows.Forms.Label lblMedida;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAnadir;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown caja_StockIdeal;
        private System.Windows.Forms.Label lbIdeal;

    }
}