using System;
using System.Windows;

namespace ProjectFlip.UserInterface.Surface
{
    public class Panel3DPositioner
    {
        private readonly Panel3DPositionCalculator _panel3DPositionCalculator;
        public Size WindowSize { get; set; }
        public Size ElementSize { get; set; }
        public double ScrollPosition { get; set; }

        public Panel3DPositioner(Size windowSize, Size elementSize, double scrollPosition, IPanel3DScaleFunction scaleFunction = null)
        {
            if (scaleFunction == null) scaleFunction = new Panel3DLinearScaleFunction(0.75);
            WindowSize = windowSize;
            ElementSize = elementSize;
            ScrollPosition = scrollPosition;
            Index = 0;
            _panel3DPositionCalculator = new Panel3DPositionCalculator(elementSize, windowSize, scrollPosition, scaleFunction);
        }

        public int Index { get; set; }

        public int CurrentRow { get { return Index / ElementsPerLine(); } }
        public int CurrentCol { get { return Index % ElementsPerLine(); } }

        public int ElementsPerLine()
        {
            return Math.Max((int)((WindowSize.Width) / ElementSize.Width), 1);
        }

        public void MoveToNext()
        {
            Index++;
        }

        public Position3D CalculateCurrentPosition()
        {
            return _panel3DPositionCalculator.Calculate(CurrentRow, CurrentCol);
        }
    }
}