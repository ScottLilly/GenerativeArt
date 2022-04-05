namespace GenerativeArt.Models;

public interface ITileGenerator
{
    List<TileShape> GetTiles();
    public void SetCanvasSize(int width, int height);
}