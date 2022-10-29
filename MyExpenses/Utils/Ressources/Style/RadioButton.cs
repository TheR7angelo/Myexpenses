using System.Collections.Generic;
using System.Linq;

namespace MyExpenses.Utils.Ressources.Style;

public class RadioButton
{
    private static readonly List<Structs.Style> List = new();

    static RadioButton()
    {
        List.Add(RdAddress());
    }
    
    public static Xamarin.Forms.Style GetStyle(string name)
    {
        var st = List.Where(s => s.Name.Equals(name)).ToList();
        return st.Count.Equals(0) ? null : st[0].VStyle;
    }

    private static Structs.Style RdAddress()
    {

        var st = new Xamarin.Forms.Style(typeof(Xamarin.Forms.RadioButton));

        return new Structs.Style()
        {
            Name = "rdAddress",
            VStyle = st
        };
    }
}