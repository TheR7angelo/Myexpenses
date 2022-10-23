using System.Collections.Generic;
using System.Linq;
using MyExpenses.Utils.Ressources.Style;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MyExpenses.Utils.Ressources;

public static class Images
{
    private static readonly List<Structs.Canvas> List;

    static Images()
    {
        List = new List<Structs.Canvas>
        {
            Money()
        };
    }
    
    public static List<Structs.Canvas> GetAllImages() => List;

    public static SKBitmap GetImage(this string name)
    {
        var st = List.Where(s => s.Name.Equals(name)).ToList();
        return st.Count.Equals(0) ? null : st[0].VCanvas;
    }

    private static Structs.Canvas Money()
    {
        var bitmap = new SKBitmap(512, 512);
        var canvas = new SKCanvas(bitmap);
        //todo la path se génére mal
        const string str1 = "M488.727,325.818H23.273C10.473,325.818,0,315.345,0,302.545V-256C0-12.8,10.473-23.273,23.273-23.273h465.455c12.8,0,23.273,10.473,23.273,23.273v256C512,315.345,501.527,325.818,488.727,325.818z";
        var p1 = SKPath.ParseSvgPathData(str1.ToUpper());
        var s1 = new SKPaint
        {
            Style = SKPaintStyle.StrokeAndFill,
            Color = SKColor.Parse("#FF0E9347"),
        };
        // canvas.DrawLine(0, 0, 2, 50, pathStyle);

        canvas.DrawPath(p1, s1);

        // var i = _geometry(
        //     "M430.545,232.727c-26.764,0-51.2,12.8-65.164,33.745C353.745,260.655,340.945,256,325.818,256\n\tc-40.727,0-74.473,30.255-80.291,69.818h243.2c11.636,0,20.945-8.145,23.273-19.782\n\tC507.345,265.309,472.436,232.727,430.545,232.727z");
        return new Structs.Canvas()
        {
            Name = "Money",
            VCanvas = bitmap
        };
    }

    private static Geometry _geometry(this string geom)
    {
        var path = new PathGeometryConverter();
        return (Geometry)path.ConvertFromInvariantString(geom);
    }

}