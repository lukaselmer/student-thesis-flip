using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ProjectFlip.Services.Interfaces
{
    public interface ICyclicCollectionView<T> : IEnumerable<T>, INotifyCollectionChanged
    {
        IList<T> Items { get; set; }

        int CurrentIndex { get; }

        Predicate<T> Filter { get; set; }
        int Count { get; }
        T CurrentItem { get; }
        T Previous { get; }
        T Next { get; }
        T this[int index] { get; }
        void Refresh();
        bool MoveCurrentTo(T o);
        void MoveCurrentToLast();
        void MoveCurrentToFirst();
        void MoveCurrentToNext();
        void MoveCurrentToPrevious();
        event EventHandler CurrentChanged;
    }
}