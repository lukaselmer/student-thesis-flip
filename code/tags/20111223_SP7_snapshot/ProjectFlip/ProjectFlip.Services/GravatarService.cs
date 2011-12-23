using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    /// <summary>
    /// The gravatar service returns the people who were
    /// involved in the project.
    /// </summary>
    /// <remarks></remarks>
    public class GravatarService : IGravatarService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <remarks></remarks>
        public GravatarService()
        {
            Persons = new List<IPerson>
                      {
                          new Person("Christina Heidt", "cheidt@hsr.ch"),
                          new Person("Delia Treichler", "dtreichl@hsr.ch"),
                          new Person("Lukas Elmer", "lelmer@hsr.ch")
                      };
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the persons.
        /// </summary>
        /// <value>The persons.</value>
        /// <remarks></remarks>
        public IList<IPerson> Persons { get; private set; }

        #endregion
    }
}
