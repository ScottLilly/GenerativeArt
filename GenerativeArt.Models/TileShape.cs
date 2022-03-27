using GenerativeArt.Core.Enums;

namespace GenerativeArt.Models;

public class TileShape : BaseShape
{
    public override ShapeType Type =>
        ShapeType.Tile;

    public int Height { get; init; }
    public int Width { get; init; }

    public int CenterShapeSize { get; set; }
    public int CenterShapeTop => Top + ((Height - CenterShapeSize - 1) / 2);
    public int CenterShapeLeft => Left + ((Width - CenterShapeSize - 1) / 2);

    public bool DisplayTopLine { get; set; }
    public bool DisplayLeftLine { get; set; }
    public bool DisplayBottomLine { get; set; }
    public bool DisplayRightLine { get; set; }

    public bool DisplayCenterShape =>
        DisplayTopLine || DisplayLeftLine || DisplayBottomLine || DisplayRightLine;
}