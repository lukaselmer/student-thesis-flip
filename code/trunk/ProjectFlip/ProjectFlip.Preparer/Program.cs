using System;
using System.Collections.Generic;
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

            var processors = new List<IProcessor> { 
                //new DownloadProcessor(),
                //new ConverterProcessor(container.Resolve<IConverter>()),
                new ImageExtractorProcessor() };

            var actions = new List<Action>();
            //int i = 0;
            foreach (var line in container.Resolve<IProjectNotesLoader>().Import())
            {
                var projectNote = container.Resolve<IProjectNote>().InitByLine(line);
                actions.Add(() => Process(processors, projectNote));
                //if(i++ > 3) break; // XXXXXXXXXXXXXXXXXXXXX DELETE ME XXXXXXXXXXXXXXXXXXXXXXXXX
            }
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 300 };
            Parallel.Invoke(parallelOptions, actions.ToArray());
        }

        private static void Process(List<IProcessor> processors, IProjectNote projectNote)
        {
            processors.ForEach(proc => proc.Process(projectNote));
        }

        private static void ConfigureContainer(UnityContainer container)
        {
            container.RegisterType<IConverter, PdfConverter>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
            container.RegisterType<IProjectNote, ProjectNote>();
        }
    }
}
