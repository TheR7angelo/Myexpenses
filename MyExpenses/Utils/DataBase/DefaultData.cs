using System.Drawing;
using System.Reflection;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    private static void FillColors()
    {
        var colorType  = typeof(Color);
        var propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
        foreach (var propInfo  in propInfos)
        {
            var color = Color.FromName(propInfo.Name);
            var hex = $"#{color.A:X}{color.R:X}{color.G:X}{color.B:X}".ToUpper();

            var insert = new TColorsClass() { Nom = propInfo.Name, Value = hex };
            _connection!.InsertAsync(insert);
        }
    }
}