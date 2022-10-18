#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyExpenses.Views;
using SQLite;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    private static readonly string Db = Home.Db;

    private static SQLiteAsyncConnection? _connection;

    public static Task<bool> Initialized(string name, string password)
    {
        var dbPath = Path.Combine(Db, $"{name}.sqlite");

        _connection?.CloseAsync().Wait();

        try
        {
            if (!File.Exists(dbPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
                _connection = new SQLiteAsyncConnection(dbPath, false);
                _connection.ExecuteAsync("PRAGMA foreignkeys = ON");
            
                #region Tables

                foreach (var cmd in new List<string> { TColors, TLieu, TTypeRecurence, TTypePayement, TTypeCategorie, TTicket, TTypeCompte, TCredit, TCompte, THistorique })
                {
                    Execute(cmd);
                }

                #endregion


                #region View

                foreach (var cmd in new List<string> { VHistorique, VToto, VTotoCategorie })
                {
                    Execute(cmd);
                }

                #endregion

                // todo à tester
                FillColors();
            
                return Task.FromResult(false);
            }
            _connection = new SQLiteAsyncConnection(dbPath, false);
            _connection.ExecuteAsync("PRAGMA foreignkeys = ON").Wait();

            return Task.FromResult(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(true);
        }
    }


    private static void Execute(string cmd) => _connection!.ExecuteAsync(cmd).Wait();
    //private static SQlite ExecuteReader(string cmd) => new SQLiteCommand(cmd, _connection).ExecuteReader();

}