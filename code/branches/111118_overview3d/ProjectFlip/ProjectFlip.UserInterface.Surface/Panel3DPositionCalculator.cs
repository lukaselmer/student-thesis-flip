using System.Windows;
using System;

namespace ProjectFlip.UserInterface.Surface
{
    public class Panel3DPositionCalculator
    {
        public IPanel3DScaleFunction ScaleFunction { get; set; }
        public int ElementsPerLine { get; set; }
        private Size _elementSize;
        private readonly Size _windowSize;
        private double _scrollPosition;
        public bool LeftAligned;

        public Panel3DPositionCalculator(Size elementSize, Size windowSize, double scrollPosition, IPanel3DScaleFunction scaleFunction, int elementsPerLine)
        {
            ScaleFunction = scaleFunction;
            ElementsPerLine = elementsPerLine;
            _elementSize = elementSize;
            _windowSize = windowSize;
            _scrollPosition = scrollPosition;
        }

        public Position3D Calculate(int row, int col)
        {
            var scaledElementWidth = _elementSize.Width * ScaleFunction.ScaleX(row);
            var elementsFromCenter = (col - ((ElementsPerLine - 1.0) / 2.0));
            var distanceFromCenter = scaledElementWidth * elementsFromCenter;
            var x = _windowSize.Width / 2 - distanceFromCenter - _elementSize.Width / 2.0;
            var alignment = elementsFromCenter > 0.1
                ? HorizontalAlignment.Right
                : (elementsFromCenter < 0.1 ? HorizontalAlignment.Left : HorizontalAlignment.Center);
            return new Position3D(x, ScaleFunction.SqueezeFactorY(row) * _elementSize.Height, ScaleFunction.ScaleX(row), ScaleFunction.ScaleY(row), alignment, elementsFromCenter);
        }
    }
}