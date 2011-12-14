#region

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

#endregion

namespace ProjectFlip.UserInterface.Surface.Helpers
{
    /// <summary>
    /// The ScrollToTopBehavior is a behaviour which is used for the scrollable lists. When the user scrolls down
    /// and then changes the items (e.g. by adding a filter criteria), this class is responsible that the list
    /// scrolls to top.
    /// </summary>
    /// <remarks>
    /// This class was inspired by http://stackoverflow.com/questions/4793030/wpf-reset-listbox-scroll-position-when-itemssource-changes
    /// It could potentially lead to memory leaks if the itemsChangedEventHandler does not unregister itself when
    /// closing the window. However, this is not that important in this project because no scrollbars are created
    /// dynamically and no additional windows will be created.
    /// </remarks>
    public static class ScrollToTopBehavior
    {
        public static readonly DependencyProperty ScrollToTopProperty =
            DependencyProperty.RegisterAttached("ScrollToTop", typeof(bool), typeof(ScrollToTopBehavior),
                new UIPropertyMetadata(false, OnScrollToTopPropertyChanged));

        public static void SetScrollToTop(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollToTopProperty, value);
        }

        /// <summary>
        /// Called when scroll to top property is changed.
        /// </summary>
        /// <param name="dpo">The dpo.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        private static void OnScrollToTopPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            var itemsControl = dpo as ItemsControl;
            if (itemsControl == null) return;

            var dependencyPropertyDescriptor =
                DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(ItemsControl));

            if (dependencyPropertyDescriptor == null) return;

            if ((bool)e.NewValue) dependencyPropertyDescriptor.AddValueChanged(itemsControl, ItemsSourceChanged);
            else dependencyPropertyDescriptor.RemoveValueChanged(itemsControl, ItemsSourceChanged);
        }

        private static void ItemsSourceChanged(object sender, EventArgs e)
        {
            var itemsControl = sender as ItemsControl;
            ItemsChangedEventHandler itemsChangedEventHandler = null;
            itemsChangedEventHandler = delegate
                                       {
                                           var scrollViewer = GetVisualChild<ScrollViewer>(itemsControl);
                                           scrollViewer.ScrollToTop();
                                           if (itemsControl != null)
                                               itemsControl.ItemContainerGenerator.ItemsChanged -= itemsChangedEventHandler;
                                       };

            if (itemsControl != null) itemsControl.ItemContainerGenerator.ItemsChanged += itemsChangedEventHandler;
        }

        private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            var child = default(T);

            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null) child = GetVisualChild<T>(v);
                else break;
            }
            return child;
        }
    }
}