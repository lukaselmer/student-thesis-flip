using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ProjectFlip.UserInterface.Surface
{
    public class Panel3D : Panel
    {
        private double scrollPosition = 0;

        public Panel3D() { }

        protected override Size MeasureOverride(Size availableSize)
        {
            var resultSize = new Size(0, 0);

            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSize);
                resultSize.Width = Math.Max(resultSize.Width, child.DesiredSize.Width);
                resultSize.Height = Math.Max(resultSize.Height, child.DesiredSize.Height);
            }

            resultSize.Width =
                double.IsPositiveInfinity(availableSize.Width) ?
                resultSize.Width : availableSize.Width;

            resultSize.Height =
                double.IsPositiveInfinity(availableSize.Height) ?
                resultSize.Height : availableSize.Height;

            return resultSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Refresh(finalSize);
            return finalSize;
        }

        private void Refresh(Size finalSize)
        {
            if (Children.OfType<FrameworkElement>().Count() == 0) return;

            foreach (FrameworkElement child in Children) child.Visibility = Visibility.Hidden;

            var width = Children.OfType<FrameworkElement>().Max(el => el.DesiredSize.Width);
            var height = Children.OfType<FrameworkElement>().Max(el => el.DesiredSize.Height);
            var positioner = new Panel3DPositioner(finalSize, new Size(width, height), scrollPosition);

            foreach (FrameworkElement childElement in Children)
            {
                var position = positioner.CalculateCurrentPosition();

                if (position.ScaleY < .01) break;
                childElement.Visibility = Visibility.Visible;

                var tg = new TransformGroup();
                tg.Children.Add(new ScaleTransform(position.ScaleX, position.ScaleY));
                if (position.HorizontalAlignment != HorizontalAlignment.Center)
                    tg.Children.Add(new SkewTransform(4.2 * position.ElementsFromCenter, 0));
                childElement.RenderTransform = tg;

                var r = new Rect(position.X, position.Y, childElement.DesiredSize.Width, childElement.DesiredSize.Height);
                childElement.Arrange(r);
                positioner.MoveToNext();
            }
        }
    }
}
