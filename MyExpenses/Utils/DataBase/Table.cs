using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    #region TableBase

    [Table("t_lieu")]
    public class Lieu
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id  { get; set; }
        [Column("nom")]
        public string Nom { get; set; }
        [Column("numeros")]
        public string Numeros { get; set; }
        [Column("rue")]
        public string Rue { get; set; }
        [Column("postal")]
        public string Postal { get; set; }
        [Column("ville")]
        public string Ville { get; set; }
        [Column("pays")]
        public string Pays { get; set; }
        [Column("date_ajout")]
        public DateTime  DateAjout { get; set; } = DateTime.Now;
        [Column("latitude")]
        public int Latitude { get; set; }
        [Column("longitude")]
        public int Longitude { get; set; }

    }

    [Table("t_type_recurence")]
    public class TypeRecurence
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id  { get; set; }
        [Column("nom")]
        public string Nom { get; set; }
    }
    
    [Table("t_type_payement")]
    public class TypePayement
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id  { get; set; }
        [Column("nom")]
        public string Nom { get; set; }
    }
    
    [Table("t_type_categorie")]
    public class TypeCategorie
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id  { get; set; }
        public string nom { get; set; }
    }
    
    [Table("t_ticket")]
    public class Ticket
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id  { get; set; }
        [Column("path")]
        public string Path { get; set; }
    }

    #endregion
    
    [Table("t_credit")]
    public class Credit
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [ForeignKey(typeof(Lieu)), Column("lieu_fk")]
        // [ManyToOne(typeof(Lieu)), Column("lieu_fk")]
        public int LieuFk { get; set; }
    }
}