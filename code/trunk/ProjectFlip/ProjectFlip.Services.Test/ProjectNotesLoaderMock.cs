using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectFlip.Services.Loader.Interfaces;

namespace ProjectFlip.Services.Test
{
    class ProjectNotesLoaderMock : IProjectNotesLoader
    {
        private List<List<string>> list;

        public ProjectNotesLoaderMock()
        {
            list = new List<List<string>>();
            list.Add(new List<string>(19) { "1", "Title", "Text", "Sector", "Customer", "Focus", "Services", "Technology", "Application", "Tools", "15.10.2011", "text", "text", "test.pdf", "text", "text", "text", "text", "text" });
        }
        public List<List<String>> Import()
        {
            return list;
        }
    }
}
