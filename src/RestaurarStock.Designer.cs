namespace MySleepy
{
    partial class RestaurarStock
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
            this.comboDia = new System.Windows.Forms.ComboBox();
            this.comboHora = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.botonSalir = new System.Windows.Forms.Button();
            this.botonImprimir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboDia
            // 
            this.comboDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDia.FormattingEnabled = true;
            this.comboDia.Location = new System.Drawing.Point(131, 42);
            this.comboDia.Name = "comboDia";
            this.comboDia.Size = new System.Drawing.Size(218, 21);
            this.comboDia.TabIndex = 2;
            this.comboDia.SelectedIndexChanged += new System.EventHandler(this.comboDia_SelectedIndexChanged);
            // 
            // comboHora
            // 
            this.comboHora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboHora.FormattingEnabled = true;
            this.comboHora.Location = new System.Drawing.Point(131, 115);
            this.comboHora.Name = "comboHora";
            this.comboHora.Size = new System.Drawing.Size(133, 21);
            this.comboHora.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "DIA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "HORA";
            // 
            // botonSalir
            // 
            this.botonSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.botonSalir.Location = new System.Drawing.Point(301, 177);
            this.botonSalir.Name = "botonSalir";
            this.botonSalir.Size = new System.Drawing.Size(48, 49);
            this.botonSalir.TabIndex = 66;
            this.botonSalir.UseVisualStyleBackColor = true;
            this.botonSalir.Click += new System.EventHandler(this.botonSalir_Click_1);
            // 
            // botonImprimir
            // 
            this.botonImprimir.Image = global::MySleepy.Properties.Resources.print1;
            this.botonImprimir.Location = new System.Drawing.Point(131, 180);
            this.botonImprimir.Name = "botonImprimir";
            this.botonImprimir.Size = new System.Drawing.Size(50, 42);
            this.botonImprimir.TabIndex = 68;
            this.botonImprimir.UseVisualStyleBackColor = true;
            this.botonImprimir.Click += new System.EventHandler(this.botonImprimir_Click);
            // 
            // RestaurarStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(393, 253);
            this.Controls.Add(this.botonImprimir);
            this.Controls.Add(this.botonSalir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboHora);
            this.Controls.Add(this.comboDia);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RestaurarStock";
            this.Text = "RestaurarStock";
            this.Load += new System.EventHandler(this.RestaurarStock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboDia;
        private System.Windows.Forms.ComboBox comboHora;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button botonSalir;
        private System.Windows.Forms.Button botonImprimir;
    }
}