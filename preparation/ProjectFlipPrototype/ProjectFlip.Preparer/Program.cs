using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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

            var webClient = new WebClient();
            foreach (var line in list)
            {
                var projectNote = container.Resolve<IProjectNote>();
                projectNote.InitByLine(line);
                if (!File.Exists(projectNote.FilepathPdf)) Download(webClient, projectNote);
                if (!File.Exists(projectNote.FilepathXps)) Convert(converter, projectNote);
            }
        }

        private static void Convert(IConverter converter, IProjectNote projectNote)
        {
            if(!File.Exists(projectNote.FilepathPdf)) throw new Exception("File \"" + projectNote.FilepathPdf + "\" does not exist");
            converter.Convert(projectNote.FilepathPdf, projectNote.FilepathXps);
        }

        private static void Download(WebClient webClient, IProjectNote projectNote)
        {
            webClient.DownloadFile(projectNote.Url, projectNote.FilepathPdf);
        }

        private static void ConfigureContainer(UnityContainer container)
        {
            container.RegisterType<IConverter, PdfConverter>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
        }
    }
}
