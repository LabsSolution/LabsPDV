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
			//Teste para validação de tecla
			if (Utils.IsValidNumberKey(e.KeyCode, out string key))
			{
				//Caso passe no teste, a tecla será listada;
				keydebug.Text = key;
			}
			//Caso contrário, será listada o código com ?
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
			//Pegamos a entrada do código do produto e verificamos se possui um 'N'x'Código' para dividir;
			string[] Splitted = t.Split('x');
			//Aqui asseguramos que o valor antes do x é um inteiro, e que tambem temos mais de um elemento na lista
			if (Utils.TryParseToInt(Splitted[0], out int Qtd) && Splitted.Length > 1)
			{
				//Somente para teste vamos deixar um label de debug para demonstar que a divisão está sendo computada corretamente;
				string Cod = Splitted[1];
				debuglabel.Text = $"{Qtd} Vezes o Produto {Cod}";
			}
			else
			{
				//caso não seja comprido os primeiros requisitos, jogamos um erro na tela
				//por enquanto é somente debug;
				debuglabel.Text = "Ocorreu um Erro ao Registrar o Produto, Tente Novamente!";
			}
		}
	}
}
