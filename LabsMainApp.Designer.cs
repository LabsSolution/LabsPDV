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
			button1 = new Button();
			SuspendLayout();
			// 
			// button1
			// 
			button1.BackColor = Color.Salmon;
			button1.BackgroundImageLayout = ImageLayout.None;
			button1.Font = new Font("Segoe UI Symbol", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
			button1.Location = new Point(12, 12);
			button1.Name = "button1";
			button1.Size = new Size(141, 81);
			button1.TabIndex = 0;
			button1.Text = "Labs Estoque";
			button1.UseVisualStyleBackColor = false;
			button1.Click += OnLabsEstoqueClick;
			// 
			// LabsMainApp
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.WindowFrame;
			BackgroundImageLayout = ImageLayout.Stretch;
			ClientSize = new Size(1584, 1061);
			Controls.Add(button1);
			Name = "LabsMainApp";
			Text = "LabsMainApp";
			WindowState = FormWindowState.Maximized;
			ResumeLayout(false);
		}

		#endregion

		private Button button1;
	}
}