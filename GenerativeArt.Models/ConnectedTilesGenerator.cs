using GenerativeArt.Core;

namespace GenerativeArt.Models;

public class ConnectedTilesGenerator : ITileGenerator
{
    private readonly int _tileSizeInPixels;
    private int _maxRowIndex;
    private int _maxColumnIndex;

    public ConnectedTilesGenerator(int maxCanvasHeight, int maxCanvasWidth, int tileSizeInPixels)
    {
        _tileSizeInPixels = tileSizeInPixels;
        _maxRowIndex = maxCanvasHeight / tileSizeInPixels;
        _maxColumnIndex = maxCanvasWidth / tileSizeInPixels;
    }

    public void SetCanvasSize(int width, int height)
    {
        _maxRowIndex = height / _tileSizeInPixels;
        _maxColumnIndex = width / _tileSizeInPixels;
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
                    Row = row,
                    Column = col,
                    FillColor = Randomizer.GetRandomColor(),
                    CenterShapeSize = _tileSizeInPixels / 3,
                    Size = _tileSizeInPixels,
                    Top = row * _tileSizeInPixels,
                    Left = col * _tileSizeInPixels,
                    DisplayTopLine = row != 0 && Randomizer.GetRandomNumberBetween(0, 1) == 1,
                    DisplayLeftLine = col != 0 && Randomizer.GetRandomNumberBetween(0, 1) == 1,
                    DisplayBottomLine = row != _maxRowIndex && Randomizer.GetRandomNumberBetween(0, 1) == 1,
                    DisplayRightLine = col != _maxColumnIndex && Randomizer.GetRandomNumberBetween(0, 1) == 1
                };

                // Clean up edges and connections with prior tiles
                if (row == 0)
                {
                    tile.DisplayTopLine = false;
                }
                else
                {
                    var tileAbove = tileShapes.First(t => t.Row == row - 1 && t.Column == col);
                    tile.DisplayTopLine = tileAbove.DisplayBottomLine;
                }

                if (row == _maxRowIndex - 1)
                {
                    tile.DisplayBottomLine = false;
                }

                if (col > 0)
                {
                    var tileLeft = tileShapes.First(t => t.Row == row && t.Column == (col - 1));
                    tile.DisplayLeftLine = tileLeft.DisplayRightLine;
                }

                if (col == _maxColumnIndex - 1)
                {
                    tile.DisplayRightLine = false;
                }

                tileShapes.Add(tile);
            }
        }

        return tileShapes;
    }
}