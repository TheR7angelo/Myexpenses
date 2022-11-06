﻿using MyExpenses.Utils.Function;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    // public static CategoryClass Insert(this CategoryClass categoryClass)
    // {
    //     _connection!.InsertAsync(categoryClass).Wait();
    //     return categoryClass;
    // }
    
    public static HistoriqueClass Insert(this HistoriqueClass historiqueClass)
    {
        _connection!.InsertAsync(historiqueClass).Wait();
        return historiqueClass;
    }

    public static LieuClass Insert(this LieuClass lieuClass)
    {
        _connection!.InsertAsync(lieuClass).Wait();
        return lieuClass;
    }

    public static ITableDisplay Insert(this ITableDisplay obj)
    {
        _connection!.InsertAsync(obj).Wait();
        return obj;
    }
    
    public static WalletClass Insert(this WalletClass walletClass)
    {
        _connection!.InsertAsync(walletClass).Wait();
        return walletClass;
    }
    
    public static WalletTypeClass Insert(this WalletTypeClass walletTypeClass)
    {
        _connection!.InsertAsync(walletTypeClass).Wait();
        return walletTypeClass;
    }
}