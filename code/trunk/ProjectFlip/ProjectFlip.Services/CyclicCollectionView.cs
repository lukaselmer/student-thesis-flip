#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    /// <summary>
    /// A implementation of the typesafe cyclic collection view.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks></remarks>
    public sealed class CyclicCollectionView<T> : NotifierModel, ICyclicCollectionView<T>
    {
        #region Declarations

        private int _currentIndex;
        private Predicate<T> _filter;
        private IList<T> _items;
        private IList<T> OriginalItems { get; set; }

        /// <summary>
        /// Occurs when the current position has changed.
        /// </summary>
        /// <remarks></remarks>
        public event EventHandler CurrentChanged;

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        /// <remarks></remarks>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CyclicCollectionView&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="projectNotes">The project notes.</param>
        /// <remarks></remarks>
        public CyclicCollectionView(IList<T> projectNotes)
        {
            Items = OriginalItems = projectNotes;
            CurrentIndex = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the items of the collection.
        /// </summary>
        /// <value>The items.</value>
        /// <remarks></remarks>
        public IList<T> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                Notify("Items");
            }
        }

        /// <summary>
        /// Gets the index of the current item.
        /// </summary>
        /// <remarks></remarks>
        public int CurrentIndex
        {
            get { return _currentIndex; }
            private set
            {
                _currentIndex = value;
                Notify("CurrentIndex");
            }
        }

        /// <summary>
        /// Gets or sets the filter which is applied to each of the items.
        /// </summary>
        /// <value>The filter.</value>
        /// <remarks></remarks>
        public Predicate<T> Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                Refresh();
                Notify("Filter");
            }
        }

        /// <summary>
        /// Gets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <remarks></remarks>
        public T this[int index] { get { return Count == 0 ? default(T) : Items[(index + Count) % Count]; } }

        /// <summary>
        /// Gets the count of the remaining items which have not been filtered away.
        /// </summary>
        /// <remarks></remarks>
        public int Count { get { return Items.Count; } }

        /// <summary>
        /// Gets the count of the original items.
        /// </summary>
        /// <remarks></remarks>
        public int OriginalCount { get { return OriginalItems.Count; } }

        /// <summary>
        /// Gets the current item.
        /// </summary>
        /// <remarks></remarks>
        public T CurrentItem { get { return this[CurrentIndex]; } }

        /// <summary>
        /// Gets the previous item.
        /// </summary>
        /// <remarks></remarks>
        public T Previous { get { return this[CurrentIndex - 1]; } }

        /// <summary>
        /// Gets the next item.
        /// </summary>
        /// <remarks></remarks>
        public T Next { get { return this[CurrentIndex + 1]; } }

        #endregion

        #region Other

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.</returns>
        /// <remarks></remarks>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        /// <remarks></remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Refreshes this instance when a new filter has been applied.
        /// </summary>
        /// <remarks></remarks>
        public void Refresh()
        {
            if (_filter == null) return;
            var selectedItem = Count > 0 ? Items[CurrentIndex] : default(T);
            Items = OriginalItems.Where(o => _filter(o)).ToList();
            UpdateIndex(selectedItem);
            OnCollectionChanged();
            OnCurrentChanged();
        }

        /// <summary>
        /// Moves the current item to the <param name="newCurrentItem"></param>.
        /// </summary>
        /// <param name="newCurrentItem">The newCurrentItem.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool MoveCurrentTo(T newCurrentItem)
        {
            if (Count == 0) return false;

            // ReSharper disable CompareNonConstrainedGenericWithNull
            if ((!typeof(T).IsValueType) && newCurrentItem == null)
            {
                CurrentIndex = 0;
                return true;
            }
            // ReSharper restore CompareNonConstrainedGenericWithNull

            if (!Items.Contains(newCurrentItem)) return false;

            CurrentIndex = Items.IndexOf(newCurrentItem);

            Debug.Assert(Equals(CurrentItem, newCurrentItem));
            return true;
        }

        /// <summary>
        /// Moves the current item to the last item.
        /// </summary>
        /// <remarks></remarks>
        public void MoveCurrentToLast()
        {
            if (Count == 0) return;
            CurrentIndex = Count - 1;
            OnCurrentChanged();
        }

        /// <summary>
        /// Moves the current item to the first item.
        /// </summary>
        /// <remarks></remarks>
        public void MoveCurrentToFirst()
        {
            if (Count == 0) return;
            CurrentIndex = 0;
            OnCurrentChanged();
        }

        /// <summary>
        /// Moves the current item to the next item.
        /// </summary>
        /// <remarks></remarks>
        public void MoveCurrentToNext()
        {
            if (Count == 0) return;
            CurrentIndex = (CurrentIndex + 1) % Count;
            OnCurrentChanged();
        }

        /// <summary>
        /// Moves the current item to the previous item.
        /// </summary>
        /// <remarks></remarks>
        public void MoveCurrentToPrevious()
        {
            if (Count == 0) return;
            CurrentIndex = (CurrentIndex - 1 + Count) % Count;
            OnCurrentChanged();
        }

        /// <summary>
        /// Sets the current index to a selected item.
        /// </summary>
        /// <param name="selectedItem">The selected item.</param>
        /// <remarks></remarks>
        private void UpdateIndex(T selectedItem)
        {
            CurrentIndex = 0;
            if (Items.Contains(selectedItem)) CurrentIndex = Items.IndexOf(selectedItem);
        }

        /// <summary>
        /// Called when the collection has changed.
        /// </summary>
        /// <remarks></remarks>
        private void OnCollectionChanged()
        {
            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            Notify("Count");
        }

        /// <summary>
        /// Raises the CurrentChanged event.
        /// </summary>
        private void OnCurrentChanged()
        {
            if (CurrentChanged != null) CurrentChanged(this, EventArgs.Empty);
            Notify("Items");
        }

        #endregion
    }
}