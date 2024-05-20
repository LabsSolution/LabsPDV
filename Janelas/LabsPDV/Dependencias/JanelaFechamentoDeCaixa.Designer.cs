namespace Labs.Janelas.LabsPDV.Dependencias
{
    partial class JanelaFechamentoDeCaixa
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
            ListaAferimentoMeios = new DataGridView();
            ColunaDescricao = new DataGridViewTextBoxColumn();
            ColunaAfericaoSistema = new DataGridViewTextBoxColumn();
            ColunaAferimentoManual = new DataGridViewTextBoxColumn();
            ListaAferimentoGeral = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            LabelDataHora = new Label();
            label1 = new Label();
            label2 = new Label();
            VoltarButton = new Button();
            RealizarFechamentoButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ListaAferimentoMeios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ListaAferimentoGeral).BeginInit();
            SuspendLayout();
            // 
            // ListaAferimentoMeios
            // 
            ListaAferimentoMeios.AllowUserToAddRows = false;
            ListaAferimentoMeios.AllowUserToDeleteRows = false;
            ListaAferimentoMeios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListaAferimentoMeios.Columns.AddRange(new DataGridViewColumn[] { ColunaDescricao, ColunaAfericaoSistema, ColunaAferimentoManual });
            ListaAferimentoMeios.Location = new Point(12, 37);
            ListaAferimentoMeios.Name = "ListaAferimentoMeios";
            ListaAferimentoMeios.Size = new Size(776, 283);
            ListaAferimentoMeios.TabIndex = 0;
            // 
            // ColunaDescricao
            // 
            ColunaDescricao.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColunaDescricao.HeaderText = "Descrição";
            ColunaDescricao.Name = "ColunaDescricao";
            ColunaDescricao.ReadOnly = true;
            // 
            // ColunaAfericaoSistema
            // 
            ColunaAfericaoSistema.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColunaAfericaoSistema.FillWeight = 50F;
            ColunaAfericaoSistema.HeaderText = "Valor Aferido Pelo Sistema";
            ColunaAfericaoSistema.Name = "ColunaAfericaoSistema";
            ColunaAfericaoSistema.ReadOnly = true;
            // 
            // ColunaAferimentoManual
            // 
            ColunaAferimentoManual.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColunaAferimentoManual.FillWeight = 50F;
            ColunaAferimentoManual.HeaderText = "Aferição Final";
            ColunaAferimentoManual.Name = "ColunaAferimentoManual";
            // 
            // ListaAferimentoGeral
            // 
            ListaAferimentoGeral.AllowUserToAddRows = false;
            ListaAferimentoGeral.AllowUserToDeleteRows = false;
            ListaAferimentoGeral.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListaAferimentoGeral.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            ListaAferimentoGeral.Location = new Point(12, 351);
            ListaAferimentoGeral.Name = "ListaAferimentoGeral";
            ListaAferimentoGeral.Size = new Size(776, 283);
            ListaAferimentoGeral.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn1.HeaderText = "Descrição";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn2.FillWeight = 50F;
            dataGridViewTextBoxColumn2.HeaderText = "Valor Aferido Pelo Sistema";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn3.FillWeight = 50F;
            dataGridViewTextBoxColumn3.HeaderText = "Aferição Final";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // LabelDataHora
            // 
            LabelDataHora.AutoSize = true;
            LabelDataHora.BackColor = Color.Transparent;
            LabelDataHora.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            LabelDataHora.ForeColor = SystemColors.Window;
            LabelDataHora.Location = new Point(451, 9);
            LabelDataHora.Name = "LabelDataHora";
            LabelDataHora.Size = new Size(337, 25);
            LabelDataHora.TabIndex = 2;
            LabelDataHora.Text = "DATA: dd/mm/yyyy | HORA: HH/mm";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(308, 25);
            label1.TabIndex = 3;
            label1.Text = "Aferimento dos Meios Recebidos:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.ForeColor = SystemColors.Window;
            label2.Location = new Point(12, 323);
            label2.Name = "label2";
            label2.Size = new Size(170, 25);
            label2.TabIndex = 4;
            label2.Text = "Aferimento Geral:";
            // 
            // VoltarButton
            // 
            VoltarButton.BackColor = SystemColors.Window;
            VoltarButton.FlatAppearance.BorderSize = 0;
            VoltarButton.FlatStyle = FlatStyle.Flat;
            VoltarButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            VoltarButton.Location = new Point(638, 640);
            VoltarButton.Name = "VoltarButton";
            VoltarButton.Size = new Size(150, 50);
            VoltarButton.TabIndex = 5;
            VoltarButton.Text = "Voltar";
            VoltarButton.UseVisualStyleBackColor = false;
            VoltarButton.Click += VoltarButton_Click;
            // 
            // RealizarFechamentoButton
            // 
            RealizarFechamentoButton.BackColor = SystemColors.Window;
            RealizarFechamentoButton.FlatAppearance.BorderSize = 0;
            RealizarFechamentoButton.FlatStyle = FlatStyle.Flat;
            RealizarFechamentoButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RealizarFechamentoButton.Location = new Point(12, 640);
            RealizarFechamentoButton.Name = "RealizarFechamentoButton";
            RealizarFechamentoButton.Size = new Size(220, 50);
            RealizarFechamentoButton.TabIndex = 6;
            RealizarFechamentoButton.Text = "Realizar Fechamento";
            RealizarFechamentoButton.UseVisualStyleBackColor = false;
            RealizarFechamentoButton.Click += RealizarFechamentoButton_Click;
            // 
            // JanelaFechamentoDeCaixa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(800, 700);
            ControlBox = false;
            Controls.Add(RealizarFechamentoButton);
            Controls.Add(VoltarButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(LabelDataHora);
            Controls.Add(ListaAferimentoGeral);
            Controls.Add(ListaAferimentoMeios);
            FormBorderStyle = FormBorderStyle.None;
            Name = "JanelaFechamentoDeCaixa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "JanelaFechamentoDeCaixa";
            ((System.ComponentModel.ISupportInitialize)ListaAferimentoMeios).EndInit();
            ((System.ComponentModel.ISupportInitialize)ListaAferimentoGeral).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView ListaAferimentoMeios;
        private DataGridView ListaAferimentoGeral;
        private Label LabelDataHora;
        private DataGridViewTextBoxColumn ColunaDescricao;
        private DataGridViewTextBoxColumn ColunaAfericaoSistema;
        private DataGridViewTextBoxColumn ColunaAferimentoManual;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private Label label1;
        private Label label2;
        private Button VoltarButton;
        private Button RealizarFechamentoButton;
    }
}