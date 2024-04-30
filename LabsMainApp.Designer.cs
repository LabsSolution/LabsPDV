namespace Labs
{
	partial class LabsMainApp
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
            LabsEstoqueButton = new Button();
            LabsPDV = new Button();
            SairButton = new Button();
            ConfiguracaoButton = new Button();
            SuspendLayout();
            // 
            // LabsEstoqueButton
            // 
            LabsEstoqueButton.BackColor = SystemColors.Window;
            LabsEstoqueButton.BackgroundImageLayout = ImageLayout.None;
            LabsEstoqueButton.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            LabsEstoqueButton.Location = new Point(12, 12);
            LabsEstoqueButton.Name = "LabsEstoqueButton";
            LabsEstoqueButton.Size = new Size(160, 80);
            LabsEstoqueButton.TabIndex = 0;
            LabsEstoqueButton.Text = "Labs Estoque";
            LabsEstoqueButton.UseVisualStyleBackColor = false;
            LabsEstoqueButton.Click += OnLabsEstoqueClick;
            // 
            // LabsPDV
            // 
            LabsPDV.BackColor = SystemColors.Window;
            LabsPDV.BackgroundImageLayout = ImageLayout.None;
            LabsPDV.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            LabsPDV.Location = new Point(178, 12);
            LabsPDV.Name = "LabsPDV";
            LabsPDV.Size = new Size(160, 80);
            LabsPDV.TabIndex = 1;
            LabsPDV.Text = "Labs PDV";
            LabsPDV.UseVisualStyleBackColor = false;
            LabsPDV.Click += OnLabsPDVClick;
            // 
            // SairButton
            // 
            SairButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SairButton.BackColor = SystemColors.Window;
            SairButton.BackgroundImageLayout = ImageLayout.None;
            SairButton.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            SairButton.Location = new Point(990, 671);
            SairButton.Name = "SairButton";
            SairButton.Size = new Size(160, 42);
            SairButton.TabIndex = 2;
            SairButton.Text = "Sair";
            SairButton.UseVisualStyleBackColor = false;
            SairButton.Click += SairButton_Click;
            // 
            // ConfiguracaoButton
            // 
            ConfiguracaoButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ConfiguracaoButton.BackColor = SystemColors.Window;
            ConfiguracaoButton.BackgroundImageLayout = ImageLayout.None;
            ConfiguracaoButton.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            ConfiguracaoButton.Location = new Point(1156, 671);
            ConfiguracaoButton.Name = "ConfiguracaoButton";
            ConfiguracaoButton.Size = new Size(190, 42);
            ConfiguracaoButton.TabIndex = 3;
            ConfiguracaoButton.Text = "Configurações";
            ConfiguracaoButton.UseVisualStyleBackColor = false;
            ConfiguracaoButton.Click += OnLabsConfigClick;
            // 
            // LabsMainApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1346, 725);
            ControlBox = false;
            Controls.Add(ConfiguracaoButton);
            Controls.Add(SairButton);
            Controls.Add(LabsPDV);
            Controls.Add(LabsEstoqueButton);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LabsMainApp";
            Text = "Lab Soluções";
            WindowState = FormWindowState.Maximized;
            Load += LabsMainApp_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button LabsEstoqueButton;
		private Button LabsPDV;
		private Button SairButton;
		private Button ConfiguracaoButton;
	}
}