namespace Labs
{
    partial class PainelLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PainelLogin));
            SairButton = new Button();
            SuspendLayout();
            // 
            // SairButton
            // 
            SairButton.BackColor = Color.FromArgb(64, 64, 64);
            SairButton.FlatAppearance.BorderColor = Color.FromArgb(255, 255, 192);
            SairButton.FlatAppearance.BorderSize = 0;
            SairButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 128);
            SairButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
            SairButton.FlatStyle = FlatStyle.Flat;
            SairButton.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            SairButton.ForeColor = SystemColors.Window;
            SairButton.Location = new Point(668, 508);
            SairButton.Name = "SairButton";
            SairButton.Size = new Size(120, 40);
            SairButton.TabIndex = 7;
            SairButton.Text = "Sair";
            SairButton.TextAlign = ContentAlignment.TopCenter;
            SairButton.UseVisualStyleBackColor = false;
            SairButton.Click += SairButton_Click;
            // 
            // PainelLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 560);
            ControlBox = false;
            Controls.Add(SairButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PainelLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PainelLogin";
            ResumeLayout(false);
        }

        #endregion
        private Button SairButton;
    }
}