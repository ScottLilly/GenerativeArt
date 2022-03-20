using GenerativeArt.Core.Enums;

namespace GenerativeArt.Models;

public class RectangleShape : BaseShape
{
    public override ShapeType Type =>
        ShapeType.Rectangle;

    public int Height { get; init; }
    public int Width { get; init; }
}