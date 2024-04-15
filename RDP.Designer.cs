namespace LabsPDV
{
	partial class RDP
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
			RDPLabel1 = new Label();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			NomeInput = new TextBox();
			QuantidadeInput = new TextBox();
			PrecoInput = new TextBox();
			CodigoInput = new TextBox();
			RegistrarButton = new Button();
			ListaProdutos = new ListBox();
			SuspendLayout();
			// 
			// RDPLabel1
			// 
			RDPLabel1.AutoSize = true;
			RDPLabel1.Font = new Font("Tahoma", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			RDPLabel1.Location = new Point(12, 9);
			RDPLabel1.Name = "RDPLabel1";
			RDPLabel1.Size = new Size(120, 35);
			RDPLabel1.TabIndex = 0;
			RDPLabel1.Text = "Nome: ";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Tahoma", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.Location = new Point(12, 44);
			label1.Name = "label1";
			label1.Size = new Size(194, 35);
			label1.TabIndex = 1;
			label1.Text = "Quantidade:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Tahoma", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.Location = new Point(12, 79);
			label2.Name = "label2";
			label2.Size = new Size(108, 35);
			label2.TabIndex = 2;
			label2.Text = "Preço:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Tahoma", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label3.Location = new Point(12, 114);
			label3.Name = "label3";
			label3.Size = new Size(126, 35);
			label3.TabIndex = 3;
			label3.Text = "Código:";
			// 
			// NomeInput
			// 
			NomeInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			NomeInput.Location = new Point(212, 15);
			NomeInput.Name = "NomeInput";
			NomeInput.Size = new Size(302, 29);
			NomeInput.TabIndex = 4;
			// 
			// QuantidadeInput
			// 
			QuantidadeInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			QuantidadeInput.Location = new Point(212, 50);
			QuantidadeInput.Name = "QuantidadeInput";
			QuantidadeInput.Size = new Size(302, 29);
			QuantidadeInput.TabIndex = 5;
			// 
			// PrecoInput
			// 
			PrecoInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			PrecoInput.Location = new Point(212, 85);
			PrecoInput.Name = "PrecoInput";
			PrecoInput.Size = new Size(302, 29);
			PrecoInput.TabIndex = 6;
			// 
			// CodigoInput
			// 
			CodigoInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			CodigoInput.Location = new Point(212, 120);
			CodigoInput.Name = "CodigoInput";
			CodigoInput.Size = new Size(302, 29);
			CodigoInput.TabIndex = 7;
			// 
			// RegistrarButton
			// 
			RegistrarButton.Location = new Point(212, 155);
			RegistrarButton.Name = "RegistrarButton";
			RegistrarButton.Size = new Size(302, 45);
			RegistrarButton.TabIndex = 8;
			RegistrarButton.Text = "RegistrarProduto";
			RegistrarButton.UseVisualStyleBackColor = true;
			RegistrarButton.Click += RegistrarButton_Click;
			// 
			// ListaProdutos
			// 
			ListaProdutos.Font = new Font("Verdana", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ListaProdutos.FormattingEnabled = true;
			ListaProdutos.ItemHeight = 18;
			ListaProdutos.Location = new Point(600, 9);
			ListaProdutos.Name = "ListaProdutos";
			ListaProdutos.Size = new Size(468, 544);
			ListaProdutos.TabIndex = 9;
			// 
			// RDP
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(1080, 577);
			Controls.Add(ListaProdutos);
			Controls.Add(RegistrarButton);
			Controls.Add(CodigoInput);
			Controls.Add(PrecoInput);
			Controls.Add(QuantidadeInput);
			Controls.Add(NomeInput);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(RDPLabel1);
			Name = "RDP";
			Text = "RDP";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label RDPLabel1;
		private Label label1;
		private Label label2;
		private Label label3;
		private TextBox NomeInput;
		private TextBox QuantidadeInput;
		private TextBox PrecoInput;
		private TextBox CodigoInput;
		private Button RegistrarButton;
		private ListBox ListaProdutos;
	}
}