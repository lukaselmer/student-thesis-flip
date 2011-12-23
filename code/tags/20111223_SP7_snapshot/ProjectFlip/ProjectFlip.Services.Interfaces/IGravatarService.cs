using System.Collections.Generic;

namespace ProjectFlip.Services.Interfaces
{
    /// <summary>
    /// The gravatar service returns the people who were
    /// involved in the project.
    /// </summary>
    /// <remarks></remarks>
    public interface IGravatarService
    {

        /// <summary>
        /// Gets the persons.
        /// </summary>
        /// <remarks></remarks>
        IList<IPerson> Persons { get; }

    }
}