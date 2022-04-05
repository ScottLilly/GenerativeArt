namespace GenerativeArt.Models;

public interface IConnectedLineGenerator
{
    public LineShape GetNextLine();
    public void SetCanvasSize(int width, int height);
}