SELECT h.compte, round(sum(h.montant), 2) as total
FROM v_historique h
GROUP BY h.compte
ORDER BY h.compte;



SELECT h.compte, h.categorie, round(sum(h.montant), 2) as total
FROM v_historique h
GROUP BY h.compte, h.categorie
ORDER BY h.compte;

