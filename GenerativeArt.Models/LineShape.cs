using GenerativeArt.Core.Enums;

namespace GenerativeArt.Models;

public class LineShape : BaseShape
{
    public override ShapeType Type =>
        ShapeType.Line;

    public int X1 { get; init; }
    public int Y1 { get; init; }
    public int X2 { get; init; }
    public int Y2 { get; init; }

    public int StrokeThickness => 5;
}