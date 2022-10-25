using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MyExpenses.Utils.Ressources.Style;
using SkiaSharp;

namespace MyExpenses.Utils.Ressources;

public static partial class Images
{
    private static readonly Size Size = new(512, 512);
    
    private static readonly List<Structs.Canvas> List;

    static Images()
    {
        List = new List<Structs.Canvas>
        {
            Bank(), Card(), Money()
        };
    }
    
    public static List<Structs.Canvas> GetAllImages() => List;

    public static SKBitmap GetImage(this string name)
    {
        var st = List.Where(s => s.Name.Equals(name)).ToList();
        return st.Count.Equals(0) ? null : st[0].VCanvas;
    }
}