namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static CategoryClass Delete(this CategoryClass categoryClass)
    {
        _connection!.DeleteAsync(categoryClass).Wait();
        return categoryClass;
    }
    
    public static LieuClass Delete(this LieuClass lieuClass)
    {
        _connection!.DeleteAsync(lieuClass).Wait();
        return lieuClass;
    }

    public static WalletTypeClass Delete(this WalletTypeClass walletTypeClass)
    {
        _connection!.DeleteAsync(walletTypeClass).Wait();
        return walletTypeClass;
    }
}