using System.Windows;

namespace ProjectFlip.UserInterface.Surface
{
    public class Position3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public double ElementsFromCenter { get; set; }

        public Position3D(double x, double y, double scaleX, double scaleY, HorizontalAlignment horizontalAlignment, double elementsFromCenter)
        {
            X = x;
            Y = y;
            ScaleX = scaleX;
            ScaleY = scaleY;
            HorizontalAlignment = horizontalAlignment;
            ElementsFromCenter = elementsFromCenter;
        }
    }
}