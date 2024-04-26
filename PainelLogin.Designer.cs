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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PainelLogin));
            UserNameInputBox = new Krypton.Toolkit.KryptonTextBox();
            label1 = new Label();
            kryptonTextBox1 = new Krypton.Toolkit.KryptonTextBox();
            label2 = new Label();
            GoogleLoginButton = new Krypton.Toolkit.KryptonButton();
            LoginPalette = new Krypton.Toolkit.KryptonCustomPaletteBase(components);
            SuspendLayout();
            // 
            // UserNameInputBox
            // 
            UserNameInputBox.CornerRoundingRadius = 12F;
            UserNameInputBox.Location = new Point(12, 177);
            UserNameInputBox.Name = "UserNameInputBox";
            UserNameInputBox.Size = new Size(235, 43);
            UserNameInputBox.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            UserNameInputBox.StateCommon.Border.Rounding = 12F;
            UserNameInputBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(12, 144);
            label1.Name = "label1";
            label1.Size = new Size(98, 30);
            label1.TabIndex = 1;
            label1.Text = "Usuário:";
            // 
            // kryptonTextBox1
            // 
            kryptonTextBox1.CornerRoundingRadius = 12F;
            kryptonTextBox1.Location = new Point(12, 256);
            kryptonTextBox1.Name = "kryptonTextBox1";
            kryptonTextBox1.Size = new Size(235, 43);
            kryptonTextBox1.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            kryptonTextBox1.StateCommon.Border.Rounding = 12F;
            kryptonTextBox1.TabIndex = 2;
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
            // GoogleLoginButton
            // 
            GoogleLoginButton.CornerRoundingRadius = 50F;
            GoogleLoginButton.Location = new Point(12, 331);
            GoogleLoginButton.Name = "GoogleLoginButton";
            GoogleLoginButton.OverrideDefault.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.None;
            GoogleLoginButton.OverrideDefault.Content.Draw = Krypton.Toolkit.InheritBool.False;
            GoogleLoginButton.Palette = LoginPalette;
            GoogleLoginButton.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            GoogleLoginButton.Size = new Size(235, 60);
            GoogleLoginButton.StateCommon.Back.Color1 = SystemColors.Window;
            GoogleLoginButton.StateCommon.Back.Color2 = SystemColors.Window;
            GoogleLoginButton.StateCommon.Border.Color1 = SystemColors.Window;
            GoogleLoginButton.StateCommon.Border.Color2 = SystemColors.Window;
            GoogleLoginButton.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            GoogleLoginButton.StateCommon.Border.Rounding = 50F;
            GoogleLoginButton.StateCommon.Content.LongText.Color1 = Color.Black;
            GoogleLoginButton.StateCommon.Content.LongText.Color2 = Color.Black;
            GoogleLoginButton.StateCommon.Content.ShortText.Color1 = Color.Black;
            GoogleLoginButton.StateCommon.Content.ShortText.Color2 = Color.Black;
            GoogleLoginButton.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            GoogleLoginButton.TabIndex = 5;
            GoogleLoginButton.Values.ImageTransparentColor = SystemColors.Window;
            GoogleLoginButton.Values.Text = "Login";
            // 
            // LoginPalette
            // 
            LoginPalette.BaseFont = new Font("Segoe UI", 9F);
            LoginPalette.BaseFontSize = 9F;
            LoginPalette.BasePaletteMode = Krypton.Toolkit.PaletteMode.SparkleOrangeLightMode;
            LoginPalette.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            LoginPalette.ThemeName = "";
            LoginPalette.UseKryptonFileDialogs = true;
            // 
            // PainelLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 560);
            ControlBox = false;
            Controls.Add(GoogleLoginButton);
            Controls.Add(label2);
            Controls.Add(kryptonTextBox1);
            Controls.Add(label1);
            Controls.Add(UserNameInputBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PainelLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PainelLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonTextBox UserNameInputBox;
        private Label label1;
        private Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private Label label2;
        private Krypton.Toolkit.KryptonButton GoogleLoginButton;
        private Krypton.Toolkit.KryptonCustomPaletteBase LoginPalette;
    }
}