using System.IO;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Preparer
{
    class ConverterProcessor : IProcessor
    {
        private readonly IConverter _converter;
        public ConverterProcessor(IConverter converter)
        {
            _converter = converter;
        }

        public bool Process(IProjectNote projectNote)
        {
            if (!File.Exists(projectNote.FilepathXps))
            {
                if (!File.Exists(projectNote.FilepathPdf)) return false;
                _converter.Convert(projectNote.FilepathPdf, projectNote.FilepathXps);
            }
            return false;
        }
    }
}
