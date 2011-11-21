using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            this.Refresh(finalSize);
            return base.ArrangeOverride(finalSize);
        }

        private void Refresh(Size finalSize)
        {
            if (Children.OfType<FrameworkElement>().Count() == 0) return;

            foreach (FrameworkElement child in Children) child.LayoutTransform = null;

            var width = Children.OfType<FrameworkElement>().Max(el => el.DesiredSize.Width);
            var height = Children.OfType<FrameworkElement>().Max(el => el.DesiredSize.Height);
            var positioner = new Panel3DPositioner(finalSize, new Size(width, height), scrollPosition, new Panel3DLinearScaleFunction(0.75));

            foreach (FrameworkElement childElement in Children)
            {
                var position = positioner.CalculateCurrentPosition();

                var tg = new TransformGroup();
                //tg.Children.Add(new RotateTransform(45, 25, 25));
                //tg.Children.Add(new ScaleTransform(.75,.75));
                //Console.WriteLine(position.Scale);
                tg.Children.Add(new ScaleTransform(position.Scale, position.Scale));
                tg.Children.Add(new SkewTransform(0, 0));
                //tg.Children.Add(new MatrixTransform(5, 0, 3, 1, 0,0));
                childElement.LayoutTransform = tg;

                //var r = new Rect(300, 300, childElement.DesiredSize.Width, childElement.DesiredSize.Height);
                var r = new Rect(position.X, position.Y, childElement.DesiredSize.Width, childElement.DesiredSize.Height);
                childElement.Arrange(r);
                positioner.MoveToNext();
            }
        }
    }
}
