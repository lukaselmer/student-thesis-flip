using System;
using System.Collections.Generic;

namespace ProjectFlip.Services.Loader
{
    public interface IProjectNotesLoader
    {
        List<List<String>> Import();
    }
}