#region

using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#endregion

namespace ProjectFlip.Services.Interfaces
{
    public interface ICyclicCollectionView<T> : IEnumerable<T>, INotifyCollectionChanged
    {
        /// <summary>
        /// Gets or sets the items of the collection.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IList<T> Items { get; set; }

        /// <summary>
        /// Gets the index of the current item.
        /// </summary>
        /// <value>
        /// The index of the current item.
        /// </value>
        int CurrentIndex { get; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        Predicate<T> Filter { get; set; }

        /// <summary>
        /// Gets the number of items.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the current item.
        /// </summary>
        T CurrentItem { get; }

        /// <summary>
        /// Gets the previous item.
        /// </summary>
        T Previous { get; }

        /// <summary>
        /// Gets the next item.
        /// </summary>
        T Next { get; }

        /// <summary>
        /// Gets the <see cref="T"/> at the specified index.
        /// </summary>
        T this[int index] { get; }

        /// <summary>
        /// Refreshes this collection.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Moves the current to the specified item
        /// </summary>
        /// <param name="o">The item.</param>
        /// <returns></returns>
        bool MoveCurrentTo(T o);

        /// <summary>
        /// Moves the current to the last item.
        /// </summary>
        void MoveCurrentToLast();

        /// <summary>
        /// Moves the current to the first item.
        /// </summary>
        void MoveCurrentToFirst();

        /// <summary>
        /// Moves the current to the next item.
        /// </summary>
        void MoveCurrentToNext();

        /// <summary>
        /// Moves the current to previous item.
        /// </summary>
        void MoveCurrentToPrevious();

        /// <summary>
        /// Occurs when [current changed].
        /// </summary>
        event EventHandler CurrentChanged;
    }
}