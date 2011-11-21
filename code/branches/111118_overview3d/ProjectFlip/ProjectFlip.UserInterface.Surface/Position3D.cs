using System.Windows;

namespace ProjectFlip.UserInterface.Surface
{
    public class Position3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Scale { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }

        public Position3D(double x, double y, double scale, HorizontalAlignment horizontalAlignment)
        {
            X = x;
            Y = y;
            Scale = scale;
            HorizontalAlignment = horizontalAlignment;
        }
    }
}