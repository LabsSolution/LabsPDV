namespace Labs
{
    partial class TesteDeImpressora
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
            ImpressorasComboBox = new ComboBox();
            PrintButton = new Button();
            SuspendLayout();
            // 
            // ImpressorasComboBox
            // 
            ImpressorasComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ImpressorasComboBox.FormattingEnabled = true;
            ImpressorasComboBox.Location = new Point(12, 12);
            ImpressorasComboBox.Name = "ImpressorasComboBox";
            ImpressorasComboBox.Size = new Size(148, 23);
            ImpressorasComboBox.TabIndex = 0;
            // 
            // PrintButton
            // 
            PrintButton.Location = new Point(194, 12);
            PrintButton.Name = "PrintButton";
            PrintButton.Size = new Size(129, 23);
            PrintButton.TabIndex = 1;
            PrintButton.Text = "Imprimir";
            PrintButton.UseVisualStyleBackColor = true;
            PrintButton.Click += PrintButton_Click;
            // 
            // TesteDeImpressora
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(854, 646);
            Controls.Add(PrintButton);
            Controls.Add(ImpressorasComboBox);
            Name = "TesteDeImpressora";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TesteDeImpressora";
            ResumeLayout(false);
        }

        #endregion

        private ComboBox ImpressorasComboBox;
        private Button PrintButton;
    }
}