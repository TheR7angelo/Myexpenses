SELECT h.compte, round(sum(h.montant), 2) as total
FROM v_historique h
GROUP BY h.compte
ORDER BY h.compte;



SELECT h.compte, h.categorie, round(sum(h.montant), 2) as total
FROM v_historique h
GROUP BY h.compte, h.categorie
ORDER BY h.compte;

SELECT c.id, c.nom, ttc.nom as type, tc.nom as color, c.image
FROM t_compte c
LEFT JOIN t_type_compte ttc
    ON c.type_compte_fk = ttc.id
LEFT JOIN t_colors tc
    ON c.color = tc.id