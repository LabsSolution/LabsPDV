namespace Labs.Janelas.Configuracoes.Dependencias
{
	partial class MeiosDePagamentoConfig
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
            MeioDePagamentoBoxInput = new TextBox();
            label1 = new Label();
            ListaMeiosRegistrados = new ListView();
            ColunaMeioDePagamento = new ColumnHeader();
            AdicionarButton = new Button();
            SemLimiteDeValor = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            RemoverButton = new Button();
            SairButton = new Button();
            SuspendLayout();
            // 
            // MeioDePagamentoBoxInput
            // 
            MeioDePagamentoBoxInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            MeioDePagamentoBoxInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MeioDePagamentoBoxInput.Location = new Point(12, 472);
            MeioDePagamentoBoxInput.Name = "MeioDePagamentoBoxInput";
            MeioDePagamentoBoxInput.PlaceholderText = "Nome do Meio de Pagamento";
            MeioDePagamentoBoxInput.Size = new Size(244, 29);
            MeioDePagamentoBoxInput.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(12, 447);
            label1.Name = "label1";
            label1.Size = new Size(168, 21);
            label1.TabIndex = 2;
            label1.Text = "Meio de Pagamento:";
            // 
            // ListaMeiosRegistrados
            // 
            ListaMeiosRegistrados.BackColor = SystemColors.Window;
            ListaMeiosRegistrados.Columns.AddRange(new ColumnHeader[] { ColunaMeioDePagamento });
            ListaMeiosRegistrados.FullRowSelect = true;
            ListaMeiosRegistrados.GridLines = true;
            ListaMeiosRegistrados.Location = new Point(12, 33);
            ListaMeiosRegistrados.MultiSelect = false;
            ListaMeiosRegistrados.Name = "ListaMeiosRegistrados";
            ListaMeiosRegistrados.Size = new Size(504, 411);
            ListaMeiosRegistrados.TabIndex = 3;
            ListaMeiosRegistrados.UseCompatibleStateImageBehavior = false;
            ListaMeiosRegistrados.View = View.Details;
            // 
            // ColunaMeioDePagamento
            // 
            ColunaMeioDePagamento.Text = "Meios Registrados";
            ColunaMeioDePagamento.Width = 500;
            // 
            // AdicionarButton
            // 
            AdicionarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AdicionarButton.BackColor = Color.FromArgb(128, 255, 128);
            AdicionarButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AdicionarButton.Location = new Point(12, 507);
            AdicionarButton.Name = "AdicionarButton";
            AdicionarButton.Size = new Size(150, 40);
            AdicionarButton.TabIndex = 17;
            AdicionarButton.Text = "Adicionar";
            AdicionarButton.UseVisualStyleBackColor = false;
            AdicionarButton.Click += AdicionarButton_Click;
            // 
            // SemLimiteDeValor
            // 
            SemLimiteDeValor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SemLimiteDeValor.AutoSize = true;
            SemLimiteDeValor.BackColor = Color.Transparent;
            SemLimiteDeValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SemLimiteDeValor.ForeColor = SystemColors.Window;
            SemLimiteDeValor.Location = new Point(262, 472);
            SemLimiteDeValor.Name = "SemLimiteDeValor";
            SemLimiteDeValor.Size = new Size(76, 25);
            SemLimiteDeValor.TabIndex = 24;
            SemLimiteDeValor.Text = "SLDV?";
            SemLimiteDeValor.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.Window;
            label2.Location = new Point(262, 447);
            label2.Name = "label2";
            label2.Size = new Size(254, 21);
            label2.TabIndex = 25;
            label2.Text = "SLDV : (Sem Limitador De Valor)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.Window;
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(153, 21);
            label3.TabIndex = 26;
            label3.Text = "Meios Registrados:";
            // 
            // RemoverButton
            // 
            RemoverButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            RemoverButton.BackColor = Color.FromArgb(255, 255, 128);
            RemoverButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RemoverButton.Location = new Point(168, 507);
            RemoverButton.Name = "RemoverButton";
            RemoverButton.Size = new Size(150, 40);
            RemoverButton.TabIndex = 27;
            RemoverButton.Text = "Remover";
            RemoverButton.UseVisualStyleBackColor = false;
            RemoverButton.Click += RemoverButton_Click;
            // 
            // SairButton
            // 
            SairButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SairButton.BackColor = Color.FromArgb(255, 128, 128);
            SairButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SairButton.Location = new Point(421, 507);
            SairButton.Name = "SairButton";
            SairButton.Size = new Size(95, 40);
            SairButton.TabIndex = 28;
            SairButton.Text = "Sair";
            SairButton.UseVisualStyleBackColor = false;
            SairButton.Click += SairButton_Click;
            // 
            // MeiosDePagamentoConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(531, 559);
            ControlBox = false;
            Controls.Add(SairButton);
            Controls.Add(RemoverButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(SemLimiteDeValor);
            Controls.Add(AdicionarButton);
            Controls.Add(ListaMeiosRegistrados);
            Controls.Add(label1);
            Controls.Add(MeioDePagamentoBoxInput);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MeiosDePagamentoConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MeiosDePagamentoConfig";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MeioDePagamentoBoxInput;
		private Label label1;
		private ListView ListaMeiosRegistrados;
		private ColumnHeader ColunaMeioDePagamento;
		private Button AdicionarButton;
		private CheckBox SemLimiteDeValor;
		private Label label2;
		private Label label3;
		private Button RemoverButton;
        private Button SairButton;
    }
}