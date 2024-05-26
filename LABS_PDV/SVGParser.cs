using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.LABS_PDV
{
    public class SVGParser
    {
        /// <summary>
        /// Returns An Image from the svg with the specified size
        /// </summary>
        /// <param name="SvgPath">The path to the svg</param>
        /// <returns>Returns a BitMap Image</returns>
        public Image GetImageFromSVG()
        {
            SvgDocument svg = SvgDocument.Open($@".\LabsBin\Background.svg");
            return svg.Draw();
        }
    }
}
