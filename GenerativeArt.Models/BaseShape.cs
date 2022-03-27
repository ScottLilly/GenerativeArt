using GenerativeArt.Core.Enums;

namespace GenerativeArt.Models;

public class BaseShape : IShape
{
    public virtual ShapeType Type => ShapeType.Unknown;

    public string? FillColor { get; set; }
    public int Top { get; set; }
    public int Left { get; set; }
    public int RotationAngle { get; set; }

    public double Opacity { get; set; } = 1.0;
}