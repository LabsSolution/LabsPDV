namespace Labs.Janelas.Configuracoes.Dependencias
{
    partial class DatabaseConfig
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
            CloudURIBox = new TextBox();
            label1 = new Label();
            LocalURIBox = new TextBox();
            label2 = new Label();
            VisualizarLocalURIButton = new Button();
            VisualizarCloudURIButton = new Button();
            SalvarButton = new Button();
            SairButton = new Button();
            SuspendLayout();
            // 
            // CloudURIBox
            // 
            CloudURIBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CloudURIBox.Location = new Point(12, 38);
            CloudURIBox.Name = "CloudURIBox";
            CloudURIBox.PasswordChar = '•';
            CloudURIBox.Size = new Size(332, 29);
            CloudURIBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(165, 21);
            label1.TabIndex = 1;
            label1.Text = "Cloud Database URI:";
            // 
            // LocalURIBox
            // 
            LocalURIBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LocalURIBox.Location = new Point(12, 94);
            LocalURIBox.Name = "LocalURIBox";
            LocalURIBox.PasswordChar = '•';
            LocalURIBox.Size = new Size(332, 29);
            LocalURIBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.Window;
            label2.Location = new Point(12, 70);
            label2.Name = "label2";
            label2.Size = new Size(160, 21);
            label2.TabIndex = 3;
            label2.Text = "Local Database URI:";
            // 
            // VisualizarLocalURIButton
            // 
            VisualizarLocalURIButton.BackColor = Color.Transparent;
            VisualizarLocalURIButton.FlatAppearance.BorderColor = SystemColors.Window;
            VisualizarLocalURIButton.FlatAppearance.MouseDownBackColor = SystemColors.ScrollBar;
            VisualizarLocalURIButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            VisualizarLocalURIButton.FlatStyle = FlatStyle.Flat;
            VisualizarLocalURIButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            VisualizarLocalURIButton.ForeColor = SystemColors.Window;
            VisualizarLocalURIButton.Location = new Point(350, 94);
            VisualizarLocalURIButton.Name = "VisualizarLocalURIButton";
            VisualizarLocalURIButton.Size = new Size(96, 30);
            VisualizarLocalURIButton.TabIndex = 4;
            VisualizarLocalURIButton.Text = "Visualizar";
            VisualizarLocalURIButton.UseVisualStyleBackColor = false;
            VisualizarLocalURIButton.MouseDown += VisualizarLocalURIButton_MouseDown;
            VisualizarLocalURIButton.MouseUp += VisualizarLocalURIButton_MouseUp;
            // 
            // VisualizarCloudURIButton
            // 
            VisualizarCloudURIButton.BackColor = Color.Transparent;
            VisualizarCloudURIButton.FlatAppearance.BorderColor = SystemColors.Window;
            VisualizarCloudURIButton.FlatAppearance.MouseDownBackColor = SystemColors.ScrollBar;
            VisualizarCloudURIButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            VisualizarCloudURIButton.FlatStyle = FlatStyle.Flat;
            VisualizarCloudURIButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            VisualizarCloudURIButton.ForeColor = SystemColors.Window;
            VisualizarCloudURIButton.Location = new Point(350, 37);
            VisualizarCloudURIButton.Name = "VisualizarCloudURIButton";
            VisualizarCloudURIButton.Size = new Size(96, 30);
            VisualizarCloudURIButton.TabIndex = 5;
            VisualizarCloudURIButton.Text = "Visualizar";
            VisualizarCloudURIButton.UseVisualStyleBackColor = false;
            VisualizarCloudURIButton.MouseDown += VisualizarCloudURIButton_MouseDown;
            VisualizarCloudURIButton.MouseUp += VisualizarCloudURIButton_MouseUp;
            // 
            // SalvarButton
            // 
            SalvarButton.BackColor = Color.Transparent;
            SalvarButton.FlatAppearance.BorderColor = SystemColors.Window;
            SalvarButton.FlatAppearance.MouseDownBackColor = SystemColors.ScrollBar;
            SalvarButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            SalvarButton.FlatStyle = FlatStyle.Flat;
            SalvarButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SalvarButton.ForeColor = SystemColors.Window;
            SalvarButton.Location = new Point(546, 558);
            SalvarButton.Name = "SalvarButton";
            SalvarButton.Size = new Size(156, 30);
            SalvarButton.TabIndex = 6;
            SalvarButton.Text = "Salvar Alterações";
            SalvarButton.UseVisualStyleBackColor = false;
            SalvarButton.Click += SalvarButton_Click;
            // 
            // SairButton
            // 
            SairButton.BackColor = Color.Transparent;
            SairButton.FlatAppearance.BorderColor = SystemColors.Window;
            SairButton.FlatAppearance.MouseDownBackColor = SystemColors.ScrollBar;
            SairButton.FlatAppearance.MouseOverBackColor = SystemColors.GrayText;
            SairButton.FlatStyle = FlatStyle.Flat;
            SairButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SairButton.ForeColor = SystemColors.Window;
            SairButton.Location = new Point(708, 558);
            SairButton.Name = "SairButton";
            SairButton.Size = new Size(80, 30);
            SairButton.TabIndex = 7;
            SairButton.Text = "Sair";
            SairButton.UseVisualStyleBackColor = false;
            SairButton.Click += SairButton_Click;
            // 
            // DatabaseConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(800, 600);
            ControlBox = false;
            Controls.Add(SairButton);
            Controls.Add(SalvarButton);
            Controls.Add(VisualizarCloudURIButton);
            Controls.Add(VisualizarLocalURIButton);
            Controls.Add(label2);
            Controls.Add(LocalURIBox);
            Controls.Add(label1);
            Controls.Add(CloudURIBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DatabaseConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DatabaseConfig";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox CloudURIBox;
        private Label label1;
        private TextBox LocalURIBox;
        private Label label2;
        private Button VisualizarLocalURIButton;
        private Button VisualizarCloudURIButton;
        private Button SalvarButton;
        private Button SairButton;
    }
}