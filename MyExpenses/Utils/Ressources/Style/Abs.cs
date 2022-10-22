using System.Collections.Generic;

namespace MyExpenses.Utils.Ressources.Style;

public abstract class Abs
{
    private List<StrucStyle> _list = new();

    public abstract Xamarin.Forms.Style GetStyle(string name);

    public struct StrucStyle
    {
        public string Name;
        public Xamarin.Forms.Style Style;
    }
    
}