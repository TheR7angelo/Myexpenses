using System.Collections.Generic;
using System.Linq;
using MyExpenses.Utils.Ressources.Style;
using SkiaSharp;
using Xamarin.Forms;
using Size = System.Drawing.Size;

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

    public static SKBitmap GetSkBitmap(this string name)
    {
        var st = List.Where(s => s.Name.Equals(name)).ToList();
        return st.Count.Equals(0) ? null : st[0].VCanvas;
    }

    public static ImageSource ParseToImageSource(this SKBitmap bitmap)
    {
        var sk = SKImage.FromPixels(bitmap.PeekPixels());
        var data = sk.Encode().AsStream();
        return ImageSource.FromStream(() => data);
    }
}