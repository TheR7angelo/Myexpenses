using System.Collections.Generic;
using System.Linq;
using MyExpenses.Utils.Ressources.Style;

namespace MyExpenses.Utils.Ressources;

public static class Images
{
    private static readonly List<Structs.Images> List = new ();

    static Images()
    {
        
    }


    public static List<Structs.Images> GetAllImages() => List;

    public static Xamarin.Forms.ImageSource GetImageSource(this string name)
    {
        var st = List.Where(s => s.Name.Equals(name)).ToList();
        return st.Count.Equals(0) ? null : st[0].VImageSource;
    }

}