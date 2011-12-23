#region

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Converter.Pdf;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader;
using ProjectFlip.Services.Loader.Interfaces;
using log4net;
using log4net.Config;

#endregion

namespace ProjectFlip.Preparer
{
    /// <summary>
    /// The main program for the preparer. <see cref="Main"/>
    /// </summary>
    /// <remarks></remarks>
    internal static class Program
    {
        #region Declarations

        /// <summary>
        /// The Logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Other

        /// <summary>
        /// This is the entry point for the preparer, which downloads the projec notes,
        /// converts each from PDF to the XPS file format, extracts an image and finally
        /// cleans up if something went wrong in the previous steps.
        /// 
        /// The program is executed parallel to improve performance.
        /// </summary>
        /// <remarks>
        /// The execution of this program will take about 5 to 20 minutes, depending on your
        /// PC, internet connection and the amount of project notes.
        /// 
        /// When executing this program, you will see many adobe reader windows opening and
        /// closing. That is ok, dont worry about it...
        /// 
        /// If an error occured processing the files, first try to rerun the program again.
        /// Secondly, try to restart the computer, because there may be some file handles
        /// which are still open and can be closed by restarting the system. If that does not
        /// work ether, delete the contents of the following folders:
        /// /Resources/PDF
        /// /Resources/Xps
        /// /Resources/Images
        /// </remarks>
        private static void Main()
        {
            BasicConfigurator.Configure();
            Logger.Info("Preparing Project Notes...");
            var container = new UnityContainer();
            ConfigureContainer(container);
            // You could adjust the MaxDegreeOfParallelism if you want and if your PC can handle it, but is
            // only for advanced users who know what they are doing (and who can recompile the project).
            Parallel.Invoke(new ParallelOptions {MaxDegreeOfParallelism = 5}, Actions(container).ToArray());
            Logger.Info("Preparation terminated.");
        }

        /// <summary>
        /// Gets a list of actions to be executed. Every project note should have one action in which the
        /// processors are.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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

        /// <summary>
        /// Gets the processors.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static List<IProcessor> Processors(IUnityContainer container)
        {
            return new List<IProcessor>
                   {
                       new DownloadProcessor(), new ConverterProcessor(container.Resolve<IConverter>()),
                       new ImageExtractorProcessor(), new CleanupProcessor()
                   };
        }

        /// <summary>
        /// Processes the specified processors.
        /// </summary>
        /// <param name="processors">The processors.</param>
        /// <param name="projectNote">The project note.</param>
        /// <remarks></remarks>
        private static void Process(List<IProcessor> processors, IProjectNote projectNote)
        {
            Logger.Info(string.Format("Processing Project Note {0}", projectNote.Id));
            try
            {
                processors.ForEach(proc => proc.Process(projectNote));
                Logger.Info(string.Format("Project Note {0} successfully processed", projectNote.Id));
            }
            catch (Exception e)
            {
                Logger.Error("Error processing Project Note", e);
            }
        }

        /// <summary>
        /// This is a helper method to configure the unity container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <remarks></remarks>
        private static void ConfigureContainer(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            container.RegisterType<IConverter, PdfConverter>();
            container.RegisterType<IProjectNote, ProjectNote>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
        }

        #endregion
    }
}