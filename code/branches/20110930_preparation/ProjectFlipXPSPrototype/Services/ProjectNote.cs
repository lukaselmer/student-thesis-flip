using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services.Interfaces;
using System.IO;
using System.Windows.Xps.Packaging;

namespace Services
{
    public class ProjectNote : IProjectNote
    {
        private XpsDocument _document;
        
        public ProjectNote(string file)
        {

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            if (fileNameWithoutExtension != null)
            {
                Name = fileNameWithoutExtension.Replace("_", " ");
            }
        }

        public string Name { get; set; }
        public XpsDocument Document
        {
            get { return _document; }
            set
            {
                _document = new XpsDocument(value.ToString(), FileAccess.Read);
            }
        }
    }
}
