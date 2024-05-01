namespace Labs
{
    partial class svgtest
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
			testeBox = new PictureBox();
			((System.ComponentModel.ISupportInitialize)testeBox).BeginInit();
			SuspendLayout();
			// 
			// testeBox
			// 
			testeBox.Dock = DockStyle.Fill;
			testeBox.Location = new Point(0, 0);
			testeBox.Name = "testeBox";
			testeBox.Size = new Size(800, 450);
			testeBox.SizeMode = PictureBoxSizeMode.StretchImage;
			testeBox.TabIndex = 0;
			testeBox.TabStop = false;
			// 
			// svgtest
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(testeBox);
			Name = "svgtest";
			Text = "svgtest";
			((System.ComponentModel.ISupportInitialize)testeBox).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private PictureBox testeBox;
	}
}