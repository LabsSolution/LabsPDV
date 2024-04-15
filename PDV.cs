using LabsPDV.LABS_PDV;

namespace LabsPDV
{
	public partial class PDV : Form
	{
		public PDV()
		{
			InitializeComponent();
			KeyDown += PDV_KeyDown;
		}

		private void PDV_KeyDown(object? sender, KeyEventArgs e)
		{
			//
			//Teste para valida��o de tecla
			if (Utils.IsValidNumberKey(e.KeyCode, out string key))
			{
				//Caso passe no teste, a tecla ser� listada;
				keydebug.Text = key;
			}
			//Caso contr�rio, ser� listada o c�digo com ?
			else { keydebug.Text = key; }
		}

		private void PDV_Load(object sender, EventArgs e)
		{

		}

		private void OnCCC(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			string t = InputCodProduto.Text;
			//Pegamos a entrada do c�digo do produto e verificamos se possui um 'N'x'C�digo' para dividir;
			string[] Splitted = t.Split('x');
			//Aqui asseguramos que o valor antes do x � um inteiro, e que tambem temos mais de um elemento na lista
			if (Utils.TryParseToInt(Splitted[0], out int Qtd) && Splitted.Length > 1)
			{
				//Somente para teste vamos deixar um label de debug para demonstar que a divis�o est� sendo computada corretamente;
				string Cod = Splitted[1];
				debuglabel.Text = $"{Qtd} Vezes o Produto {Cod}";
			}
			else
			{
				//caso n�o seja comprido os primeiros requisitos, jogamos um erro na tela
				//por enquanto � somente debug;
				debuglabel.Text = "Ocorreu um Erro ao Registrar o Produto, Tente Novamente!";
			}
		}
	}
}
