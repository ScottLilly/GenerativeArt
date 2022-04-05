namespace GenerativeArt.Models;

public interface IRectangleGenerator
{
    RectangleShape GetRectangle();
    public void SetCanvasSize(int width, int height);
}