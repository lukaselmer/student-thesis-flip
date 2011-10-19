using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Converter.Pdf;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader;

namespace ProjectFlip.Preparer
{
    class Program
    {
        static void Main()
        {
            var container = new UnityContainer();
            ConfigureContainer(container);

            var converter = container.Resolve<IConverter>();
            var loader = container.Resolve<IProjectNotesLoader>();
            var webClient = new WebClient();

            foreach (var line in loader.Import())
            {
                var projectNote = container.Resolve<IProjectNote>().InitByLine(line);
                var parallelOptions = new ParallelOptions {MaxDegreeOfParallelism = 20};
                Parallel.Invoke(parallelOptions, () => DownloadAndConvert(webClient, converter, projectNote));
            }
        }

        private static void DownloadAndConvert(WebClient webClient, IConverter converter, IProjectNote projectNote)
        {
            if (!File.Exists(projectNote.FilepathPdf)) Download(webClient, projectNote);
            if (!File.Exists(projectNote.FilepathXps)) Convert(converter, projectNote);
        }

        private static void Convert(IConverter converter, IProjectNote projectNote)
        {
            if (!File.Exists(projectNote.FilepathPdf)) return;
            converter.Convert(projectNote.FilepathPdf, projectNote.FilepathXps);
        }

        private static bool Download(WebClient webClient, IProjectNote projectNote, int i = 0)
        {
            try
            {
                var regex = new Regex(@"\.pdf$");
                var downloadUrl = regex.Replace(projectNote.Url, (i == 0 ? "" : ("_0" + i)) + ".pdf");
                webClient.DownloadFile(downloadUrl, projectNote.FilepathPdf);
                return true;
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.ProtocolError) throw;
                if (i >= 9) return false;
                return Download(webClient, projectNote, i + 1);
            }
        }

        private static void ConfigureContainer(UnityContainer container)
        {
            container.RegisterType<IConverter, PdfConverter>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
            container.RegisterType<IProjectNote, ProjectNote>();
        }
    }
}
