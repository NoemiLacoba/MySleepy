namespace MySleepy
{
    partial class ArticulosTotales
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
            this.botonSimples = new System.Windows.Forms.Button();
            this.botonCompuestos = new System.Windows.Forms.Button();
            this.botonSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // botonSimples
            // 
            this.botonSimples.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonSimples.Location = new System.Drawing.Point(66, 83);
            this.botonSimples.Name = "botonSimples";
            this.botonSimples.Size = new System.Drawing.Size(105, 50);
            this.botonSimples.TabIndex = 0;
            this.botonSimples.Text = "ARTICULOS SIMPLES";
            this.botonSimples.UseVisualStyleBackColor = true;
            this.botonSimples.Click += new System.EventHandler(this.botonSimples_Click);
            // 
            // botonCompuestos
            // 
            this.botonCompuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonCompuestos.Location = new System.Drawing.Point(211, 83);
            this.botonCompuestos.Name = "botonCompuestos";
            this.botonCompuestos.Size = new System.Drawing.Size(106, 50);
            this.botonCompuestos.TabIndex = 1;
            this.botonCompuestos.Text = "ARTICULOS COMPUESTOS";
            this.botonCompuestos.UseVisualStyleBackColor = true;
            this.botonCompuestos.Click += new System.EventHandler(this.botonCompuestos_Click);
            // 
            // botonSalir
            // 
            this.botonSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.botonSalir.Location = new System.Drawing.Point(311, 192);
            this.botonSalir.Name = "botonSalir";
            this.botonSalir.Size = new System.Drawing.Size(48, 49);
            this.botonSalir.TabIndex = 2;
            this.botonSalir.UseVisualStyleBackColor = true;
            this.botonSalir.Click += new System.EventHandler(this.botonSalir_Click);
            // 
            // ArticulosTotales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.ClientSize = new System.Drawing.Size(394, 267);
            this.Controls.Add(this.botonSalir);
            this.Controls.Add(this.botonCompuestos);
            this.Controls.Add(this.botonSimples);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArticulosTotales";
            this.Text = "Articulos";
            this.Load += new System.EventHandler(this.ArticulosTotales_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button botonSimples;
        private System.Windows.Forms.Button botonCompuestos;
        private System.Windows.Forms.Button botonSalir;
    }
}