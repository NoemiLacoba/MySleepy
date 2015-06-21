namespace MySleepy
{
    partial class LiquidarDeuda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiquidarDeuda));
            this.label1 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.botonAceptar = new System.Windows.Forms.Button();
            this.botonSalir = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbPorcentual = new System.Windows.Forms.RadioButton();
            this.rbDirecta = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Importe";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(122, 49);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(140, 20);
            this.txtImporte.TabIndex = 1;
            this.txtImporte.TextChanged += new System.EventHandler(this.txtImporte_TextChanged);
            this.txtImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporte_KeyPress);
            // 
            // botonAceptar
            // 
            this.botonAceptar.Image = global::MySleepy.Properties.Resources.guardar;
            this.botonAceptar.Location = new System.Drawing.Point(112, 161);
            this.botonAceptar.Name = "botonAceptar";
            this.botonAceptar.Size = new System.Drawing.Size(51, 55);
            this.botonAceptar.TabIndex = 32;
            this.botonAceptar.UseVisualStyleBackColor = true;
            this.botonAceptar.Click += new System.EventHandler(this.botonAceptar_Click);
            // 
            // botonSalir
            // 
            this.botonSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.botonSalir.Location = new System.Drawing.Point(262, 161);
            this.botonSalir.Name = "botonSalir";
            this.botonSalir.Size = new System.Drawing.Size(53, 55);
            this.botonSalir.TabIndex = 33;
            this.botonSalir.UseVisualStyleBackColor = true;
            this.botonSalir.Click += new System.EventHandler(this.botonSalir_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox4.Location = new System.Drawing.Point(277, 47);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 22);
            this.pictureBox4.TabIndex = 46;
            this.pictureBox4.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Forma a pagar";
            // 
            // rbPorcentual
            // 
            this.rbPorcentual.AutoSize = true;
            this.rbPorcentual.Location = new System.Drawing.Point(226, 112);
            this.rbPorcentual.Name = "rbPorcentual";
            this.rbPorcentual.Size = new System.Drawing.Size(76, 17);
            this.rbPorcentual.TabIndex = 48;
            this.rbPorcentual.Text = "Porcentual";
            this.rbPorcentual.UseVisualStyleBackColor = true;
            this.rbPorcentual.CheckedChanged += new System.EventHandler(this.rbPorcentual_CheckedChanged);
            // 
            // rbDirecta
            // 
            this.rbDirecta.AutoSize = true;
            this.rbDirecta.Checked = true;
            this.rbDirecta.Location = new System.Drawing.Point(135, 112);
            this.rbDirecta.Name = "rbDirecta";
            this.rbDirecta.Size = new System.Drawing.Size(59, 17);
            this.rbDirecta.TabIndex = 47;
            this.rbDirecta.TabStop = true;
            this.rbDirecta.Text = "Directa";
            this.rbDirecta.UseVisualStyleBackColor = true;
            this.rbDirecta.CheckedChanged += new System.EventHandler(this.rbDirecta_CheckedChanged);
            // 
            // LiquidarDeuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.ClientSize = new System.Drawing.Size(344, 241);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbPorcentual);
            this.Controls.Add(this.rbDirecta);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.botonSalir);
            this.Controls.Add(this.botonAceptar);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LiquidarDeuda";
            this.Text = "LiquidarDeuda";
            this.Load += new System.EventHandler(this.LiquidarDeuda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Button botonAceptar;
        private System.Windows.Forms.Button botonSalir;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbPorcentual;
        private System.Windows.Forms.RadioButton rbDirecta;
    }
}