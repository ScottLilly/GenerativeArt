using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class ConnectedTilesGenerator : ITileGenerator
{
    private readonly int _maxCanvasHeight;
    private readonly int _maxCanvasWidth;
    private readonly int _tileSizeInPixels;
    private readonly int _maxRowIndex;
    private readonly int _maxColumnIndex;

    public ConnectedTilesGenerator(int maxCanvasHeight, int maxCanvasWidth, int tileSizeInPixels)
    {
        _maxCanvasHeight = maxCanvasHeight;
        _maxCanvasWidth = maxCanvasWidth;
        _tileSizeInPixels = tileSizeInPixels;
        _maxRowIndex = maxCanvasHeight / tileSizeInPixels;
        _maxColumnIndex = maxCanvasWidth / tileSizeInPixels;
    }

    public List<TileShape> GetTiles()
    {
        List<TileShape> tileShapes = new List<TileShape>();

        for (int row = 0; row < _maxRowIndex; row++)
        {
            for (int col = 0; col < _maxColumnIndex; col++)
            {
                var tile = new TileShape
                {
                    FillColor = Randomizer.GetRandomColor(),
                    CenterShapeSize = 8,
                    Height = _tileSizeInPixels,
                    Width = _tileSizeInPixels,
                    Top = row * _tileSizeInPixels,
                    Left = col * _tileSizeInPixels,
                    DisplayTopLine = row != 0 && Randomizer.GetRandomNumberBetween(0, 1) == 1,
                    DisplayLeftLine = col != 0 && Randomizer.GetRandomNumberBetween(0, 1) == 1,
                    DisplayBottomLine = row != _maxRowIndex && Randomizer.GetRandomNumberBetween(0, 1) == 1,
                    DisplayRightLine = col != _maxColumnIndex && Randomizer.GetRandomNumberBetween(0, 1) == 1
                };

                tileShapes.Add(tile);
            }
        }

        return tileShapes;
    }
}