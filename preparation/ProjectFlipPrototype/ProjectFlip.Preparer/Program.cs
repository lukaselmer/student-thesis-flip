using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Converter.Pdf;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader;

namespace ProjectFlip.Preparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            ConfigureContainer(container);

            var converter = container.Resolve<IConverter>();
            var loader = container.Resolve<IProjectNotesLoader>();
            var list = loader.Import();

            foreach (var line in list)
            {
                // TODO: implement this
                if(false) converter.Convert("", "");
            }
        }

        private static void ConfigureContainer(UnityContainer container)
        {
            container.RegisterType<IConverter, PdfConverter>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
        }
    }
}
