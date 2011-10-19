﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Preparer
{
    class DownloadProcessor : IProcessor
    {
        public bool Process(IProjectNote projectNote)
        {

            if (!File.Exists(projectNote.FilepathPdf)) Download(projectNote);
            return true;
        }

        private bool Download(IProjectNote projectNote, int i = 0)
        {
            try
            {
                var regex = new Regex(@"\.pdf$");
                var downloadUrl = regex.Replace(projectNote.Url, (i == 0 ? "" : ("_0" + i)) + ".pdf");
                new WebClient().DownloadFile(downloadUrl, projectNote.FilepathPdf);
                return true;
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.ProtocolError) throw;
                if (i >= 9) return false;
                return Download(projectNote, i + 1);
            }
        }
    }
}
