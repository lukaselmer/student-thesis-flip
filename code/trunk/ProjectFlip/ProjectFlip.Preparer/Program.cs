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
    internal static class Program
    {
        private static void Main()
        {
            new PdfConverter();
            var container = new UnityContainer();
            ConfigureContainer(container);
            Parallel.Invoke(new ParallelOptions {MaxDegreeOfParallelism = 5}, Actions(container).ToArray());
        }

        private static List<Action> Actions(IUnityContainer container)
        {
            var actions = new List<Action>();
            foreach (var line in container.Resolve<IProjectNotesLoader>().Import())
            {
                var projectNote = container.Resolve<IProjectNote>();
                projectNote.Line = line;
                actions.Add(() => Process(Processors(container), projectNote));
            }
            return actions;
        }

        private static List<IProcessor> Processors(IUnityContainer container)
        {
            return new List<IProcessor>
                   {
                       new DownloadProcessor(), new ConverterProcessor(container.Resolve<IConverter>()),
                       new ImageExtractorProcessor(), new CleanupProcessor()
                   };
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

        private static void ConfigureContainer(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            container.RegisterType<IConverter, PdfConverter>();
            container.RegisterType<IProjectNote, ProjectNote>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
        }
    }
}