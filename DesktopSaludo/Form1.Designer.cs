namespace DesktopSaludo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblIdioma = new Label();
            cmbIdioma = new ComboBox();
            btnSaludar = new Button();
            lblResultadoTitulo = new Label();
            lblResultado = new Label();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(32, 32);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(310, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Consumir API saludo";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(35, 109);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(64, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(35, 133);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ingresa tu nombre";
            txtNombre.Size = new Size(338, 27);
            txtNombre.TabIndex = 2;
            txtNombre.Text = "ed";
            txtNombre.TextChanged += txtNombre_TextChanged;
            // 
            // lblIdioma
            // 
            lblIdioma.AutoSize = true;
            lblIdioma.Location = new Point(35, 193);
            lblIdioma.Name = "lblIdioma";
            lblIdioma.Size = new Size(56, 20);
            lblIdioma.TabIndex = 3;
            lblIdioma.Text = "Idioma";
            // 
            // cmbIdioma
            // 
            cmbIdioma.FormattingEnabled = true;
            cmbIdioma.Location = new Point(35, 217);
            cmbIdioma.Margin = new Padding(3, 4, 3, 4);
            cmbIdioma.Name = "cmbIdioma";
            cmbIdioma.Size = new Size(338, 28);
            cmbIdioma.TabIndex = 4;
            // 
            // btnSaludar
            // 
            btnSaludar.Location = new Point(35, 280);
            btnSaludar.Margin = new Padding(3, 4, 3, 4);
            btnSaludar.Name = "btnSaludar";
            btnSaludar.Size = new Size(137, 47);
            btnSaludar.TabIndex = 5;
            btnSaludar.Text = "Saludar";
            btnSaludar.UseVisualStyleBackColor = true;
            btnSaludar.Click += btnSaludar_Click;
            // 
            // lblResultadoTitulo
            // 
            lblResultadoTitulo.AutoSize = true;
            lblResultadoTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblResultadoTitulo.Location = new Point(35, 359);
            lblResultadoTitulo.Name = "lblResultadoTitulo";
            lblResultadoTitulo.Size = new Size(106, 28);
            lblResultadoTitulo.TabIndex = 6;
            lblResultadoTitulo.Text = "Resultado";
            // 
            // lblResultado
            // 
            lblResultado.BorderStyle = BorderStyle.FixedSingle;
            lblResultado.Location = new Point(35, 400);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(338, 79);
            lblResultado.TabIndex = 7;
            lblResultado.Text = "Resultado...";
            lblResultado.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 519);
            Controls.Add(lblResultado);
            Controls.Add(lblResultadoTitulo);
            Controls.Add(btnSaludar);
            Controls.Add(cmbIdioma);
            Controls.Add(lblIdioma);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(lblTitulo);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DesktopSaludo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblIdioma;
        private ComboBox cmbIdioma;
        private Button btnSaludar;
        private Label lblResultadoTitulo;
        private Label lblResultado;
    }
}