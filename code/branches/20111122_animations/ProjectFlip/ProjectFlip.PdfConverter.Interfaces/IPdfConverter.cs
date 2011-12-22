using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFlip.PdfConverter.Interfaces
{
    public interface IPdfConverter
    {
        string AcrobatLocation { get; set; }
        bool Convert(string from, string to);

    }
}
