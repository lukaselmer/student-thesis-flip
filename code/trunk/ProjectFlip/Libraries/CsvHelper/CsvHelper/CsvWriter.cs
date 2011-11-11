﻿#region

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

#endregion

namespace Common.Csv
{
    /// <summary>
    /// Class to write data to a csv file
    /// </summary>
    public sealed class CsvWriter : IDisposable
    {
        #region Members

        private string _carriageReturnAndLineFeedReplacement = ",";
        private bool _replaceCarriageReturnsAndLineFeedsFromFieldValues = true;
        private StreamWriter _streamWriter;

        #endregion Members

        #region Properties

        /// <summary>
        /// Gets or sets whether carriage returns and line feeds should be removed from 
        /// field values, the default is true 
        /// </summary>
        public bool ReplaceCarriageReturnsAndLineFeedsFromFieldValues
        {
            // ReSharper disable MemberCanBePrivate.Global
            get // ReSharper restore MemberCanBePrivate.Global
            { return _replaceCarriageReturnsAndLineFeedsFromFieldValues; }
            set { _replaceCarriageReturnsAndLineFeedsFromFieldValues = value; }
        }

        /// <summary>
        /// Gets or sets what the carriage return and line feed replacement characters should be
        /// </summary>
        // ReSharper disable MemberCanBePrivate.Global
        public string CarriageReturnAndLineFeedReplacement // ReSharper restore MemberCanBePrivate.Global
        {
            get { return _carriageReturnAndLineFeedReplacement; }
            // ReSharper disable UnusedMember.Global
            set // ReSharper restore UnusedMember.Global
            { _carriageReturnAndLineFeedReplacement = value; }
        }

        #endregion Properties

        #region Methods

        #region CsvFile write methods

        /// <summary>
        /// Writes csv content to a file
        /// </summary>
        /// <param name="csvFile">CsvFile</param>
        /// <param name="filePath">File path</param>
        /// <param name="encoding">Encoding</param>
        public void WriteCsv(CsvFile csvFile, string filePath, Encoding encoding = null)
        {
            if (File.Exists(filePath)) File.Delete(filePath);

            using (var writer = new StreamWriter(filePath, false, encoding ?? Encoding.Default))
            {
                WriteToStream(csvFile, writer);
                writer.Flush();
                writer.Close();
            }
        }

        /// <summary>
        /// Writes csv content to a stream
        /// </summary>
        /// <param name="csvFile">CsvFile</param>
        /// <param name="stream">Stream</param>
        /// <param name="encoding">Encoding</param>
        public void WriteCsv(CsvFile csvFile, Stream stream, Encoding encoding = null)
        {
            stream.Position = 0;
            _streamWriter = new StreamWriter(stream, encoding ?? Encoding.Default);
            WriteToStream(csvFile, _streamWriter);
            _streamWriter.Flush();
            stream.Position = 0;
        }

        /// <summary>
        /// Writes csv content to a string
        /// </summary>
        /// <param name="csvFile">CsvFile</param>
        /// <param name="encoding">Encoding</param>
        /// <returns>Csv content in a string</returns>
        public string WriteCsv(CsvFile csvFile, Encoding encoding)
        {
            string content;

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream, encoding ?? Encoding.Default))
                {
                    WriteToStream(csvFile, writer);
                    writer.Flush();
                    memoryStream.Position = 0;

                    using (var reader = new StreamReader(memoryStream, encoding ?? Encoding.Default))
                    {
                        content = reader.ReadToEnd();
                        writer.Close();
                        reader.Close();
                        memoryStream.Close();
                    }
                }
            }

            return content;
        }

        #endregion CsvFile write methods

        #region DataTable write methods

        /// <summary>
        /// Writes a DataTable to a file
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="filePath">File path</param>
        /// <param name="encoding">Encoding</param>
        public void WriteCsv(DataTable dataTable, string filePath, Encoding encoding = null)
        {
            if (File.Exists(filePath)) File.Delete(filePath);

            using (var writer = new StreamWriter(filePath, false, encoding ?? Encoding.Default))
            {
                WriteToStream(dataTable, writer);
                writer.Flush();
                writer.Close();
            }
        }

        /// <summary>
        /// Writes a DataTable to a stream
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="stream">Stream</param>
        /// <param name="encoding">Encoding</param>
        public void WriteCsv(DataTable dataTable, Stream stream, Encoding encoding = null)
        {
            stream.Position = 0;
            _streamWriter = new StreamWriter(stream, encoding ?? Encoding.Default);
            WriteToStream(dataTable, _streamWriter);
            _streamWriter.Flush();
            stream.Position = 0;
        }

        /// <summary>
        /// Writes the DataTable to a string
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="encoding">Encoding</param>
        /// <returns>Csv content in a string</returns>
        public string WriteCsv(DataTable dataTable, Encoding encoding)
        {
            string content;

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream, encoding ?? Encoding.Default))
                {
                    WriteToStream(dataTable, writer);
                    writer.Flush();
                    memoryStream.Position = 0;

                    using (var reader = new StreamReader(memoryStream, encoding ?? Encoding.Default))
                    {
                        content = reader.ReadToEnd();
                        writer.Close();
                        reader.Close();
                        memoryStream.Close();
                    }
                }
            }

            return content;
        }

        #endregion DataTable write methods

        /// <summary>
        /// Disposes of all unmanaged resources
        /// </summary>
        public void Dispose()
        {
            if (_streamWriter == null) return;

            _streamWriter.Close();
            _streamWriter.Dispose();
        }

        /// <summary>
        /// Writes the Csv File
        /// </summary>
        /// <param name="csvFile">CsvFile</param>
        /// <param name="writer">TextWriter</param>
        private void WriteToStream(CsvFile csvFile, TextWriter writer)
        {
            if (csvFile.Headers.Count > 0) WriteRecord(csvFile.Headers, writer);

            csvFile.Records.ForEach(record => WriteRecord(record.Fields, writer));
        }

        /// <summary>
        /// Writes the Csv File
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="writer">TextWriter</param>
        private void WriteToStream(DataTable dataTable, TextWriter writer)
        {
            var fields = (from DataColumn column in dataTable.Columns select column.ColumnName).ToList();
            WriteRecord(fields, writer);

            foreach (DataRow row in dataTable.Rows)
            {
                fields.Clear();
                fields.AddRange(row.ItemArray.Select(o => o.ToString()));
                WriteRecord(fields, writer);
            }
        }

        /// <summary>
        /// Writes the record to the underlying stream
        /// </summary>
        /// <param name="fields">Fields</param>
        /// <param name="writer">TextWriter</param>
        private void WriteRecord(IList<string> fields, TextWriter writer)
        {
            for (var i = 0; i < fields.Count; i++)
            {
                var quotesRequired = fields[i].Contains(",");
                var escapeQuotes = fields[i].Contains("\"");
                var fieldValue = (escapeQuotes ? fields[i].Replace("\"", "\"\"") : fields[i]);

                if (ReplaceCarriageReturnsAndLineFeedsFromFieldValues &&
                    (fieldValue.Contains("\r") || fieldValue.Contains("\n")))
                {
                    quotesRequired = true;
                    fieldValue = fieldValue.Replace("\r\n", CarriageReturnAndLineFeedReplacement);
                    fieldValue = fieldValue.Replace("\r", CarriageReturnAndLineFeedReplacement);
                    fieldValue = fieldValue.Replace("\n", CarriageReturnAndLineFeedReplacement);
                }

                writer.Write(string.Format("{0}{1}{0}{2}", (quotesRequired || escapeQuotes ? "\"" : string.Empty),
                    fieldValue, (i < (fields.Count - 1) ? "," : string.Empty)));
            }

            writer.WriteLine();
        }

        #endregion Methods
    }
}