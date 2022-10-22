using SQLite;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    #region Script

    #region TableBase

    private const string TColors = @"
    create table t_colors
    (
        id         INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT,
        nom        TEXT,
        value      TEXT
    );";

    private const string TImages = @"
    CREATE TABLE t_images
    (
        id INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT,
        name TEXT
    );";

    private const string TLieu = @"
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
        date_ajout TEXT default CURRENT_DATE,
        latitude   REAL,
        longitude  REAL
    );";

    private const string TTypeRecurence = @"
    create table t_type_recurence
    (
        id  INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        nom TEXT
    );";

    private const string TTypePayement = @"
    create table t_type_payement
    (
        id  INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        nom TEXT
    );";

    private const string TTypeCategorie = @"
    create table t_type_categorie
    (
        id  INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        nom TEXT
    );";

    private const string TTicket = @"
    create table t_ticket
    (
        id   INTEGER NOT NULL
            PRIMARY KEY AUTOINCREMENT ,
        path TEXT
    );";

    private const string TTypeCompte = @"
    create table t_type_compte
    (
        id  INTEGER
            constraint t_type_compte_pk
                primary key autoincrement,
        nom TEXT
    );
    ";

    #endregion

    #region Table Foreigner

    private const string TCompte = @"
    create table t_compte
    (
        id             integer
            constraint t_compte_pk
                primary key autoincrement,
        nom            text,
        type_compte_fk integer
            constraint t_compte_t_type_compte_id_fk
                references t_type_compte (id),
        color          interval
            constraint t_compte_t_colors_id_fk
                        references t_colors (id),
        image          text
    );
    ";

    private const string TCredit = @"
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

    private const string THistorique = @"
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
                references t_credit (id)
    );";

    #endregion

    #endregion

    #region Class

    #region Table

    [Table("t_historique")]
    public class THistoriqueClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("compte_fk")] public int WalletFk { get; set; }
        [Column("ordre")] public string Order { get; set; }
        [Column("type_categorie_fk")] public int? TypeCategorieFk { get; set; }
        [Column("type_payement_fk")] public int? TypePayementFk { get; set; }
        [Column("montant")] public decimal Montant { get; set; }
        [Column("date")] public string date { get; set; }
        [Column("lieu_fk")] public int? LieuFk { get; set; }
        [Column("ticket_fk")] public int? TicketFk { get; set; }
        [Column("credit_fk")] public int? CreditFk { get; set; }
    }

    [Table("t_compte")]
    public class TWalletClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("nom")] public string Name { get; set; }
        [Column("type_compte_fk")] public int TypeCompteFk { get; set; }
        [Column("color")] public int Color { get; set; }
        [Column("image")] public int? Image { get; set; }
    }

    [Table("t_colors")]
    public class TColorsClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("nom")] public string Nom { get; set; }
        [Column("value")] public string Value { get; set; }
    }

    [Table("t_images")]
    public class TImageClass
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("name")] public string Name { get; set; }
    }

    [Table("t_type_compte")]
    public class TWalletType
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("nom")] public string Name { get; set; }
    }

    #endregion

    #region Vue

    [Table("v_total_by_categorie")]
    public class VTotalByWaletClass
    {
        [Column("compte")] public string Name { get; set; }
        [Column("restant")] public float Remaining { get; set; }
    }

    public class VWalletClass
    {
        [Column("id")] public int Id { get; set; }
        [Column("nom")] public string Name { get; set; }
        [Column("type")] public string Type { get; set; }
        [Column("color_name")] public string ColorName { get; set; }
        [Column("color_value")] public string ColorValue { get; set; }
        [Column("image")] public string Image { get; set; }
    }

    #endregion

    #endregion
}