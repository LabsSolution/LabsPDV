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
			SuspendLayout();
			// 
			// MeiosDePagamentoConfigButton
			// 
			MeiosDePagamentoConfigButton.BackColor = Color.LightSkyBlue;
			MeiosDePagamentoConfigButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			MeiosDePagamentoConfigButton.Location = new Point(12, 12);
			MeiosDePagamentoConfigButton.Name = "MeiosDePagamentoConfigButton";
			MeiosDePagamentoConfigButton.Size = new Size(235, 50);
			MeiosDePagamentoConfigButton.TabIndex = 0;
			MeiosDePagamentoConfigButton.Text = "Meios de Pagamento";
			MeiosDePagamentoConfigButton.UseVisualStyleBackColor = false;
			MeiosDePagamentoConfigButton.Click += MeiosDePagamentoConfigButton_Click;
			// 
			// LabsConfig
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(884, 561);
			Controls.Add(MeiosDePagamentoConfigButton);
			Name = "LabsConfig";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "LabsConfig";
			ResumeLayout(false);
		}

		#endregion

		private Button MeiosDePagamentoConfigButton;
	}
}