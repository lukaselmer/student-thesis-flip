#region

using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    internal interface IProcessor
    {
        bool Process(IProjectNote projectNote);
    }
}