namespace Labs.Janelas.LabsEstoque.Dependencias
{
	partial class CadastrarProduto
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
			DescricaoProdutoOutput = new TextBox();
			DescricaoManualInput = new TextBox();
			QuantEstoqueInput = new TextBox();
			PrecoInput = new TextBox();
			CodBarras = new TextBox();
			CadastrarButton = new Button();
			LimparButton = new Button();
			SairButton = new Button();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(409, 50);
			label1.TabIndex = 0;
			label1.Text = "Descrição do Produto:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.Location = new Point(12, 122);
			label2.Name = "label2";
			label2.Size = new Size(383, 50);
			label2.TabIndex = 1;
			label2.Text = "Quant. Para Estoque:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label3.Location = new Point(12, 235);
			label3.Name = "label3";
			label3.Size = new Size(284, 50);
			label3.TabIndex = 2;
			label3.Text = "Preço Unitário:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label4.Location = new Point(12, 348);
			label4.Name = "label4";
			label4.Size = new Size(331, 50);
			label4.TabIndex = 3;
			label4.Text = "Código de Barras:";
			// 
			// DescricaoProdutoOutput
			// 
			DescricaoProdutoOutput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			DescricaoProdutoOutput.Enabled = false;
			DescricaoProdutoOutput.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			DescricaoProdutoOutput.Location = new Point(12, 62);
			DescricaoProdutoOutput.Name = "DescricaoProdutoOutput";
			DescricaoProdutoOutput.Size = new Size(860, 57);
			DescricaoProdutoOutput.TabIndex = 4;
			DescricaoProdutoOutput.TextAlign = HorizontalAlignment.Center;
			// 
			// DescricaoManualInput
			// 
			DescricaoManualInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			DescricaoManualInput.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
			DescricaoManualInput.Location = new Point(417, 17);
			DescricaoManualInput.Name = "DescricaoManualInput";
			DescricaoManualInput.PlaceholderText = "Insira Aqui a Descrição Manual";
			DescricaoManualInput.Size = new Size(455, 39);
			DescricaoManualInput.TabIndex = 5;
			DescricaoManualInput.KeyUp += DescricaoManualInput_KeyUp;
			// 
			// QuantEstoqueInput
			// 
			QuantEstoqueInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			QuantEstoqueInput.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			QuantEstoqueInput.Location = new Point(12, 175);
			QuantEstoqueInput.Name = "QuantEstoqueInput";
			QuantEstoqueInput.Size = new Size(860, 57);
			QuantEstoqueInput.TabIndex = 6;
			QuantEstoqueInput.TextAlign = HorizontalAlignment.Center;
			// 
			// PrecoInput
			// 
			PrecoInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			PrecoInput.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PrecoInput.Location = new Point(12, 288);
			PrecoInput.Name = "PrecoInput";
			PrecoInput.Size = new Size(860, 57);
			PrecoInput.TabIndex = 7;
			PrecoInput.TextAlign = HorizontalAlignment.Center;
			// 
			// CodBarras
			// 
			CodBarras.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CodBarras.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			CodBarras.Location = new Point(12, 401);
			CodBarras.Name = "CodBarras";
			CodBarras.Size = new Size(860, 57);
			CodBarras.TabIndex = 8;
			CodBarras.TextAlign = HorizontalAlignment.Center;
			// 
			// CadastrarButton
			// 
			CadastrarButton.BackColor = Color.FromArgb(128, 255, 128);
			CadastrarButton.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			CadastrarButton.Location = new Point(12, 464);
			CadastrarButton.Name = "CadastrarButton";
			CadastrarButton.Size = new Size(284, 81);
			CadastrarButton.TabIndex = 9;
			CadastrarButton.Text = "Cadastrar";
			CadastrarButton.UseVisualStyleBackColor = false;
			CadastrarButton.Click += CadastrarButton_Click;
			// 
			// LimparButton
			// 
			LimparButton.BackColor = Color.FromArgb(255, 255, 128);
			LimparButton.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			LimparButton.Location = new Point(302, 464);
			LimparButton.Name = "LimparButton";
			LimparButton.Size = new Size(284, 81);
			LimparButton.TabIndex = 10;
			LimparButton.Text = "Limpar Tudo";
			LimparButton.UseVisualStyleBackColor = false;
			LimparButton.Click += LimparButton_Click;
			// 
			// SairButton
			// 
			SairButton.BackColor = Color.FromArgb(255, 128, 128);
			SairButton.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			SairButton.Location = new Point(592, 464);
			SairButton.Name = "SairButton";
			SairButton.Size = new Size(284, 81);
			SairButton.TabIndex = 11;
			SairButton.Text = "Sair";
			SairButton.UseVisualStyleBackColor = false;
			SairButton.Click += SairButton_Click;
			// 
			// CadastrarProduto
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(884, 561);
			ControlBox = false;
			Controls.Add(SairButton);
			Controls.Add(LimparButton);
			Controls.Add(CadastrarButton);
			Controls.Add(CodBarras);
			Controls.Add(PrecoInput);
			Controls.Add(QuantEstoqueInput);
			Controls.Add(DescricaoManualInput);
			Controls.Add(DescricaoProdutoOutput);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.Fixed3D;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "CadastrarProduto";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "CadastrarProduto";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox DescricaoProdutoOutput;
		private TextBox DescricaoManualInput;
		private TextBox QuantEstoqueInput;
		private TextBox PrecoInput;
		private TextBox CodBarras;
		private Button CadastrarButton;
		private Button LimparButton;
		private Button SairButton;
	}
}