using System.Collections.Generic;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static List<TAccountClass> GetAllAccount() => _connection!.QueryAsync<TAccountClass>("SELECT * FROM t_compte").Result;

    public static List<TColorsClass> GetAllColor() =>
        _connection!.QueryAsync<TColorsClass>("SELECT * FROM t_colors").Result;

    public static List<VTotalByAccountClass> GetVTotalByAccountClass() => _connection!
        .QueryAsync<VTotalByAccountClass>(
            "SELECT h.compte, round(sum(h.montant), 2) as restant  FROM v_historique h GROUP BY h.compte;").Result;
}