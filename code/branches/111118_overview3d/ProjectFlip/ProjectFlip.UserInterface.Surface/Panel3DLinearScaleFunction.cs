using System;

namespace ProjectFlip.UserInterface.Surface
{
    public class Panel3DLinearScaleFunction : IPanel3DScaleFunction
    {
        private readonly double _factorX;
        private readonly double _factorY;

        public Panel3DLinearScaleFunction(double factorX, double factorY)
        {
            _factorX = factorX;
            _factorY = factorY;
        }

        // See: geometrische Reihe mit konstanter Basis
        public double ScaleX(int row)
        {
            return Math.Pow(_factorX, row);
        }

        public double ScaleY(int row)
        {
            return Math.Pow(_factorX, row) * Math.Pow(_factorY, row);
        }

        public double SqueezeFactorX(int row)
        {
            if (Math.Abs(_factorX - 1) < 0.01) return row;
            var factor = Math.Pow(_factorX, row) - _factorX;
            //var factorX = Math.Pow(_factorX, row + 1) - _factorX;
            var dividor = _factorX - 1;
            return factor/dividor + 1;
            //if (row <= 0) return 1;
            //return SqueezeFactorX(row - 1) * _factorX;
        }

        public double SqueezeFactorY(int row)
        {
            if (Math.Abs(_factorX * _factorY - 1) < 0.01) return row;
            var factor = Math.Pow(_factorX * _factorY, row) - _factorX * _factorY;
            //var factorX = Math.Pow(_factorX, row + 1) - _factorX;
            var dividor = _factorX * _factorY - 1;
            return factor / dividor + 1;
            //if (row <= 0) return 1;
            //return SqueezeFactorX(row - 1) * _factorX;
        }
    }
}