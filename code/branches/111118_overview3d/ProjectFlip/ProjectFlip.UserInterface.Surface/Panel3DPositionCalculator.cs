using System.Windows;
using System;

namespace ProjectFlip.UserInterface.Surface
{
    public class Panel3DPositionCalculator
    {
        public IPanel3DScaleFunction ScaleFunction { get; set; }
        private Size _elementSize;
        private readonly Size _windowSize;
        private double _scrollPosition;
        public bool LeftAligned;

        public Panel3DPositionCalculator(Size elementSize, Size windowSize, double scrollPosition, IPanel3DScaleFunction scaleFunction)
        {
            ScaleFunction = scaleFunction;
            _elementSize = elementSize;
            _windowSize = windowSize;
            _scrollPosition = scrollPosition;
        }

        public Position3D Calculate(int row, int col)
        {
            var positionLeftAligned = new Position3D(col * _elementSize.Width, ScaleFunction.SqueezeFactor(row) * _elementSize.Height, ScaleFunction.Scale(row));
            if (row == 0 || LeftAligned) return positionLeftAligned;
            var halfWindowSize = _windowSize.Width/2;
            if (halfWindowSize < positionLeftAligned.X) return positionLeftAligned; // Left aligned
            var marginLeft = _elementSize.Width - _elementSize.Width * positionLeftAligned.Scale;
            var centered = halfWindowSize - _elementSize.Width < positionLeftAligned.X;
            return new Position3D(positionLeftAligned.X + marginLeft * (centered ? 0.5 : 1), positionLeftAligned.Y, positionLeftAligned.Scale);
        }
    }
}