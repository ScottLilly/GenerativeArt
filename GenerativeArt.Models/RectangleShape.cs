using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class RectangleShape : IShape
{
    public Enums.ShapeType Type => Enums.ShapeType.Rectangle;
    public string? FillColor { get; set; }
    public int RotationAngle { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Top { get; set; }
    public int Left { get; set; }
}