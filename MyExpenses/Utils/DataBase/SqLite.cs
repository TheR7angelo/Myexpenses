﻿#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    private static SQLiteAsyncConnection? _connection;
    
    public static Task<bool> Initialized(string name, string password)
    {
        var dbPath = Path.Combine(Root, "dbFiles", $"{name}.sqlite");

        try
        {
            if (!File.Exists(dbPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
                _connection = new SQLiteAsyncConnection(dbPath, false);
                _connection.ExecuteAsync("PRAGMA foreignkeys = ON");
            
                #region Tables

                foreach (var cmd in new List<string>
                             { TLieu, TTypeRecurence, TTypePayement, TTypeCategorie, TTicket, TTypeCompte, TCredit, TCompte, THistorique })
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
            
                return Task.FromResult(true);
            }

            _connection = new SQLiteAsyncConnection(dbPath, false);
            _connection.ExecuteAsync("PRAGMA foreignkeys = ON").Wait();
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }


    private static void Execute(string cmd) => _connection!.ExecuteAsync(cmd).Wait();
    // private static SQLiteDataReader ExecuteReader(string cmd) => new SQLiteCommand(cmd, _connection).ExecuteReader();

}