namespace Nanoray.Umbral.TwoD;

public interface IAxis2D : IAxis
{
    Axis2D Axis { get; }

    public interface IHorizontal : IAxis2D
    {
        Axis2D IAxis2D.Axis => Axis2D.Horizontal;
    }

    public interface IVertical : IAxis2D
    {
        Axis2D IAxis2D.Axis => Axis2D.Vertical;
    }
}
