using MyExpenses.Utils.Database;

namespace MyExpenses.Utils.Function;

public interface ITableDisplay
{
    int Id { get; set; }
    string Name { get; set; }

    public ITableDisplay Delete(ITableDisplay obj) => obj.Delete();

    public ITableDisplay Insert(ITableDisplay obj) => obj.Insert();

    public ITableDisplay Update(ITableDisplay obj) => obj.Update();
}