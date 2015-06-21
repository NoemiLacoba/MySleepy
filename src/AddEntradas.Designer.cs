namespace MySleepy
{
    partial class AddEntradas
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
            this.comboTipo = new System.Windows.Forms.ComboBox();
            this.comboProcedencia = new System.Windows.Forms.ComboBox();
            this.cajaImporte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.botonAceptar = new System.Windows.Forms.Button();
            this.botonSalir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cajaConcepto = new System.Windows.Forms.TextBox();
            this.botonLimpiar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.rbAhora = new System.Windows.Forms.RadioButton();
            this.rbPendiente = new System.Windows.Forms.RadioButton();
            this.agencia = new System.Windows.Forms.Label();
            this.comboAgencia = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboTipo
            // 
            this.comboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipo.FormattingEnabled = true;
            this.comboTipo.Location = new System.Drawing.Point(110, 76);
            this.comboTipo.Name = "comboTipo";
            this.comboTipo.Size = new System.Drawing.Size(204, 21);
            this.comboTipo.TabIndex = 19;
            this.comboTipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboTipo_KeyPress);
            // 
            // comboProcedencia
            // 
            this.comboProcedencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProcedencia.FormattingEnabled = true;
            this.comboProcedencia.Location = new System.Drawing.Point(110, 36);
            this.comboProcedencia.Name = "comboProcedencia";
            this.comboProcedencia.Size = new System.Drawing.Size(204, 21);
            this.comboProcedencia.TabIndex = 18;
            this.comboProcedencia.SelectedIndexChanged += new System.EventHandler(this.comboProcedencia_SelectedIndexChanged);
            this.comboProcedencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboProcedencia_KeyPress);
            // 
            // cajaImporte
            // 
            this.cajaImporte.Location = new System.Drawing.Point(110, 242);
            this.cajaImporte.Name = "cajaImporte";
            this.cajaImporte.Size = new System.Drawing.Size(100, 20);
            this.cajaImporte.TabIndex = 17;
            this.cajaImporte.TextChanged += new System.EventHandler(this.cajaImporte_TextChanged);
            this.cajaImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cajaImporte_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Importe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tipo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Procedencia";
            // 
            // botonAceptar
            // 
            this.botonAceptar.Image = global::MySleepy.Properties.Resources.guardar;
            this.botonAceptar.Location = new System.Drawing.Point(80, 395);
            this.botonAceptar.Name = "botonAceptar";
            this.botonAceptar.Size = new System.Drawing.Size(51, 55);
            this.botonAceptar.TabIndex = 20;
            this.botonAceptar.UseVisualStyleBackColor = true;
            this.botonAceptar.Click += new System.EventHandler(this.botonAceptar_Click);
            // 
            // botonSalir
            // 
            this.botonSalir.Image = global::MySleepy.Properties.Resources.Door_converted;
            this.botonSalir.Location = new System.Drawing.Point(313, 395);
            this.botonSalir.Name = "botonSalir";
            this.botonSalir.Size = new System.Drawing.Size(53, 55);
            this.botonSalir.TabIndex = 21;
            this.botonSalir.UseVisualStyleBackColor = true;
            this.botonSalir.Click += new System.EventHandler(this.botonSalir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Concepto";
            // 
            // cajaConcepto
            // 
            this.cajaConcepto.Location = new System.Drawing.Point(110, 287);
            this.cajaConcepto.Multiline = true;
            this.cajaConcepto.Name = "cajaConcepto";
            this.cajaConcepto.Size = new System.Drawing.Size(256, 78);
            this.cajaConcepto.TabIndex = 23;
            this.cajaConcepto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cajaConcepto_KeyPress);
            // 
            // botonLimpiar
            // 
            this.botonLimpiar.Image = global::MySleepy.Properties.Resources.prestamos;
            this.botonLimpiar.Location = new System.Drawing.Point(209, 395);
            this.botonLimpiar.Name = "botonLimpiar";
            this.botonLimpiar.Size = new System.Drawing.Size(52, 55);
            this.botonLimpiar.TabIndex = 24;
            this.botonLimpiar.UseVisualStyleBackColor = true;
            this.botonLimpiar.Click += new System.EventHandler(this.botonLimpiar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(110, 121);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(204, 20);
            this.txtCliente.TabIndex = 26;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::MySleepy.Properties.Resources.clientes;
            this.btnBuscar.Location = new System.Drawing.Point(342, 92);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(53, 49);
            this.btnBuscar.TabIndex = 38;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // rbAhora
            // 
            this.rbAhora.AutoSize = true;
            this.rbAhora.Location = new System.Drawing.Point(110, 165);
            this.rbAhora.Name = "rbAhora";
            this.rbAhora.Size = new System.Drawing.Size(83, 17);
            this.rbAhora.TabIndex = 39;
            this.rbAhora.TabStop = true;
            this.rbAhora.Text = "Pagar ahora";
            this.rbAhora.UseVisualStyleBackColor = true;
            this.rbAhora.CheckedChanged += new System.EventHandler(this.rbAhora_CheckedChanged);
            // 
            // rbPendiente
            // 
            this.rbPendiente.AutoSize = true;
            this.rbPendiente.Location = new System.Drawing.Point(199, 165);
            this.rbPendiente.Name = "rbPendiente";
            this.rbPendiente.Size = new System.Drawing.Size(115, 17);
            this.rbPendiente.TabIndex = 40;
            this.rbPendiente.TabStop = true;
            this.rbPendiente.Text = "Pendiente de pago";
            this.rbPendiente.UseVisualStyleBackColor = true;
            this.rbPendiente.CheckedChanged += new System.EventHandler(this.rbPendiente_CheckedChanged);
            // 
            // agencia
            // 
            this.agencia.AutoSize = true;
            this.agencia.Location = new System.Drawing.Point(40, 197);
            this.agencia.Name = "agencia";
            this.agencia.Size = new System.Drawing.Size(46, 13);
            this.agencia.TabIndex = 41;
            this.agencia.Text = "Agencia";
            // 
            // comboAgencia
            // 
            this.comboAgencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAgencia.FormattingEnabled = true;
            this.comboAgencia.Location = new System.Drawing.Point(110, 194);
            this.comboAgencia.Name = "comboAgencia";
            this.comboAgencia.Size = new System.Drawing.Size(204, 21);
            this.comboAgencia.TabIndex = 42;
            this.comboAgencia.SelectedIndexChanged += new System.EventHandler(this.comboAgencia_SelectedIndexChanged);
            // 
            // AddEntradas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(418, 476);
            this.Controls.Add(this.comboAgencia);
            this.Controls.Add(this.agencia);
            this.Controls.Add(this.rbPendiente);
            this.Controls.Add(this.rbAhora);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.botonLimpiar);
            this.Controls.Add(this.cajaConcepto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.botonSalir);
            this.Controls.Add(this.botonAceptar);
            this.Controls.Add(this.comboTipo);
            this.Controls.Add(this.comboProcedencia);
            this.Controls.Add(this.cajaImporte);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddEntradas";
            this.Text = "Añadir Apunte";
            this.Load += new System.EventHandler(this.AddEntradas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboTipo;
        private System.Windows.Forms.ComboBox comboProcedencia;
        private System.Windows.Forms.TextBox cajaImporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button botonAceptar;
        private System.Windows.Forms.Button botonSalir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cajaConcepto;
        private System.Windows.Forms.Button botonLimpiar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.RadioButton rbAhora;
        private System.Windows.Forms.RadioButton rbPendiente;
        private System.Windows.Forms.Label agencia;
        private System.Windows.Forms.ComboBox comboAgencia;
    }
}