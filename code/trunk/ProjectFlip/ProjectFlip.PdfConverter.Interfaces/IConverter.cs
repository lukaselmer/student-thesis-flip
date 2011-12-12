namespace ProjectFlip.Converter.Interfaces
{
    public interface IConverter
    {
        // ReSharper disable UnusedMemberInSuper.Global
        /// <summary>
        /// Gets or sets the location of the Adobe Acrobat Reader (Version X)
        /// Example path: C:\Program Files (x86)\Adobe\Reader 10.0\Reader\AcroRd32.exe
        /// </summary>
        /// <value>
        /// The acrobat location.
        /// </value>
        string AcrobatLocation { get; set; }

        // ReSharper restore UnusedMemberInSuper.Global
        /// <summary>
        /// Converts a pdf file to an XPS file
        /// </summary>
        /// <param name="from">Location of the pdf file</param>
        /// <param name="to">Location of the xps file</param>
        /// <returns></returns>
        bool Convert(string from, string to);
    }
}