namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
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