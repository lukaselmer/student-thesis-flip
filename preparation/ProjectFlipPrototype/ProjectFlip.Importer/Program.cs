using System;

namespace ProjectFlip.Services.Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            var importer = new ProjectNotesLoader();
            var list = importer.Import();

            list.ForEach(line => line.ForEach(Console.WriteLine));

            Console.ReadKey();
        }
    }
}
