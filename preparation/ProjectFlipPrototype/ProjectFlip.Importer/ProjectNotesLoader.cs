﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ProjectFlip.Services.Loader
{
    public class ProjectNotesLoader : IProjectNotesLoader
    {
        private List<List<string>> _lines;

        public ProjectNotesLoader(string filename = null)
        {
            Filename = filename ?? @"..\..\..\Resources\metadata.txt";
        }

        public string Filename { get; set; }

        public List<List<String>> Import()
        {
            LoadText();
            CheckText();
            CleanFields();
            return Rows();
        }

        private void CleanFields()
        {
            _lines = _lines.ConvertAll(line => line.ConvertAll(f => f.Trim()));
        }

        private List<List<string>> Rows()
        {
            return _lines.GetRange(1, _lines.Count - 1);
        }

        private void CheckText()
        {
            foreach (var ss in _lines)
            {
                Debug.Assert(ss.Count == 19, "Wrong text file: Expected each Line to contain 19 elements (18 tabs).");
            }
        }

        private void LoadText()
        {
            _lines = new List<List<string>>();
            using (var sr = new StreamReader(Filename))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    _lines.Add(new List<string>(line.Split('\t')));
                }
            }
        }
    }
}