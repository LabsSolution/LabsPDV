namespace Labs.Janelas.Configuracoes
{
	partial class LabsConfig
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
            MeiosDePagamentoConfigButton = new Button();
            SairButton = new Button();
            DataBaseConfigsButton = new Button();
            SuspendLayout();
            // 
            // MeiosDePagamentoConfigButton
            // 
            MeiosDePagamentoConfigButton.BackColor = SystemColors.Window;
            MeiosDePagamentoConfigButton.FlatAppearance.BorderSize = 0;
            MeiosDePagamentoConfigButton.FlatStyle = FlatStyle.Flat;
            MeiosDePagamentoConfigButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            MeiosDePagamentoConfigButton.Location = new Point(12, 12);
            MeiosDePagamentoConfigButton.Name = "MeiosDePagamentoConfigButton";
            MeiosDePagamentoConfigButton.Size = new Size(235, 50);
            MeiosDePagamentoConfigButton.TabIndex = 0;
            MeiosDePagamentoConfigButton.Text = "Meios de Pagamento";
            MeiosDePagamentoConfigButton.UseVisualStyleBackColor = false;
            MeiosDePagamentoConfigButton.Click += MeiosDePagamentoConfigButton_Click;
            // 
            // SairButton
            // 
            SairButton.BackColor = SystemColors.Window;
            SairButton.FlatAppearance.BorderSize = 0;
            SairButton.FlatStyle = FlatStyle.Flat;
            SairButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            SairButton.Location = new Point(792, 499);
            SairButton.Name = "SairButton";
            SairButton.Size = new Size(80, 50);
            SairButton.TabIndex = 1;
            SairButton.Text = "Sair";
            SairButton.UseVisualStyleBackColor = false;
            SairButton.Click += SairButton_Click;
            // 
            // DataBaseConfigsButton
            // 
            DataBaseConfigsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DataBaseConfigsButton.BackColor = SystemColors.Window;
            DataBaseConfigsButton.FlatAppearance.BorderSize = 0;
            DataBaseConfigsButton.FlatStyle = FlatStyle.Flat;
            DataBaseConfigsButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            DataBaseConfigsButton.Location = new Point(637, 12);
            DataBaseConfigsButton.Name = "DataBaseConfigsButton";
            DataBaseConfigsButton.Size = new Size(235, 50);
            DataBaseConfigsButton.TabIndex = 2;
            DataBaseConfigsButton.Text = "Database Configs";
            DataBaseConfigsButton.UseVisualStyleBackColor = false;
            DataBaseConfigsButton.Click += DataBaseConfig_Click;
            // 
            // LabsConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(884, 561);
            Controls.Add(DataBaseConfigsButton);
            Controls.Add(SairButton);
            Controls.Add(MeiosDePagamentoConfigButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LabsConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LabsConfig";
            Load += LabsConfig_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button MeiosDePagamentoConfigButton;
        private Button SairButton;
        private Button DataBaseConfigsButton;
    }
}