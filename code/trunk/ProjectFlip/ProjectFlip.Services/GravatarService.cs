using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class GravatarService : IGravatarService
    {
        #region Constructor

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

        public IList<IPerson> Persons { get; set; }

        #endregion
    }
}
