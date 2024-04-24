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

		private List<MeioDePagamento> MeiosDePagamento = new();
		private MeioDePagamento MeioDePagamentoModel { get; set; } = new("");


		public MeiosDePagamentoConfig()
		{
			InitializeComponent();

		}
		//
		public async void LoadFromDataBase()
		{
			// Por Segurança iniciamos do zero para que a database dite a configuração durante o carregamento
			MeiosDePagamento.Clear();
			MeioDePagamentoModel = new("");
			ListaMeiosRegistrados.Items.Clear();
			MeiosDePagamento = await CloudDataBase.GetMeiosDePagamentoAsync();
			//
			foreach (var meio in MeiosDePagamento)
			{
				var nModos = 0;
				var nBandeiras = 0;
				var nParcelas = 0;
				//
				if (meio.PossuiModos)
				{
					meio.Modos.ForEach(x => { nBandeiras = x.Bandeiras.Count; nParcelas = x.Parcelas.Count; });
				}
				//
				var item = new ListViewItem([$"{meio.Meio}", $"{nModos}", $"{nBandeiras}", $"{nParcelas}"]);
				//
				ListaMeiosRegistrados.Items.Add(item);
			}
		}
		/// <summary>
		/// Reseta os Modos de Pagamento para o Default (Vazio);
		/// e Também esconde os campos dependentes do modo de pagamento
		/// </summary>
		void ResetModos()
		{
			BandeirasDropDown.Items.Clear();
			ParcelasDropDown.Items.Clear();
			//Esconde os campos
			BandeirasLabel.Visible = PossuiModosCheckBox.Checked;
			PossuiBandeirasCheckBox.Visible = PossuiModosCheckBox.Checked;
			PossuiBandeirasCheckBox.Checked = false;
			BandeirasDropDown.Visible = false;
			RemoverBandeiraButton.Visible = false;
			//
			ParcelasLabel.Visible = PossuiModosCheckBox.Checked;
			PossuiParcelasCheckBox.Visible = PossuiModosCheckBox.Checked;
			PossuiParcelasCheckBox.Checked = false;
			ParcelasDropDown.Visible = false;
			RemoverParcelasButton.Visible = false;
		}
		//
		void Reset()
		{
			MeioDePagamentoBoxInput.Text = null;
			ModosPagamentoDropDown.Items.Clear();
			PossuiModosCheckBox.Checked = false;
			RemoverModoButton.Visible = false;
			BandeirasDropDown.Items.Clear();
			ParcelasDropDown.Items.Clear();
			MeioDePagamentoModel = new("");
			MeioDePagamentoModel.Modos = new();
			ResetModos();
		}
		void RemoveModo()
		{
			int index = ModosPagamentoDropDown.SelectedIndex;
			if (index != -1)
			{
				MeioDePagamentoModel.Modos.RemoveAt(index);
				ModosPagamentoDropDown.Items.RemoveAt(index);
			}
			else { Modais.MostrarAviso("Selecione Um Modo Para Remover"); }//
			if (ModosPagamentoDropDown.Items.Count == 0)
			{ ResetModos(); }
		}
		void RemoveBandeira()
		{
			int index = BandeirasDropDown.SelectedIndex;
			if (index != -1)
			{
				var modo = MeioDePagamentoModel.Modos[index];
				if (modo.Bandeiras.Contains(BandeirasDropDown.Items[index]))
				{
					BandeirasDropDown.Items.RemoveAt(index);
					modo.Bandeiras.RemoveAt(index);
				}
			}
			else { Modais.MostrarAviso("Selecione uma Bandeira Para Remover"); }
		}
		void RemoveParcela()
		{
			int index = ParcelasDropDown.SelectedIndex;
			if (index != -1)
			{
				var modo = MeioDePagamentoModel.Modos[index];
				if (modo.Parcelas.Contains(ParcelasDropDown.Items[index]))
				{
					ParcelasDropDown.Items.RemoveAt(index);
					modo.Parcelas.RemoveAt(index);
				}
			}
			else { Modais.MostrarAviso("Selecione uma Parcela Para Remover"); }
		}
		//-------------------------//
		//         EVENTOS
		//-------------------------//

		private void SairButton_Click(object sender, EventArgs e)
		{
			Reset();
			this.Close();
		}

		private void OnPossuiCheckChange(object sender, EventArgs e)
		{
			CheckBox? check = sender as CheckBox;
			//
			//Manipulação de dados
			if(check == SemLimiteDeValor)
			{
				MeioDePagamentoModel.PodeUltrapassarOValorTotal = SemLimiteDeValor.Checked;
			}
			//
			if (check == PossuiModosCheckBox)
			{
				if (Utils.IsNotValidString(MeioDePagamentoBoxInput.Text) && check.Checked) { Modais.MostrarAviso("Você Deve Primeiro Digitar o Meio de Pagamento!"); check.Checked = false; return; }
				MeioDePagamentoModel.PossuiModos = check.Checked;
				//
				ModosPagamentoDropDown.Visible = check.Checked;
				ModosPagamentoDropDown.Enabled = check.Checked;
				BandeirasLabel.Visible = check.Checked;
				PossuiBandeirasCheckBox.Visible = check.Checked;
				ParcelasLabel.Visible = check.Checked;
				PossuiParcelasCheckBox.Visible = check.Checked;
				//
				RemoverModoButton.Visible = check.Checked;
				RemoverModoButton.BringToFront();
			}
			if (check == PossuiBandeirasCheckBox)
			{
				if (MeioDePagamentoModel.Modos != null)
				{
					if ((ModosPagamentoDropDown.SelectedIndex == -1) && check.Checked) { Modais.MostrarAviso("Você Deve Primeiro Selecionar um Modo Para adicionar as Bandeiras!"); check.Checked = false; return; }
					//
					var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
					if (t != null)
					{
						t.PossuiBandeira = PossuiBandeirasCheckBox.Checked;
						//
						BandeirasDropDown.Visible = check.Checked;
						BandeirasDropDown.Enabled = check.Checked;
						//
						RemoverBandeiraButton.Visible = check.Checked;
						RemoverBandeiraButton.BringToFront();
					}
				}
			}
			if (check == PossuiParcelasCheckBox)
			{
				if (MeioDePagamentoModel.Modos != null)
				{
					if ((ModosPagamentoDropDown.SelectedIndex == -1) && check.Checked) { Modais.MostrarAviso("Você Deve Primeiro Selecionar um Modo Para adicionar as Parcelas!"); check.Checked = false; return; }
					//
					var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
					if (t != null)
					{
						t.PossuiParcelas = PossuiParcelasCheckBox.Checked;
						//
						ParcelasDropDown.Visible = check.Checked;
						ParcelasDropDown.Enabled = check.Checked;
						//
						RemoverParcelasButton.Visible = check.Checked;
						RemoverParcelasButton.BringToFront();
					}
				}
			}
		}
		//
		private void OnDropDownKeyUP(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (sender == ModosPagamentoDropDown)
				{
					if (Utils.IsNotValidString(ModosPagamentoDropDown.Text)) { return; }
					//
					if (MeioDePagamentoModel.Modos == null) { MeioDePagamentoModel.Modos = new(); } // Iniciamos uma nova lista de modos caso não exista
																									//
					var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
					//
					if (t == null) // se o modo de pagamento não existe, criamos ele dentro do Modelo de meio de pagamento
					{
						ModosPagamentoDropDown.Items.Add(ModosPagamentoDropDown.Text);
						ModoDePagamento modo = new(ModosPagamentoDropDown.Text);
						//Iniciamos a lista de bandeiras(mesmo que não tenha)
						modo.PossuiBandeira = PossuiBandeirasCheckBox.Checked;
						modo.Bandeiras = new();
						//Fazemos o mesmo com a lista de parcelas
						modo.PossuiParcelas = PossuiParcelasCheckBox.Checked;
						modo.Parcelas = new();
						//logo após, adicionamos na lista de modos
						modo.Taxa = 0; // esse campo ainda será usado mas por enquanto está escondido
						MeioDePagamentoModel.Modos.Add(modo);
						//Limpamos o texto
						ModosPagamentoDropDown.Text = null;
					}
				}
				if (sender == BandeirasDropDown) // Aqui Manejamos a adição de Bandeiras 
				{
					if (Utils.IsNotValidString(BandeirasDropDown.Text)) { return; }// se não tem nada escrito não tem porque continuar
																				   //
					var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
					if (t != null)
					{
						var tt = t.Bandeiras.Find(x => x == BandeirasDropDown.Text);
						if (tt != null) { return; } // se tiver alguma bandeira já adicionada retornamos para não adicionar mais de uma vez
													//
						t.Bandeiras.Add(BandeirasDropDown.Text);
						//
						BandeirasDropDown.Items.Add(BandeirasDropDown.Text);
						//
						//Limpamos o texto
						BandeirasDropDown.Text = null;
					}
				}
				if (sender == ParcelasDropDown) // Aqui manejamos a adição de Parcelas
				{
					if (Utils.IsNotValidString(ParcelasDropDown.Text)) { return; }// se não tem nada escrito não tem porque continuar
																				  //
					var t = MeioDePagamentoModel.Modos.Find(x => x.Modo == ModosPagamentoDropDown.Text);
					if (t != null)
					{
						var tt = t.Parcelas.Find(x => x == ParcelasDropDown.Text);
						if (tt != null) { return; } // Mesma lógica das bandeiras
													//
						t.Parcelas.Add(ParcelasDropDown.Text);
						//
						ParcelasDropDown.Items.Add(ParcelasDropDown.Text);
						//Limpamos o texto
						ParcelasDropDown.Text = null;
					}
				}
			}
		}
		//
		private void OnDropDownIndexChange(object sender, EventArgs e)
		{
			if (sender == ModosPagamentoDropDown)
			{
				int index = ModosPagamentoDropDown.SelectedIndex;
				if (index != -1)
				{
					ModoDePagamento? modo = MeioDePagamentoModel.Modos[index];
					if (modo != null)
					{
						//Limpamos os dois drop downs para poder atualizar
						BandeirasDropDown.Items.Clear();
						ParcelasDropDown.Items.Clear();
						//
						// Fazemos com foreach imbutido para economizar espaço ;D
						modo.Bandeiras.ForEach(x => BandeirasDropDown.Items.Add(x));
						modo.Parcelas.ForEach(x => ParcelasDropDown.Items.Add(x));
						//
						//
						BandeirasDropDown.Visible = modo.PossuiBandeira;
						RemoverBandeiraButton.Visible = modo.PossuiBandeira;
						RemoverBandeiraButton.BringToFront();
						PossuiBandeirasCheckBox.Checked = modo.PossuiBandeira;
						//
						//
						ParcelasDropDown.Visible = modo.PossuiParcelas;
						RemoverParcelasButton.Visible = modo.PossuiParcelas;
						RemoverParcelasButton.BringToFront();
						PossuiParcelasCheckBox.Checked = modo.PossuiParcelas;
					}
				}
			}
		}
		//
		private void MeioDePagamentoBoxInput_KeyUp(object sender, KeyEventArgs e)
		{
			if (MeioDePagamentoBoxInput.Text.Length > 0)
			{
				MeioDePagamentoModel.Meio = MeioDePagamentoBoxInput.Text;
			}
		}
		//
		private void OnRemoverButtonClick(object sender, EventArgs e)
		{
			if (sender == RemoverModoButton) { RemoveModo(); }
			if (sender == RemoverBandeiraButton) { RemoveBandeira(); }
			if (sender == RemoverParcelasButton) { RemoveParcela(); }
		}

		private void OnListaMeiosRegistradosClick(object sender, EventArgs e)
		{
			if(ListaMeiosRegistrados.SelectedIndices.Count > 0)
			{
				var index = ListaMeiosRegistrados.SelectedIndices[0];
				if(MeiosDePagamento.Count > index)
				{
					MeioDePagamentoModel = MeiosDePagamento[index];
					SemLimiteDeValor.Checked = MeioDePagamentoModel.PodeUltrapassarOValorTotal;
					if(MeioDePagamentoModel != null)
					{
						MeioDePagamentoBoxInput.Text = MeioDePagamentoModel.Meio;
						//
						PossuiModosCheckBox.Checked = MeioDePagamentoModel.PossuiModos;
						// a chamada é depois pois o resetModos depende da variável acima
						ResetModos();
						if (MeioDePagamentoModel.PossuiModos)
						{
							ModosPagamentoDropDown.Items.Clear();
							MeioDePagamentoModel.Modos.ForEach(x =>
							{
								ModosPagamentoDropDown.Items.Add(x.Modo);
								//
								x.Bandeiras.ForEach(x => { BandeirasDropDown.Items.Add(x); });
								x.Parcelas.ForEach(x => { ParcelasDropDown.Items.Add(x); });
								//
							});
						}
					}
				}
			}
		}
		//
		//
		private void AdicionarMeioDePagamento_Click(object sender, EventArgs e)
		{
			//
			MeioDePagamentoModel.Meio = MeioDePagamentoBoxInput.Text;
			//
			if (MeiosDePagamento.Contains(MeioDePagamentoModel))
			{
				CloudDataBase.UpdateMeioDePagamentoAsync(MeioDePagamentoModel);
				Modais.MostrarInfo("Meio de Pagamento Atualizado Com Sucesso!");
				LoadFromDataBase();
			}
			else
			{
				CloudDataBase.RegisterMeioDePagamentoAsync(MeioDePagamentoModel);
				MeiosDePagamento.Add(MeioDePagamentoModel);
				Modais.MostrarInfo("Meio de Pagamento Adicionado Com Sucesso!");
			}
			Reset();
		}

	}
}
