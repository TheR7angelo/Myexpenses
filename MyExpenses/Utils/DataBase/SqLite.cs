﻿#nullable enable
using System.Collections.Generic;
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

        foreach (var cmd in new List<string>
                     { Lieu, TypeRecurence, TypePayement, TypeCategorie, Ticket, TypeCompte, Credit, Compte, Historique })
        {
           Execute(cmd);
        }

        return Task.CompletedTask;
    }


    private static void Execute(string cmd) => _connection!.ExecuteAsync(cmd).Wait();
    // private static SQLiteDataReader ExecuteReader(string cmd) => new SQLiteCommand(cmd, _connection).ExecuteReader();

}