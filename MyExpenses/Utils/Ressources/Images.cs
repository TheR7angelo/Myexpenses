using System.Collections.Generic;
using System.Linq;
using MyExpenses.Utils.Ressources.Style;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace MyExpenses.Utils.Ressources;

public static class Images
{
    private static readonly List<Structs.Paths> List;

    static Images()
    {
        List = new List<Structs.Paths>
        {
            Money()
        };
    }
    
    public static List<Structs.Paths> GetAllImages() => List;

    public static Path GetImage(this string name)
    {
        var st = List.Where(s => s.Name.Equals(name)).ToList();
        return st.Count.Equals(0) ? null : st[0].VPath;
    }

    private static Structs.Paths Money()
    {
        var path = new Path();
        var group = new GeometryGroup();


        var i = _geometry(
            "M430.545,232.727c-26.764,0-51.2,12.8-65.164,33.745C353.745,260.655,340.945,256,325.818,256\n\tc-40.727,0-74.473,30.255-80.291,69.818h243.2c11.636,0,20.945-8.145,23.273-19.782\n\tC507.345,265.309,472.436,232.727,430.545,232.727z");

        group.Children.Add(i);





        path.Data = group;
        return new Structs.Paths()
        {
            Name = "Money",
            VPath = path
        };
    }

    private static Geometry _geometry(this string geom)
    {
        var path = new PathGeometryConverter();
        return (Geometry)path.ConvertFromInvariantString(geom);
    }

}