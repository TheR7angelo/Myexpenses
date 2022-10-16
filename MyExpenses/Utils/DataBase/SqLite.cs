#nullable enable
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    private static SQLiteAsyncConnection? _connection;
    
    public static Task Initialized(string name, string password)
    {
        var dbPath = Path.Combine(Root, "dbFiles", $"{name}.sqlite");

        if (!File.Exists(dbPath)) Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        _connection = new SQLiteAsyncConnection(dbPath, false);
        _connection.ExecuteAsync("PRAGMA foreignkeys = ON");

        _connection.CreateTablesAsync<Lieu, TypeRecurence, TypePayement, TypeCategorie, Ticket>().Wait();
        _connection.CreateTableAsync<Credit>().Wait();
        return Task.CompletedTask;
    }

    public static Task<int> InsertLieu(Lieu lieu) => _connection!.InsertAsync(lieu);


    // private static void Execute(string cmd) => new SQLiteCommand(cmd, _connection).ExecuteNonQuery();
    // private static SQLiteDataReader ExecuteReader(string cmd) => new SQLiteCommand(cmd, _connection).ExecuteReader();

}