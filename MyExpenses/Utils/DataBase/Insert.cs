namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static HistoriqueClass InsertHistorique(this HistoriqueClass historiqueClass)
    {
        _connection!.InsertAsync(historiqueClass).Wait();
        return historiqueClass;
    }

    public static LieuClass InsertLieu(this LieuClass lieuClass)
    {
        _connection!.InsertAsync(lieuClass).Wait();
        return lieuClass;
    }
    
    public static WalletClass InsertWallet(this WalletClass walletClass)
    {
        _connection!.InsertAsync(walletClass).Wait();
        return walletClass;
    }
    
    public static WalletType InsertWalletType(this WalletType walletType)
    {
        _connection!.InsertAsync(walletType).Wait();
        return walletType;
    }
}