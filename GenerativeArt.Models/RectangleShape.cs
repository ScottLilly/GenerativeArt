using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class RectangleShape : BaseShape
{
    public override Enums.ShapeType Type =>
        Enums.ShapeType.Rectangle;

    public int Height { get; init; }
    public int Width { get; init; }
}