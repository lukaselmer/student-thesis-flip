using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Preparer
{
    internal interface IProcessor
    {
        bool Process(IProjectNote projectNote);
    }
}