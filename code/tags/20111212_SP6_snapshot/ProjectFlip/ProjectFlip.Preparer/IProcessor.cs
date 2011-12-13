#region

using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    internal interface IProcessor
    {

        /// <summary>
        /// Implements a step in the process of the preprocessing of the specified project note.
        /// </summary>
        /// <param name="projectNote">The project note.</param>
        /// <returns></returns>
        bool Process(IProjectNote projectNote);
    }
}