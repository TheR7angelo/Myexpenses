using System.Collections.Generic;
using MyExpenses.Utils.Ressources.Style;
using SkiaSharp;

namespace MyExpenses.Utils.Ressources;

public static partial class Images
{
        private static Structs.Canvas Bank()
    {
        var bitmap = new SKBitmap(Size.Width, Size.Height);
        var canvas = new SKCanvas(bitmap);

        var st0 = "#FFAEE69C"._SkPaint();
        var st1 = "#FF89DAA4"._SkPaint();
        var st2 = "#FFFFE179"._SkPaint();
        var st3 = "#FFFBD268"._SkPaint();
        var st4 = "#FFFFFFFF"._SkPaint();
        var st5 = "#FF3D6D93"._SkPaint();
        var st6 = "#FF335E80"._SkPaint();
        var st7 = "#FFEAF6FF"._SkPaint();
        var st8 = "#FFD8ECFE"._SkPaint();

        const string s0 = "M0,137.6L241,3.9c9.3-5.2,20.6-5.2,30,0l241,133.7H0z";
        const string s1 = "M271,3.9c-9.3-5.2-20.6-5.2-30,0l-4.6,2.5l236.4,131.1H512L271,3.9z";
        const string s2 = "M256,243.9c-5.3,0-10.4,0.5-15.5,1.5c37.2,7.2,65.4,40,65.4,79.4s-28.1,72.1-65.4,79.4c5,1,10.2,1.5,15.5,1.544.6,0,80.8-36.2,80.8-80.8S300.6,243.9,256,243.9L256,243.9z";
        const string s3 = "M265.4,317h-1.7v-26.8h1.7c3.4,0,6.4,3.7,6.4,8.1c0,4.3,3.5,7.7,7.7,7.7s7.7-3.5,7.7-7.7c0-13-9.8-23.6-21.8-23.6h-1.7v-4.3c0-4.3-3.5-7.7-7.7-7.7s-7.7,3.5-7.7,7.7v4.3h-1.7c-12,0-21.8,10.6-21.8,23.6v10.6c0,13,9.8,23.6,21.8,23.6h1.7v26.8h-1.7c-3.4,0-6.4-3.7-6.4-8.1c0-4.3-3.5-7.7-7.7-7.7s-7.7,3.5-7.7,7.7c0,13,9.8,23.6,21.8,23.6h1.7v4.3c0,4.3,3.5,7.7,7.7,7.7c4.3,0,7.7-3.5,7.7-7.7v-4.3h1.7c12,0,21.8-10.6,21.8-23.6v-10.6C287.2,327.6,277.4,317,265.4,317zc0,4.4-2.9,8.1-6.4,8.1h-1.7v-26.8h1.7c3.4,0,6.4,3.7,6.4,8.1L271.8,351.2z";
        const string s4 = "M256,46.6c-5.7,0-11,1.7-15.5,4.7c7.5,5,12.4,13.5,12.4,23.1s-4.9,18.1-12.4,23.1c4.4,3,9.7,4.7,15.5,4.7c15.4,0,27.8-12.5,27.8-27.8S271.4,46.6,256,46.6z";
        const string s5 = "M481.1,465.6H357.2l30.9-43.5h62.1L481.1,465.6z";
        const string s6 = "M450.2,422.1h-30.9l30.9,43.5h30.9L450.2,422.1z";
        const string s7 = "M481.1,183.9H357.2l30.9,43.5h62.1L481.1,183.9z";
        const string s8 = "M450.2,183.9l-30.9,43.5h30.9l30.9-43.5H450.2z";
        const string s9 = "M388.1,227.5h62.1v194.6h-62.1V227.5z";
        const string s10 = "M419.3,227.5h30.9v194.6h-30.9V227.5z";
        
        
        var p0 = SKPath.ParseSvgPathData(s0);
        var p1 = SKPath.ParseSvgPathData(s1);
        var p2 = SKPath.ParseSvgPathData(s2);
        var p3 = SKPath.ParseSvgPathData(s3);
        var p4 = SKPath.ParseSvgPathData(s4);
        var p5 = SKPath.ParseSvgPathData(s5);
        var p6 = SKPath.ParseSvgPathData(s6);
        var p7 = SKPath.ParseSvgPathData(s7);
        var p8 = SKPath.ParseSvgPathData(s8);
        var p9 = SKPath.ParseSvgPathData(s9);
        var p10 = SKPath.ParseSvgPathData(s10);
        
        canvas.DrawPaths(new List<Structs.DrawPath>
        {
            new(){ Path = p0, Paint = st0}, new(){ Path = p1, Paint = st1}
        });
        
        canvas.DrawOval(256f, 324.8f, 80.8f, 80.8f, st1);
        
        canvas.DrawPaths(new List<Structs.DrawPath>
        {
            new(){ Path = p2, Paint = st3}, new(){ Path = p3, Paint = st4}
        });
        
        canvas.DrawOval(256f, 74.5f, 27.8f, 27.8f, st5);
        
        canvas.DrawPaths(new List<Structs.DrawPath>
        {
            new(){ Path = p4, Paint = st6}, new(){ Path = p5, Paint = st0}, new(){ Path = p6, Paint = st1},
            new(){ Path = p7, Paint = st0}, new(){ Path = p8, Paint = st1}, new(){ Path = p9, Paint = st7},
            new(){ Path = p10, Paint = st8}
        });
        
        
        
        
        
        return new Structs.Canvas
        {
            Name = "Bank",
            VCanvas = bitmap
        };
    }
}