using MyExpenses.Utils.Function;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static LieuClass Update(this LieuClass lieuClass)
    {
        _connection!.UpdateAsync(lieuClass).Wait();
        return lieuClass;
    }

    public static ITableDisplay Update(this ITableDisplay display)
    {
        _connection!.UpdateAsync(display).Wait();
        return display;
    }

    public static WalletTypeClass Update(this WalletTypeClass walletTypeClass)
    {
        _connection!.UpdateAsync(walletTypeClass).Wait();
        return walletTypeClass;
    }
}