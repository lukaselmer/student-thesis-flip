using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media.Imaging;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface.ViewModels
{
    public class GravatarsViewModel : ViewModelBase
    {

        #region Constructor

        public GravatarsViewModel(IGravatarService gravatarService)
        {
            Persons = gravatarService.Persons;
        }

        #endregion

        #region Properties

        public IList<IPerson> Persons { get; private set; }

        #endregion

    }
}