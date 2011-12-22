using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;


namespace ProjectFlip.UserInterface.Surface
{
    public class Panel3DLuke : Panel
    {
        private PanelPosition3D _position;
        private double _scrollPosition = 0;

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

            //if (_position == null || Math.Abs(_position.FinalSize.Height - finalSize.Height) > 1 || 
            //    Math.Abs(_position.FinalSize.Width - finalSize.Width) > 1 || _position.Children.Count() != Children.Count)
            _position = new PanelPosition3D(finalSize, Children.OfType<FrameworkElement>().ToList());

            foreach (FrameworkElement childElement in Children)
            {
                //var alignX = childElement.DesiredSize.Width / 2;
                //var alignY = childElement.DesiredSize.Height / 2;


                var tg = new TransformGroup();
                //tg.Children.Add(new RotateTransform(45, 25, 25));
                tg.Children.Add(new ScaleTransform(_position.Scale, _position.Scale));
                //var x = _position.Scale;
                childElement.LayoutTransform = tg;

                var r = new Rect(_position.X, _position.Y, childElement.DesiredSize.Width, childElement.DesiredSize.Height);
                childElement.Arrange(r);


                _position.MoveToNext();
                continue;

                /*PlaneProjection projection = new PlaneProjection();
                if (projection != null)
                {
                    projection.CenterOfRotationX = 0.5;
                    projection.CenterOfRotationY = 0.5;
                    projection.CenterOfRotationZ = 0.5;
                    projection.RotationX = (angle - 90) * -1;
                    projection.GlobalOffsetX = row * (-430) + this.OffsetX;
                    projection.GlobalOffsetY = y;
                    //projection.GlobalOffsetX = x;
                    //projection.GlobalOffsetY = row * (-330) + this.OffsetY;
                    projection.GlobalOffsetZ = z - this.Distance;
                }*/


                // Variable zLevel changes value of ZIndex for each item in the ListBox.
                // This way the reflex of elements at the top will be placed behind the item below it.
                //Canvas.SetZIndex(childElement, depth - (++zLevel * 10));

                /*double alignX = 0;
                double alignY = 0;
                switch (this.Align)
                {
                    case AlignmentOptions.Left:
                        alignX = 0;
                        alignY = 0;
                        break;
                    case AlignmentOptions.Center:
                        alignX = childElement.DesiredSize.Width / 2;
                        alignY = childElement.DesiredSize.Height / 2;
                        break;
                    case AlignmentOptions.Right:
                        alignX = childElement.DesiredSize.Width;
                        alignY = childElement.DesiredSize.Height;
                        break;
                }*/


                childElement.Opacity = 0.5;
                /*var tg = new TransformGroup();
                tg.Children.Add(new RotateTransform(45, 25, 25));
                childElement.LayoutTransform = tg;*/

                /*var alignX = childElement.DesiredSize.Width / 2;
                var alignY = childElement.DesiredSize.Height / 2;
                //Rect r = new Rect(this.Width / 2 - alignX, this.Height / 2 - alignY, childElement.DesiredSize.Width, childElement.DesiredSize.Height);

                Rect r = new Rect(finalSize.Width / 2 - alignX, finalSize.Height / 2 - alignY, childElement.DesiredSize.Width, childElement.DesiredSize.Height);



                //var x = plane.TransformToVisual(childElement);

                //childElement.Projection = projection;
                //childElement.Arrange(new Rect(this.Width / 2 - alignX, this.Height / 2 - alignY, childElement.DesiredSize.Width, childElement.DesiredSize.Height));
                childElement.Arrange(r);*/

            }
            _position.Reset();

        }
    }
}