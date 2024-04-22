using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{
	public class PrintManager
	{
		private PrintDocument printDocument = new PrintDocument();
		private String stringToPrint = "";

		public void EXECUTA(string Printer)
		{
			imprimeComprovante(Printer);
		}

		private void geraComprovante()
		{
			FileStream fs = new FileStream("a.txt", FileMode.Create);
			StreamWriter writer = new StreamWriter(fs);
			writer.WriteLine("==========================================");
			writer.WriteLine("          NOME DA EMPRESA AQUI            ");
			writer.WriteLine("==========================================");
			writer.Close();
			fs.Close();
		}

		private void imprimeComprovante(string Printer)
		{
			FileStream fs = new FileStream("a.txt", FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			stringToPrint = sr.ReadToEnd();
			printDocument.PrinterSettings.PrinterName = Printer;
			printDocument.PrintPage += new PrintPageEventHandler(printPage);
			printDocument.Print();
			sr.Close();
			fs.Close();
		}

		private void printPage(object sender, PrintPageEventArgs e)
		{
			int charactersOnPage = 0;
			int linesPerPage = 0;
			Graphics graphics = e.Graphics;

			// Sets the value of charactersOnPage to the number of characters 
			// of stringToPrint that will fit within the bounds of the page.
			graphics.MeasureString(stringToPrint, new Font("Times New Roman", 12),
				e.MarginBounds.Size, StringFormat.GenericTypographic,
				out charactersOnPage, out linesPerPage);

			// Draws the string within the bounds of the page
			graphics.DrawString(stringToPrint, new Font("Times New Roman", 12), Brushes.Black,
				e.MarginBounds, StringFormat.GenericTypographic);

			// Remove the portion of the string that has been printed.
			stringToPrint = stringToPrint.Substring(charactersOnPage);

			// Check to see if more pages are to be printed.
			e.HasMorePages = (stringToPrint.Length > 0);
		}
	}
}
