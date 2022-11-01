﻿using System.Collections.Generic;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    #region Color

    public static IEnumerable<ColorsClass> GetAllColor() =>
        _connection!.QueryAsync<ColorsClass>("SELECT * FROM t_colors").Result;

    public static string GetColorHex(this string name)
    {
        var colors = _connection!.QueryAsync<ColorsClass>($"SELECT * FROM t_colors WHERE nom='{name}'").Result;
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

    public static List<WalletType> GetAllWalletType() =>
        _connection!.QueryAsync<WalletType>("SELECT * FROM t_type_compte").Result;

    public static List<LieuClass> GetAllLieu() => _connection!.QueryAsync<LieuClass>("SELECT * FROM t_lieu").Result;

    #region Image

    public static IEnumerable<ImageClass> GetAllImages() =>
        _connection!.QueryAsync<ImageClass>("SELECT * FROM t_images").Result;

    #endregion
    
    public static List<VTotalByWaletClass> GetVTotalByWalletClass() => _connection!
        .QueryAsync<VTotalByWaletClass>(
            "SELECT h.compte, round(sum(h.montant), 2) as restant  FROM v_historique h GROUP BY h.compte;").Result;
}