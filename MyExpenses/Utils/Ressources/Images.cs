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

        var st0 = "#FF0E9347"._SkPaint();
        var st1 = "#FF0D8944"._SkPaint();
        var st2 = "#FF3BB54A"._SkPaint();
        
        const string s0 = 
            "M488.7,325.8H23.3C10.5,325.8,0,315.3,0,302.5v-256c0-12.8,10.5-23.3,23.3-23.3h465.5c12.8,0,23.3,10.5,23.3,23.3v256C512,315.3,501.5,325.8,488.7,325.8z";
        var p0 = SKPath.ParseSvgPathData(s0);
        
        
        const string s1 = 
            "M430.5,232.7c-26.8,0-51.2,12.8-65.2,33.7c-11.6-5.8-24.4-10.5-39.6-10.5c-40.7,0-74.5,30.3-80.3,69.8h243.2c11.6,0,20.9-8.1,23.3-19.8C507.3,265.3,472.4,232.7,430.5,232.7z";
        var p1 = SKPath.ParseSvgPathData(s1);
        
        
        const string s2 =
            "M442.2,302.5H69.8c0-25.6-20.9-46.5-46.5-46.5V93.1c25.6,0,46.5-20.9,46.5-46.5h372.4c0,25.6,20.9,46.5,46.5,46.5V256C463.1,256,442.2,276.9,442.2,302.5z";
        var p2 = SKPath.ParseSvgPathData(s2);


        const string s3 =
            "M430.5,232.7c-26.8,0-51.2,12.8-65.2,33.7c-11.6-5.8-24.4-10.5-39.6-10.5c-32.6,0-60.5,18.6-73.3,46.5h189.7c0-25.6,19.8-45.4,45.4-46.5C472.4,242,452.7,232.7,430.5,232.7z";
        var p3 = SKPath.ParseSvgPathData(s3);
            
        var list = new List<DrawPath>
        {
            new(){ Path = p0, Paint = st0}, new(){ Path = p1, Paint = st1}, new(){ Path = p2, Paint = st2},
            new(){ Path = p3, Paint = st0}
        };

        foreach (var lDrawPath in list) canvas.DrawPath(lDrawPath.Path, lDrawPath.Paint);

        canvas.DrawOval(256f, 174.5f, 93.1f, 104.7f, st0);
        canvas.DrawCircle(116.4f, 174.5f, 23.3f, st0);
        canvas.DrawCircle(395.6f, 174.5f, 23.3f, st0);
        
        return new Structs.Canvas()
        {
            Name = "Money",
            VCanvas = bitmap
        };
    }


    private static SKPaint _SkPaint(this string color) => new()
    {
        Style = SKPaintStyle.StrokeAndFill, Color = SKColor.Parse(color)
    };

    public struct DrawPath
    {
        public SKPath Path;
        public SKPaint Paint;
    }
}