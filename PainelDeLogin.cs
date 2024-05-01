using Labs.LABS_PDV;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs
{
    public partial class PainelDeLogin : Form
    {


        public PainelDeLogin()
        {
            InitializeComponent();
			//
			SvgDocument svg = SvgDocument.Open($@".\LabsBin\test.svg");
			//svg = AdjustSize(svg);
			Image img = svg.Draw();
			testeBox.Image = img;
        }





		/// <summary>
		/// Makes sure that the image does not exceed the maximum size, while preserving aspect ratio.
		/// </summary>
		/// <param name="document">The SVG document to resize.</param>
		/// <returns>Returns a resized or the original document depending on the document.</returns>
		private SvgDocument AdjustSize(SvgDocument document)
		{
			if (document.Height > MaximumSize.Height)
			{
				document.Width = (int)((document.Width / (double)document.Height) * MaximumSize.Height);
				document.Height = MaximumSize.Height;
			}
			return document;
		}
	}
}
