namespace Labs.Janelas.LabsEstoque.Dependencias
{
	partial class AtualizarProduto
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
			Label label4;
			Label label3;
			Label label2;
			Label label1;
			SairButton = new Button();
			AtualizarButton = new Button();
			CodBarras = new TextBox();
			PrecoInput = new TextBox();
			QuantEstoqueInput = new TextBox();
			DescricaoManualInput = new TextBox();
			DescricaoProdutoOutput = new TextBox();
			label4 = new Label();
			label3 = new Label();
			label2 = new Label();
			label1 = new Label();
			SuspendLayout();
			// 
			// SairButton
			// 
			SairButton.BackColor = Color.FromArgb(255, 128, 128);
			SairButton.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			SairButton.Location = new Point(592, 464);
			SairButton.Name = "SairButton";
			SairButton.Size = new Size(281, 84);
			SairButton.TabIndex = 23;
			SairButton.Text = "Sair";
			SairButton.UseVisualStyleBackColor = false;
			// 
			// AtualizarButton
			// 
			AtualizarButton.BackColor = Color.FromArgb(128, 255, 128);
			AtualizarButton.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			AtualizarButton.Location = new Point(12, 464);
			AtualizarButton.Name = "AtualizarButton";
			AtualizarButton.Size = new Size(281, 84);
			AtualizarButton.TabIndex = 21;
			AtualizarButton.Text = "Atualizar";
			AtualizarButton.UseVisualStyleBackColor = false;
			// 
			// CodBarras
			// 
			CodBarras.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CodBarras.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			CodBarras.Location = new Point(12, 401);
			CodBarras.Name = "CodBarras";
			CodBarras.Size = new Size(858, 57);
			CodBarras.TabIndex = 20;
			CodBarras.TextAlign = HorizontalAlignment.Center;
			// 
			// PrecoInput
			// 
			PrecoInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			PrecoInput.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PrecoInput.Location = new Point(12, 288);
			PrecoInput.Name = "PrecoInput";
			PrecoInput.Size = new Size(858, 57);
			PrecoInput.TabIndex = 19;
			PrecoInput.TextAlign = HorizontalAlignment.Center;
			// 
			// QuantEstoqueInput
			// 
			QuantEstoqueInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			QuantEstoqueInput.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			QuantEstoqueInput.Location = new Point(12, 175);
			QuantEstoqueInput.Name = "QuantEstoqueInput";
			QuantEstoqueInput.Size = new Size(858, 57);
			QuantEstoqueInput.TabIndex = 18;
			QuantEstoqueInput.TextAlign = HorizontalAlignment.Center;
			// 
			// DescricaoManualInput
			// 
			DescricaoManualInput.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
			DescricaoManualInput.Location = new Point(417, 17);
			DescricaoManualInput.Name = "DescricaoManualInput";
			DescricaoManualInput.PlaceholderText = "Insira Aqui a Descrição Manual";
			DescricaoManualInput.Size = new Size(452, 39);
			DescricaoManualInput.TabIndex = 17;
			// 
			// DescricaoProdutoOutput
			// 
			DescricaoProdutoOutput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			DescricaoProdutoOutput.Enabled = false;
			DescricaoProdutoOutput.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			DescricaoProdutoOutput.Location = new Point(12, 62);
			DescricaoProdutoOutput.Name = "DescricaoProdutoOutput";
			DescricaoProdutoOutput.Size = new Size(858, 57);
			DescricaoProdutoOutput.TabIndex = 16;
			DescricaoProdutoOutput.TextAlign = HorizontalAlignment.Center;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label4.Location = new Point(12, 348);
			label4.Name = "label4";
			label4.Size = new Size(331, 50);
			label4.TabIndex = 15;
			label4.Text = "Código de Barras:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label3.Location = new Point(12, 235);
			label3.Name = "label3";
			label3.Size = new Size(284, 50);
			label3.TabIndex = 14;
			label3.Text = "Preço Unitário:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.Location = new Point(12, 122);
			label2.Name = "label2";
			label2.Size = new Size(383, 50);
			label2.TabIndex = 13;
			label2.Text = "Quant. Para Estoque:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(409, 50);
			label1.TabIndex = 12;
			label1.Text = "Descrição do Produto:";
			// 
			// AtualizarProduto
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(884, 561);
			Controls.Add(SairButton);
			Controls.Add(AtualizarButton);
			Controls.Add(CodBarras);
			Controls.Add(PrecoInput);
			Controls.Add(QuantEstoqueInput);
			Controls.Add(DescricaoManualInput);
			Controls.Add(DescricaoProdutoOutput);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Name = "AtualizarProduto";
			Text = "AtualizarProduto";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button SairButton;
		private Button AtualizarButton;
		private TextBox CodBarras;
		private TextBox PrecoInput;
		private TextBox QuantEstoqueInput;
		private TextBox DescricaoManualInput;
		private TextBox DescricaoProdutoOutput;
	}
}