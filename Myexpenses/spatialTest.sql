SELECT load_extension('C:/Users/ZP6177/Creative Cloud Files/Programmation/C#/Ineo Infracom/Myexpenses/Myexpenses/fgggffg/mod_spatialite.dll');
SELECT InitSpatialMetaData(1);

drop table t_lieu;
create table t_lieu
(
    id      integer
        constraint t_lieu_pk
            primary key autoincrement,
    nom     text,
    numeros text,
    rue     text,
    postal  integer,
    ville   text,
    pays    text,
    ajout   text default CURRENT_TIMESTAMP
);


SELECT AddGeometryColumn('t_lieu','geom' , 4326, 'POINT', 2);

INSERT INTO t_lieu(nom, numeros, rue, postal, ville, pays, geom)
VALUES ('test', '52', 'fgf', 33000, 'fffd', 'FRANCE', MakePoint(44.859869, -0.553392, 4326));

