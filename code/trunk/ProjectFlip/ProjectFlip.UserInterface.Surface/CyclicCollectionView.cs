using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Data;

namespace ProjectFlip.UserInterface.Surface
{
    public class CyclicCollectionView : ListCollectionView, ICyclicCollectionView
    {
        public CyclicCollectionView(IList projectNotes) : base(projectNotes) { }

        private int NextIndex()
        {
            if (Count == 0) return 0;
            return (CurrentPosition + 1 + Count) % Count;
        }

        private int PreviousIndex()
        {
            if (Count == 0) return 0;
            return (CurrentPosition - 1 + Count) % Count;
        }

        public override bool MoveCurrentToNext()
        {
            return MoveCurrentToPosition(NextIndex());
        }

        public override bool MoveCurrentToPrevious()
        {
            return MoveCurrentToPosition(PreviousIndex());
        }

        public object Next { get { return Count == 0 ? null : base.GetItemAt(NextIndex()); } }
        public object Previous { get { return Count == 0 ? null : base.GetItemAt(PreviousIndex()); } }
    }
}