﻿using GenerativeArt.Core;

namespace GenerativeArt.Models;

public interface IShape
{
    public Enums.ShapeType Type { get; }
    public string FillColor { get; set; }
    public int Top { get; set; }
    public int Left { get; set; }
    public int RotationAngle { get; set; }
}