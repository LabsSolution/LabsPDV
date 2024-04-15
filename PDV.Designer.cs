﻿namespace LabsPDV
{
	partial class PDV
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
			TituloPDV = new Label();
			InputCodProduto = new TextBox();
			NomeProduto = new Label();
			CodProdutoLabel = new Label();
			keydebug = new Label();
			RegistrarProdutos = new Button();
			SuspendLayout();
			// 
			// TituloPDV
			// 
			TituloPDV.Anchor = AnchorStyles.Top;
			TituloPDV.AutoSize = true;
			TituloPDV.Font = new Font("Microsoft Sans Serif", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			TituloPDV.ForeColor = SystemColors.Window;
			TituloPDV.Location = new Point(12, 9);
			TituloPDV.Name = "TituloPDV";
			TituloPDV.Size = new Size(195, 42);
			TituloPDV.TabIndex = 0;
			TituloPDV.Text = "Labs PDV";
			// 
			// InputCodProduto
			// 
			InputCodProduto.Font = new Font("Arial Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
			InputCodProduto.Location = new Point(155, 128);
			InputCodProduto.Name = "InputCodProduto";
			InputCodProduto.Size = new Size(347, 41);
			InputCodProduto.TabIndex = 1;
			InputCodProduto.TextChanged += CodProdutoTextChange;
			// 
			// NomeProduto
			// 
			NomeProduto.AutoSize = true;
			NomeProduto.Font = new Font("Arial", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			NomeProduto.Location = new Point(155, 101);
			NomeProduto.Name = "NomeProduto";
			NomeProduto.Size = new Size(151, 24);
			NomeProduto.TabIndex = 2;
			NomeProduto.Text = "NomeProduto";
			// 
			// CodProdutoLabel
			// 
			CodProdutoLabel.Anchor = AnchorStyles.Top;
			CodProdutoLabel.AutoSize = true;
			CodProdutoLabel.BackColor = SystemColors.WindowFrame;
			CodProdutoLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			CodProdutoLabel.ForeColor = SystemColors.Window;
			CodProdutoLabel.Location = new Point(16, 138);
			CodProdutoLabel.Name = "CodProdutoLabel";
			CodProdutoLabel.Size = new Size(133, 24);
			CodProdutoLabel.TabIndex = 3;
			CodProdutoLabel.Text = "Cód Produto:";
			// 
			// keydebug
			// 
			keydebug.AutoSize = true;
			keydebug.Font = new Font("Arial", 15.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			keydebug.Location = new Point(12, 220);
			keydebug.Name = "keydebug";
			keydebug.Size = new Size(71, 24);
			keydebug.TabIndex = 4;
			keydebug.Text = "label1";
			// 
			// RegistrarProdutos
			// 
			RegistrarProdutos.Location = new Point(16, 619);
			RegistrarProdutos.Name = "RegistrarProdutos";
			RegistrarProdutos.Size = new Size(242, 35);
			RegistrarProdutos.TabIndex = 5;
			RegistrarProdutos.Text = "Registrar Novos Produtos";
			RegistrarProdutos.UseVisualStyleBackColor = true;
			RegistrarProdutos.Click += OnRDPClick;
			// 
			// PDV
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(1436, 666);
			Controls.Add(RegistrarProdutos);
			Controls.Add(keydebug);
			Controls.Add(CodProdutoLabel);
			Controls.Add(NomeProduto);
			Controls.Add(InputCodProduto);
			Controls.Add(TituloPDV);
			KeyPreview = true;
			Name = "PDV";
			Text = "Lab's PDV";
			Load += PDV_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label TituloPDV;
		private TextBox InputCodProduto;
		private Label NomeProduto;
		private Label CodProdutoLabel;
		private Label keydebug;
		private Button RegistrarProdutos;
	}
}
