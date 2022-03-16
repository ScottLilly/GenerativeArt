using GenerativeArt.Core;
using GenerativeArt.Models;

namespace GenerativeArt.Services;

public static class RandomShapeGenerator
{
    public static EllipseShape GetEllipse(int canvasMaxHeight, int canvasMaxWidth)
    {
        int radius = Randomizer.GetRandomNumberBetween(25, 100);

        var maxRectangleDimension = Math.Max(radius, radius);

        int max =
            (int)Math.Max(
                (canvasMaxHeight - maxRectangleDimension),
                (canvasMaxWidth - maxRectangleDimension));

        var edgeOffset = (int)(maxRectangleDimension / 2);

        var top = Randomizer.GetRandomNumberBetween(1 + edgeOffset, max);
        var left = Randomizer.GetRandomNumberBetween(1, max);

        return new EllipseShape
        {
            FillColor = Randomizer.GetRandomColor(),
            Radius = radius,
            RotationAngle = 0,
            Top = top,
            Left = left
        };
    }
}