namespace Labs.Janelas.LabsPDV
{
	partial class LabsPDV
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
            Label label1;
            Label label2;
            Label label3;
            Label label4;
            Label label5;
            Label label6;
            Label label7;
            Label label8;
            ColunaItemID = new ColumnHeader();
            CaixaStateLabel = new Label();
            ListaDeVenda = new ListView();
            ColunaDescricao = new ColumnHeader();
            ColunaCodBarras = new ColumnHeader();
            ColunaUnidade = new ColumnHeader();
            ColunaPreco = new ColumnHeader();
            ColunaQuantidade = new ColumnHeader();
            ColunaTotalItem = new ColumnHeader();
            ColunaDesconto = new ColumnHeader();
            DescricaoProdutoBox = new TextBox();
            QuantidadeBox = new TextBox();
            PrecoUnitarioBox = new TextBox();
            SubTotalBox = new TextBox();
            QuantidadeInput = new TextBox();
            CodBarrasInput = new TextBox();
            PagamentoButton = new Button();
            CancelarVendaButton = new Button();
            ExcluirItemButton = new Button();
            VoltarButton = new Button();
            AbrirFecharCaixaButton = new Button();
            PagamentoTotalBox = new TextBox();
            DebugLabel = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(12, 380);
            label1.Name = "label1";
            label1.Size = new Size(139, 30);
            label1.TabIndex = 5;
            label1.Text = "Quantidade:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.Window;
            label2.Location = new Point(198, 380);
            label2.Name = "label2";
            label2.Size = new Size(165, 30);
            label2.TabIndex = 6;
            label2.Text = "Valor Unitário:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.Window;
            label3.Location = new Point(395, 380);
            label3.Name = "label3";
            label3.Size = new Size(117, 30);
            label3.TabIndex = 9;
            label3.Text = "Sub Total:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Window;
            label4.Location = new Point(198, 418);
            label4.Name = "label4";
            label4.Size = new Size(47, 30);
            label4.TabIndex = 10;
            label4.Text = "R$:";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.Window;
            label5.Location = new Point(395, 418);
            label5.Name = "label5";
            label5.Size = new Size(47, 30);
            label5.TabIndex = 12;
            label5.Text = "R$:";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.Window;
            label6.Location = new Point(12, 504);
            label6.Name = "label6";
            label6.Size = new Size(175, 25);
            label6.TabIndex = 14;
            label6.Text = "(F4)*Quantidade:";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.Window;
            label7.Location = new Point(219, 504);
            label7.Name = "label7";
            label7.Size = new Size(205, 30);
            label7.TabIndex = 16;
            label7.Text = "*Código de Barras:";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = SystemColors.Window;
            label8.Location = new Point(187, 537);
            label8.Name = "label8";
            label8.Size = new Size(26, 37);
            label8.TabIndex = 17;
            label8.Text = ":";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ColunaItemID
            // 
            ColunaItemID.Text = "N°";
            ColunaItemID.Width = 40;
            // 
            // CaixaStateLabel
            // 
            CaixaStateLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CaixaStateLabel.BackColor = Color.DarkSalmon;
            CaixaStateLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CaixaStateLabel.Location = new Point(-5, -5);
            CaixaStateLabel.Name = "CaixaStateLabel";
            CaixaStateLabel.Size = new Size(1359, 52);
            CaixaStateLabel.TabIndex = 1;
            CaixaStateLabel.Text = "CAIXA FECHADO";
            CaixaStateLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ListaDeVenda
            // 
            ListaDeVenda.AllowColumnReorder = true;
            ListaDeVenda.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ListaDeVenda.Columns.AddRange(new ColumnHeader[] { ColunaItemID, ColunaDescricao, ColunaCodBarras, ColunaUnidade, ColunaPreco, ColunaQuantidade, ColunaTotalItem, ColunaDesconto });
            ListaDeVenda.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ListaDeVenda.FullRowSelect = true;
            ListaDeVenda.GridLines = true;
            ListaDeVenda.Location = new Point(12, 50);
            ListaDeVenda.MultiSelect = false;
            ListaDeVenda.Name = "ListaDeVenda";
            ListaDeVenda.ShowGroups = false;
            ListaDeVenda.Size = new Size(1322, 272);
            ListaDeVenda.TabIndex = 2;
            ListaDeVenda.UseCompatibleStateImageBehavior = false;
            ListaDeVenda.View = View.Details;
            // 
            // ColunaDescricao
            // 
            ColunaDescricao.Text = "Descricao";
            ColunaDescricao.TextAlign = HorizontalAlignment.Center;
            ColunaDescricao.Width = 200;
            // 
            // ColunaCodBarras
            // 
            ColunaCodBarras.Text = "Código";
            ColunaCodBarras.TextAlign = HorizontalAlignment.Center;
            ColunaCodBarras.Width = 150;
            // 
            // ColunaUnidade
            // 
            ColunaUnidade.Text = "Unidade";
            ColunaUnidade.TextAlign = HorizontalAlignment.Center;
            ColunaUnidade.Width = 150;
            // 
            // ColunaPreco
            // 
            ColunaPreco.Text = "Valor Unitário";
            ColunaPreco.TextAlign = HorizontalAlignment.Center;
            ColunaPreco.Width = 200;
            // 
            // ColunaQuantidade
            // 
            ColunaQuantidade.Text = "Quantidade";
            ColunaQuantidade.TextAlign = HorizontalAlignment.Center;
            ColunaQuantidade.Width = 200;
            // 
            // ColunaTotalItem
            // 
            ColunaTotalItem.Text = "Total do Item";
            ColunaTotalItem.TextAlign = HorizontalAlignment.Center;
            ColunaTotalItem.Width = 200;
            // 
            // ColunaDesconto
            // 
            ColunaDesconto.Text = "Desconto";
            ColunaDesconto.TextAlign = HorizontalAlignment.Center;
            ColunaDesconto.Width = 170;
            // 
            // DescricaoProdutoBox
            // 
            DescricaoProdutoBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DescricaoProdutoBox.Enabled = false;
            DescricaoProdutoBox.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DescricaoProdutoBox.Location = new Point(12, 328);
            DescricaoProdutoBox.Name = "DescricaoProdutoBox";
            DescricaoProdutoBox.PlaceholderText = "Descrição do Produto";
            DescricaoProdutoBox.Size = new Size(552, 50);
            DescricaoProdutoBox.TabIndex = 3;
            DescricaoProdutoBox.TextAlign = HorizontalAlignment.Center;
            // 
            // QuantidadeBox
            // 
            QuantidadeBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            QuantidadeBox.Enabled = false;
            QuantidadeBox.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            QuantidadeBox.Location = new Point(12, 413);
            QuantidadeBox.Name = "QuantidadeBox";
            QuantidadeBox.PlaceholderText = "0";
            QuantidadeBox.Size = new Size(169, 39);
            QuantidadeBox.TabIndex = 4;
            QuantidadeBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PrecoUnitarioBox
            // 
            PrecoUnitarioBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PrecoUnitarioBox.Enabled = false;
            PrecoUnitarioBox.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PrecoUnitarioBox.Location = new Point(251, 413);
            PrecoUnitarioBox.Name = "PrecoUnitarioBox";
            PrecoUnitarioBox.PlaceholderText = "0,00";
            PrecoUnitarioBox.Size = new Size(116, 39);
            PrecoUnitarioBox.TabIndex = 7;
            PrecoUnitarioBox.TextAlign = HorizontalAlignment.Center;
            // 
            // SubTotalBox
            // 
            SubTotalBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SubTotalBox.Enabled = false;
            SubTotalBox.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SubTotalBox.Location = new Point(448, 413);
            SubTotalBox.Name = "SubTotalBox";
            SubTotalBox.PlaceholderText = "0,00";
            SubTotalBox.Size = new Size(116, 39);
            SubTotalBox.TabIndex = 11;
            SubTotalBox.TextAlign = HorizontalAlignment.Center;
            // 
            // QuantidadeInput
            // 
            QuantidadeInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            QuantidadeInput.Enabled = false;
            QuantidadeInput.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            QuantidadeInput.Location = new Point(12, 537);
            QuantidadeInput.Name = "QuantidadeInput";
            QuantidadeInput.PlaceholderText = "0";
            QuantidadeInput.Size = new Size(169, 39);
            QuantidadeInput.TabIndex = 13;
            QuantidadeInput.TextAlign = HorizontalAlignment.Center;
            QuantidadeInput.KeyUp += OnQuantidadeKeyUp;
            // 
            // CodBarrasInput
            // 
            CodBarrasInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CodBarrasInput.Enabled = false;
            CodBarrasInput.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CodBarrasInput.Location = new Point(219, 537);
            CodBarrasInput.Name = "CodBarrasInput";
            CodBarrasInput.PlaceholderText = "0000000000000";
            CodBarrasInput.Size = new Size(345, 39);
            CodBarrasInput.TabIndex = 15;
            CodBarrasInput.TextAlign = HorizontalAlignment.Center;
            CodBarrasInput.KeyUp += OnCodBarrasKeyUp;
            // 
            // PagamentoButton
            // 
            PagamentoButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PagamentoButton.BackColor = Color.FromArgb(128, 255, 128);
            PagamentoButton.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PagamentoButton.Location = new Point(12, 636);
            PagamentoButton.Name = "PagamentoButton";
            PagamentoButton.Size = new Size(180, 80);
            PagamentoButton.TabIndex = 18;
            PagamentoButton.Text = "Pagamento (F1)";
            PagamentoButton.UseVisualStyleBackColor = false;
            PagamentoButton.Click += PagamentoButton_Click;
            // 
            // CancelarVendaButton
            // 
            CancelarVendaButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CancelarVendaButton.BackColor = Color.FromArgb(255, 128, 128);
            CancelarVendaButton.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CancelarVendaButton.Location = new Point(198, 636);
            CancelarVendaButton.Name = "CancelarVendaButton";
            CancelarVendaButton.Size = new Size(180, 80);
            CancelarVendaButton.TabIndex = 19;
            CancelarVendaButton.Text = "Cancelar Venda (F2)";
            CancelarVendaButton.UseVisualStyleBackColor = false;
            CancelarVendaButton.Click += CancelarVendaButton_Click;
            // 
            // ExcluirItemButton
            // 
            ExcluirItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ExcluirItemButton.BackColor = Color.FromArgb(255, 192, 128);
            ExcluirItemButton.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ExcluirItemButton.Location = new Point(384, 636);
            ExcluirItemButton.Name = "ExcluirItemButton";
            ExcluirItemButton.Size = new Size(180, 80);
            ExcluirItemButton.TabIndex = 20;
            ExcluirItemButton.Text = "Excluir Item (F3)";
            ExcluirItemButton.UseVisualStyleBackColor = false;
            ExcluirItemButton.Click += ExcluirItemButton_Click;
            // 
            // VoltarButton
            // 
            VoltarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            VoltarButton.BackColor = Color.FromArgb(255, 255, 128);
            VoltarButton.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            VoltarButton.Location = new Point(1154, 633);
            VoltarButton.Name = "VoltarButton";
            VoltarButton.Size = new Size(180, 80);
            VoltarButton.TabIndex = 22;
            VoltarButton.Text = "Voltar";
            VoltarButton.UseVisualStyleBackColor = false;
            VoltarButton.Click += VoltarButton_Click;
            // 
            // AbrirFecharCaixaButton
            // 
            AbrirFecharCaixaButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AbrirFecharCaixaButton.BackColor = Color.FromArgb(128, 255, 128);
            AbrirFecharCaixaButton.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AbrirFecharCaixaButton.Location = new Point(1154, 538);
            AbrirFecharCaixaButton.Name = "AbrirFecharCaixaButton";
            AbrirFecharCaixaButton.Size = new Size(180, 40);
            AbrirFecharCaixaButton.TabIndex = 21;
            AbrirFecharCaixaButton.Text = "Abrir Caixa";
            AbrirFecharCaixaButton.UseVisualStyleBackColor = false;
            AbrirFecharCaixaButton.Click += AbrirFecharCaixaButton_Click;
            // 
            // PagamentoTotalBox
            // 
            PagamentoTotalBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PagamentoTotalBox.Enabled = false;
            PagamentoTotalBox.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PagamentoTotalBox.Location = new Point(570, 328);
            PagamentoTotalBox.Name = "PagamentoTotalBox";
            PagamentoTotalBox.PlaceholderText = "Total a Pagar R$: 0,00";
            PagamentoTotalBox.Size = new Size(764, 50);
            PagamentoTotalBox.TabIndex = 23;
            // 
            // DebugLabel
            // 
            DebugLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DebugLabel.AutoSize = true;
            DebugLabel.BackColor = Color.Transparent;
            DebugLabel.Font = new Font("Segoe UI", 16F);
            DebugLabel.Location = new Point(665, 673);
            DebugLabel.Name = "DebugLabel";
            DebugLabel.Size = new Size(87, 30);
            DebugLabel.TabIndex = 24;
            DebugLabel.Text = "DEBUG:";
            // 
            // LabsPDV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1346, 725);
            ControlBox = false;
            Controls.Add(DebugLabel);
            Controls.Add(PagamentoTotalBox);
            Controls.Add(AbrirFecharCaixaButton);
            Controls.Add(VoltarButton);
            Controls.Add(ExcluirItemButton);
            Controls.Add(CancelarVendaButton);
            Controls.Add(PagamentoButton);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(CodBarrasInput);
            Controls.Add(label6);
            Controls.Add(QuantidadeInput);
            Controls.Add(SubTotalBox);
            Controls.Add(label3);
            Controls.Add(PrecoUnitarioBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(QuantidadeBox);
            Controls.Add(DescricaoProdutoBox);
            Controls.Add(ListaDeVenda);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(CaixaStateLabel);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LabsPDV";
            WindowState = FormWindowState.Maximized;
            KeyUp += OnPDVKeyUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label CaixaStateLabel;
		private ListView ListaDeVenda;
		private ColumnHeader ColunaDescricao;
		private ColumnHeader ColunaCodBarras;
		private ColumnHeader ColunaUnidade;
		private ColumnHeader ColunaPreco;
		private ColumnHeader ColunaQuantidade;
		private ColumnHeader ColunaTotalItem;
		private ColumnHeader ColunaDesconto;
		private TextBox DescricaoProdutoBox;
		private TextBox QuantidadeBox;
		private Label label1;
		private TextBox PrecoUnitarioBox;
		private TextBox SubTotalBox;
		private TextBox QuantidadeInput;
		private TextBox CodBarrasInput;
		private Button PagamentoButton;
		private Button CancelarVendaButton;
		private Button ExcluirItemButton;
		private Button VoltarButton;
		private Button AbrirFecharCaixaButton;
		private TextBox PagamentoTotalBox;
		private ColumnHeader ColunaItemID;
        private Label DebugLabel;
    }
}