using System.Drawing;
using GenerativeArt.Core;
using GenerativeArt.Models;

namespace GenerativeArt.Services;

public static class RandomShapeGenerator
{
    public static RectangleShape GetRectangle(int canvasMaxHeight, int canvasMaxWidth)
    {
        int rectHeight = RngCreator.GetNumberBetween(10, 50);
        var rectWidth = RngCreator.GetNumberBetween(25, 200);

        var maxRectangleDimension = Math.Max(rectHeight, rectWidth);

        int max =
            (int)Math.Max(
                (canvasMaxHeight - maxRectangleDimension),
                (canvasMaxWidth - maxRectangleDimension));

        var edgeOffset = (int)(maxRectangleDimension / 2);

        var rotationAngle = RngCreator.GetNumberBetween(0, 359);
        var top = RngCreator.GetNumberBetween(1 + edgeOffset, max);
        var left = RngCreator.GetNumberBetween(1, max);

        return new RectangleShape
        {
            FillColor = GetRandomColor(),
            Height = rectHeight,
            Width = rectWidth,
            RotationAngle = rotationAngle,
            Top = top,
            Left = left
        };
    }

    private static string GetRandomColor()
    {
        return ColorTranslator.ToHtml(Color.FromArgb(255,
            RandomByteValue(), RandomByteValue(), RandomByteValue()));
    }

    private static byte RandomByteValue()
    {
        return (byte)RngCreator.GetNumberBetween(0, byte.MaxValue + 1);
        //return (byte)RngCreator.GetNumberBetween(0, (byte.MaxValue + 1)/2); // Dark colors
        //return (byte)RngCreator.GetNumberBetween(128, byte.MaxValue + 1); // Light colors
    }
}