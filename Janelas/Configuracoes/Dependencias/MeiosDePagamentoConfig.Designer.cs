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
			SairButton = new Button();
			MeioDePagamentoBoxInput = new TextBox();
			label1 = new Label();
			ListaMeiosRegistrados = new ListView();
			ColunaMeioDePagamento = new ColumnHeader();
			ColunaNumModos = new ColumnHeader();
			ColunaNumBandeiras = new ColumnHeader();
			ColunaNumParcelas = new ColumnHeader();
			ModosDePagamentoLabel = new Label();
			PossuiModosCheckBox = new CheckBox();
			PossuiBandeirasCheckBox = new CheckBox();
			BandeirasLabel = new Label();
			PossuiParcelasCheckBox = new CheckBox();
			ParcelasLabel = new Label();
			button1 = new Button();
			ModosPagamentoDropDown = new ComboBox();
			BandeirasDropDown = new ComboBox();
			RemoverModoButton = new Button();
			RemoverBandeiraButton = new Button();
			SemLimiteDeValor = new CheckBox();
			label2 = new Label();
			label3 = new Label();
			button2 = new Button();
			ParcelasBoxInput = new TextBox();
			NParcelaLabel = new Label();
			SuspendLayout();
			// 
			// SairButton
			// 
			SairButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			SairButton.BackColor = Color.FromArgb(255, 128, 128);
			SairButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			SairButton.Location = new Point(752, 509);
			SairButton.Name = "SairButton";
			SairButton.Size = new Size(120, 40);
			SairButton.TabIndex = 0;
			SairButton.Text = "Sair";
			SairButton.UseVisualStyleBackColor = false;
			SairButton.Click += SairButton_Click;
			// 
			// MeioDePagamentoBoxInput
			// 
			MeioDePagamentoBoxInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			MeioDePagamentoBoxInput.Location = new Point(12, 36);
			MeioDePagamentoBoxInput.Name = "MeioDePagamentoBoxInput";
			MeioDePagamentoBoxInput.PlaceholderText = "Nome do Meio";
			MeioDePagamentoBoxInput.Size = new Size(272, 29);
			MeioDePagamentoBoxInput.TabIndex = 1;
			MeioDePagamentoBoxInput.KeyUp += MeioDePagamentoBoxInput_KeyUp;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.ForeColor = SystemColors.Window;
			label1.Location = new Point(12, 12);
			label1.Name = "label1";
			label1.Size = new Size(168, 21);
			label1.TabIndex = 2;
			label1.Text = "Meio de Pagamento:";
			// 
			// ListaMeiosRegistrados
			// 
			ListaMeiosRegistrados.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ListaMeiosRegistrados.BackColor = SystemColors.Window;
			ListaMeiosRegistrados.Columns.AddRange(new ColumnHeader[] { ColunaMeioDePagamento, ColunaNumModos, ColunaNumBandeiras, ColunaNumParcelas });
			ListaMeiosRegistrados.FullRowSelect = true;
			ListaMeiosRegistrados.GridLines = true;
			ListaMeiosRegistrados.Location = new Point(12, 348);
			ListaMeiosRegistrados.MultiSelect = false;
			ListaMeiosRegistrados.Name = "ListaMeiosRegistrados";
			ListaMeiosRegistrados.Size = new Size(860, 155);
			ListaMeiosRegistrados.TabIndex = 3;
			ListaMeiosRegistrados.UseCompatibleStateImageBehavior = false;
			ListaMeiosRegistrados.View = View.Details;
			ListaMeiosRegistrados.Click += OnListaMeiosRegistradosClick;
			// 
			// ColunaMeioDePagamento
			// 
			ColunaMeioDePagamento.Text = "Meios Registrados";
			ColunaMeioDePagamento.Width = 150;
			// 
			// ColunaNumModos
			// 
			ColunaNumModos.Text = "Num. Modos";
			ColunaNumModos.TextAlign = HorizontalAlignment.Center;
			ColunaNumModos.Width = 100;
			// 
			// ColunaNumBandeiras
			// 
			ColunaNumBandeiras.Text = "Num. Bandeiras";
			ColunaNumBandeiras.TextAlign = HorizontalAlignment.Center;
			ColunaNumBandeiras.Width = 120;
			// 
			// ColunaNumParcelas
			// 
			ColunaNumParcelas.Text = "Num. Parcelas";
			ColunaNumParcelas.TextAlign = HorizontalAlignment.Center;
			ColunaNumParcelas.Width = 100;
			// 
			// ModosDePagamentoLabel
			// 
			ModosDePagamentoLabel.AutoSize = true;
			ModosDePagamentoLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ModosDePagamentoLabel.ForeColor = SystemColors.Window;
			ModosDePagamentoLabel.Location = new Point(12, 68);
			ModosDePagamentoLabel.Name = "ModosDePagamentoLabel";
			ModosDePagamentoLabel.Size = new Size(181, 21);
			ModosDePagamentoLabel.TabIndex = 5;
			ModosDePagamentoLabel.Text = "Modos de Pagamento:";
			// 
			// PossuiModosCheckBox
			// 
			PossuiModosCheckBox.AutoSize = true;
			PossuiModosCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PossuiModosCheckBox.ForeColor = SystemColors.Window;
			PossuiModosCheckBox.Location = new Point(199, 68);
			PossuiModosCheckBox.Name = "PossuiModosCheckBox";
			PossuiModosCheckBox.Size = new Size(85, 25);
			PossuiModosCheckBox.TabIndex = 6;
			PossuiModosCheckBox.Text = "Possui?";
			PossuiModosCheckBox.UseVisualStyleBackColor = true;
			PossuiModosCheckBox.CheckedChanged += OnPossuiCheckChange;
			// 
			// PossuiBandeirasCheckBox
			// 
			PossuiBandeirasCheckBox.AutoSize = true;
			PossuiBandeirasCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PossuiBandeirasCheckBox.ForeColor = SystemColors.Window;
			PossuiBandeirasCheckBox.Location = new Point(441, 68);
			PossuiBandeirasCheckBox.Name = "PossuiBandeirasCheckBox";
			PossuiBandeirasCheckBox.Size = new Size(85, 25);
			PossuiBandeirasCheckBox.TabIndex = 9;
			PossuiBandeirasCheckBox.Text = "Possui?";
			PossuiBandeirasCheckBox.UseVisualStyleBackColor = true;
			PossuiBandeirasCheckBox.Visible = false;
			PossuiBandeirasCheckBox.CheckedChanged += OnPossuiCheckChange;
			// 
			// BandeirasLabel
			// 
			BandeirasLabel.AutoSize = true;
			BandeirasLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BandeirasLabel.ForeColor = SystemColors.Window;
			BandeirasLabel.Location = new Point(345, 68);
			BandeirasLabel.Name = "BandeirasLabel";
			BandeirasLabel.Size = new Size(89, 21);
			BandeirasLabel.TabIndex = 8;
			BandeirasLabel.Text = "Bandeiras:";
			BandeirasLabel.Visible = false;
			// 
			// PossuiParcelasCheckBox
			// 
			PossuiParcelasCheckBox.AutoSize = true;
			PossuiParcelasCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			PossuiParcelasCheckBox.ForeColor = SystemColors.Window;
			PossuiParcelasCheckBox.Location = new Point(696, 68);
			PossuiParcelasCheckBox.Name = "PossuiParcelasCheckBox";
			PossuiParcelasCheckBox.Size = new Size(85, 25);
			PossuiParcelasCheckBox.TabIndex = 12;
			PossuiParcelasCheckBox.Text = "Possui?";
			PossuiParcelasCheckBox.UseVisualStyleBackColor = true;
			PossuiParcelasCheckBox.Visible = false;
			PossuiParcelasCheckBox.CheckedChanged += OnPossuiCheckChange;
			// 
			// ParcelasLabel
			// 
			ParcelasLabel.AutoSize = true;
			ParcelasLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ParcelasLabel.ForeColor = SystemColors.Window;
			ParcelasLabel.Location = new Point(600, 68);
			ParcelasLabel.Name = "ParcelasLabel";
			ParcelasLabel.Size = new Size(77, 21);
			ParcelasLabel.TabIndex = 11;
			ParcelasLabel.Text = "Parcelas:";
			ParcelasLabel.Visible = false;
			// 
			// button1
			// 
			button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			button1.BackColor = Color.FromArgb(128, 255, 128);
			button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			button1.Location = new Point(12, 509);
			button1.Name = "button1";
			button1.Size = new Size(272, 40);
			button1.TabIndex = 17;
			button1.Text = "Adicionar Meio";
			button1.UseVisualStyleBackColor = false;
			button1.Click += AdicionarMeioDePagamento_Click;
			// 
			// ModosPagamentoDropDown
			// 
			ModosPagamentoDropDown.DropDownStyle = ComboBoxStyle.Simple;
			ModosPagamentoDropDown.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ModosPagamentoDropDown.FormattingEnabled = true;
			ModosPagamentoDropDown.Location = new Point(12, 92);
			ModosPagamentoDropDown.Name = "ModosPagamentoDropDown";
			ModosPagamentoDropDown.Size = new Size(272, 197);
			ModosPagamentoDropDown.TabIndex = 18;
			ModosPagamentoDropDown.Visible = false;
			ModosPagamentoDropDown.SelectedIndexChanged += OnDropDownIndexChange;
			ModosPagamentoDropDown.KeyUp += OnDropDownKeyUP;
			// 
			// BandeirasDropDown
			// 
			BandeirasDropDown.DropDownStyle = ComboBoxStyle.Simple;
			BandeirasDropDown.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			BandeirasDropDown.FormattingEnabled = true;
			BandeirasDropDown.Location = new Point(345, 92);
			BandeirasDropDown.Name = "BandeirasDropDown";
			BandeirasDropDown.Size = new Size(181, 197);
			BandeirasDropDown.TabIndex = 19;
			BandeirasDropDown.Visible = false;
			BandeirasDropDown.SelectedIndexChanged += OnDropDownIndexChange;
			BandeirasDropDown.KeyUp += OnDropDownKeyUP;
			// 
			// RemoverModoButton
			// 
			RemoverModoButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			RemoverModoButton.BackColor = Color.FromArgb(255, 128, 128);
			RemoverModoButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			RemoverModoButton.Location = new Point(12, 285);
			RemoverModoButton.Name = "RemoverModoButton";
			RemoverModoButton.Size = new Size(105, 36);
			RemoverModoButton.TabIndex = 21;
			RemoverModoButton.Text = "Remover";
			RemoverModoButton.UseVisualStyleBackColor = false;
			RemoverModoButton.Visible = false;
			RemoverModoButton.Click += OnRemoverButtonClick;
			// 
			// RemoverBandeiraButton
			// 
			RemoverBandeiraButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			RemoverBandeiraButton.BackColor = Color.FromArgb(255, 128, 128);
			RemoverBandeiraButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			RemoverBandeiraButton.Location = new Point(345, 285);
			RemoverBandeiraButton.Name = "RemoverBandeiraButton";
			RemoverBandeiraButton.Size = new Size(105, 36);
			RemoverBandeiraButton.TabIndex = 22;
			RemoverBandeiraButton.Text = "Remover";
			RemoverBandeiraButton.UseVisualStyleBackColor = false;
			RemoverBandeiraButton.Visible = false;
			RemoverBandeiraButton.Click += OnRemoverButtonClick;
			// 
			// SemLimiteDeValor
			// 
			SemLimiteDeValor.AutoSize = true;
			SemLimiteDeValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			SemLimiteDeValor.ForeColor = SystemColors.Window;
			SemLimiteDeValor.Location = new Point(208, 12);
			SemLimiteDeValor.Name = "SemLimiteDeValor";
			SemLimiteDeValor.Size = new Size(76, 25);
			SemLimiteDeValor.TabIndex = 24;
			SemLimiteDeValor.Text = "SLDV?";
			SemLimiteDeValor.UseVisualStyleBackColor = true;
			SemLimiteDeValor.CheckedChanged += OnPossuiCheckChange;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.ForeColor = SystemColors.Window;
			label2.Location = new Point(618, 9);
			label2.Name = "label2";
			label2.Size = new Size(254, 21);
			label2.TabIndex = 25;
			label2.Text = "SLDV : (Sem Limitador De Valor)";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label3.ForeColor = SystemColors.Window;
			label3.Location = new Point(12, 324);
			label3.Name = "label3";
			label3.Size = new Size(153, 21);
			label3.TabIndex = 26;
			label3.Text = "Meios Registrados:";
			// 
			// button2
			// 
			button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			button2.BackColor = Color.FromArgb(255, 255, 128);
			button2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			button2.Location = new Point(290, 509);
			button2.Name = "button2";
			button2.Size = new Size(272, 40);
			button2.TabIndex = 27;
			button2.Text = "Remover Meio";
			button2.UseVisualStyleBackColor = false;
			button2.Click += RemoverMeioDePagamentoClick;
			// 
			// ParcelasBoxInput
			// 
			ParcelasBoxInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			ParcelasBoxInput.Location = new Point(706, 92);
			ParcelasBoxInput.Name = "ParcelasBoxInput";
			ParcelasBoxInput.Size = new Size(75, 29);
			ParcelasBoxInput.TabIndex = 28;
			ParcelasBoxInput.Visible = false;
			ParcelasBoxInput.KeyUp += OnDropDownKeyUP;
			// 
			// NParcelaLabel
			// 
			NParcelaLabel.AutoSize = true;
			NParcelaLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			NParcelaLabel.ForeColor = SystemColors.Window;
			NParcelaLabel.Location = new Point(600, 92);
			NParcelaLabel.Name = "NParcelaLabel";
			NParcelaLabel.Size = new Size(100, 21);
			NParcelaLabel.TabIndex = 29;
			NParcelaLabel.Text = "N° Parcelas:";
			NParcelaLabel.Visible = false;
			// 
			// MeiosDePagamentoConfig
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(884, 561);
			ControlBox = false;
			Controls.Add(NParcelaLabel);
			Controls.Add(ParcelasBoxInput);
			Controls.Add(button2);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(SemLimiteDeValor);
			Controls.Add(BandeirasDropDown);
			Controls.Add(ModosPagamentoDropDown);
			Controls.Add(button1);
			Controls.Add(PossuiParcelasCheckBox);
			Controls.Add(ParcelasLabel);
			Controls.Add(PossuiBandeirasCheckBox);
			Controls.Add(BandeirasLabel);
			Controls.Add(PossuiModosCheckBox);
			Controls.Add(ModosDePagamentoLabel);
			Controls.Add(ListaMeiosRegistrados);
			Controls.Add(label1);
			Controls.Add(MeioDePagamentoBoxInput);
			Controls.Add(SairButton);
			Controls.Add(RemoverBandeiraButton);
			Controls.Add(RemoverModoButton);
			FormBorderStyle = FormBorderStyle.FixedSingle;
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

		private Button SairButton;
		private TextBox MeioDePagamentoBoxInput;
		private Label label1;
		private ListView ListaMeiosRegistrados;
		private ColumnHeader ColunaMeioDePagamento;
		private Label ModosDePagamentoLabel;
		private CheckBox PossuiModosCheckBox;
		private CheckBox PossuiBandeirasCheckBox;
		private Label BandeirasLabel;
		private CheckBox PossuiParcelasCheckBox;
		private Label ParcelasLabel;
		private Button button1;
		private ComboBox ModosPagamentoDropDown;
		private ComboBox BandeirasDropDown;
		private Button RemoverModoButton;
		private Button RemoverBandeiraButton;
		private ColumnHeader ColunaNumModos;
		private ColumnHeader ColunaNumBandeiras;
		private ColumnHeader ColunaNumParcelas;
		private CheckBox SemLimiteDeValor;
		private Label label2;
		private Label label3;
		private Button button2;
		private TextBox ParcelasBoxInput;
		private Label NParcelaLabel;
	}
}