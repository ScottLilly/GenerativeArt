using GenerativeArt.Core.Enums;

namespace GenerativeArt.Models;

public class EllipseShape : BaseShape
{
    public override ShapeType Type =>
        ShapeType.Ellipse;

    public int Radius { get; init; }
}