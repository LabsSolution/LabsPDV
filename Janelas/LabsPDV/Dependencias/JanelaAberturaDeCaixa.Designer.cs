namespace Labs.Janelas.LabsPDV.Dependencias
{
	partial class JanelaAberturaDeCaixa
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
            label1 = new Label();
            CapitalTotalBox = new TextBox();
            AdicionarCapitalInputBox = new TextBox();
            AdicionarCapitalButton = new Button();
            ListaCapitaisAdicionados = new ListView();
            ColunaCapitalAdicionado = new ColumnHeader();
            ColunaValorCapitalAdicionado = new ColumnHeader();
            RemoverCapitalButton = new Button();
            label2 = new Label();
            CapitalEmCaixaBox = new TextBox();
            label3 = new Label();
            AbrirCaixaButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(8, 65);
            label1.Name = "label1";
            label1.Size = new Size(110, 21);
            label1.TabIndex = 0;
            label1.Text = "Capital Total:";
            // 
            // CapitalTotalBox
            // 
            CapitalTotalBox.Enabled = false;
            CapitalTotalBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CapitalTotalBox.Location = new Point(8, 89);
            CapitalTotalBox.Name = "CapitalTotalBox";
            CapitalTotalBox.PlaceholderText = "R$ 0,00";
            CapitalTotalBox.Size = new Size(153, 29);
            CapitalTotalBox.TabIndex = 1;
            // 
            // AdicionarCapitalInputBox
            // 
            AdicionarCapitalInputBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AdicionarCapitalInputBox.Location = new Point(12, 145);
            AdicionarCapitalInputBox.Name = "AdicionarCapitalInputBox";
            AdicionarCapitalInputBox.PlaceholderText = "R$ 0,00";
            AdicionarCapitalInputBox.Size = new Size(149, 29);
            AdicionarCapitalInputBox.TabIndex = 3;
            AdicionarCapitalInputBox.KeyDown += OnAdicionarCapitalInputBoxKeyDown;
            // 
            // AdicionarCapitalButton
            // 
            AdicionarCapitalButton.BackColor = Color.Transparent;
            AdicionarCapitalButton.FlatAppearance.BorderColor = SystemColors.Window;
            AdicionarCapitalButton.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
            AdicionarCapitalButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            AdicionarCapitalButton.FlatStyle = FlatStyle.Flat;
            AdicionarCapitalButton.ForeColor = SystemColors.Window;
            AdicionarCapitalButton.Location = new Point(12, 190);
            AdicionarCapitalButton.Name = "AdicionarCapitalButton";
            AdicionarCapitalButton.Size = new Size(76, 29);
            AdicionarCapitalButton.TabIndex = 4;
            AdicionarCapitalButton.Text = "Adicionar";
            AdicionarCapitalButton.UseVisualStyleBackColor = false;
            AdicionarCapitalButton.Click += AdicionarCapitalButton_Click;
            // 
            // ListaCapitaisAdicionados
            // 
            ListaCapitaisAdicionados.Columns.AddRange(new ColumnHeader[] { ColunaCapitalAdicionado, ColunaValorCapitalAdicionado });
            ListaCapitaisAdicionados.FullRowSelect = true;
            ListaCapitaisAdicionados.Location = new Point(164, 9);
            ListaCapitaisAdicionados.MultiSelect = false;
            ListaCapitaisAdicionados.Name = "ListaCapitaisAdicionados";
            ListaCapitaisAdicionados.Size = new Size(244, 177);
            ListaCapitaisAdicionados.TabIndex = 5;
            ListaCapitaisAdicionados.UseCompatibleStateImageBehavior = false;
            ListaCapitaisAdicionados.View = View.Details;
            // 
            // ColunaCapitalAdicionado
            // 
            ColunaCapitalAdicionado.Text = "Cap. Adicionado";
            ColunaCapitalAdicionado.Width = 165;
            // 
            // ColunaValorCapitalAdicionado
            // 
            ColunaValorCapitalAdicionado.Text = "Valor";
            ColunaValorCapitalAdicionado.Width = 72;
            // 
            // RemoverCapitalButton
            // 
            RemoverCapitalButton.BackColor = Color.Transparent;
            RemoverCapitalButton.FlatAppearance.BorderColor = SystemColors.Window;
            RemoverCapitalButton.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
            RemoverCapitalButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            RemoverCapitalButton.FlatStyle = FlatStyle.Flat;
            RemoverCapitalButton.ForeColor = SystemColors.Window;
            RemoverCapitalButton.Location = new Point(94, 190);
            RemoverCapitalButton.Name = "RemoverCapitalButton";
            RemoverCapitalButton.Size = new Size(67, 29);
            RemoverCapitalButton.TabIndex = 6;
            RemoverCapitalButton.Text = "Remover";
            RemoverCapitalButton.UseVisualStyleBackColor = false;
            RemoverCapitalButton.Click += RemoverCapitalButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.Window;
            label2.Location = new Point(8, 121);
            label2.Name = "label2";
            label2.Size = new Size(146, 21);
            label2.TabIndex = 7;
            label2.Text = "Adicionar Capital:";
            // 
            // CapitalEmCaixaBox
            // 
            CapitalEmCaixaBox.Enabled = false;
            CapitalEmCaixaBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CapitalEmCaixaBox.Location = new Point(8, 33);
            CapitalEmCaixaBox.Name = "CapitalEmCaixaBox";
            CapitalEmCaixaBox.PlaceholderText = "R$ 0,00";
            CapitalEmCaixaBox.Size = new Size(153, 29);
            CapitalEmCaixaBox.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.Window;
            label3.Location = new Point(8, 9);
            label3.Name = "label3";
            label3.Size = new Size(142, 21);
            label3.TabIndex = 8;
            label3.Text = "Capital Em Caixa:";
            // 
            // AbrirCaixaButton
            // 
            AbrirCaixaButton.BackColor = Color.Transparent;
            AbrirCaixaButton.FlatAppearance.BorderColor = SystemColors.Window;
            AbrirCaixaButton.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
            AbrirCaixaButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            AbrirCaixaButton.FlatStyle = FlatStyle.Flat;
            AbrirCaixaButton.ForeColor = SystemColors.Window;
            AbrirCaixaButton.Location = new Point(167, 192);
            AbrirCaixaButton.Name = "AbrirCaixaButton";
            AbrirCaixaButton.Size = new Size(241, 29);
            AbrirCaixaButton.TabIndex = 10;
            AbrirCaixaButton.Text = "Abrir Caixa";
            AbrirCaixaButton.UseVisualStyleBackColor = false;
            AbrirCaixaButton.Click += AbrirCaixaButton_Click;
            // 
            // JanelaAberturaDeCaixa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(420, 230);
            ControlBox = false;
            Controls.Add(AbrirCaixaButton);
            Controls.Add(CapitalEmCaixaBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(RemoverCapitalButton);
            Controls.Add(ListaCapitaisAdicionados);
            Controls.Add(AdicionarCapitalButton);
            Controls.Add(AdicionarCapitalInputBox);
            Controls.Add(CapitalTotalBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "JanelaAberturaDeCaixa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "JanelaAberturaDeCaixa";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox CapitalTotalBox;
        private TextBox AdicionarCapitalInputBox;
        private Button AdicionarCapitalButton;
        private ListView ListaCapitaisAdicionados;
        private ColumnHeader ColunaCapitalAdicionado;
        private ColumnHeader ColunaValorCapitalAdicionado;
        private Button RemoverCapitalButton;
        private Label label2;
        private TextBox CapitalEmCaixaBox;
        private Label label3;
        private Button AbrirCaixaButton;
    }
}