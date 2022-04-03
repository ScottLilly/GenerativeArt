using GenerativeArt.Core.Enums;

namespace GenerativeArt.Models;

public class LineShape : BaseShape
{
    public override ShapeType Type =>
        ShapeType.Line;

    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }

    public int StrokeThickness { get; set; } = 3;
}