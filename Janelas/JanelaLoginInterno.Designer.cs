namespace Labs.Janelas
{
    partial class JanelaLoginInterno
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
            label2 = new Label();
            label1 = new Label();
            UsernameInputBox = new TextBox();
            label3 = new Label();
            PasswordInputBox = new TextBox();
            LoginButton = new Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label2.ForeColor = SystemColors.Window;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(344, 38);
            label2.TabIndex = 1;
            label2.Text = "Login";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(12, 59);
            label1.Name = "label1";
            label1.Size = new Size(320, 32);
            label1.TabIndex = 2;
            label1.Text = "Nome de Usuário:";
            label1.TextAlign = ContentAlignment.BottomLeft;
            // 
            // UsernameInputBox
            // 
            UsernameInputBox.Font = new Font("Segoe UI", 16F);
            UsernameInputBox.Location = new Point(12, 94);
            UsernameInputBox.Name = "UsernameInputBox";
            UsernameInputBox.Size = new Size(320, 36);
            UsernameInputBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label3.ForeColor = SystemColors.Window;
            label3.Location = new Point(12, 133);
            label3.Name = "label3";
            label3.Size = new Size(320, 30);
            label3.TabIndex = 4;
            label3.Text = "Senha:";
            label3.TextAlign = ContentAlignment.BottomLeft;
            // 
            // PasswordInputBox
            // 
            PasswordInputBox.Font = new Font("Segoe UI", 16F);
            PasswordInputBox.Location = new Point(12, 166);
            PasswordInputBox.Name = "PasswordInputBox";
            PasswordInputBox.PasswordChar = '•';
            PasswordInputBox.Size = new Size(320, 36);
            PasswordInputBox.TabIndex = 5;
            // 
            // LoginButton
            // 
            LoginButton.FlatAppearance.BorderColor = SystemColors.Window;
            LoginButton.FlatAppearance.BorderSize = 3;
            LoginButton.FlatAppearance.MouseDownBackColor = SystemColors.ButtonShadow;
            LoginButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            LoginButton.ForeColor = SystemColors.Window;
            LoginButton.Location = new Point(12, 261);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(320, 67);
            LoginButton.TabIndex = 6;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // JanelaLoginInterno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(344, 336);
            Controls.Add(LoginButton);
            Controls.Add(PasswordInputBox);
            Controls.Add(label3);
            Controls.Add(UsernameInputBox);
            Controls.Add(label1);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "JanelaLoginInterno";
            Text = "Login de Operador";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Label label1;
        private TextBox UsernameInputBox;
        private Label label3;
        private TextBox PasswordInputBox;
        private Button LoginButton;
    }
}