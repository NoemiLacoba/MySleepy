namespace MySleepy
{
    partial class ModificarStock
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
            this.caja_nombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.caja_causa = new System.Windows.Forms.TextBox();
            this.caja_StockIdeal = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.caja_StockReal = new System.Windows.Forms.NumericUpDown();
            this.lblStock = new System.Windows.Forms.Label();
            this.btnAnadir = new System.Windows.Forms.Button();
            this.botonSalir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockIdeal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockReal)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.caja_nombre);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.caja_causa);
            this.groupBox1.Controls.Add(this.caja_StockIdeal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.caja_StockReal);
            this.groupBox1.Controls.Add(this.lblStock);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cambiar Stock";
            // 
            // caja_nombre
            // 
            this.caja_nombre.Location = new System.Drawing.Point(236, 26);
            this.caja_nombre.Name = "caja_nombre";
            this.caja_nombre.ReadOnly = true;
            this.caja_nombre.Size = new System.Drawing.Size(170, 20);
            this.caja_nombre.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(100, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "NOMBRE ARTICULO:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(288, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Causa:";
            // 
            // caja_causa
            // 
            this.caja_causa.Location = new System.Drawing.Point(334, 68);
            this.caja_causa.Multiline = true;
            this.caja_causa.Name = "caja_causa";
            this.caja_causa.Size = new System.Drawing.Size(210, 67);
            this.caja_causa.TabIndex = 55;
            // 
            // caja_StockIdeal
            // 
            this.caja_StockIdeal.Location = new System.Drawing.Point(102, 107);
            this.caja_StockIdeal.Name = "caja_StockIdeal";
            this.caja_StockIdeal.Size = new System.Drawing.Size(100, 20);
            this.caja_StockIdeal.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Stock Ideal:";
            // 
            // caja_StockReal
            // 
            this.caja_StockReal.Location = new System.Drawing.Point(102, 64);
            this.caja_StockReal.Name = "caja_StockReal";
            this.caja_StockReal.Size = new System.Drawing.Size(100, 20);
            this.caja_StockReal.TabIndex = 52;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(42, 68);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(63, 13);
            this.lblStock.TabIndex = 51;
            this.lblStock.Text = "Stock Real:";
            // 
            // btnAnadir
            // 
            this.btnAnadir.Image = global::MySleepy.Properties.Resources.guardar;
            this.btnAnadir.Location = new System.Drawing.Point(422, 194);
            this.btnAnadir.Name = "btnAnadir";
            this.btnAnadir.Size = new System.Drawing.Size(52, 49);
            this.btnAnadir.TabIndex = 52;
            this.btnAnadir.UseVisualStyleBackColor = true;
            this.btnAnadir.Click += new System.EventHandler(this.btnAnadir_Click);
            // 
            // botonSalir
            // 
            this.botonSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.botonSalir.Location = new System.Drawing.Point(568, 194);
            this.botonSalir.Name = "botonSalir";
            this.botonSalir.Size = new System.Drawing.Size(48, 49);
            this.botonSalir.TabIndex = 51;
            this.botonSalir.UseVisualStyleBackColor = true;
            this.botonSalir.Click += new System.EventHandler(this.botonSalir_Click);
            // 
            // ModificarStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(644, 255);
            this.Controls.Add(this.btnAnadir);
            this.Controls.Add(this.botonSalir);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModificarStock";
            this.Text = "Cambiar Stock";
            this.Load += new System.EventHandler(this.ModificarStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockIdeal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.caja_StockReal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox caja_causa;
        private System.Windows.Forms.NumericUpDown caja_StockIdeal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown caja_StockReal;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Button btnAnadir;
        private System.Windows.Forms.Button botonSalir;
        private System.Windows.Forms.TextBox caja_nombre;
        private System.Windows.Forms.Label label3;
    }
}