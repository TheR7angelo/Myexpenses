namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static LieuClass Delete(this LieuClass lieuClass)
    {
        _connection!.DeleteAsync(lieuClass).Wait();
        return lieuClass;
    }
}