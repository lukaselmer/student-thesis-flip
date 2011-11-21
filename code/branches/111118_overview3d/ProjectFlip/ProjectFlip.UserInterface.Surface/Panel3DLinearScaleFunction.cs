using System;

namespace ProjectFlip.UserInterface.Surface
{
    public class Panel3DLinearScaleFunction : IPanel3DScaleFunction
    {
        private readonly double _factor;

        public Panel3DLinearScaleFunction(double factor)
        {
            _factor = factor;
        }

        // See: geometrische Reihe mit konstanter Basis
        public double Scale(int row)
        {
            return Math.Pow(_factor, row);
        }

        public double SqueezeFactor(int row)
        {
            if (Math.Abs(_factor - 1) < 0.01) return row;
            var factor = Math.Pow(_factor, row) - _factor;
            //var factor = Math.Pow(_factor, row + 1) - _factor;
            var dividor = _factor - 1;
            return factor/dividor + 1;
            //if (row <= 0) return 1;
            //return SqueezeFactor(row - 1) * _factor;
        }
    }
}