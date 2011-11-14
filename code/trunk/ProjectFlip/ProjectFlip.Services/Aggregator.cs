using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    internal static class Aggregator
    {
        private const string Separator = "\t";
        public static string MappingFilePath = @"..\..\..\Resources\mapping.txt";
        static Dictionary<string, IMetadata> _mapping = new Dictionary<string, IMetadata>();
        private static readonly string[] Header = new[] { "Kategorie", "Mapping nach", "Mapping von 1", "Mapping von 2", "Mapping von 3", "usw. ..." };

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
                        var elements = line.Split(new[] { Separator.ToCharArray()[0] });
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
            var reverseMapping = ReverseMapping();
            var lines = new List<List<string>> { Header.ToList() };
            reverseMapping.Keys.ToList().ForEach(metadata =>
            {
                var line = new List<string> { metadata.Type.Name, metadata.Description };
                line.AddRange(reverseMapping[metadata]);
                lines.Add(line);
            });

            WriteFile(lines);

        }

        private static Dictionary<IMetadata, List<string>> ReverseMapping()
        {
            var reverseMapping = new Dictionary<IMetadata, List<string>>();
            _mapping.Keys.ToList().ForEach(mappingFrom =>
                {
                    var metadata = _mapping[mappingFrom];
                    if (!reverseMapping.ContainsKey(metadata)) reverseMapping[metadata] = new List<string>();
                    reverseMapping[metadata].Add(mappingFrom);
                });
            return reverseMapping;
        }

        private static void WriteFile(List<List<string>> lines)
        {
            using (var handle = new StreamWriter(MappingFilePath))
            {
                lines.ForEach(line => handle.WriteLine(String.Join(Separator, line)));
            }
        }

        public static IMetadata AggregateMetadata(IMetadata metadata)
        {
            if (!_mapping.ContainsKey(metadata.Description)) _mapping[metadata.Description] = metadata;
            return _mapping[metadata.Description];
        }
    }
}