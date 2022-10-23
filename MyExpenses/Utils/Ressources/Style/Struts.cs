using SkiaSharp;

namespace MyExpenses.Utils.Ressources.Style;

public partial class Structs
{
    public struct DrawPath
    {
        public SKPath Path;
        public SKPaint Paint;
    }
    
    public struct DrawCircle
    {
        public SKPoint Point;
        public float Radius;
        public SKPaint Paint;
    }
}