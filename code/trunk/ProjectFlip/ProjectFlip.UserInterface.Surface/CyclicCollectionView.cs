#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using ProjectFlip.Services;

#endregion

namespace ProjectFlip.UserInterface.Surface
{
    public class CyclicCollectionView<T> : NotifierModel, IEnumerable<T>, INotifyCollectionChanged
    {
        private int _currentIndex;
        private Predicate<T> _filter;

        private IList<T> _items;

        public CyclicCollectionView(IList<T> projectNotes)
        {
            Items = OriginalItems = projectNotes;
            CurrentIndex = 0;
        }

        // ReSharper disable MemberCanBePrivate.Global
        public IList<T> Items
        // ReSharper restore MemberCanBePrivate.Global
        {
            get { return _items; }
            set
            {
                _items = value;
                Notify("Items");
            }
        }

        // ReSharper disable MemberCanBePrivate.Global
        public int CurrentIndex
        // ReSharper restore MemberCanBePrivate.Global
        {
            get { return _currentIndex; }
            private set
            {
                _currentIndex = value;
                Notify("CurrentIndex");
            }
        }

        private IList<T> OriginalItems { get; set; }

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

        // ReSharper disable MemberCanBePrivate.Global
        public T this[int index] { get { return Count == 0 ? default(T) : Items[(index + Count) % Count]; } }
        // ReSharper restore MemberCanBePrivate.Global

        public int Count { get { return Items.Count; } }

        public T CurrentItem { get { return this[CurrentIndex]; } }

        public T Previous { get { return this[CurrentIndex - 1]; } }

        public T Next { get { return this[CurrentIndex + 1]; } }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        public void Refresh()
        {
            if (_filter == null) return;
            var selectedItem = Count > 0 ? Items[CurrentIndex] : default(T);
            Items = OriginalItems.Where(o => _filter(o)).ToList();
            UpdateIndex(selectedItem);
            OnCollectionChanged();
            OnCurrentChanged();
        }

        private void UpdateIndex(T selectedItem)
        {
            CurrentIndex = 0;
            if (Items.Contains(selectedItem)) CurrentIndex = Items.IndexOf(selectedItem);
        }

        private void OnCollectionChanged()
        {
            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool MoveCurrentTo(T o)
        {
            if (Count == 0) return false;

            // ReSharper disable CompareNonConstrainedGenericWithNull
            if ((!typeof(T).IsValueType) && o == null)
            {
                CurrentIndex = 0;
                return true;
            }
            // ReSharper restore CompareNonConstrainedGenericWithNull

            if (!Items.Contains(o)) return false;

            CurrentIndex = Items.IndexOf(o);

            Debug.Assert(Equals(CurrentItem, o));
            return true;
        }

        public void MoveCurrentToLast()
        {
            if (Count == 0) return;
            CurrentIndex = Count - 1;
            OnCurrentChanged();
        }

        public void MoveCurrentToFirst()
        {
            if (Count == 0) return;
            CurrentIndex = 0;
            OnCurrentChanged();
        }

        public void MoveCurrentToNext()
        {
            if (Count == 0) return;
            CurrentIndex = (CurrentIndex + 1) % Count;
            OnCurrentChanged();
        }

        public void MoveCurrentToPrevious()
        {
            if (Count == 0) return;
            CurrentIndex = (CurrentIndex - 1 + Count) % Count;
            OnCurrentChanged();
        }

        public event EventHandler CurrentChanged;

        /// <summary>
        /// Raises the CurrentChanged event
        /// </summary>
        protected virtual void OnCurrentChanged()
        {
            if (CurrentChanged != null) CurrentChanged(this, EventArgs.Empty);
            Notify("Items");
        }
    }
}