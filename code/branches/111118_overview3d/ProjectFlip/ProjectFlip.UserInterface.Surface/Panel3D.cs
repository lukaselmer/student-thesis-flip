using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace ProjectFlip.UserInterface.Surface
{
    public class Panel3D : Panel
    {
        private Point mouseStart, mouseNow;
        private double destinationDistance = 10000;

        public enum AlignmentOptions { Left, Center, Right };

        private const double MouseWheelScaleFactor = 2.5;
        private const double Easing = 0.3;
        private const double MaximumDistance = 10000;
        private const double MinimumDistance = 200;
        private const double MouseEasing = 0.03;

        #region Dependency Properties

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(Panel3D), new PropertyMetadata(Panel3D.RadiusChanged));
        public static readonly DependencyProperty AngleItemProperty = DependencyProperty.Register("AngleItem", typeof(double), typeof(Panel3D), new PropertyMetadata(Panel3D.AngleItemChanged));
        public static readonly DependencyProperty InitialAngleProperty = DependencyProperty.Register("InitialAngle", typeof(double), typeof(Panel3D), new PropertyMetadata(Panel3D.InitialAngleChanged));
        public static readonly DependencyProperty AlignProperty = DependencyProperty.Register("Align", typeof(AlignmentOptions), typeof(Panel3D), new PropertyMetadata(Panel3D.AlignChanged));
        public static readonly DependencyProperty IsDragOnProperty = DependencyProperty.Register("IsDragOn", typeof(bool), typeof(Panel3D), new PropertyMetadata(Panel3D.IsDragChanged));
        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register("OffsetX", typeof(double), typeof(Panel3D), new PropertyMetadata(Panel3D.OffsetXChanged));
        public static readonly DependencyProperty DistanceProperty = DependencyProperty.Register("Distance", typeof(double), typeof(Panel3D), new PropertyMetadata(Panel3D.DistanceChanged));

        private static void RadiusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((Panel3D)sender).Refresh(); }
        private static void AngleItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((Panel3D)sender).Refresh(); }
        private static void InitialAngleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((Panel3D)sender).Refresh(); }
        private static void AlignChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((Panel3D)sender).Refresh(); }
        private static void IsDragChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((Panel3D)sender).OnIsDragChanged(e); }
        private static void OffsetXChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((Panel3D)sender).Refresh(); }
        private static void DistanceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Panel3D panel3D = (Panel3D)sender;
            panel3D.destinationDistance = (double)e.NewValue;
            panel3D.Refresh();
        }

        [Category("Circular Panel 3D")]
        public double Radius
        {
            get { return (double)this.GetValue(Panel3D.RadiusProperty); }
            set { this.SetValue(Panel3D.RadiusProperty, value); }
        }

        [Category("Circular Panel 3D")]
        public double AngleItem
        {
            get { return (double)this.GetValue(Panel3D.AngleItemProperty); }
            set { this.SetValue(Panel3D.AngleItemProperty, value); }
        }

        [Category("Circular Panel 3D")]
        public double InitialAngle
        {
            get { return (double)this.GetValue(Panel3D.InitialAngleProperty); }
            set { this.SetValue(Panel3D.InitialAngleProperty, value); }
        }

        [Category("Circular Panel 3D")]
        public AlignmentOptions Align
        {
            get { return (AlignmentOptions)this.GetValue(Panel3D.AlignProperty); }
            set { this.SetValue(Panel3D.AlignProperty, value); }
        }

        [Category("Circular Panel 3D")]
        public bool IsDragOn
        {
            get { return (bool)this.GetValue(Panel3D.IsDragOnProperty); }
            set { this.SetValue(Panel3D.IsDragOnProperty, value); }
        }

        [Category("Circular Panel 3D")]
        public double OffsetX
        {
            get { return (double)this.GetValue(Panel3D.OffsetXProperty); }
            set { this.SetValue(Panel3D.OffsetXProperty, value); }
        }

        [Category("Circular Panel 3D")]
        public double Distance
        {
            get { return (double)this.GetValue(Panel3D.DistanceProperty); }
            set { this.SetValue(Panel3D.DistanceProperty, value); }
        }

        #endregion

        #region Dependency Property Events

        private void OnIsDragChanged(DependencyPropertyChangedEventArgs e)
        {
            this.CaptureMouse();
            this.MouseLeftButtonDown += new MouseButtonEventHandler(this.CircularPanel3D_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(this.CircularPanel3D_MouseLeftButtonUp);
        }

        #endregion

        #region Mouse Interaction

        private void CircularPanel3D_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.CaptureMouse();
            this.mouseStart = e.GetPosition(this);
            this.mouseNow = this.mouseStart;

            CompositionTarget.Rendering += this.ApplyRotationFromMouseMove;

            this.MouseMove += new MouseEventHandler(this.CircularPanel3D_MouseMove);
        }

        private void CircularPanel3D_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            this.MouseMove -= new MouseEventHandler(this.CircularPanel3D_MouseMove);
        }

        // Updates mouse position when mouse moves
        private void CircularPanel3D_MouseMove(object sender, MouseEventArgs e)
        {
            CompositionTarget.Rendering += this.ApplyRotationFromMouseMove;

            this.mouseNow = e.GetPosition(this);
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double newDistance = this.destinationDistance + Panel3D.MouseWheelScaleFactor * -e.Delta;

            if (newDistance <= Panel3D.MaximumDistance &&
                newDistance >= Panel3D.MinimumDistance)
            {
                this.destinationDistance += Panel3D.MouseWheelScaleFactor * -e.Delta;
            }
        }

        private void ApplyZoomDistanceAnimation(object sender, EventArgs e)
        {
            if (Math.Abs(this.Distance - destinationDistance) < 1)
            {
                this.Distance = destinationDistance;
            }

            this.Distance += (destinationDistance - this.Distance) * Panel3D.Easing;

            if (this.Distance > Panel3D.MaximumDistance)
            {
                this.destinationDistance = Panel3D.MaximumDistance;
            }
            else if (this.Distance < Panel3D.MinimumDistance)
            {
                this.destinationDistance = Panel3D.MinimumDistance;
            }
        }

        #endregion

        #region Custom Panel Methods

        protected override Size MeasureOverride(Size availableSize)
        {
            this.MouseWheel += this.HandleMouseWheel;
            CompositionTarget.Rendering += this.ApplyZoomDistanceAnimation;

            Size resultSize = new Size(0, 0);

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
            this.Refresh();
            return base.ArrangeOverride(finalSize);
        }

        #endregion

        #region Programmatic Animation Methods

        private void ApplyRotationFromMouseMove(object sender, EventArgs e)
        {
            // Rotates Circular Panel 3D using MouseMove (Y)
            if (Math.Abs(this.mouseNow.Y - this.mouseStart.Y) < 1)
            {
                this.mouseNow.Y = this.mouseStart.Y;
            }
            var movement = (this.mouseNow.Y - this.mouseStart.Y);
            if ((this.InitialAngle > 100 && movement <= 0) || (this.InitialAngle < 400 && movement >= 0))
                this.InitialAngle += (movement * Panel3D.MouseEasing) / 2.2;

            this.mouseStart.Y += ((this.mouseNow.Y - this.mouseStart.Y) * Panel3D.MouseEasing);
            if ((Math.Abs(this.mouseNow.Y - this.mouseStart.Y) < 1))
            {
                CompositionTarget.Rendering -= this.ApplyRotationFromMouseMove;
            }
        }

        private void Refresh()
        {
            int count = 0;
            int col = 0;
            int row = 0;
            int zLevel = 0;

            foreach (FrameworkElement childElement in this.Children)
            {
                double angle = (this.AngleItem * count++) - this.InitialAngle;
                double y = this.Radius * Math.Cos(Math.PI * angle / 180);
                double z = this.Radius * Math.Sin(Math.PI * angle / 180);


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

                int depth = (int)(z * 100);

                double pDist = (this.Distance - 1000) / 2000;
                double pZ = ((z + 1000) / 2000) + 0.5;

                double opacity = (pZ - pDist) + 0.4;
                if (opacity >= 1)
                {
                    childElement.Opacity = (2 - opacity);
                }
                else if (opacity < 0)
                {
                    childElement.Opacity = 0;
                }
                else
                {
                    childElement.Opacity = opacity;
                }

                // Variable zLevel changes value of ZIndex for each item in the ListBox.
                // This way the reflex of elements at the top will be placed behind the item below it.
                Canvas.SetZIndex(childElement, depth - (++zLevel * 10));

                double alignX = 0;
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
                }


                var plane = new Plane();
                plane.RotationCenterX = plane.RotationCenterY = plane.RotationCenterZ = 0.5;
                plane.RotationX = (angle - 90) * -1;
                ModelVisual3D x = new ModelVisual3D();

                var tg = new TransformGroup();
                tg.Children.Add(new RotateTransform(45, 25, 25));
                tg.Children.Add(new RotateTransform(45, 25, 25));
                childElement.LayoutTransform = tg;
                
                
                Rect r = new Rect(this.Width / 2 - alignX, this.Height / 2 - alignY, childElement.DesiredSize.Width, childElement.DesiredSize.Height);
                


                //var x = plane.TransformToVisual(childElement);
                
                //childElement.Projection = projection;
                //childElement.Arrange(new Rect(this.Width / 2 - alignX, this.Height / 2 - alignY, childElement.DesiredSize.Width, childElement.DesiredSize.Height));
                childElement.Arrange(r);

                col++;
                if (col > 14)
                {
                    col = 0;
                    row++;
                }
            }
        }

        #endregion
    }
}




