#region

using System;

#endregion

namespace ProjectFlip.Services.Loader
{
    internal static class Program
    {
        private static void Main()
        {
            var importer = new ProjectNotesLoader();
            var list = importer.Import();
            list.ForEach(line => line.ForEach(Console.WriteLine));
            Console.ReadKey();
        }
    }
}