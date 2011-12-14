#region

using System.IO;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    /// <summary>
    /// This is the processor for the PDF to XPS conversion.
    /// </summary>
    /// <remarks></remarks>
    internal class ConverterProcessor : IProcessor
    {
        #region Declarations

        private readonly IConverter _converter;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConverterProcessor"/> class.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <remarks></remarks>
        public ConverterProcessor(IConverter converter)
        {
            _converter = converter;
        }

        #endregion

        #region Properties

        #endregion

        #region Other

        /// <summary>
        /// Converts a PDF project note to a XPS project note.
        /// </summary>
        /// <param name="projectNote">The project note.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Process(IProjectNote projectNote)
        {
            if (File.Exists(projectNote.FilepathXps)) return false;
            if (!File.Exists(projectNote.FilepathPdf)) return false;
            return _converter.Convert(projectNote.FilepathPdf, projectNote.FilepathXps);
        }

        #endregion
    }
}