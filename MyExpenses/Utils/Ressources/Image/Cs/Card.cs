using System.Collections.Generic;
using MyExpenses.Utils.Ressources.Style;
using SkiaSharp;

namespace MyExpenses.Utils.Ressources;

public static partial class Images
{
    private static Structs.Canvas Card()
    {
        var bitmap = new SKBitmap(Size.Width, Size.Height);
        var canvas = new SKCanvas(bitmap);

        var st0 = "#FF67AC5C"._SkPaint();
        var st1 = "#FF85BD7D"._SkPaint();
        var st2 = "#FFE0E0E0"._SkPaint();
        var st3 = "#FFF5F5F5"._SkPaint();
        var st4 = "#FF518C46"._SkPaint();
        var st5 = "#FFF7C244"._SkPaint();
        var st6 = "#FFFDDFB1"._SkPaint();
        var st7 = "#FFF9CE69"._SkPaint();
        var st8 = "#FFFDE5C1"._SkPaint();

        const string s0 = "M16,176h432l0,0v232c0,22.1-17.9,40-40,40H56c-22.1,0-40-17.9-40-40V176L16,176z";
        const string s1 = "M16,176v176h344c13.3,0,24-10.7,24-24V176H16z";
        const string s2 = "M392,64c57.4,0,104,46.6,104,104s-46.6,104-104,104s-104-46.6-104-104c0-25.3,9.1-48.5,24.1-66.6C331.2,78.6,359.9,64,392,64z";
        const string s3 = "M392,64c-57.4,0-104,46.6-104,104c0,13.7,2.7,27.3,8,40h136c13.3,0,24-10.7,24-24V86.1C437.8,71.8,415.2,64,392,64z";
        const string s4 = "M48,208h224v144H48V208z";
        const string s5 = "M48,208v104h160c13.3,0,24-10.7,24-24v-80H48z";
        const string s6 = "M16,376h432v16H16V376z";
        const string s7 = "M440,224h-96l0,0v-32c0-8.8,7.2-16,16-16h64c8.8,0,16,7.2,16,16V224L440,224z";
        const string s8 = "M392,160L392,160c-13.3,0-24-10.7-24-24v-16c0-13.3,10.7-24,24-24l0,0c13.3,0,24,10.7,24,24v16C416,149.3,405.3,160,392,160z";
        const string s9 = "M96,256c4.4,0,8,3.6,8,8h16c0-10.1-6.4-19.1-16-22.5V232H88v9.5c-12.5,4.4-19,18.1-14.6,30.5c3.4,9.6,12.5,16,22.6,16c4.4,0,8,3.6,8,8s-3.6,8-8,8s-8-3.6-8-8H72c0,10.1,6.4,19.1,16,22.5v9.5h16v-9.5c12.5-4.4,19-18.1,14.6-30.5c-3.4-9.6-12.5-16-22.6-16c-4.4,0-8-3.6-8-8S91.6,256,96,256z";
        const string s10 = "M160,256c4.4,0,8,3.6,8,8h16c0-10.1-6.4-19.1-16-22.5V232h-16v9.5c-12.5,4.4-19,18.1-14.6,30.5c3.4,9.6,12.5,16,22.6,16c4.4,0,8,3.6,8,8s-3.6,8-8,8s-8-3.6-8-8h-16c0,10.1,6.4,19.1,16,22.5v9.5h16v-9.5c12.5-4.4,19-18.1,14.6-30.5c-3.4-9.6-12.5-16-22.6-16c-4.4,0-8-3.6-8-8S155.6,256,160,256z";
        const string s11 = "M224,256c4.4,0,8,3.6,8,8h16c0-10.1-6.4-19.1-16-22.5V232h-16v9.5c-12.5,4.4-19,18.1-14.6,30.5c3.4,9.6,12.5,16,22.6,16c4.4,0,8,3.6,8,8s-3.6,8-8,8s-8-3.6-8-8h-16c0,10.1,6.4,19.1,16,22.5v9.5h16v-9.5c12.5-4.4,19-18.1,14.6-30.5c-3.4-9.6-12.5-16-22.6-16c-4.4,0-8-3.6-8-8S219.6,256,224,256z";
        const string s12 = "M296,344h128v16H296V344z";
        const string s13 = "M296,312h56v16h-56V312z";
        const string s14 = "M368,312h56v16h-56V312z";
        const string s15 = "M392,208c13.3,0,24-10.7,24-24v-8h-56c-8.8,0-16,7.2-16,16v16H392z";
        const string s16 = "M392,96c-13.3,0-24,10.7-24,24v16c0,2.7,0.5,5.4,1.5,8h6.5c13.3,0,24-10.7,24-24V97.5C397.4,96.5,394.7,96,392,96z";

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
        
        canvas.DrawPaths(new List<Structs.DrawPath>
        {
            new(){Path = p0, Paint = st0}, new(){Path = p1, Paint = st1}, new(){Path = p2, Paint = st2},
            new(){Path = p3, Paint = st3}, new(){Path = p4, Paint = st2}, new(){Path = p5, Paint = st3},
            new(){Path = p6, Paint = st4}, new(){Path = p7, Paint = st5}, new(){Path = p8, Paint = st6},
            new(){Path = p9, Paint = st4}, new(){Path = p10, Paint = st4}, new(){Path = p11, Paint = st4},
            new(){Path = p12, Paint = st4}, new(){Path = p13, Paint = st4}, new(){Path = p14, Paint = st4},
            new(){Path = p15, Paint = st7}, new(){Path = p16, Paint = st8}
        });
        
        return new Structs.Canvas
        {
            Name = "Card",
            VCanvas = bitmap
        };
    }
}



