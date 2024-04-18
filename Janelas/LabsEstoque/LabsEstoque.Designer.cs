namespace Labs.Janelas.LabsEstoque
{
	partial class LabsEstoque
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
			ListaProdutosEstoque = new ListView();
			ColunaID = new ColumnHeader();
			ColunaDescricao = new ColumnHeader();
			ColunaEstoque = new ColumnHeader();
			ColunaPreco = new ColumnHeader();
			ColunaCodBarras = new ColumnHeader();
			CadastrarButton = new Button();
			AtualizarButton = new Button();
			RemoverButton = new Button();
			VoltarButton = new Button();
			SuspendLayout();
			// 
			// ListaProdutosEstoque
			// 
			ListaProdutosEstoque.AllowColumnReorder = true;
			ListaProdutosEstoque.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ListaProdutosEstoque.Columns.AddRange(new ColumnHeader[] { ColunaID, ColunaDescricao, ColunaEstoque, ColunaPreco, ColunaCodBarras });
			ListaProdutosEstoque.FullRowSelect = true;
			ListaProdutosEstoque.GridLines = true;
			ListaProdutosEstoque.Location = new Point(12, 168);
			ListaProdutosEstoque.MultiSelect = false;
			ListaProdutosEstoque.Name = "ListaProdutosEstoque";
			ListaProdutosEstoque.Size = new Size(1346, 725);
			ListaProdutosEstoque.TabIndex = 0;
			ListaProdutosEstoque.UseCompatibleStateImageBehavior = false;
			ListaProdutosEstoque.View = View.Details;
			// 
			// ColunaID
			// 
			ColunaID.Text = "ID";
			ColunaID.Width = 30;
			// 
			// ColunaDescricao
			// 
			ColunaDescricao.Text = "Descrição";
			ColunaDescricao.TextAlign = HorizontalAlignment.Center;
			ColunaDescricao.Width = 300;
			// 
			// ColunaEstoque
			// 
			ColunaEstoque.Text = "Estoque";
			ColunaEstoque.TextAlign = HorizontalAlignment.Center;
			ColunaEstoque.Width = 100;
			// 
			// ColunaPreco
			// 
			ColunaPreco.Text = "Preço Unitário";
			ColunaPreco.TextAlign = HorizontalAlignment.Center;
			ColunaPreco.Width = 120;
			// 
			// ColunaCodBarras
			// 
			ColunaCodBarras.Text = "Codigo de Barras";
			ColunaCodBarras.TextAlign = HorizontalAlignment.Center;
			ColunaCodBarras.Width = 150;
			// 
			// CadastrarButton
			// 
			CadastrarButton.BackColor = Color.DarkGray;
			CadastrarButton.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			CadastrarButton.Location = new Point(12, 12);
			CadastrarButton.Name = "CadastrarButton";
			CadastrarButton.Size = new Size(190, 150);
			CadastrarButton.TabIndex = 1;
			CadastrarButton.Text = "Cadastro de Produtos";
			CadastrarButton.UseVisualStyleBackColor = false;
			CadastrarButton.Click += CadastrarButton_Click;
			// 
			// AtualizarButton
			// 
			AtualizarButton.BackColor = Color.DarkGray;
			AtualizarButton.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			AtualizarButton.Location = new Point(208, 12);
			AtualizarButton.Name = "AtualizarButton";
			AtualizarButton.Size = new Size(190, 150);
			AtualizarButton.TabIndex = 2;
			AtualizarButton.Text = "Atualizar Produto Selecionado";
			AtualizarButton.UseVisualStyleBackColor = false;
			AtualizarButton.Click += AtualizarButton_Click;
			// 
			// RemoverButton
			// 
			RemoverButton.BackColor = Color.DarkGray;
			RemoverButton.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			RemoverButton.Location = new Point(404, 12);
			RemoverButton.Name = "RemoverButton";
			RemoverButton.Size = new Size(190, 150);
			RemoverButton.TabIndex = 3;
			RemoverButton.Text = "Remover Produto Selecionado";
			RemoverButton.UseVisualStyleBackColor = false;
			RemoverButton.Click += RemoverButton_Click;
			// 
			// VoltarButton
			// 
			VoltarButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			VoltarButton.BackColor = Color.FromArgb(255, 255, 128);
			VoltarButton.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			VoltarButton.Location = new Point(1204, 12);
			VoltarButton.Name = "VoltarButton";
			VoltarButton.Size = new Size(154, 51);
			VoltarButton.TabIndex = 4;
			VoltarButton.Text = "Voltar";
			VoltarButton.UseVisualStyleBackColor = false;
			VoltarButton.Click += VoltarButton_Click;
			// 
			// LabsEstoque
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(1370, 749);
			Controls.Add(VoltarButton);
			Controls.Add(RemoverButton);
			Controls.Add(AtualizarButton);
			Controls.Add(CadastrarButton);
			Controls.Add(ListaProdutosEstoque);
			KeyPreview = true;
			Name = "LabsEstoque";
			Text = "LabsEstoque";
			WindowState = FormWindowState.Maximized;
			Load += OnLabsEstoqueLoad;
			KeyUp += OnLabsEstoqueKeyUp;
			ResumeLayout(false);
		}

		#endregion

		private ListView ListaProdutosEstoque;
		private ColumnHeader ColunaID;
		private ColumnHeader ColunaDescricao;
		private ColumnHeader ColunaEstoque;
		private ColumnHeader ColunaPreco;
		private ColumnHeader ColunaCodBarras;
		private Button CadastrarButton;
		private Button AtualizarButton;
		private Button RemoverButton;
		private Button VoltarButton;
	}
}