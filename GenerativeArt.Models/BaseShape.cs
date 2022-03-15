using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class BaseShape : IShape
{
    public virtual Enums.ShapeType Type => Enums.ShapeType.Unknown;

    public string? FillColor { get; set; }
    public int Top { get; set; }
    public int Left { get; set; }
    public int RotationAngle { get; set; }
}