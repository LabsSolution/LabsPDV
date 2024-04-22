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
			// 
			// MeiosDePagamentoConfig
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(884, 561);
			ControlBox = false;
			Controls.Add(SairButton);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "MeiosDePagamentoConfig";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "MeiosDePagamentoConfig";
			Load += MeiosDePagamentoConfig_Load;
			ResumeLayout(false);
		}

		#endregion

		private Button SairButton;
	}
}