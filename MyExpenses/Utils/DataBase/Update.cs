namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static CategoryClass Update(this CategoryClass categoryClass)
    {
        _connection!.UpdateAsync(categoryClass).Wait();
        return categoryClass;
    }
    
    public static LieuClass Update(this LieuClass lieuClass)
    {
        _connection!.UpdateAsync(lieuClass).Wait();
        return lieuClass;
    }
}