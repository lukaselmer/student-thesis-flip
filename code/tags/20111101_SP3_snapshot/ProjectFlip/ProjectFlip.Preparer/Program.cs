#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Converter.Pdf;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader;
using ProjectFlip.Services.Loader.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    internal class Program
    {
        private static void Main()
        {
            var container = new UnityContainer();
            ConfigureContainer(container);

            var processors = new List<IProcessor>
                             {
                                 new DownloadProcessor(), new ConverterProcessor(container.Resolve<IConverter>()),
                                 new ImageExtractorProcessor(), new CleanupProcessor()
                             };

            var actions = new List<Action>();
            foreach (var line in container.Resolve<IProjectNotesLoader>().Import())
            {
                var projectNote = container.Resolve<IProjectNote>();
                projectNote.Line = line;
                actions.Add(() => Process(processors, projectNote));
            }
            var parallelOptions = new ParallelOptions {MaxDegreeOfParallelism = 300};
            Parallel.Invoke(parallelOptions, actions.ToArray());
        }

        private static void Process(List<IProcessor> processors, IProjectNote projectNote)
        {
            try
            {
                processors.ForEach(proc => proc.Process(projectNote));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error processing PN: ");
                Console.WriteLine(e);
            }
        }

        private static void ConfigureContainer(UnityContainer container)
        {
            container.RegisterType<IConverter, PdfConverter>();
            container.RegisterType<IProjectNote, ProjectNote>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
        }
    }
}