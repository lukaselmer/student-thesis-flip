#region

using System.IO;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    internal class ConverterProcessor : IProcessor
    {
        private readonly IConverter _converter;

        public ConverterProcessor(IConverter converter)
        {
            _converter = converter;
        }

        #region IProcessor Members

        public bool Process(IProjectNote projectNote)
        {
            if (File.Exists(projectNote.FilepathXps)) return false;
            if (!File.Exists(projectNote.FilepathPdf)) return false;
            return _converter.Convert(projectNote.FilepathPdf, projectNote.FilepathXps);
        }

        #endregion
    }
}