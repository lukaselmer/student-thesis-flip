#region

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Common.Csv.Test
{
    /// <summary>
    /// Summary description for UnitTests
    /// </summary>
    [TestClass]
    public class UnitTests
    {
        #region Test Data

        private const string TestData1 = @"column one,column two,column three
1,data 2,2010-05-01 11:26:01
";

        private const string TestData2 = @"""column, one"",column two,""column, three""
data 1,""data, 2"",data 3
";

        private const string TestData3 =
            @"""column, one"",""column """"two"",""column, three""
""data """"1"",""data, 2"",data 3
";

        private const string TestData4 =
            @"""column, one"",""column """"two"",""column, three""
""data """",1"",""data, 2"",data 3
";

        private const string TestData5 =
            @"""column, one"",""column """"two"",""column, three""
""data """""""",1"",""dat""""""""""""sa, 2"",data 3
";

        private const string TestData6 =
            @" column one ,  column two  ,   column three   
   1   ,  data 2  , 2010-05-01 11:26:01 
";

        #endregion Test Data

        private static string FilePath
        {
            get
            {
                var filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                if (!filePath.EndsWith("\\")) filePath += "\\";

                return filePath + "abc123xyz.csv";
            }
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [ClassCleanup]
        public static void Cleanup()
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);
        }

        [TestMethod]
        public void CsvReaderTestReadingFromFile()
        {
            File.WriteAllText(FilePath, TestData1, Encoding.Default);

            using (var reader = new CsvReader(FilePath, Encoding.Default))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                Assert.IsTrue(records.Count == 2);

                var csvFile = CreateCsvFile(records[0], records[1]);
                VerifyTestData1(csvFile.Headers, csvFile.Records);
            }

            File.Delete(FilePath);
        }

        [TestMethod]
        public void CsvReaderTestReadingFromStream()
        {
            using (var memoryStream = new MemoryStream(TestData1.Length))
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(TestData1);
                    streamWriter.Flush();

                    using (var reader = new CsvReader(memoryStream, Encoding.Default))
                    {
                        var records = new List<List<string>>();

                        while (reader.ReadNextRecord()) records.Add(reader.Fields);

                        Assert.IsTrue(records.Count == 2);

                        var csvFile = CreateCsvFile(records[0], records[1]);
                        VerifyTestData1(csvFile.Headers, csvFile.Records);
                    }
                }
            }
        }

        [TestMethod]
        public void CsvReaderTestReadingFromString()
        {
            using (var reader = new CsvReader(Encoding.Default, TestData1))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                Assert.IsTrue(records.Count == 2);

                var csvFile = CreateCsvFile(records[0], records[1]);
                VerifyTestData1(csvFile.Headers, csvFile.Records);
            }
        }

        [TestMethod]
        public void CsvReaderTestReadIntoDataTableWithTypes()
        {
            DataTable dataTable;

            using (var reader = new CsvReader(Encoding.Default, TestData1) { HasHeaderRow = true })
            {
                dataTable = reader.ReadIntoDataTable(new[] { typeof(int), typeof(string), typeof(DateTime) });
            }

            var file = CreateCsvFileFromDataTable(dataTable);
            VerifyTestData1(file.Headers, file.Records);
        }

        [TestMethod]
        public void CsvReaderTestReadIntoDataTableWithoutTypes()
        {
            DataTable dataTable;

            using (var reader = new CsvReader(Encoding.Default, TestData1) { HasHeaderRow = true })
            {
                dataTable = reader.ReadIntoDataTable();
            }

            var file = CreateCsvFileFromDataTable(dataTable);
            VerifyTestData1(file.Headers, file.Records);
        }

        [TestMethod]
        public void CsvReaderTestReadingFromStringWithSampleData2()
        {
            using (var reader = new CsvReader(Encoding.Default, TestData2))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                Assert.IsTrue(records.Count == 2);

                var csvFile = CreateCsvFile(records[0], records[1]);
                VerifyTestData2(csvFile.Headers, csvFile.Records);
            }
        }

        [TestMethod]
        public void CsvReaderTestReadingFromStringWithSampleData3()
        {
            using (var reader = new CsvReader(Encoding.Default, TestData3))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                Assert.IsTrue(records.Count == 2);

                var csvFile = CreateCsvFile(records[0], records[1]);
                VerifyTestData3(csvFile.Headers, csvFile.Records);
            }
        }

        [TestMethod]
        public void CsvReaderTestReadingFromStringWithSampleData4()
        {
            using (var reader = new CsvReader(Encoding.Default, TestData4))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                Assert.IsTrue(records.Count == 2);

                var csvFile = CreateCsvFile(records[0], records[1]);
                VerifyTestData4(csvFile.Headers, csvFile.Records);
            }
        }

        [TestMethod]
        public void CsvReaderTestReadingFromStringWithSampleData5()
        {
            using (var reader = new CsvReader(Encoding.Default, TestData5))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                Assert.IsTrue(records.Count == 2);

                var csvFile = CreateCsvFile(records[0], records[1]);
                VerifyTestData5(csvFile.Headers, csvFile.Records);
            }
        }

        [TestMethod]
        public void CsvReaderTestReadingFromStringWithSampleData6()
        {
            using (var reader = new CsvReader(Encoding.Default, TestData6))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                Assert.IsTrue(records.Count == 2);

                var csvFile = CreateCsvFile(records[0], records[1]);
                VerifyTestData6(csvFile.Headers, csvFile.Records);
            }
        }

        [TestMethod]
        public void CsvReaderTestColumnTrimming()
        {
            using (var reader = new CsvReader(Encoding.Default, TestData6) { TrimColumns = true })
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                Assert.IsTrue(records.Count == 2);

                var csvFile = CreateCsvFile(records[0], records[1]);
                VerifyTestData6Trimmed(csvFile.Headers, csvFile.Records);
            }
        }

        [TestMethod]
        public void CsvFilePopulateFromFileWithHeader()
        {
            CsvFile csvFile1;
            using (var reader = new CsvReader(Encoding.Default, TestData5))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                csvFile1 = CreateCsvFile(records[0], records[1]);
            }

            if (File.Exists(FilePath)) File.Delete(FilePath);

            using (var writer = new CsvWriter())
            {
                writer.WriteCsv(csvFile1, FilePath, Encoding.Default);
            }

            var file = new CsvFile();
            file.Populate(FilePath, true);
            VerifyTestData5(file.Headers, file.Records);

            File.Delete(FilePath);
        }

        [TestMethod]
        public void CsvFilePopulateFromFileWithoutHeader()
        {
            CsvFile csvFile1;
            using (var reader = new CsvReader(Encoding.Default, TestData5))
            {
                var records = new List<List<string>>();

                while (reader.ReadNextRecord()) records.Add(reader.Fields);

                csvFile1 = CreateCsvFile(records[0], records[1]);
            }

            if (File.Exists(FilePath)) File.Delete(FilePath);

            using (var writer = new CsvWriter())
            {
                writer.WriteCsv(csvFile1, FilePath, Encoding.Default);
            }

            var file = new CsvFile();
            file.Populate(FilePath, false);
            VerifyTestData5Alternative(file.Records);

            File.Delete(FilePath);
        }

        [TestMethod]
        public void CsvFilePopulateFromStream()
        {
            using (var memoryStream = new MemoryStream(TestData5.Length))
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(TestData5);
                    streamWriter.Flush();

                    var file = new CsvFile();
                    file.Populate(memoryStream, true);
                    VerifyTestData5(file.Headers, file.Records);
                }
            }
        }

        [TestMethod]
        public void CsvFilePopulateFromString()
        {
            var file = new CsvFile();
            file.Populate(true, TestData5);
            VerifyTestData5(file.Headers, file.Records);
        }

        [TestMethod]
        public void CsvFileIndexers()
        {
            var file = new CsvFile();
            file.Populate(true, TestData2);

            Assert.IsTrue(file[0] == file.Records[0]);
            Assert.IsTrue(string.Compare(file[0, 1], "data, 2") == 0);
            Assert.IsTrue(string.Compare(file[0, "column two"], "data, 2") == 0);
        }

        [TestMethod]
        public void CsvWriterWriteCsvFileObjectToFile()
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);

            var csvFile = new CsvFile();
            csvFile.Populate(true, TestData5);

            using (var writer = new CsvWriter())
            {
                writer.WriteCsv(csvFile, FilePath);
            }

            csvFile = new CsvFile();
            csvFile.Populate(FilePath, true);

            VerifyTestData5(csvFile.Headers, csvFile.Records);

            File.Delete(FilePath);
        }

        [TestMethod]
        public void CsvWriterWriteCsvFileObjectToStream()
        {
            string content;

            using (var memoryStream = new MemoryStream())
            {
                var csvFile = new CsvFile();
                csvFile.Populate(true, TestData5);

                using (var writer = new CsvWriter())
                {
                    writer.WriteCsv(csvFile, memoryStream);
                    using (var reader = new StreamReader(memoryStream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }

            Assert.IsTrue(string.Compare(content, TestData5) == 0);
        }

        [TestMethod]
        public void CsvWriterWriteCsvFileObjectToString()
        {
            var csvFile = new CsvFile();
            csvFile.Populate(true, TestData5);
            string content;

            using (var writer = new CsvWriter())
            {
                content = writer.WriteCsv(csvFile, Encoding.Default);
            }

            Assert.IsTrue(string.Compare(content, TestData5) == 0);
        }

        [TestMethod]
        public void CsvWriterWriteDataTableToFile()
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);

            DataTable table;

            using (var reader = new CsvReader(Encoding.Default, TestData5))
            {
                table = reader.ReadIntoDataTable();
            }

            using (var writer = new CsvWriter())
            {
                writer.WriteCsv(table, FilePath);
            }

            var csvFile = CreateCsvFileFromDataTable(table);
            VerifyTestData5(csvFile.Headers, csvFile.Records);
            File.Delete(FilePath);
        }

        [TestMethod]
        public void CsvWriterWriteDataTableToStream()
        {
            string content;

            using (var memoryStream = new MemoryStream())
            {
                DataTable table;

                using (var reader = new CsvReader(Encoding.Default, TestData5))
                {
                    table = reader.ReadIntoDataTable();
                }

                using (var writer = new CsvWriter())
                {
                    writer.WriteCsv(table, memoryStream);

                    using (var reader = new StreamReader(memoryStream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }

            Assert.IsTrue(string.Compare(content, TestData5) == 0);
        }

        [TestMethod]
        public void CsvWriterWriteDataTableToString()
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);

            DataTable table;

            using (var reader = new CsvReader(Encoding.Default, TestData5))
            {
                table = reader.ReadIntoDataTable();
            }

            string content;

            using (var writer = new CsvWriter())
            {
                content = writer.WriteCsv(table, Encoding.Default);
            }

            File.Delete(FilePath);
            Assert.IsTrue(string.Compare(content, TestData5) == 0);
        }

        [TestMethod]
        public void CsvWriterVerifyThatCarriageReturnsAreHandledCorrectlyInFieldValues()
        {
            var csvFile = new CsvFile();
            csvFile.Headers.Add("header ,1");
            csvFile.Headers.Add("header\r\n2");
            csvFile.Headers.Add("header 3");

            var record = new CsvRecord();
            record.Fields.Add("da,ta 1");
            record.Fields.Add("\"data\" 2");
            record.Fields.Add("data\n3");
            csvFile.Records.Add(record);

            string content;

            using (var writer = new CsvWriter())
            {
                content = writer.WriteCsv(csvFile, Encoding.Default);
            }

            Assert.IsTrue(
                string.Compare(content,
                    "\"header ,1\",\"header,2\",header 3\r\n\"da,ta 1\",\"\"\"data\"\" 2\",\"data,3\"\r\n") == 0);

            using (var writer = new CsvWriter { ReplaceCarriageReturnsAndLineFeedsFromFieldValues = false })
            {
                content = writer.WriteCsv(csvFile, Encoding.Default);
            }

            Assert.IsTrue(
                string.Compare(content,
                    "\"header ,1\",header\r\n2,header 3\r\n\"da,ta 1\",\"\"\"data\"\" 2\",data\n3\r\n") == 0);
        }

        private CsvFile CreateCsvFileFromDataTable(DataTable table)
        {
            var file = new CsvFile();

            foreach (DataColumn column in table.Columns) file.Headers.Add(column.ColumnName);

            foreach (DataRow row in table.Rows)
            {
                var record = new CsvRecord();

                foreach (var o in row.ItemArray)
                {
                    if (o is DateTime) record.Fields.Add(((DateTime)o).ToString("yyyy-MM-dd hh:mm:ss"));
                    else record.Fields.Add(o.ToString());
                }

                file.Records.Add(record);
            }

            return file;
        }

        private CsvFile CreateCsvFile(List<string> headers, List<string> fields)
        {
            var csvFile = new CsvFile();

            headers.ForEach(header => csvFile.Headers.Add(header));
            var record = new CsvRecord();
            fields.ForEach(field => record.Fields.Add(field));
            csvFile.Records.Add(record);
            return csvFile;
        }

        #region Verification methods

        private void VerifyTestData1(List<string> headers, CsvRecords records)
        {
            Assert.IsTrue(headers.Count == 3);
            Assert.IsTrue(records.Count == 1);
            Assert.AreEqual("column one", headers[0]);
            Assert.AreEqual("column two", headers[1]);
            Assert.AreEqual("column three", headers[2]);
            Assert.AreEqual("1", records[0].Fields[0]);
            Assert.AreEqual("data 2", records[0].Fields[1]);
            Assert.AreEqual("2010-05-01 11:26:01", records[0].Fields[2]);
        }

        private void VerifyTestData2(List<string> headers, CsvRecords records)
        {
            Assert.IsTrue(headers.Count == 3);
            Assert.IsTrue(records.Count == 1);
            Assert.AreEqual("column, one", headers[0]);
            Assert.AreEqual("column two", headers[1]);
            Assert.AreEqual("column, three", headers[2]);
            Assert.AreEqual("data 1", records[0].Fields[0]);
            Assert.AreEqual("data, 2", records[0].Fields[1]);
            Assert.AreEqual("data 3", records[0].Fields[2]);
        }

        private void VerifyTestData3(List<string> headers, CsvRecords records)
        {
            Assert.IsTrue(headers.Count == 3);
            Assert.IsTrue(records.Count == 1);
            Assert.AreEqual("column, one", headers[0]);
            Assert.AreEqual("column \"two", headers[1]);
            Assert.AreEqual("column, three", headers[2]);
            Assert.AreEqual("data \"1", records[0].Fields[0]);
            Assert.AreEqual("data, 2", records[0].Fields[1]);
            Assert.AreEqual("data 3", records[0].Fields[2]);
        }

        private void VerifyTestData4(List<string> headers, CsvRecords records)
        {
            Assert.IsTrue(headers.Count == 3);
            Assert.IsTrue(records.Count == 1);
            Assert.AreEqual("column, one", headers[0]);
            Assert.AreEqual("column \"two", headers[1]);
            Assert.AreEqual("column, three", headers[2]);
            Assert.AreEqual("data \",1", records[0].Fields[0]);
            Assert.AreEqual("data, 2", records[0].Fields[1]);
            Assert.AreEqual("data 3", records[0].Fields[2]);
        }

        private void VerifyTestData5(List<string> headers, CsvRecords records)
        {
            Assert.IsTrue(headers.Count == 3);
            Assert.IsTrue(records.Count == 1);
            Assert.AreEqual("column, one", headers[0]);
            Assert.AreEqual("column \"two", headers[1]);
            Assert.AreEqual("column, three", headers[2]);
            Assert.AreEqual("data \"\",1", records[0].Fields[0]);
            Assert.AreEqual("dat\"\"\"sa, 2", records[0].Fields[1]);
            Assert.AreEqual("data 3", records[0].Fields[2]);
        }

        private void VerifyTestData5Alternative(CsvRecords records)
        {
            Assert.IsTrue(records.Count == 2);
            Assert.AreEqual("column, one", records[0].Fields[0]);
            Assert.AreEqual("column \"two", records[0].Fields[1]);
            Assert.AreEqual("column, three", records[0].Fields[2]);
            Assert.AreEqual("data \"\",1", records[1].Fields[0]);
            Assert.AreEqual("dat\"\"\"sa, 2", records[1].Fields[1]);
            Assert.AreEqual("data 3", records[1].Fields[2]);
        }

        private void VerifyTestData6(List<string> headers, CsvRecords records)
        {
            Assert.IsTrue(headers.Count == 3);
            Assert.IsTrue(records.Count == 1);
            Assert.AreEqual(" column one ", headers[0]);
            Assert.AreEqual("  column two  ", headers[1]);
            Assert.AreEqual("   column three   ", headers[2]);
            Assert.AreEqual("   1   ", records[0].Fields[0]);
            Assert.AreEqual("  data 2  ", records[0].Fields[1]);
            Assert.AreEqual(" 2010-05-01 11:26:01 ", records[0].Fields[2]);
        }

        private void VerifyTestData6Trimmed(List<string> headers, CsvRecords records)
        {
            Assert.IsTrue(headers.Count == 3);
            Assert.IsTrue(records.Count == 1);
            Assert.AreEqual("column one", headers[0]);
            Assert.AreEqual("column two", headers[1]);
            Assert.AreEqual("column three", headers[2]);
            Assert.AreEqual("1", records[0].Fields[0]);
            Assert.AreEqual("data 2", records[0].Fields[1]);
            Assert.AreEqual("2010-05-01 11:26:01", records[0].Fields[2]);
        }

        #endregion Verification methods
    }
}