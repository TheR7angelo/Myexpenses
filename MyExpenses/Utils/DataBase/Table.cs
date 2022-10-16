using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    #region TableBase

    private const string Lieu = @"
    create table t_lieu
    (
        id         INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT,
        nom        TEXT,
        numeros    TEXT,
        rue        TEXT,
        postal     TEXT,
        ville      TEXT,
        pays       TEXT,
        date_ajout TEXT,
        latitude   INTEGER,
        longitude  INTEGER
    );";

    private const string TypeRecurence = @"
    create table t_type_recurence
    (
        id  INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        nom TEXT
    );";

    private const string TypePayement = @"
    create table t_type_payement
    (
        id  INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        nom TEXT
    );";

    private const string TypeCategorie = @"
    create table t_type_categorie
    (
        id  INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        nom TEXT
    );";

    private const string Ticket = @"
    create table t_ticket
    (
        id   INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        path TEXT
    );";

    private const string TypeCompte = @"
    create table t_type_compte
    (
        id  INTEGER
            constraint t_type_compte_pk
                primary key autoincrement,
        nom TEXT
    );
    ";

    #endregion

    private const string Compte = @"
    create table t_compte
    (
        id             integer
            constraint t_compte_pk
                primary key autoincrement,
        nom            text,
        type_compte_fk integer
            constraint t_compte_t_type_compte_id_fk
                references t_type_compte (id)
    );
    ";
    
    private const string Credit = @"
    create table t_credit
    (
        id                integer
            constraint t_credit_pk
                primary key autoincrement,
        lieu_fk           integer
            constraint t_credit_t_lieu_id_fk
                references t_lieu (id),
        raison            text,
        type_payement     integer
            constraint t_credit_t_type_payement_id_fk
                references t_type_payement (id),
        recurence         integer,
        type_recurence_fk integer
            constraint t_credit_t_type_recurence_id_fk
                references t_type_recurence (id),
        montant           real
    );
    ";

    private const string Historique = @"
    create table t_historique
    (
        id               integer
            constraint t_historique_pk
                primary key autoincrement,
        compte_fk        integer
            constraint t_historique_t_compte_id_fk
                references t_compte (id),
        ordre            text,
        type_categorie   integer
            constraint t_historique_t_type_categorie_id_fk
                references t_type_categorie (id),
        type_payement_fk integer
            constraint t_historique_t_type_payement_null_fk
                references t_type_payement (id),
        montant          real,
        date             text default current_date,
        lieu_fk          integer
            constraint t_historique_t_lieu_id_fk
                references t_lieu (id),
        ticket_fk        integer
            constraint t_historique_t_ticket_id_fk
                references t_ticket (id),
        credit_fk        integer
            constraint t_historique_t_credit_id_fk
                references t_credit (id)
    );
    ";
}