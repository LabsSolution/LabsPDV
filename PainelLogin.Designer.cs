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
            label1 = new Label();
            label2 = new Label();
            UserNameInputBox = new TextBox();
            PasswordInputBox = new TextBox();
            LoginButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(12, 158);
            label1.Name = "label1";
            label1.Size = new Size(98, 30);
            label1.TabIndex = 1;
            label1.Text = "Usuário:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label2.Location = new Point(12, 223);
            label2.Name = "label2";
            label2.Size = new Size(81, 30);
            label2.TabIndex = 3;
            label2.Text = "Senha:";
            // 
            // UserNameInputBox
            // 
            UserNameInputBox.Font = new Font("Segoe UI", 12F);
            UserNameInputBox.Location = new Point(12, 191);
            UserNameInputBox.Name = "UserNameInputBox";
            UserNameInputBox.Size = new Size(244, 29);
            UserNameInputBox.TabIndex = 4;
            // 
            // PasswordInputBox
            // 
            PasswordInputBox.Font = new Font("Segoe UI", 12F);
            PasswordInputBox.Location = new Point(12, 256);
            PasswordInputBox.Name = "PasswordInputBox";
            PasswordInputBox.PasswordChar = '*';
            PasswordInputBox.Size = new Size(244, 29);
            PasswordInputBox.TabIndex = 5;
            // 
            // LoginButton
            // 
            LoginButton.BackColor = Color.Transparent;
            LoginButton.BackgroundImageLayout = ImageLayout.Zoom;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            LoginButton.ForeColor = SystemColors.Window;
            LoginButton.ImageAlign = ContentAlignment.MiddleLeft;
            LoginButton.Location = new Point(12, 291);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(244, 47);
            LoginButton.TabIndex = 6;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = false;
            // 
            // PainelLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 560);
            ControlBox = false;
            Controls.Add(LoginButton);
            Controls.Add(PasswordInputBox);
            Controls.Add(UserNameInputBox);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PainelLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PainelLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private TextBox UserNameInputBox;
        private TextBox PasswordInputBox;
        private Button LoginButton;
    }
}