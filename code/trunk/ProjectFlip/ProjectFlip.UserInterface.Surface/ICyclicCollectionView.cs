#region

using System.ComponentModel;

#endregion

namespace ProjectFlip.UserInterface.Surface
{
    public interface ICyclicCollectionView : ICollectionView
    {
        object Next { get; }
        object Previous { get; }
    }
}