namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static LieuClass UpdateLieuClass(this LieuClass lieuClass)
    {
        _connection!.UpdateAsync(lieuClass).Wait();
        return lieuClass;
    }
}