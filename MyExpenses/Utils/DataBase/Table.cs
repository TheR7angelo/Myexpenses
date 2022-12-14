using System;
using MyExpenses.Utils.Function;
using SQLite;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{

    #region Script

    #region TableBase

    private const string Colors = @"
    create table t_colors
    (
        id         INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT,
        nom        TEXT,
        value      TEXT
    );";

    private const string Images = @"
    CREATE TABLE t_images
    (
        id INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT,
        name TEXT
    );";

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
        country_code TEXT,
        date_ajout TEXT default CURRENT_DATE,
        latitude   REAL,
        longitude  REAL
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
        nom TEXT,
        can_delete integer default 1
    );";

    private const string TypeCategorie = @"
    create table t_type_categorie
    (
        id  INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        nom TEXT,
        can_delete integer default 1
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
                primary key AUTOINCREMENT,
        nom TEXT,
        can_delete integer default 1
    );";

    #endregion

    #region Table Foreigner

    private const string Abonement = @"
    CREATE TABLE t_abonements(
        id INTEGER NOT NULL
            constraint t_abonements_pk
                PRIMARY KEY AUTOINCREMENT,
        lieu_fk           integer
            constraint t_credit_t_lieu_id_fk
                references t_lieu (id),
        raison            text,
        type_payement_fk     integer
            constraint t_credit_t_type_payement_id_fk
                references t_type_payement (id),
        recurence         integer,
        type_recurence1_fk integer
            constraint t_credit_t_type_recurence1_id_fk
                references t_type_recurence (id),
        montant           real,
        duree             int,
        type_recurence2_fk integer
            constraint t_credit_t_type_recurence2_id_fk
                references t_type_recurence (id)
    );";
    
    private const string Compte = @"
    create table t_compte
    (
        id             integer
            constraint t_compte_pk
                primary key autoincrement,
        nom            text,
        type_compte_fk integer
            constraint t_compte_t_type_compte_id_fk
                references t_type_compte (id),
        color_fk          int
            constraint t_compte_t_colors_id_fk
                        references t_colors (id),
        image_fk        int
            constraint t_compte_t_images_id_fk
                        references t_images(id),
        externe         int
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
        duree            integer,
        type_recurence_duree_fk integer
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
        type_categorie_fk   integer
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
                references t_credit (id),
        virement_fk     integer
            constraint t_historique_t_virement_id_fk
                references t_virement(id),
        abonements_fk   INTEGER
            constraint t_historique_t_abonements_id_fk
                references t_abonements(id)
    );";
    
    private const string Virement = @"
    create table t_virement
    (
        id  INTEGER
            constraint t_type_compte_pk
                primary key autoincrement,
        montant REAL,
        compte_emeteur integer
            constraint t_virement_t_compte_id_fk
                references t_compte(id),
        compte_benificiaire integer
            constraint t_virement_t_compte_id_fk
                references t_compte(id)
    );";

    #endregion

    #endregion

    #region Class

    #region Table
    
    [Table("t_abonements")]
    public class SubscriptionClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("lieu_fk")]
        public int LieuFk { get; set; }
        [Column("raison")]
        public string? Raison { get; set; }
        [Column("type_payement")]
        public int PaymentTypeFk { get; set; }
        [Column("recurence")]
        public int Recurence { get; set; }
        [Column("type_recurence1_fk")]
        public int RecurenceType1Fk { get; set; }
        [Column("montant")]
        public decimal Amount { get; set; }
        [Collation("duree")]
        public int Duration { get; set; }
        [Column("type_recurence2_fk")]
        public int RecurenceType2Fk { get; set; }
    }
    
    [Table("t_historique")]
    public class HistoriqueClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("compte_fk")] public int? WalletFk { get; set; }
        [Column("ordre")] public string Order { get; set; } = null!;
        [Column("type_categorie_fk")] public int? TypeCategorieFk { get; set; }
        [Column("type_payement_fk")] public int? TypePayementFk { get; set; }
        [Column("montant")] public decimal Montant { get; set; }
        [Column("date")] public DateTime Date { get; set; } = DateTime.UtcNow;
        [Column("lieu_fk")] public int? LieuFk { get; set; }
        [Column("ticket_fk")] public int? TicketFk { get; set; }
        [Column("credit_fk")] public int? CreditFk { get; set; }
        [Column("abonements_fk")] public int? AbonementsFk { get; set; }
    }

    [Table("t_compte")]
    public class WalletClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("nom")] public string Name { get; set; } = null!;
        [Column("type_compte_fk")] public int TypeCompteFk { get; set; }
        [Column("color_fk")] public int Color { get; set; }
        [Column("image_fk")] public int Image { get; set; }
        [Column("externe")] public bool External { get; set; } = false;
    }

    [Table("t_type_categorie")]
    public class CategoryClass : ITableDisplay
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string Name { get; set; } = null!;
        [Column("can_delete")]
        public bool CanDelete { get; set; } = true;
    }
    
    [Table("t_type_payement")]
    public class PaymentClass : ITableDisplay
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string Name { get; set; } = null!;
        [Column("can_delete")]
        public bool CanDelete { get; set; } = true;
    }

    [Table("t_colors")]
    public class ColorsClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("nom")] public string Nom { get; set; } = null!;
        [Column("value")] public string Value { get; set; } = null!;
    }

    [Table("t_images")]
    public class ImageClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("name")] public string Name { get; set; } = null!;
    }
    
    [Table("t_lieu")]
    public class LieuClass
    {
        [PrimaryKey, AutoIncrement, Collation("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string Name { get; set; } = null!;

        [Column("numeros")]
        public string? Nums { get; set; }
        [Column("rue")]
        public string Rue { get; set; } = null!;

        [Column("postal")]
        public string Postal { get; set; } = null!;

        [Column("ville")]
        public string City { get; set; } = null!;

        [Column("pays")]
        public string Pays { get; set; } = null!;

        [Column("country_code")]
        public string CountryCode { get; set; } = null!;

        [Column("date_ajout")]
        public DateTime DateAdd { get; set; } = DateTime.UtcNow;
        [Column("latitude")]
        public double? Latitude { get; set; }
        [Column("longitude")]
        public double? Longitude { get; set; }
    }

    [Table("t_type_compte")]
    public class WalletTypeClass : ITableDisplay
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("nom")]
        public string Name { get; set; } = null!;
        [Column("can_delete")]
        public bool CanDelete { get; set; } = true;
    }

    #endregion

    #region Vue

    [Table("v_total_by_categorie")]
    public class VTotalByWaletClass
    {
        [Column("compte")] public string Name { get; set; } = null!;
        [Column("restant")] public float Remaining { get; set; }
    }

    public class VWalletClass
    {
        [Column("id")] public int Id { get; set; }
        [Column("nom")] public string Name { get; set; } = null!;
        [Column("type")] public string Type { get; set; } = null!;
        [Column("color_name")] public string ColorName { get; set; } = null!;
        [Column("color_value")] public string ColorValue { get; set; } = null!;
        [Column("image")] public string Image { get; set; } = null!;
        [Column("externe")] public bool External { get; set; } = false;
    }

    #endregion

    #endregion
}