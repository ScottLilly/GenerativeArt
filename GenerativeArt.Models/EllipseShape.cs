using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class EllipseShape : BaseShape
{
    public override Enums.ShapeType Type =>
        Enums.ShapeType.Ellipse;

    public int Radius { get; init; }
}