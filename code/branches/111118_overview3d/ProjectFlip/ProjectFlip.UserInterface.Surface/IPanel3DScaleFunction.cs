namespace ProjectFlip.UserInterface.Surface
{
    public interface IPanel3DScaleFunction
    {
        double ScaleX(int row);
        double ScaleY(int row);
        double SqueezeFactorX(int row);
        double SqueezeFactorY(int row);
    }
}