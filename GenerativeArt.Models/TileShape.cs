using GenerativeArt.Core.Enums;

namespace GenerativeArt.Models;

public class TileShape : BaseShape
{
    public override ShapeType Type =>
        ShapeType.Tile;

    public int Size { get; init; }

    public int CenterShapeSize { get; init; }
    public int CenterShapeTop => Top + ((Size - CenterShapeSize) / 2);
    public int CenterShapeLeft => Left + ((Size - CenterShapeSize) / 2);

    public bool DisplayTopLine { get; set; }
    public bool DisplayLeftLine { get; set; }
    public bool DisplayBottomLine { get; set; }
    public bool DisplayRightLine { get; set; }

    public int MinCoordinate => 0;
    public int CenterCoordinate => Size / 2;
    public int MaxCoordinate => Size;

    public bool DisplayCenterShape =>
        DisplayTopLine || DisplayLeftLine || DisplayBottomLine || DisplayRightLine;
}