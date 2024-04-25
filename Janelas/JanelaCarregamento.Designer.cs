namespace Labs.Janelas
{
	partial class JanelaCarregamento
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
			LoadingBar = new ProgressBar();
			TituloLabel = new Label();
			SuspendLayout();
			// 
			// LoadingBar
			// 
			LoadingBar.Location = new Point(12, 226);
			LoadingBar.Name = "LoadingBar";
			LoadingBar.Size = new Size(260, 23);
			LoadingBar.TabIndex = 0;
			// 
			// TituloLabel
			// 
			TituloLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
			TituloLabel.Location = new Point(12, 9);
			TituloLabel.Name = "TituloLabel";
			TituloLabel.Size = new Size(260, 214);
			TituloLabel.TabIndex = 1;
			TituloLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// JanelaCarregamento
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			ClientSize = new Size(284, 261);
			ControlBox = false;
			Controls.Add(TituloLabel);
			Controls.Add(LoadingBar);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "JanelaCarregamento";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Labs";
			ResumeLayout(false);
		}

		#endregion

		private ProgressBar LoadingBar;
		private Label TituloLabel;
	}
}