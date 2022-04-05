namespace GenerativeArt.Models;

public interface IEllipseGenerator
{
    EllipseShape GetEllipse();
    public void SetCanvasSize(int width, int height);
}