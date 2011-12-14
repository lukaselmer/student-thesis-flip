using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media.Imaging;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface.ViewModels
{
    /// <summary>
    /// The gravatars view model provides the GUI with persons.
    /// </summary>
    /// <remarks></remarks>
    public class GravatarsViewModel : ViewModelBase
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarsViewModel"/> class.
        /// </summary>
        /// <param name="gravatarService">The gravatar service.</param>
        /// <remarks></remarks>
        public GravatarsViewModel(IGravatarService gravatarService)
        {
            Persons = gravatarService.Persons;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the persons.
        /// </summary>
        /// <remarks></remarks>
        public IList<IPerson> Persons { get; private set; }

        #endregion

    }
}