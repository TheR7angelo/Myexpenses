namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
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
    
    public static WalletClass Insert(this WalletClass walletClass)
    {
        _connection!.InsertAsync(walletClass).Wait();
        return walletClass;
    }
    
    public static WalletType Insert(this WalletType walletType)
    {
        _connection!.InsertAsync(walletType).Wait();
        return walletType;
    }
}