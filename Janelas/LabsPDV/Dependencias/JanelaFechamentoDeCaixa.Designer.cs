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
            ListaAferimentoGeral = new DataGridView();
            LabelDataHora = new Label();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            ColunaDescricao = new DataGridViewTextBoxColumn();
            ColunaAfericaoSistema = new DataGridViewTextBoxColumn();
            ColunaAferimentoManual = new DataGridViewTextBoxColumn();
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
            ListaAferimentoMeios.Location = new Point(12, 53);
            ListaAferimentoMeios.Name = "ListaAferimentoMeios";
            ListaAferimentoMeios.Size = new Size(776, 283);
            ListaAferimentoMeios.TabIndex = 0;
            // 
            // ListaAferimentoGeral
            // 
            ListaAferimentoGeral.AllowUserToAddRows = false;
            ListaAferimentoGeral.AllowUserToDeleteRows = false;
            ListaAferimentoGeral.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListaAferimentoGeral.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            ListaAferimentoGeral.Location = new Point(12, 342);
            ListaAferimentoGeral.Name = "ListaAferimentoGeral";
            ListaAferimentoGeral.Size = new Size(776, 283);
            ListaAferimentoGeral.TabIndex = 1;
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
            // JanelaFechamentoDeCaixa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(800, 700);
            ControlBox = false;
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
    }
}