using System.Collections.Generic;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static IEnumerable<VAccountClass> GetAllAccount() => _connection!.QueryAsync<VAccountClass>("SELECT * FROM t_compte").Result;

    #region Color

    public static IEnumerable<TColorsClass> GetAllColor() =>
        _connection!.QueryAsync<TColorsClass>("SELECT * FROM t_colors").Result;

    public static string GetColorHex(this string name)
    {
        var colors = _connection!.QueryAsync<TColorsClass>($"SELECT * FROM t_colors WHERE nom='{name}'").Result;
        return colors[0].Value;
    }

    #endregion
    
    public static List<VTotalByAccountClass> GetVTotalByAccountClass() => _connection!
        .QueryAsync<VTotalByAccountClass>(
            "SELECT h.compte, round(sum(h.montant), 2) as restant  FROM v_historique h GROUP BY h.compte;").Result;
}