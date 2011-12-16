using System.ComponentModel;
using System.Windows.Data;
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
            Persons = new CollectionView(gravatarService.Persons);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the persons.
        /// </summary>
        /// <remarks></remarks>
        public ICollectionView Persons { get; private set; }

        #endregion

    }
}