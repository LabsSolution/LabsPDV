using Labs.LABS_PDV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Labs.LABS_PDV.Modelos;

namespace Labs.Janelas.Configuracoes.Dependencias
{
	public partial class MeiosDePagamentoConfig : Form
	{

		private MeioDePagamento MeioDePagamentoModel { get; set; } = new("");


		public MeiosDePagamentoConfig()
		{
			InitializeComponent();
		}
		//

		void Reset()
		{
			MeioDePagamentoBoxInput.Text = null;
			ModosPagamentoDropDown.Items.Clear();
			BandeirasDropDown.Items.Clear();
			ParcelasDropDown.Items.Clear();
			MeioDePagamentoModel = new("");
		}

		//-------------------------//
		//         EVENTOS
		//-------------------------//
		//
		private void SairButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void OnPossuiCheckChange(object sender, EventArgs e)
		{
			CheckBox? check = sender as CheckBox;
			//Manipulação de Items
			if (check == PossuiModosCheckBox)
			{
				if (check.Checked)
				{
					ModosPagamentoDropDown.Visible = true;
					ModosPagamentoDropDown.Enabled = true;
					BandeirasLabel.Visible = true;
					PossuiBandeirasCheckBox.Visible = true;
				}
				else
				{
					ModosPagamentoDropDown.Visible = false;
					ModosPagamentoDropDown.Enabled = false;
					BandeirasLabel.Visible = false;
					PossuiBandeirasCheckBox.Visible = false;
					PossuiBandeirasCheckBox.Checked = false;
				}
			}
			if (check == PossuiBandeirasCheckBox)
			{
				if (check.Checked)
				{
					BandeirasDropDown.Visible = true;
					BandeirasDropDown.Enabled = true;
					ParcelasLabel.Visible = true;
					PossuiParcelasCheckBox.Visible = true;
				}
				else
				{
					BandeirasDropDown.Visible = false;
					BandeirasDropDown.Enabled = false;
					ParcelasLabel.Visible = false;
					PossuiParcelasCheckBox.Visible = false;
					PossuiParcelasCheckBox.Checked = false;
				}
			}
			if (check == PossuiParcelasCheckBox)
			{
				if (check.Checked)
				{
					ParcelasDropDown.Visible = true;
					ParcelasDropDown.Enabled = true;
				}
				else
				{
					ParcelasDropDown.Visible = false;
					ParcelasDropDown.Enabled = false;
				}
			}
			//Manipulação de dados
			if (check == PossuiModosCheckBox)
			{
				MeioDePagamentoModel.PossuiModos = check.Checked;
			}
			if (check == PossuiBandeirasCheckBox)
			{
				var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
				if(t != null) { t.PossuiBandeira = PossuiBandeirasCheckBox.Checked; }
			}
			if (check == PossuiParcelasCheckBox)
			{
				var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
				if (t != null) { t.PossuiParcelas = PossuiParcelasCheckBox.Checked; }
			}
		}

		private void UU_Click(object sender, EventArgs e)
		{
			//
			MeioDePagamentoModel.Meio = MeioDePagamentoBoxInput.Text;
			//
			CloudDataBase.RegisterMeioDePagamentoAsync(MeioDePagamentoModel);
		}

		private void OnDropDownKeyUP(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (sender == ModosPagamentoDropDown) 
				{ 
					if(MeioDePagamentoModel.Modos == null) { MeioDePagamentoModel.Modos = new(); }
					//
					var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
					//
					if(t == null)
					{
						ModosPagamentoDropDown.Items.Add(ModosPagamentoDropDown.Text);
						MeioDePagamentoModel.Modos.Add(new(ModosPagamentoDropDown.Text,PossuiBandeirasCheckBox.Checked,new(),PossuiParcelasCheckBox.Checked,new(),0));
					}
				}
				if (sender == BandeirasDropDown) 
				{
					var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
					if(t != null)
					{
						t.Bandeiras.Add(BandeirasDropDown.Text);
						//
						BandeirasDropDown.Items.Add(BandeirasDropDown.Text);
						//
					}
				}
				if (sender == ParcelasDropDown) 
				{
					var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
					if (t != null)
					{
						t.Parcelas.Add(ParcelasDropDown.Text);
						//
						ParcelasDropDown.Items.Add(ParcelasDropDown.Text);
						//
					}
				}
			}
		}

		private void OnDropDownIndexChange(object sender, EventArgs e)
		{
			if (sender == ModosPagamentoDropDown)
			{
			}
			if (sender == BandeirasDropDown) 
			{
			}
			if (sender == ParcelasDropDown) { }
		}

		private void MeioDePagamentoBoxInput_KeyUp(object sender, KeyEventArgs e)
		{
			if(MeioDePagamentoBoxInput.Text.Length > 0)
			{
				MeioDePagamentoModel.Meio = MeioDePagamentoBoxInput.Text;
			}
		}
		//

	}
}
