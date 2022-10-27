using System.Drawing;
using System.Linq;
using System.Reflection;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    #region Color

    private static void FillColors()
    {
        var colorType  = typeof(Color);
        var propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
        foreach (var propInfo  in propInfos)
        {
            var color = Color.FromName(propInfo.Name);
            var a = color.A.ParseToHex();
            var r = color.R.ParseToHex();
            var g = color.G.ParseToHex();
            var b = color.B.ParseToHex();
            
            var hex = $"#{a}{r}{g}{b}".ToUpper();

            var insert = new TColorsClass { Nom = propInfo.Name, Value = hex };
            _connection!.InsertAsync(insert).Wait();
        }
    }

    private static string ParseToHex(this byte value)
    {
        var hex = value.ToString("X");
        if (hex.Length.Equals(1)) hex = $"0{hex}";

        return hex;
    } 

    #endregion


    #region Images

    private static void FillImages()
    {
        var images = Ressources.Images.GetAllImages();
        var sqlImage = GetAllImages().Select(s => s.Name).ToList();
        
        foreach (var image in images)
        {
            if (sqlImage.Contains(image.Name)) continue;
            
            var insert = new TImageClass { Name = image.Name };
            _connection!.InsertAsync(insert).Wait();
        }
    }

    #endregion
}