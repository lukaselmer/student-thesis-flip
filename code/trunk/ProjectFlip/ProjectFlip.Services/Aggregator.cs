using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Csv;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    internal static class Aggregator
    {
        static Dictionary<string, IMetadata> _mapping = new Dictionary<string, IMetadata>();

        public static void LoadMapping(string filePath)
        {
            if (!File.Exists(filePath)) return;


            using (var handle = new StreamReader(filePath))
            {
                while (!handle.EndOfStream)
                {
                    var line = handle.ReadLine();
                    //line.Split(new []{','}, 2);
                    //MetadataType.TryParse()
                }
            }
            _mapping = new Dictionary<string, IMetadata>();
        }

        public static void SaveMapping(string filePath)
        {
            var csvFile = new CsvFile();
            csvFile.Populate(filePath, true);
            
            using (var writer = new CsvWriter())
            {
                writer.WriteCsv(csvFile, filePath);
            }
        }

        public static IMetadata AggregateMetadata(IMetadata metadata)
        {
            return _mapping.ContainsKey(metadata.Description) ? _mapping[metadata.Description] : metadata;
        }
    }
}