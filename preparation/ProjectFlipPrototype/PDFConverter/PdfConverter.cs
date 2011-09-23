using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfConverter
{
    internal class PdfConverter
    {
        public void Convert(string from, string to)
        {
            var acrobatLocations = new string[] { @"C:\Program Files (x86)\Adobe\Reader 10.0\Reader\" };
            var command = "AcroRd32.exe /N /T " + from + " \"Microsoft XPS Document Writer\" /t " + to;
            throw new NotImplementedException();
        }

    }
}
