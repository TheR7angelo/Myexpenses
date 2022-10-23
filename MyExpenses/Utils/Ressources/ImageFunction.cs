using System.Collections.Generic;
using MyExpenses.Utils.Ressources.Style;
using SkiaSharp;

namespace MyExpenses.Utils.Ressources;

public static partial class Images
{

    #region SkCircle

    private static void DrawCircles(this SKCanvas canvas, IEnumerable<Structs.DrawCircle> drawCircles)
    {
        foreach (var drawCircle in drawCircles) canvas.DrawCircle(drawCircle.Point, drawCircle.Radius, drawCircle.Paint);
    }

    #endregion
    
    #region SkPath

    private static void DrawPaths(this SKCanvas canvas, List<Structs.DrawPath> drawPaths)
    {
        foreach (var drawPath in drawPaths) canvas.DrawPath(drawPath.Path, drawPath.Paint);
    }

    private static SKPaint _SkPaint(this string color) => new()
    {
        Style = SKPaintStyle.StrokeAndFill, Color = SKColor.Parse(color)
    };
    #endregion
}