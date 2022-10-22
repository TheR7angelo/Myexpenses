using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MyExpenses.Utils.Ressources.Style;

public static class Button
{
    private static readonly List<Structs.Style> List = new();

    static Button()
    {
        List.Add(StyleWallet());
    }
    
    public static Xamarin.Forms.Style GetStyle(this string name)
    {
        var st = List.Where(s => s.Name.Equals(name)).ToList();
        return st.Count.Equals(0) ? null : st[0].VStyle;
    }
    
    private static Structs.Style StyleWallet()
    {
        var style = new Xamarin.Forms.Style(typeof(Xamarin.Forms.Button));
        
        style.Setters.Add(new Setter{Property = View.MarginProperty, Value = 7});
        style.Setters.Add(new Setter{Property = VisualElement.HeightRequestProperty, Value = 50});
        style.Setters.Add(new Setter{Property = VisualElement.WidthRequestProperty, Value = 100});
        style.Setters.Add(new Setter{Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center});
        style.Setters.Add(new Setter{Property = View.VerticalOptionsProperty, Value = LayoutOptions.Start});
        style.Setters.Add(new Setter{Property = Xamarin.Forms.Button.BorderColorProperty, Value = Color.Black});
        style.Setters.Add(new Setter{Property = Xamarin.Forms.Button.BorderWidthProperty, Value = 2});
        style.Setters.Add(new Setter{Property = Xamarin.Forms.Button.CornerRadiusProperty, Value = 10});
        style.Setters.Add(new Setter{Property = VisualElement.BackgroundColorProperty, Value = Color.LightGray});

        return new Structs.Style { Name = "ButtonWallet", VStyle = style };
    }
}