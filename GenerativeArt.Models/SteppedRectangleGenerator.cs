using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class SteppedRectangleGenerator : IRectangleGenerator
{
    private readonly int _canvasMaxHeight;
    private readonly int _canvasMaxWidth;

    private RectangleShape _latestRectangle;

    public SteppedRectangleGenerator(int canvasMaxHeight, int canvasMaxWidth)
    {
        _canvasMaxHeight = canvasMaxHeight;
        _canvasMaxWidth = canvasMaxWidth;
    }

    public RectangleShape GetRectangle()
    {
        int rectHeight = Randomizer.GetRandomNumberBetween(10, 50);
        var rectWidth = Randomizer.GetRandomNumberBetween(25, 200);

        var top = _latestRectangle == null ?
            1 :
            _latestRectangle.Top + (_latestRectangle.Height /2);
        var left = _latestRectangle == null ? 
            1 :
            _latestRectangle.Left + (_latestRectangle.Width / 2);

        var rectangle = new RectangleShape
        {
            FillColor = Randomizer.GetRandomColor(),
            Height = rectHeight,
            Width = rectWidth,
            RotationAngle = 0,
            Top = top,
            Left = left
        };

        _latestRectangle = rectangle;

        return rectangle;
    }
}