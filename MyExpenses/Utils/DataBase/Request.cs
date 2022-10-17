using System.Collections.Generic;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    public static List<TCompteClass> GetAllAccount()
    {
        const string cmd = "SELECT * FROM t_compte";
        return _connection!.QueryAsync<TCompteClass>(cmd).Result;
    }
}