#region

using System;
using System.Collections.Generic;
using ProjectFlip.Services.Loader.Interfaces;

#endregion

namespace ProjectFlip.Services.Test
{
    internal class ProjectNotesLoaderMock : IProjectNotesLoader
    {
        public ProjectNotesLoaderMock()
        {
            List = new List<List<string>>
                   {
                       new List<string>(19)
                       {
                           "1", "Title", "Text", "Sector", "Customer", "Focus", "Services", "Technology", "Application",
                           "Tools", "15.10.2011", "text", "text", "test.pdf", "text", "text", "text", "text", "text"
                       }
                   };
        }

        private List<List<string>> List { get; set; }

        #region IProjectNotesLoader Members

        public List<List<String>> Import()
        {
            return List;
        }

        public string Filename { get; set; }

        #endregion
    }
}