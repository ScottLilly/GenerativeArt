using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class RandomRectangleGenerator : IRectangleGenerator
{
    private readonly int _canvasMaxHeight;
    private readonly int _canvasMaxWidth;

    public RandomRectangleGenerator(int canvasMaxHeight, int canvasMaxWidth)
    {
        _canvasMaxHeight = canvasMaxHeight;
        _canvasMaxWidth = canvasMaxWidth;
    }

    public RectangleShape GetRectangle()
    {
        int rectHeight = Randomizer.GetRandomNumberBetween(10, 50);
        var rectWidth = Randomizer.GetRandomNumberBetween(25, 200);

        var maxRectangleDimension = Math.Max(rectHeight, rectWidth);

        int max =
            (int)Math.Max(
                (_canvasMaxHeight - maxRectangleDimension),
                (_canvasMaxWidth - maxRectangleDimension));

        var edgeOffset = (int)(maxRectangleDimension / 2);

        var rotationAngle = Randomizer.GetRandomNumberBetween(0, 359);
        var top = Randomizer.GetRandomNumberBetween(1 + edgeOffset, max);
        var left = Randomizer.GetRandomNumberBetween(1, max);

        return new RectangleShape
        {
            FillColor = Randomizer.GetRandomColor(),
            Height = rectHeight,
            Width = rectWidth,
            RotationAngle = rotationAngle,
            Top = top,
            Left = left
        };
    }
}