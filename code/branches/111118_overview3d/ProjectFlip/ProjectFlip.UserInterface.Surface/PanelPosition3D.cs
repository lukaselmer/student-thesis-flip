using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace ProjectFlip.UserInterface.Surface
{
    internal class PanelPosition3D
    {
        private Dictionary<int, int> LineHeights;
        public Size FinalSize { get; set; }
        public List<FrameworkElement> Children { get; private set; }
        public int CountX { get; private set; }
        public int CountY { get; private set; }
        public int ItemWidth { get; private set; }
        public int ItemHeight { get; private set; }
        public int Index;
        public int XIndex { get { return Index == 0 ? 0 : CountX % Index; } }
        public int YIndex { get { return Index == 0 ? 0 : CountY / Index; } }

        public PanelPosition3D(Size finalSize, List<FrameworkElement> children)
        {
            Debug.Assert(children != null, "children != null");
            FinalSize = finalSize;
            Children = children;
            var itemWidth = (int)children.Max(el => el.DesiredSize.Width) + 1;
            var itemHeight = (int)children.Max(el => el.DesiredSize.Height) + 1;

            CountX = (int)(finalSize.Width / itemWidth) - 1;
            CountY = Children.Count / CountX;
            ItemWidth = itemWidth;
            ItemHeight = itemHeight;

            LineHeights = new Dictionary<int, int>();
            while (MoveToNext())
            {
                if (!LineHeights.ContainsKey(YIndex)) LineHeights[YIndex] = 0;
                LineHeights[YIndex] = (int)(Math.Max(children[Index].DesiredSize.Height, LineHeights[YIndex]) * CalculateScale(YIndex));
            }
            Console.WriteLine();
            Console.WriteLine(LineHeights.Count);
            Console.WriteLine(Children.Count);
            Console.WriteLine(CountX);
            Console.WriteLine(CountY);
        }

        public int X { get { return XIndex * ItemWidth; } }
        public int Y { get { return YIndex * TotalLineHeight(YIndex); } }

        public int TotalLineHeight(int line)
        {
            //LineHeights[line]
            return LineHeights.ToList().GetRange(0, line + 1).Sum(x => x.Key + x.Value);
            //var elementsOnCurrentLine = Children.GetRange(line * CountX, 1); // CountY
            //return (int)elementsOnCurrentLine.Max(el => el.DesiredSize.Height) + (line == 0 ? 0 : TotalLineHeight(line - 1));
        }


        public bool MoveToNext()
        {
            if (!HasNext()) return false;
            Index++;
            return true;
        }

        public bool HasNext()
        {
            return Index < (Children.Count - 1);
        }

        private double CalculateScale(int line)
        {
            return 1.0 / (line + 1.0);
        }
        public double Scale { get { return CalculateScale(YIndex); } }

        public void Reset()
        {
            Index = 0;
        }
    }
}