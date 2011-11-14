using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using ComLib;
//using ComLib.CsvParse;
using ComLib.CsvParse;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    internal static class Aggregator
    {
        public static string MappingFilePath = @"..\..\..\Resources\mapping.txt";
        static Dictionary<string, IMetadata> _mapping = new Dictionary<string, IMetadata>();

        public static void LoadMapping()
        {
            try
            {
                if (!File.Exists(MappingFilePath)) return;
                _mapping = new Dictionary<string, IMetadata>();

                using (var handle = new StreamReader(MappingFilePath))
                {
                    handle.ReadLine(); // Skip header line
                    while (!handle.EndOfStream)
                    {
                        var line = handle.ReadLine();
                        Debug.Assert(line != null, "line != null");
                        var elements = line.Split(new[] { '\t' });
                        if (elements.Length < 2) continue;
                        var category = elements[0];
                        var mapTo = elements[1];
                        var mappingsTo = elements.Skip(2);
                        mappingsTo.ToList().ForEach(el => _mapping[el] = Metadata.Get(MetadataType.Get(category), mapTo));
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void SaveMapping()
        {
            try
            {
                var reverseMapping = new Dictionary<IMetadata, List<string>>();
                _mapping.Keys.ToList().ForEach(mappingFrom =>
                    {
                        var metadata = _mapping[mappingFrom];
                        if (!reverseMapping.ContainsKey(metadata)) reverseMapping[metadata] = new List<string>();
                        reverseMapping[metadata].Add(mappingFrom);
                    });

                using (var handle = new StreamWriter(MappingFilePath))
                {
                    var header = new[]
                    {
                        "Kategorie", "Mapping nach", "Mapping von 1", "Mapping von 2", "Mapping von 3",
                        "usw. ..."
                    };
                    handle.WriteLine(String.Join("\t", header)); // Write header line
                    reverseMapping.Keys.ToList().ForEach(metadata =>
                        {
                            var line = new List<string> { metadata.Type.Name, metadata.Description };
                            line.AddRange(reverseMapping[metadata]);
                            handle.WriteLine(String.Join("\t", line));
                        });
                    handle.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            //Csv.Write(filePath, objectsToWrite, false);
            /*using(var writer = new CsvWriter(filePath, objectsToWrite, ",", columns, false, false, "\"", "\n", false))
            {
                writer.Write();
            }*/

            //var csv = Csv.LoadText(text, true);
            /*var csvFile = new CsvFile();
            csvFile.Populate(filePath, true);
            
            using (var writer = new CsvWriter())
            {
                writer.WriteCsv(csvFile, filePath);
            }*/
        }

        private static void WriteLineToMapping(string obj)
        {
        }

        public static IMetadata AggregateMetadata(IMetadata metadata)
        {
            if (!_mapping.ContainsKey(metadata.Description)) _mapping[metadata.Description] = metadata;
            return _mapping[metadata.Description];
        }
    }
}