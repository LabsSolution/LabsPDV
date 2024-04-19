namespace Labs.Janelas.LabsPDV.Dependencias
{
	partial class JanelaDePagamento
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
			ListaProdutosPagamento = new ListView();
			ColunaDescricao = new ColumnHeader();
			ColunaUnidade = new ColumnHeader();
			ColunaValorUnitario = new ColumnHeader();
			ColunaQuantidade = new ColumnHeader();
			Total = new ColumnHeader();
			PagamentoTotalBox = new TextBox();
			SuspendLayout();
			// 
			// ListaProdutosPagamento
			// 
			ListaProdutosPagamento.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ListaProdutosPagamento.BorderStyle = BorderStyle.FixedSingle;
			ListaProdutosPagamento.Columns.AddRange(new ColumnHeader[] { ColunaDescricao, ColunaUnidade, ColunaValorUnitario, ColunaQuantidade, Total });
			ListaProdutosPagamento.Enabled = false;
			ListaProdutosPagamento.Location = new Point(445, 12);
			ListaProdutosPagamento.MultiSelect = false;
			ListaProdutosPagamento.Name = "ListaProdutosPagamento";
			ListaProdutosPagamento.Size = new Size(427, 487);
			ListaProdutosPagamento.TabIndex = 0;
			ListaProdutosPagamento.UseCompatibleStateImageBehavior = false;
			ListaProdutosPagamento.View = View.Details;
			// 
			// ColunaDescricao
			// 
			ColunaDescricao.Text = "Produto";
			ColunaDescricao.Width = 120;
			// 
			// ColunaUnidade
			// 
			ColunaUnidade.Text = "Unidade";
			ColunaUnidade.TextAlign = HorizontalAlignment.Center;
			// 
			// ColunaValorUnitario
			// 
			ColunaValorUnitario.Text = "Valor Unitario R$";
			ColunaValorUnitario.TextAlign = HorizontalAlignment.Center;
			ColunaValorUnitario.Width = 120;
			// 
			// ColunaQuantidade
			// 
			ColunaQuantidade.Text = "Quant.";
			ColunaQuantidade.TextAlign = HorizontalAlignment.Center;
			// 
			// Total
			// 
			Total.Text = "Total R$";
			// 
			// PagamentoTotalBox
			// 
			PagamentoTotalBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			PagamentoTotalBox.Enabled = false;
			PagamentoTotalBox.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PagamentoTotalBox.Location = new Point(445, 505);
			PagamentoTotalBox.Name = "PagamentoTotalBox";
			PagamentoTotalBox.PlaceholderText = "Total a Pagar R$:";
			PagamentoTotalBox.Size = new Size(427, 44);
			PagamentoTotalBox.TabIndex = 1;
			// 
			// JanelaDePagamento
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(884, 561);
			ControlBox = false;
			Controls.Add(PagamentoTotalBox);
			Controls.Add(ListaProdutosPagamento);
			Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "JanelaDePagamento";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Labs PDV - Pagamento";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ListView ListaProdutosPagamento;
		private ColumnHeader ColunaDescricao;
		private ColumnHeader ColunaUnidade;
		private ColumnHeader ColunaValorUnitario;
		private ColumnHeader ColunaQuantidade;
		private ColumnHeader Total;
		private TextBox PagamentoTotalBox;
	}
}