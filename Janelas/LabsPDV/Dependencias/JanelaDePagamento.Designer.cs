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
			ValorTotalComDescontoBox = new TextBox();
			PorcentagemDescontoBoxInput = new TextBox();
			ValorTotalBox = new TextBox();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			ListaPagamentosEfetuados = new ListView();
			ColunaPagamentoEfetuado = new ColumnHeader();
			ColunaTaxaDoPagamento = new ColumnHeader();
			button2 = new Button();
			button1 = new Button();
			ValorRecebidoBox = new TextBox();
			label1 = new Label();
			label5 = new Label();
			TrocoBox = new TextBox();
			ListaMeioDePagamento = new ListView();
			ColunaMeioDePagamento = new ColumnHeader();
			textBox6 = new TextBox();
			label6 = new Label();
			label7 = new Label();
			textBox7 = new TextBox();
			label8 = new Label();
			label9 = new Label();
			textBox8 = new TextBox();
			listView1 = new ListView();
			ColunaParcelas = new ColumnHeader();
			PagamentoBoxInput = new TextBox();
			label10 = new Label();
			label11 = new Label();
			FaltaReceberValorBox = new TextBox();
			ColunaMDPTaxa = new ColumnHeader();
			SuspendLayout();
			// 
			// ValorTotalComDescontoBox
			// 
			ValorTotalComDescontoBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ValorTotalComDescontoBox.Enabled = false;
			ValorTotalComDescontoBox.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ValorTotalComDescontoBox.Location = new Point(537, 201);
			ValorTotalComDescontoBox.Name = "ValorTotalComDescontoBox";
			ValorTotalComDescontoBox.PlaceholderText = "R$ 0,00";
			ValorTotalComDescontoBox.Size = new Size(335, 44);
			ValorTotalComDescontoBox.TabIndex = 5;
			ValorTotalComDescontoBox.TextAlign = HorizontalAlignment.Center;
			// 
			// PorcentagemDescontoBoxInput
			// 
			PorcentagemDescontoBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			PorcentagemDescontoBoxInput.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PorcentagemDescontoBoxInput.Location = new Point(537, 119);
			PorcentagemDescontoBoxInput.Name = "PorcentagemDescontoBoxInput";
			PorcentagemDescontoBoxInput.PlaceholderText = "% 0,00";
			PorcentagemDescontoBoxInput.Size = new Size(335, 44);
			PorcentagemDescontoBoxInput.TabIndex = 6;
			PorcentagemDescontoBoxInput.TextAlign = HorizontalAlignment.Center;
			// 
			// ValorTotalBox
			// 
			ValorTotalBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ValorTotalBox.Enabled = false;
			ValorTotalBox.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ValorTotalBox.Location = new Point(537, 37);
			ValorTotalBox.Name = "ValorTotalBox";
			ValorTotalBox.PlaceholderText = "R$ 0,00";
			ValorTotalBox.Size = new Size(335, 44);
			ValorTotalBox.TabIndex = 7;
			ValorTotalBox.TextAlign = HorizontalAlignment.Center;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
			label2.ForeColor = SystemColors.Window;
			label2.Location = new Point(552, 166);
			label2.Name = "label2";
			label2.Size = new Size(320, 32);
			label2.TabIndex = 8;
			label2.Text = "Valor Total Com Desconto";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
			label3.ForeColor = SystemColors.Window;
			label3.Location = new Point(726, 84);
			label3.Name = "label3";
			label3.Size = new Size(146, 32);
			label3.TabIndex = 9;
			label3.Text = "Desconto%";
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
			label4.ForeColor = SystemColors.Window;
			label4.Location = new Point(729, 3);
			label4.Name = "label4";
			label4.Size = new Size(143, 32);
			label4.TabIndex = 10;
			label4.Text = "Valor Total";
			// 
			// ListaPagamentosEfetuados
			// 
			ListaPagamentosEfetuados.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			ListaPagamentosEfetuados.BorderStyle = BorderStyle.FixedSingle;
			ListaPagamentosEfetuados.Columns.AddRange(new ColumnHeader[] { ColunaPagamentoEfetuado, ColunaTaxaDoPagamento });
			ListaPagamentosEfetuados.Enabled = false;
			ListaPagamentosEfetuados.GridLines = true;
			ListaPagamentosEfetuados.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			ListaPagamentosEfetuados.Location = new Point(12, 362);
			ListaPagamentosEfetuados.MultiSelect = false;
			ListaPagamentosEfetuados.Name = "ListaPagamentosEfetuados";
			ListaPagamentosEfetuados.Size = new Size(519, 140);
			ListaPagamentosEfetuados.TabIndex = 11;
			ListaPagamentosEfetuados.UseCompatibleStateImageBehavior = false;
			ListaPagamentosEfetuados.View = View.Details;
			// 
			// ColunaPagamentoEfetuado
			// 
			ColunaPagamentoEfetuado.Text = "Pagamento Efetuado";
			ColunaPagamentoEfetuado.Width = 300;
			// 
			// ColunaTaxaDoPagamento
			// 
			ColunaTaxaDoPagamento.Text = "Taxa Adicional";
			ColunaTaxaDoPagamento.TextAlign = HorizontalAlignment.Center;
			ColunaTaxaDoPagamento.Width = 150;
			// 
			// button2
			// 
			button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			button2.BackColor = Color.FromArgb(255, 128, 128);
			button2.Font = new Font("Segoe UI Black", 15F, FontStyle.Bold);
			button2.Location = new Point(707, 505);
			button2.Name = "button2";
			button2.Size = new Size(165, 50);
			button2.TabIndex = 13;
			button2.Text = "Cancelar (F3)";
			button2.UseVisualStyleBackColor = false;
			// 
			// button1
			// 
			button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			button1.BackColor = Color.FromArgb(128, 255, 128);
			button1.Font = new Font("Segoe UI Black", 15F, FontStyle.Bold);
			button1.Location = new Point(537, 505);
			button1.Name = "button1";
			button1.Size = new Size(165, 50);
			button1.TabIndex = 14;
			button1.Text = "Finalizar (F2)";
			button1.UseVisualStyleBackColor = false;
			// 
			// ValorRecebidoBox
			// 
			ValorRecebidoBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ValorRecebidoBox.Enabled = false;
			ValorRecebidoBox.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ValorRecebidoBox.Location = new Point(537, 283);
			ValorRecebidoBox.Name = "ValorRecebidoBox";
			ValorRecebidoBox.PlaceholderText = "R$ 0,00";
			ValorRecebidoBox.Size = new Size(335, 44);
			ValorRecebidoBox.TabIndex = 15;
			ValorRecebidoBox.TextAlign = HorizontalAlignment.Center;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
			label1.ForeColor = SystemColors.Window;
			label1.Location = new Point(752, 248);
			label1.Name = "label1";
			label1.Size = new Size(120, 32);
			label1.TabIndex = 16;
			label1.Text = "Recebido";
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
			label5.ForeColor = SystemColors.Window;
			label5.Location = new Point(791, 420);
			label5.Name = "label5";
			label5.Size = new Size(81, 32);
			label5.TabIndex = 17;
			label5.Text = "Troco";
			// 
			// TrocoBox
			// 
			TrocoBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			TrocoBox.Enabled = false;
			TrocoBox.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			TrocoBox.Location = new Point(537, 455);
			TrocoBox.Name = "TrocoBox";
			TrocoBox.PlaceholderText = "R$ 0,00";
			TrocoBox.Size = new Size(335, 44);
			TrocoBox.TabIndex = 18;
			TrocoBox.TextAlign = HorizontalAlignment.Center;
			// 
			// ListaMeioDePagamento
			// 
			ListaMeioDePagamento.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			ListaMeioDePagamento.BorderStyle = BorderStyle.FixedSingle;
			ListaMeioDePagamento.Columns.AddRange(new ColumnHeader[] { ColunaMeioDePagamento, ColunaMDPTaxa });
			ListaMeioDePagamento.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ListaMeioDePagamento.FullRowSelect = true;
			ListaMeioDePagamento.GridLines = true;
			ListaMeioDePagamento.Location = new Point(12, 166);
			ListaMeioDePagamento.MultiSelect = false;
			ListaMeioDePagamento.Name = "ListaMeioDePagamento";
			ListaMeioDePagamento.Size = new Size(304, 190);
			ListaMeioDePagamento.TabIndex = 19;
			ListaMeioDePagamento.UseCompatibleStateImageBehavior = false;
			ListaMeioDePagamento.View = View.Details;
			// 
			// ColunaMeioDePagamento
			// 
			ColunaMeioDePagamento.Text = "Meio De Pagamento";
			ColunaMeioDePagamento.Width = 170;
			// 
			// textBox6
			// 
			textBox6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			textBox6.Location = new Point(12, 37);
			textBox6.Name = "textBox6";
			textBox6.PlaceholderText = "Nome do Cliente";
			textBox6.Size = new Size(304, 29);
			textBox6.TabIndex = 20;
			textBox6.TextAlign = HorizontalAlignment.Center;
			// 
			// label6
			// 
			label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
			label6.ForeColor = SystemColors.Window;
			label6.Location = new Point(12, 131);
			label6.Name = "label6";
			label6.Size = new Size(303, 30);
			label6.TabIndex = 21;
			label6.Text = "Formas de Pagamento (F4)";
			// 
			// label7
			// 
			label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label7.AutoSize = true;
			label7.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold);
			label7.ForeColor = SystemColors.Window;
			label7.Location = new Point(12, 9);
			label7.Name = "label7";
			label7.Size = new Size(78, 25);
			label7.TabIndex = 22;
			label7.Text = "Cliente";
			// 
			// textBox7
			// 
			textBox7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			textBox7.Location = new Point(322, 37);
			textBox7.Name = "textBox7";
			textBox7.PlaceholderText = "CPF ou CNPJ";
			textBox7.Size = new Size(209, 29);
			textBox7.TabIndex = 23;
			textBox7.TextAlign = HorizontalAlignment.Center;
			// 
			// label8
			// 
			label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label8.AutoSize = true;
			label8.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold);
			label8.ForeColor = SystemColors.Window;
			label8.Location = new Point(322, 9);
			label8.Name = "label8";
			label8.Size = new Size(104, 25);
			label8.TabIndex = 24;
			label8.Text = "CPF/CNPJ";
			// 
			// label9
			// 
			label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label9.AutoSize = true;
			label9.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold);
			label9.ForeColor = SystemColors.Window;
			label9.Location = new Point(12, 70);
			label9.Name = "label9";
			label9.Size = new Size(80, 25);
			label9.TabIndex = 25;
			label9.Text = "E-MAIL";
			// 
			// textBox8
			// 
			textBox8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			textBox8.Location = new Point(12, 98);
			textBox8.Name = "textBox8";
			textBox8.PlaceholderText = "EmailDoCliente@Dominio.com";
			textBox8.Size = new Size(519, 29);
			textBox8.TabIndex = 26;
			textBox8.TextAlign = HorizontalAlignment.Center;
			// 
			// listView1
			// 
			listView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			listView1.BorderStyle = BorderStyle.FixedSingle;
			listView1.Columns.AddRange(new ColumnHeader[] { ColunaParcelas });
			listView1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			listView1.FullRowSelect = true;
			listView1.GridLines = true;
			listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			listView1.Location = new Point(322, 166);
			listView1.MultiSelect = false;
			listView1.Name = "listView1";
			listView1.Size = new Size(209, 190);
			listView1.TabIndex = 27;
			listView1.UseCompatibleStateImageBehavior = false;
			listView1.View = View.Details;
			listView1.Visible = false;
			// 
			// ColunaParcelas
			// 
			ColunaParcelas.Text = "Parcelas";
			ColunaParcelas.Width = 200;
			// 
			// PagamentoBoxInput
			// 
			PagamentoBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			PagamentoBoxInput.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PagamentoBoxInput.Location = new Point(322, 512);
			PagamentoBoxInput.Name = "PagamentoBoxInput";
			PagamentoBoxInput.PlaceholderText = "R$ 0,00";
			PagamentoBoxInput.Size = new Size(209, 44);
			PagamentoBoxInput.TabIndex = 29;
			PagamentoBoxInput.TextAlign = HorizontalAlignment.Center;
			// 
			// label10
			// 
			label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label10.AutoSize = true;
			label10.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
			label10.ForeColor = SystemColors.Window;
			label10.Location = new Point(12, 513);
			label10.Name = "label10";
			label10.Size = new Size(298, 32);
			label10.TabIndex = 30;
			label10.Text = "Efetuar Pagamento (F1):";
			// 
			// label11
			// 
			label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label11.AutoSize = true;
			label11.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
			label11.ForeColor = SystemColors.Window;
			label11.Location = new Point(707, 330);
			label11.Name = "label11";
			label11.Size = new Size(169, 32);
			label11.TabIndex = 31;
			label11.Text = "Falta Receber";
			// 
			// FaltaReceberValorBox
			// 
			FaltaReceberValorBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			FaltaReceberValorBox.Enabled = false;
			FaltaReceberValorBox.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			FaltaReceberValorBox.Location = new Point(537, 362);
			FaltaReceberValorBox.Name = "FaltaReceberValorBox";
			FaltaReceberValorBox.PlaceholderText = "R$ 0,00";
			FaltaReceberValorBox.Size = new Size(335, 44);
			FaltaReceberValorBox.TabIndex = 32;
			FaltaReceberValorBox.TextAlign = HorizontalAlignment.Center;
			// 
			// ColunaMDPTaxa
			// 
			ColunaMDPTaxa.Text = "Taxa";
			ColunaMDPTaxa.Width = 130;
			// 
			// JanelaDePagamento
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(884, 561);
			ControlBox = false;
			Controls.Add(FaltaReceberValorBox);
			Controls.Add(label11);
			Controls.Add(label10);
			Controls.Add(PagamentoBoxInput);
			Controls.Add(listView1);
			Controls.Add(textBox8);
			Controls.Add(label9);
			Controls.Add(label8);
			Controls.Add(textBox7);
			Controls.Add(label7);
			Controls.Add(label6);
			Controls.Add(textBox6);
			Controls.Add(ListaMeioDePagamento);
			Controls.Add(TrocoBox);
			Controls.Add(label5);
			Controls.Add(label1);
			Controls.Add(ValorRecebidoBox);
			Controls.Add(button1);
			Controls.Add(button2);
			Controls.Add(ListaPagamentosEfetuados);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(ValorTotalBox);
			Controls.Add(PorcentagemDescontoBoxInput);
			Controls.Add(ValorTotalComDescontoBox);
			Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			KeyPreview = true;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "JanelaDePagamento";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Labs PDV - Pagamento";
			KeyUp += OnJanelaDePagamentoKeyUp;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private TextBox ValorTotalComDescontoBox;
		private TextBox PorcentagemDescontoBoxInput;
		private TextBox ValorTotalBox;
		private Label label2;
		private Label label3;
		private Label label4;
		private ListView ListaPagamentosEfetuados;
		private Button button2;
		private Button button1;
		private TextBox ValorRecebidoBox;
		private Label label1;
		private Label label5;
		private TextBox TrocoBox;
		private ListView ListaMeioDePagamento;
		private TextBox textBox6;
		private Label label6;
		private ColumnHeader ColunaMeioDePagamento;
		private Label label7;
		private TextBox textBox7;
		private Label label8;
		private Label label9;
		private TextBox textBox8;
		private ColumnHeader ColunaPagamentoEfetuado;
		private ColumnHeader ColunaTaxaDoPagamento;
		private ListView listView1;
		private ColumnHeader ColunaParcelas;
		private TextBox PagamentoBoxInput;
		private Label label10;
		private Label label11;
		private TextBox FaltaReceberValorBox;
		private ColumnHeader ColunaMDPTaxa;
	}
}