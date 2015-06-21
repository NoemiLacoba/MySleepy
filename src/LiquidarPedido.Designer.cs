namespace MySleepy
{
    partial class LiquidarPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiquidarPedido));
            this.label3 = new System.Windows.Forms.Label();
            this.rbPorcentual = new System.Windows.Forms.RadioButton();
            this.rbDirecta = new System.Windows.Forms.RadioButton();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.botonSalir = new System.Windows.Forms.Button();
            this.botonAceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.caja_Restante = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Forma a pagar";
            // 
            // rbPorcentual
            // 
            this.rbPorcentual.AutoSize = true;
            this.rbPorcentual.Location = new System.Drawing.Point(230, 120);
            this.rbPorcentual.Name = "rbPorcentual";
            this.rbPorcentual.Size = new System.Drawing.Size(76, 17);
            this.rbPorcentual.TabIndex = 56;
            this.rbPorcentual.Text = "Porcentual";
            this.rbPorcentual.UseVisualStyleBackColor = true;
            this.rbPorcentual.CheckedChanged += new System.EventHandler(this.rbPorcentual_CheckedChanged);
            // 
            // rbDirecta
            // 
            this.rbDirecta.AutoSize = true;
            this.rbDirecta.Checked = true;
            this.rbDirecta.Location = new System.Drawing.Point(139, 120);
            this.rbDirecta.Name = "rbDirecta";
            this.rbDirecta.Size = new System.Drawing.Size(59, 17);
            this.rbDirecta.TabIndex = 55;
            this.rbDirecta.TabStop = true;
            this.rbDirecta.Text = "Directa";
            this.rbDirecta.UseVisualStyleBackColor = true;
            this.rbDirecta.CheckedChanged += new System.EventHandler(this.rbDirecta_CheckedChanged);
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(126, 74);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(140, 20);
            this.txtImporte.TabIndex = 51;
            this.txtImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporte_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Importe a pagar";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox4.Location = new System.Drawing.Point(281, 72);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 22);
            this.pictureBox4.TabIndex = 54;
            this.pictureBox4.TabStop = false;
            // 
            // botonSalir
            // 
            this.botonSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.botonSalir.Location = new System.Drawing.Point(242, 171);
            this.botonSalir.Name = "botonSalir";
            this.botonSalir.Size = new System.Drawing.Size(53, 55);
            this.botonSalir.TabIndex = 53;
            this.botonSalir.UseVisualStyleBackColor = true;
            this.botonSalir.Click += new System.EventHandler(this.botonSalir_Click);
            // 
            // botonAceptar
            // 
            this.botonAceptar.Image = global::MySleepy.Properties.Resources.guardar;
            this.botonAceptar.Location = new System.Drawing.Point(92, 171);
            this.botonAceptar.Name = "botonAceptar";
            this.botonAceptar.Size = new System.Drawing.Size(51, 55);
            this.botonAceptar.TabIndex = 52;
            this.botonAceptar.UseVisualStyleBackColor = true;
            this.botonAceptar.Click += new System.EventHandler(this.botonAceptar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Importe restante";
            // 
            // caja_Restante
            // 
            this.caja_Restante.Enabled = false;
            this.caja_Restante.Location = new System.Drawing.Point(126, 34);
            this.caja_Restante.Name = "caja_Restante";
            this.caja_Restante.Size = new System.Drawing.Size(140, 20);
            this.caja_Restante.TabIndex = 59;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(281, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 22);
            this.pictureBox1.TabIndex = 60;
            this.pictureBox1.TabStop = false;
            // 
            // LiquidarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(367, 248);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.caja_Restante);
            this.Controls.Add(this.label2);
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
            this.Name = "LiquidarPedido";
            this.Text = "LiquidarPedido";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbPorcentual;
        private System.Windows.Forms.RadioButton rbDirecta;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button botonSalir;
        private System.Windows.Forms.Button botonAceptar;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox caja_Restante;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}