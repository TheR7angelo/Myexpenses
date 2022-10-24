using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MyExpenses.Utils.Ressources.Style;
using SkiaSharp;

namespace MyExpenses.Utils.Ressources;

public static partial class Images
{
    private static readonly Size _size = new(512, 512);
    
    private static readonly List<Structs.Canvas> List;

    static Images()
    {
        List = new List<Structs.Canvas>
        {
            Bank(), Money()
        };
    }
    
    public static List<Structs.Canvas> GetAllImages() => List;

    public static SKBitmap GetImage(this string name)
    {
        var st = List.Where(s => s.Name.Equals(name)).ToList();
        return st.Count.Equals(0) ? null : st[0].VCanvas;
    }

    private static Structs.Canvas Bank()
    {
        var bitmap = new SKBitmap(_size.Width, _size.Height);
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

        var p0 = SKPath.ParseSvgPathData(s0);
        var p1 = SKPath.ParseSvgPathData(s1);
        var p2 = SKPath.ParseSvgPathData(s2);
        var p3 = SKPath.ParseSvgPathData(s3);
        
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
        
        return new Structs.Canvas
        {
            Name = "Bank",
            VCanvas = bitmap
        };
    }
    
    private static Structs.Canvas Money()
    {
        var bitmap = new SKBitmap(_size.Width, _size.Height);
        var canvas = new SKCanvas(bitmap);

        var st0 = "#FF0E9347"._SkPaint();
        var st1 = "#FF0D8944"._SkPaint();
        var st2 = "#FF3BB54A"._SkPaint();
        var st3 = "#FF89C763"._SkPaint();
        var st4 = "#FFFFCA5D"._SkPaint();
        var st5 = "#FFF6B545"._SkPaint();
        var st6 = "#FFFFCB5B"._SkPaint();
        var st7 = "#FFFFE27A"._SkPaint();
        var st8 = "#FFF19920"._SkPaint();
        var st9 = "#FFE78825"._SkPaint();
        
        const string s0 = "M488.7,325.8H23.3C10.5,325.8,0,315.3,0,302.5v-256c0-12.8,10.5-23.3,23.3-23.3h465.5c12.8,0,23.3,10.5,23.3,23.3v256C512,315.3,501.5,325.8,488.7,325.8z";
        const string s1 = "M430.5,232.7c-26.8,0-51.2,12.8-65.2,33.7c-11.6-5.8-24.4-10.5-39.6-10.5c-40.7,0-74.5,30.3-80.3,69.8h243.2c11.6,0,20.9-8.1,23.3-19.8C507.3,265.3,472.4,232.7,430.5,232.7z";
        const string s2 = "M442.2,302.5H69.8c0-25.6-20.9-46.5-46.5-46.5V93.1c25.6,0,46.5-20.9,46.5-46.5h372.4c0,25.6,20.9,46.5,46.5,46.5V256C463.1,256,442.2,276.9,442.2,302.5z";
        const string s3 = "M430.5,232.7c-26.8,0-51.2,12.8-65.2,33.7c-11.6-5.8-24.4-10.5-39.6-10.5c-32.6,0-60.5,18.6-73.3,46.5h189.7c0-25.6,19.8-45.4,45.4-46.5C472.4,242,452.7,232.7,430.5,232.7z";
        const string s4 = "M267.6,162.9h-23.3c-7,0-11.6-4.7-11.6-11.6c0-7,4.7-11.6,11.6-11.6h34.9c7,0,11.6-4.7,11.6-11.6c0-7-4.7-11.6-11.6-11.6h-11.6c0-7-4.7-11.6-11.6-11.6c-7,0-11.6,4.7-11.6,11.6c-19.8,0-34.9,15.1-34.9,34.9s15.1,34.9,34.9,34.9h23.3c7,0,11.6,4.7,11.6,11.6c0,7-4.7,11.6-11.6,11.6h-34.9c-7,0-11.6,4.7-11.6,11.6c0,7,4.7,11.6,11.6,11.6h11.6c0,7,4.7,11.6,11.6,11.6c7,0,11.6-4.7,11.6-11.6c19.8,0,34.9-15.1,34.9-34.9S287.4,162.9,267.6,162.9z";
        const string s5 = "M407.3,232.7c-45.4,0-81.5,36.1-81.5,81.5s36.1,81.5,81.5,81.5s81.5-36.1,81.5-81.5S452.7,232.7,407.3,232.7zM407.3,372.4c-32.6,0-58.2-25.6-58.2-58.2s25.6-58.2,58.2-58.2s58.2,25.6,58.2,58.2S439.9,372.4,407.3,372.4z";
        const string s6 = "M407.3,394.5L407.3,394.5c45.4,1.2,81.5-36.1,81.5-80.3h-1.2c-5.8,0-10.5,4.7-11.6,10.5c-2.3,16.3-10.5,31.4-22.1,41.9c-9.3,9.3-22.1,15.1-36.1,17.5C411.9,384,407.3,388.7,407.3,394.5z";
        const string s7 = "M407.3,233.9L407.3,233.9c-45.4-1.2-81.5,36.1-81.5,80.3h1.2c5.8,0,10.5-4.7,11.6-10.5c2.3-16.3,10.5-31.4,22.1-41.9c9.3-9.3,22.1-15.1,36.1-17.5C402.6,244.4,407.3,239.7,407.3,233.9z";
        const string s8 = "M357.2,322.3c0-32.6,25.6-58.2,58.2-58.2c14,0,26.8,4.7,37.2,14c-11.6-14-26.8-22.1-45.4-22.1c-32.6,0-58.2,25.6-58.2,58.2c0,18.6,8.1,33.7,20.9,44.2C361.9,349.1,357.2,336.3,357.2,322.3z";
        const string s9 = "M442.2,350.3c-1.2,0-2.3,0-3.5-1.2c-2.3-2.3-2.3-5.8,0-8.1c7-7,10.5-17.5,10.5-26.8c0-3.5,2.3-5.8,5.8-5.8s5.8,2.3,5.8,5.8c0,12.8-4.7,25.6-12.8,34.9C445.7,350.3,443.3,350.3,442.2,350.3z";
        const string s10 = "M407.3,349.1c-7,0-11.6-4.7-11.6-11.6v-46.5c0-7,4.7-11.6,11.6-11.6s11.6,4.7,11.6,11.6v46.5C418.9,344.4,414.3,349.1,407.3,349.1z";
        const string s11 = "M450.3,382.8c-8.1-29.1-29.1-52.4-55.9-62.8c-4.7-26.8-22.1-50-44.2-64c-14,15.1-23.3,34.9-23.3,57c0,45.4,36.1,81.5,81.5,81.5C423.6,395.6,437.5,391,450.3,382.8z";
        const string s12 = "M370,358.4c-8.1-10.5-14-23.3-14-37.2c0-16.3,7-31.4,18.6-41.9c-2.3-3.5-4.7-5.8-7-8.1c-11.6,10.5-18.6,25.6-18.6,43.1C349.1,332.8,357.2,347.9,370,358.4z";
        const string s13 = "M395.6,321.2v16.3c0,7,4.7,11.6,11.6,11.6s11.6-4.7,11.6-11.6v-2.3C411.9,329.3,403.8,324.7,395.6,321.2z";
        const string s14 = "M450.3,382.8c-2.3-8.1-4.7-15.1-9.3-22.1c-9.3,7-20.9,11.6-33.7,11.6c-32.6,0-58.2-25.6-58.2-58.2c0-17.5,7-32.6,18.6-43.1c-5.8-5.8-11.6-10.5-18.6-14c-14,15.1-23.3,34.9-23.3,57c0,45.4,36.1,81.5,81.5,81.5C423.6,395.6,437.5,391,450.3,382.8z";
        const string s15 = "M446.8,372.4c-8.1,5.8-18.6,9.3-29.1,11.6c-5.8,1.2-10.5,5.8-10.5,11.6l0,0c16.3,0,30.3-4.7,43.1-12.8C449.2,379.3,448,375.9,446.8,372.4z";
        const string s16 = "M349.1,257.2c-14,15.1-23.3,34.9-23.3,57h1.2c5.8,0,10.5-4.7,11.6-10.5c2.3-16.3,10.5-30.3,20.9-40.7C356.1,261.8,352.6,259.5,349.1,257.2z";
        const string s17 = "M290.9,395.6c0,8.1,1.2,15.1,3.5,23.3c2.3,0,5.8,0,8.1,0c45.4,0,81.5-36.1,81.5-81.5c0-8.1-1.2-15.1-3.5-23.3c-2.3,0-5.8,0-8.1,0C327,314.2,290.9,350.3,290.9,395.6z";
        const string s18 = "M302.5,256c-45.4,0-81.5,36.1-81.5,81.5s36.1,81.5,81.5,81.5s81.5-36.1,81.5-81.5S347.9,256,302.5,256zM302.5,395.6c-32.6,0-58.2-25.6-58.2-58.2s25.6-58.2,58.2-58.2s58.2,25.6,58.2,58.2S335.1,395.6,302.5,395.6z";
        const string s19 = "M302.5,417.7L302.5,417.7c45.4,1.2,81.5-36.1,81.5-80.3h-1.2c-5.8,0-10.5,4.7-11.6,10.5c-2.3,16.3-10.5,31.4-22.1,41.9c-9.3,9.3-22.1,15.1-36.1,17.5C307.2,407.3,302.5,411.9,302.5,417.7z";
        const string s20 = "M302.5,257.2L302.5,257.2c-45.4-1.2-81.5,36.1-81.5,80.3h1.2c5.8,0,10.5-4.7,11.6-10.5c2.3-16.3,10.5-31.4,22.1-41.9c9.3-9.3,22.1-15.1,36.1-17.5C297.9,267.6,302.5,263,302.5,257.2z";
        const string s21 = "M252.5,345.6c0-32.6,25.6-58.2,58.2-58.2c14,0,26.8,4.7,37.2,14c-11.6-14-26.8-22.1-45.4-22.1c-32.6,0-58.2,25.6-58.2,58.2c0,18.6,8.1,33.7,20.9,44.2C257.2,372.4,252.5,359.6,252.5,345.6z";
        const string s22 = "M337.5,373.5c-1.2,0-2.3,0-3.5-1.2c-2.3-2.3-2.3-5.8,0-8.1c7-7,10.5-17.5,10.5-26.8c0-3.5,2.3-5.8,5.8-5.8s5.8,2.3,5.8,5.8c0,12.8-4.7,25.6-12.8,34.9C340.9,373.5,338.6,373.5,337.5,373.5z";
        const string s23 = "M302.5,372.4c-7,0-11.6-4.7-11.6-11.6v-46.5c0-7,4.7-11.6,11.6-11.6c7,0,11.6,4.7,11.6,11.6v46.5C314.2,367.7,309.5,372.4,302.5,372.4z";
        const string s24 = "M356.1,315.3c2.3,7,4.7,14,4.7,22.1c0,32.6-25.6,58.2-58.2,58.2c-3.5,0-8.1,0-11.6-1.2v1.2c0,8.1,1.2,15.1,3.5,23.3c2.3,0,5.8,0,8.1,0c45.4,0,81.5-36.1,81.5-81.5c0-8.1-1.2-15.1-3.5-23.3c-2.3,0-5.8,0-8.1,0C366.5,314.2,361.9,314.2,356.1,315.3z";
        const string s25 = "M360.7,325.8c-45.4,0-81.5,36.1-81.5,81.5s36.1,81.5,81.5,81.5s81.5-36.1,81.5-81.5S406.1,325.8,360.7,325.8zM360.7,465.5c-32.6,0-58.2-25.6-58.2-58.2s25.6-58.2,58.2-58.2s58.2,25.6,58.2,58.2S393.3,465.5,360.7,465.5z";
        const string s26 = "M360.7,487.6L360.7,487.6c45.4,1.2,81.5-36.1,81.5-80.3H441c-5.8,0-10.5,4.7-11.6,10.5c-2.3,16.3-10.5,31.4-22.1,41.9c-9.3,9.3-22.1,15.1-36.1,17.5C365.4,477.1,360.7,481.7,360.7,487.6z";
        const string s27 = "M360.7,327L360.7,327c-45.4-1.2-81.5,36.1-81.5,80.3h1.2c5.8,0,10.5-4.7,11.6-10.5c2.3-16.3,10.5-31.4,22.1-41.9c9.3-9.3,22.1-15.1,36.1-17.5C356.1,337.5,360.7,332.8,360.7,327z";
        const string s28 = "M310.7,415.4c0-32.6,25.6-58.2,58.2-58.2c14,0,26.8,4.7,37.2,14c-11.6-14-26.8-22.1-45.4-22.1c-32.6,0-58.2,25.6-58.2,58.2c0,18.6,8.1,33.7,20.9,44.2C315.3,442.2,310.7,429.4,310.7,415.4z";
        const string s29 = "M395.6,443.3c-1.2,0-2.3,0-3.5-1.2c-2.3-2.3-2.3-5.8,0-8.1c7-7,10.5-17.5,10.5-26.8c0-3.5,2.3-5.8,5.8-5.8s5.8,2.3,5.8,5.8c0,12.8-4.7,25.6-12.8,34.9C399.1,443.3,396.8,443.3,395.6,443.3z";
        const string s30 = "M360.7,442.2c-7,0-11.6-4.7-11.6-11.6V384c0-7,4.7-11.6,11.6-11.6c7,0,11.6,4.7,11.6,11.6v46.5C372.4,437.5,367.7,442.2,360.7,442.2z";
        
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
        var p11 = SKPath.ParseSvgPathData(s11);
        var p12 = SKPath.ParseSvgPathData(s12);
        var p13 = SKPath.ParseSvgPathData(s13);
        var p14 = SKPath.ParseSvgPathData(s14);
        var p15 = SKPath.ParseSvgPathData(s15);
        var p16 = SKPath.ParseSvgPathData(s16);
        var p17 = SKPath.ParseSvgPathData(s17);
        var p18 = SKPath.ParseSvgPathData(s18);
        var p19 = SKPath.ParseSvgPathData(s19);
        var p20 = SKPath.ParseSvgPathData(s20);
        var p21 = SKPath.ParseSvgPathData(s21);
        var p22 = SKPath.ParseSvgPathData(s22);
        var p23 = SKPath.ParseSvgPathData(s23);
        var p24 = SKPath.ParseSvgPathData(s24);
        var p25 = SKPath.ParseSvgPathData(s25);
        var p26 = SKPath.ParseSvgPathData(s26);
        var p27 = SKPath.ParseSvgPathData(s27);
        var p28 = SKPath.ParseSvgPathData(s28);
        var p29 = SKPath.ParseSvgPathData(s29);
        var p30 = SKPath.ParseSvgPathData(s30);

        canvas.DrawPaths(new List<Structs.DrawPath>
        {
            new(){ Path = p0, Paint = st0}, new(){ Path = p1, Paint = st1}, new(){ Path = p2, Paint = st2},
            new(){ Path = p3, Paint = st0}
        });

        canvas.DrawOval(256f, 174.5f, 93.1f, 104.7f, st0);
        canvas.DrawCircles(new List<Structs.DrawCircle>
        {
            new(){ Point = new SKPoint{ X = 116.4f, Y = 174.5f }, Radius = 23.3f, Paint = st0},
            new(){ Point = new SKPoint{ X = 395.6f, Y = 174.5f }, Radius = 23.3f, Paint = st0}
        });
        
        canvas.DrawPath(p4, st3);
        canvas.DrawCircles(new List<Structs.DrawCircle>
        {
            new(){Point = new SKPoint{X = 302.5f, Y = 337.5f}, Radius = 81.5f, Paint = st4},
            new(){Point = new SKPoint{X = 407.3f, Y = 314.2f}, Radius = 81.5f, Paint = st5}
        });
        
        canvas.DrawPaths(new List<Structs.DrawPath>
        {
            new(){ Path = p5, Paint = st6}, new(){ Path = p6, Paint = st5}, new(){ Path = p7, Paint = st7},
            new(){ Path = p8, Paint = st8}, new(){ Path = p9, Paint = st6}, new(){ Path = p10, Paint = st8},
            new(){ Path = p11, Paint = st8}, new(){ Path =  p12, Paint = st9}, new(){ Path = p13, Paint = st9},
            new(){ Path = p14, Paint = st5}, new(){ Path =  p15, Paint = st8}, new(){ Path = p16, Paint = st6}
        });
        
        canvas.DrawCircle(302.5f, 337.5f, 81.5f, st5);
        
        canvas.DrawPaths(new List<Structs.DrawPath>
        {
            new(){Path = p17, Paint = st8}, new(){Path = p18, Paint = st6}, new(){Path = p19, Paint = st5},
            new(){Path = p20, Paint = st7}, new(){Path = p21, Paint = st8}, new(){Path = p22, Paint = st6},
            new(){Path = p23, Paint = st8}, new(){Path = p24, Paint = st5}
        });
        
        canvas.DrawCircle(360.7f, 407.3f, 81.5f, st5);
        
        canvas.DrawPaths(new List<Structs.DrawPath>
        {
            new(){Path = p25, Paint = st6}, new(){Path = p26, Paint = st5}, new(){Path = p27, Paint = st7},
            new(){Path = p28, Paint = st8}, new(){Path = p29, Paint = st6}, new(){Path = p30, Paint = st8}
        });
        
        return new Structs.Canvas
        {
            Name = "Money",
            VCanvas = bitmap
        };
    }
}