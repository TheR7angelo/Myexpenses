using System.Collections.Generic;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static List<VAccountClass> GetAllAccount()
    {
        const string cmd = @"
        SELECT c.id, c.nom, ttc.nom as type, tc.nom as color, c.image
        FROM t_compte c
        LEFT JOIN t_type_compte ttc
            ON c.type_compte_fk = ttc.id
        LEFT JOIN t_colors tc
            ON c.color = tc.id";
        return _connection!.QueryAsync<VAccountClass>(cmd).Result;
    }

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