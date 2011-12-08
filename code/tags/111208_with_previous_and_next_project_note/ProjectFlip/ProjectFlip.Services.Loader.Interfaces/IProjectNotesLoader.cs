#region

using System;
using System.Collections.Generic;

#endregion

namespace ProjectFlip.Services.Loader.Interfaces
{
    public interface IProjectNotesLoader
    {
        // ReSharper disable UnusedMemberInSuper.Global
        string Filename { get; set; }
        List<List<String>> Import();
        // ReSharper restore UnusedMemberInSuper.Global
    }
}