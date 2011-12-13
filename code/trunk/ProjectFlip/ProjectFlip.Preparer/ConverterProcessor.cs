#region

using System.IO;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    internal class ConverterProcessor : IProcessor
    {
        #region Declarations

        private readonly IConverter _converter;

        #endregion

        #region Constructor

        public ConverterProcessor(IConverter converter)
        {
            _converter = converter;
        }

        #endregion

        #region Properties

        #endregion

        #region Other

        public bool Process(IProjectNote projectNote)
        {
            if (File.Exists(projectNote.FilepathXps)) return false;
            if (!File.Exists(projectNote.FilepathPdf)) return false;
            return _converter.Convert(projectNote.FilepathPdf, projectNote.FilepathXps);
        }

        #endregion
    }
}