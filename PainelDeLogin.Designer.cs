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
            RealizarLoginButton = new Button();
            SairButton = new Button();
            SuspendLayout();
            // 
            // RealizarLoginButton
            // 
            RealizarLoginButton.BackColor = Color.Transparent;
            RealizarLoginButton.FlatAppearance.BorderColor = SystemColors.Window;
            RealizarLoginButton.FlatAppearance.BorderSize = 2;
            RealizarLoginButton.FlatAppearance.MouseDownBackColor = SystemColors.ButtonShadow;
            RealizarLoginButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            RealizarLoginButton.FlatStyle = FlatStyle.Flat;
            RealizarLoginButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RealizarLoginButton.ForeColor = SystemColors.Window;
            RealizarLoginButton.Location = new Point(462, 528);
            RealizarLoginButton.Name = "RealizarLoginButton";
            RealizarLoginButton.Size = new Size(160, 60);
            RealizarLoginButton.TabIndex = 0;
            RealizarLoginButton.Text = "Realizar Login";
            RealizarLoginButton.UseVisualStyleBackColor = false;
            RealizarLoginButton.Click += RealizarLoginButton_Click;
            // 
            // SairButton
            // 
            SairButton.BackColor = Color.Transparent;
            SairButton.FlatAppearance.BorderColor = SystemColors.Window;
            SairButton.FlatAppearance.BorderSize = 2;
            SairButton.FlatAppearance.MouseDownBackColor = SystemColors.ButtonShadow;
            SairButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            SairButton.FlatStyle = FlatStyle.Flat;
            SairButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SairButton.ForeColor = SystemColors.Window;
            SairButton.Location = new Point(628, 528);
            SairButton.Name = "SairButton";
            SairButton.Size = new Size(160, 60);
            SairButton.TabIndex = 1;
            SairButton.Text = "Sair";
            SairButton.UseVisualStyleBackColor = false;
            SairButton.Click += SairButton_Click;
            // 
            // PainelDeLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 600);
            Controls.Add(SairButton);
            Controls.Add(RealizarLoginButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PainelDeLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Labs Login";
            ResumeLayout(false);
        }

        #endregion

        private Button RealizarLoginButton;
        private Button SairButton;
    }
}