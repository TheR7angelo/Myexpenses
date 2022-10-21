using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    
    #region Get

    public static List<VWalletClass> GetAllWallet()
    {
        const string cmd = @"
        SELECT c.id, c.nom, ttc.nom as type, tc.nom as color, c.image
        FROM t_compte c
        LEFT JOIN t_type_compte ttc
            ON c.type_compte_fk = ttc.id
        LEFT JOIN t_colors tc
            ON c.color = tc.id";
        return _connection!.QueryAsync<VWalletClass>(cmd).Result;
    }

    public static List<TWalletType> GetAllWalletType() =>
        _connection!.QueryAsync<TWalletType>("SELECT * FROM main.t_type_compte").Result;

    #region Color

    public static IEnumerable<TColorsClass> GetAllColor() =>
        _connection!.QueryAsync<TColorsClass>("SELECT * FROM t_colors").Result;

    public static string GetColorHex(this string name)
    {
        var colors = _connection!.QueryAsync<TColorsClass>($"SELECT * FROM t_colors WHERE nom='{name}'").Result;
        return colors[0].Value;
    }

    #endregion
    
    public static List<VTotalByWaletClass> GetVTotalByWalletClass() => _connection!
        .QueryAsync<VTotalByWaletClass>(
            "SELECT h.compte, round(sum(h.montant), 2) as restant  FROM v_historique h GROUP BY h.compte;").Result;

    #endregion

    #region Insert

    public static TWalletClass InsertWallet(this TWalletClass walletClass)
    {
        _connection!.InsertAsync(walletClass).Wait();
        return walletClass;
    }
    
    public static TWalletType InsertWalletType(this TWalletType walletType)
    {
        _connection!.InsertAsync(walletType).Wait();
        return walletType;
    }

    // public static void Insert<T>(this T a) where T : new()
    // {
    //     _connection!.InsertAsync(a).Wait();
    // }

    #endregion
}