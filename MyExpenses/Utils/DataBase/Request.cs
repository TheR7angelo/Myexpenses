using System.Collections.Generic;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    
    #region Get

    #region Color

    public static IEnumerable<TColorsClass> GetAllColor() =>
        _connection!.QueryAsync<TColorsClass>("SELECT * FROM t_colors").Result;

    public static string GetColorHex(this string name)
    {
        var colors = _connection!.QueryAsync<TColorsClass>($"SELECT * FROM t_colors WHERE nom='{name}'").Result;
        return colors[0].Value;
    }

    #endregion
    
    public static List<VWalletClass> GetAllWallet()
    {
        const string cmd = @"
        SELECT c.id, c.nom, ttc.nom as type, tc.nom as color_name, tc.value as color_value, ti.name as image
        FROM t_compte c
        LEFT JOIN t_type_compte ttc
            ON c.type_compte_fk = ttc.id
        LEFT JOIN t_colors tc
            ON c.color_fk = tc.id
        LEFT JOIN t_images ti
            ON c.image_fk = ti.id";
        return _connection!.QueryAsync<VWalletClass>(cmd).Result;
    }

    public static List<TWalletType> GetAllWalletType() =>
        _connection!.QueryAsync<TWalletType>("SELECT * FROM t_type_compte").Result;

    public static List<TLieuClass> GetAllLieu() => _connection!.QueryAsync<TLieuClass>("SELECT * FROM t_lieu").Result;

    #region Image

    public static IEnumerable<TImageClass> GetAllImages() =>
        _connection!.QueryAsync<TImageClass>("SELECT * FROM t_images").Result;

    #endregion
    
    public static List<VTotalByWaletClass> GetVTotalByWalletClass() => _connection!
        .QueryAsync<VTotalByWaletClass>(
            "SELECT h.compte, round(sum(h.montant), 2) as restant  FROM v_historique h GROUP BY h.compte;").Result;

    #endregion

    #region Insert

    public static THistoriqueClass InsertHistorique(this THistoriqueClass historiqueClass)
    {
        _connection!.InsertAsync(historiqueClass).Wait();
        return historiqueClass;
    }
    
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

    #endregion

    #region Set

    public static void RefreshImage()
    {
        FillImages();
    }

    #endregion
}