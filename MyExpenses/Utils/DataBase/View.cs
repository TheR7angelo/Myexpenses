namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    private const string VSubscribe = @"
    CREATE VIEW v_abonements as
    SELECT sub.id,
       tl.nom   as lieu,
       sub.raison,
       ttp.nom  as payment_type,
       sub.recurence,
       ttr1.nom as recurence_type1,
       sub.montant,
       sub.duree,
       ttr2.nom as recurence_type2
    FROM t_abonements sub
       LEFT JOIN t_lieu tl
                 on sub.lieu_fk = tl.id
       LEFT JOIN t_type_payement ttp
                 on sub.type_payement_fk = ttp.id
       LEFT JOIN t_type_recurence ttr1
                 on sub.type_recurence1_fk = ttr1.id
       LEFT JOIN t_type_recurence ttr2
                 on sub.type_recurence2_fk = ttr2.id
    ";
    
    private const string VHistorique = @"
    CREATE VIEW v_historique as
    SELECT th.id,
           tc.nom as compte,
           th.ordre,
           ttc.nom as categorie,
           ttp.nom as payement,
           th.montant,
           th.date,
           tl.nom as lieu,
           tt.path,
           t.raison
    FROM t_historique th
             LEFT JOIN t_compte tc
                       on tc.id = th.compte_fk
             LEFT JOIN t_type_categorie ttc
                       on th.type_categorie_fk = ttc.id
             LEFT JOIN t_type_payement ttp
                       on th.type_payement_fk = ttp.id
             LEFT JOIN t_lieu tl
                       on th.lieu_fk = tl.id
             LEFT JOIN t_ticket tt
                       on th.ticket_fk = tt.id
             LEFT JOIN t_credit t
                       on tl.id = t.lieu_fk;
    ";


    private const string VToto = @"
    CREATE VIEW v_total as
    SELECT h.compte, round(sum(h.montant), 2) as restant
    FROM v_historique h
    GROUP BY h.compte;
    ";

    private const string VTotoCategorie = @"
    CREATE VIEW v_total_by_categorie as
    Select h.compte, h.categorie, round(sum(h.montant), 2) as montant
    FROM v_historique h
    GROUP BY h.compte, h.categorie;
    ";
}