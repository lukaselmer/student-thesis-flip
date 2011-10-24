using System;
using System.Collections.Generic;

namespace ProjectFlip.Services.Loader.Interfaces
{
    public interface IProjectNotesLoader
    {
        List<List<String>> Import();
    }
}