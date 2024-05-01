namespace Labs
{
    partial class PainelDeLogin
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
            testeBox.BackColor = SystemColors.WindowFrame;
            testeBox.Dock = DockStyle.Fill;
            testeBox.Location = new Point(0, 0);
            testeBox.Name = "testeBox";
            testeBox.Size = new Size(800, 600);
            testeBox.SizeMode = PictureBoxSizeMode.StretchImage;
            testeBox.TabIndex = 0;
            testeBox.TabStop = false;
            // 
            // PainelDeLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(testeBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PainelDeLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Labs Login";
            ((System.ComponentModel.ISupportInitialize)testeBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox testeBox;
	}
}