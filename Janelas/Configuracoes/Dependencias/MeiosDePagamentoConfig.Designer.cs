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
			ModosDePagamentoLabel = new Label();
			PossuiModosCheckBox = new CheckBox();
			PossuiBandeirasCheckBox = new CheckBox();
			BandeirasLabel = new Label();
			PossuiParcelasCheckBox = new CheckBox();
			ParcelasLabel = new Label();
			UU = new Button();
			button1 = new Button();
			ModosPagamentoDropDown = new ComboBox();
			BandeirasDropDown = new ComboBox();
			ParcelasDropDown = new ComboBox();
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
			MeioDePagamentoBoxInput.Size = new Size(181, 29);
			MeioDePagamentoBoxInput.TabIndex = 1;
			MeioDePagamentoBoxInput.KeyUp += MeioDePagamentoBoxInput_KeyUp;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.Location = new Point(12, 12);
			label1.Name = "label1";
			label1.Size = new Size(168, 21);
			label1.TabIndex = 2;
			label1.Text = "Meio de Pagamento:";
			// 
			// ListaMeiosRegistrados
			// 
			ListaMeiosRegistrados.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ListaMeiosRegistrados.Columns.AddRange(new ColumnHeader[] { ColunaMeioDePagamento });
			ListaMeiosRegistrados.Location = new Point(12, 327);
			ListaMeiosRegistrados.Name = "ListaMeiosRegistrados";
			ListaMeiosRegistrados.Size = new Size(860, 176);
			ListaMeiosRegistrados.TabIndex = 3;
			ListaMeiosRegistrados.UseCompatibleStateImageBehavior = false;
			ListaMeiosRegistrados.View = View.Details;
			// 
			// ColunaMeioDePagamento
			// 
			ColunaMeioDePagamento.Text = "Meios de Pagamento Registrados";
			ColunaMeioDePagamento.Width = 300;
			// 
			// ModosDePagamentoLabel
			// 
			ModosDePagamentoLabel.AutoSize = true;
			ModosDePagamentoLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
			ParcelasLabel.Location = new Point(600, 68);
			ParcelasLabel.Name = "ParcelasLabel";
			ParcelasLabel.Size = new Size(77, 21);
			ParcelasLabel.TabIndex = 11;
			ParcelasLabel.Text = "Parcelas:";
			ParcelasLabel.Visible = false;
			// 
			// UU
			// 
			UU.Location = new Point(311, 509);
			UU.Name = "UU";
			UU.Size = new Size(75, 23);
			UU.TabIndex = 16;
			UU.Text = "UU";
			UU.UseVisualStyleBackColor = true;
			UU.Click += UU_Click;
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
			// 
			// ModosPagamentoDropDown
			// 
			ModosPagamentoDropDown.DropDownStyle = ComboBoxStyle.Simple;
			ModosPagamentoDropDown.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ModosPagamentoDropDown.FormattingEnabled = true;
			ModosPagamentoDropDown.Location = new Point(12, 92);
			ModosPagamentoDropDown.Name = "ModosPagamentoDropDown";
			ModosPagamentoDropDown.Size = new Size(272, 218);
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
			BandeirasDropDown.Size = new Size(181, 218);
			BandeirasDropDown.TabIndex = 19;
			BandeirasDropDown.Visible = false;
			BandeirasDropDown.SelectedIndexChanged += OnDropDownIndexChange;
			BandeirasDropDown.KeyUp += OnDropDownKeyUP;
			// 
			// ParcelasDropDown
			// 
			ParcelasDropDown.DropDownStyle = ComboBoxStyle.Simple;
			ParcelasDropDown.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			ParcelasDropDown.FormattingEnabled = true;
			ParcelasDropDown.Location = new Point(600, 92);
			ParcelasDropDown.Name = "ParcelasDropDown";
			ParcelasDropDown.Size = new Size(181, 218);
			ParcelasDropDown.TabIndex = 20;
			ParcelasDropDown.Visible = false;
			ParcelasDropDown.SelectedIndexChanged += OnDropDownIndexChange;
			ParcelasDropDown.KeyUp += OnDropDownKeyUP;
			// 
			// MeiosDePagamentoConfig
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(884, 561);
			ControlBox = false;
			Controls.Add(ParcelasDropDown);
			Controls.Add(BandeirasDropDown);
			Controls.Add(ModosPagamentoDropDown);
			Controls.Add(button1);
			Controls.Add(UU);
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
			FormBorderStyle = FormBorderStyle.FixedSingle;
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
		private Button UU;
		private Button button1;
		private ComboBox ModosPagamentoDropDown;
		private ComboBox BandeirasDropDown;
		private ComboBox ParcelasDropDown;
	}
}