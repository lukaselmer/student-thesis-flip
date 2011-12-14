using System.Collections.Generic;

namespace ProjectFlip.Services.Interfaces
{
    public interface IGravatarService {
        IList<IPerson> Persons { get; }
    }
}