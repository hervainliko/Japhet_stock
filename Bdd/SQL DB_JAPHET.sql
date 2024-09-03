create database db_japhet;
use db_japhet;
create table customes(
	Codeclient int auto_increment  primary key,
    noms varchar(255)  not null,
    genre varchar(50) not null,
    Lieu_Naiss varchar(255)  null,
    DateNaisse varchar(255) null,
    piece varchar(255) null,
    Numpiece varchar(255) null,
    adresse varchar(255) not null,
	fonction varchar(255) not null,
	telephone varchar(20) null
);
create table usedb(
	compteUser int auto_increment primary key,
	NomUser varchar(255) not null,
    UserPass varchar(20) not null
);
create table dateexercice(
	IDdate int auto_increment primary key,
	DateExec date
);
create table roles(
	idRole int auto_increment primary key,
	NameRole varchar(255) not null
);
create table role_attribute(
	idAttrib int auto_increment primary key,
    dateRole date,
    compteUser int, 
    idRole int,
    foreign key (compteUser )references usedb(compteUser) on delete cascade on update cascade,
    foreign key (idRole )references roles(idRole) on delete cascade on update cascade
);
create table caisse(
	compteCaisse int auto_increment primary key,
    intituleCaisse varchar(255) not null,
    Montantcaisse float
);

create table ApprovCaisse(
	IDApprov int auto_increment  primary key,
    libelle varchar(255) not null,
    ApproVEntree float null, 
    ApprovSortie float null,
	compteUser int, 
    compteCaisse int,
	IDdate int,
    foreign key (compteUser )references usedb(compteUser) on delete cascade on update cascade,
    foreign key(IDdate) references dateexercice(IDdate) on delete cascade on update cascade,
     foreign key(compteCaisse) references caisse(compteCaisse) on delete cascade on update cascade
);

alter table approvcaisse add constraint fk_user foreign key (compteUser )references usedb(compteUser) on delete cascade on update cascade;
alter table approvcaisse add constraint fk_date foreign key(IDdate) references dateexercice(IDdate) on delete cascade on update cascade;
alter table approvcaisse add constraint foreign key(compteCaisse) references caisse(compteCaisse) on delete cascade on update cascade;

create table Toperations(
	IDoperation int auto_increment  primary key,
    Codeclient int,
    MontantDepot float null,
    MontantRetrait float null,
    IDdate int,
    libelle varchar(255) not null,
    compteUser int, 
    foreign key(Codeclient) references customes(Codeclient) on delete cascade on update cascade,
	 foreign key (compteUser )references usedb(compteUser) on delete cascade on update cascade,
	foreign key(IDdate) references dateexercice(IDdate) on delete cascade on update cascade
);

/* a un user on approvisionne 1 ou plusieurs fois pendant la journee, chaque user doit faire sa cloture journaliere a propos de l'approvisionnement qu'il a recu */

/* pesage de la matiere premiere. un client effectue 1 ou plusieurs operation de pese, un user valide un ou plusieurs operation de pese pendant la journee */
 create table pesage(
 NumPese  int auto_increment primary key not null,
 Codeclient int, foreign key(Codeclient) references customes(Codeclient) on delete cascade on update cascade,
 compteUser int, foreign key (compteUser )references usedb(compteUser) on delete cascade on update cascade,
IDdate int,foreign key(IDdate) references dateexercice(IDdate) on delete cascade on update cascade
 );
 create table DescPese (
		IdDesc int auto_increment primary key not null,
        numerateur float not null,
		indice float not null,
		pourcentage float not null,
		PU  float not null, 
        poids float,
        NumPese  int, foreign key(NumPese) references pesage(NumPese) on delete cascade on update cascade
);
/* plan epargne du client */
  create table TPlanEpargne(
 IDEPARG int auto_increment primary key not null,
 libelle varchar(255),
 montentdepot float null,
 Montretrait float null,
 Codeclient int, foreign key(Codeclient) references customes(Codeclient) on delete cascade on update cascade,
 compteUser int, foreign key (compteUser )references usedb(compteUser) on delete cascade on update cascade,
 IDdate int,foreign key(IDdate) references dateexercice(IDdate) on delete cascade on update cascade
 );
/* requette pour le releve du cleint ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
create view ReleveClient as (
 select customes.Codeclient,customes.noms,customes.genre,customes.telephone,customes.adresse,
 customes.fonction,dateexercice.IDdate,date_format(DateExec,'%d-%m-%Y') as DateExec, toperations.IDoperation,toperations.libelle,toperations.MontantDepot,toperations.MontantRetrait,(MontantDepot)-(MontantRetrait)  as SOLDE
 FROM customes,toperations,dateexercice
 where customes.Codeclient=toperations.Codeclient  and
		toperations.IDdate=dateexercice.IDdate
 order by toperations.IDoperation ASC);
 
 select customes.Codeclient,customes.noms,customes.genre,customes.telephone,customes.adresse,
 customes.fonction,dateexercice.IDdate,date_format(DateExec,'%d-%m-%Y') as DateExec, toperations.IDoperation,toperations.libelle,toperations.MontantDepot,toperations.MontantRetrait,
 sum(coalesce(MontantDepot,0))as MontantDepot,sum(coalesce(MontantRetrait,0)) as MontantRetrait ,(MontantDepot)-(MontantRetrait)  as SOLDE
 FROM customes,toperations,dateexercice
 where customes.Codeclient=toperations.Codeclient  and
		toperations.IDdate=dateexercice.IDdate
 order by toperations.IDoperation ASC;
/* approvisionnement caissier--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
create view sortieCaisse AS (
select usedb.compteUser,usedb.NomUser, role_attribute.idAttrib,role_attribute.idRole,roles.NameRole,approvcaisse.IDApprov,approvcaisse.ApproVEntree,dateexercice.IDdate,dateexercice.DateExec,approvcaisse.libelle 
from usedb,role_attribute,roles,approvcaisse,dateexercice 
where usedb.compteUser=approvcaisse.compteUser and 
		usedb.compteUser=role_attribute.compteUser and 
        roles.idRole=role_attribute.idRole and 
        dateexercice.IDdate=approvcaisse.IDdate order by dateexercice.IDdate DESC;);
        
/*delestage caisse -------- --------------------------------------------------------------------------------------------------------------------------------------------------------------------*/       
	create view entreeCaisse AS (
select usedb.compteUser,usedb.NomUser, role_attribute.idAttrib,role_attribute.idRole,roles.NameRole,approvcaisse.IDApprov,approvcaisse.ApprovSortie,dateexercice.IDdate,dateexercice.DateExec,approvcaisse.libelle 
from usedb,role_attribute,roles,approvcaisse,dateexercice 
where usedb.compteUser=approvcaisse.compteUser and 
		usedb.compteUser=role_attribute.compteUser and 
        roles.idRole=role_attribute.idRole and 
        dateexercice.IDdate=approvcaisse.IDdate order by dateexercice.IDdate DESC);

/* RELEVE DES EPARGNES----------------------------------------------------------------------------------------------------------------------------------------------------- */
create view ReleveEpargne as(				
 select customes.Codeclient,customes.noms,customes.genre,customes.telephone,customes.adresse,
 customes.fonction,tplanepargne.IDEPARG,tplanepargne.libelle,tplanepargne.montentdepot,tplanepargne.Montretrait,
 dateexercice.IDdate,date_format(DateExec,'%d-%m-%Y') as DateExec,(montentdepot)-(Montretrait) AS SOLDE FROM customes,tplanepargne,dateexercice
WHERE customes.Codeclient=tplanepargne.Codeclient AND
	   dateexercice.IDdate=tplanepargne.IDdate ORDER BY IDdate ASC);
/*Recu pesage---------------------------------------------------------------------------------------------------------------------------*/ 
create view RecuPese as (      
/* affichage du solde */
select customes.Codeclient,customes.noms,dateexercice.IDdate,dateexercice.DateExec,descpese.IdDesc,descpese.numerateur,descpese.indice,
descpese.poids,descpese.pourcentage,descpese.PU,pesage.NumPese,(PU*poids) as PT FROM customes,dateexercice,pesage,descpese
 where pesage.Codeclient=customes.Codeclient and
		dateexercice.IDdate=pesage.IDdate and
        descpese.NumPese=pesage.NumPese
        order by IdDesc);
        
 select Codeclient,dateexercice.IDdate,dateexercice.DateExec,sum(MontantDepot) as DEPOT,sum(MontantRetrait) AS RETRAIT,sum((MontantDepot)-(MontantRetrait)) AS SOLDE 
 from toperations,dateexercice 
 where toperations.IDdate = dateexercice.IDdate 
 AND  Codeclient= '"+ compteclient+"' group by Codeclient;
 
SELECT Codeclient,IDoperation,DateExec,libelle,MontantDepot,MontantRetrait 
FROM toperations,dateexercice WHERE toperations.IDdate=dateexercice.IDdate order by IDoperation DESC;

select toperations.Codeclient,noms,genre,telephone,fonction,dateexercice.IDdate,dateexercice.DateExec,sum(MontantDepot) as DEPOT,sum(MontantRetrait) AS RETRAIT,sum((MontantDepot)-(MontantRetrait)) AS SOLDE 
 from toperations,dateexercice,customes 
 where toperations.IDdate = dateexercice.IDdate and
		customes.Codeclient=toperations.Codeclient
 group by Codeclient having  DateExec >='2024-04-19';
 
 select Codeclient,dateexercice.IDdate,dateexercice.DateExec, sum(montentdepot) as DEPOT,sum(Montretrait) as RETRAIT,SUM((montentdepot)-(Montretrait)) AS SOLDE FROM tplanepargne,dateexercice where tplanepargne.IDdate=dateexercice.IDdate and codeclient='2';
 select IDApprov,libelle,DateExec,ApproVEntree,ApprovSortie from approvcaisse,dateexercice where approvcaisse.IDdate=dateexercice.IDdate;
 
 
 
select  approvcaisse.compteCaisse,approvcaisse.IDdate,approvcaisse.compteUser,sum(ApproVEntree) as DISPONIBLE FROM approvcaisse,caisse,dateexercice,usedb WHERE caisse.compteCaisse=approvcaisse.compteCaisse AND dateexercice.IDdate=approvcaisse.IDdate AND usedb.compteUser=approvcaisse.compteUser;

create view Rjournalier as(
select customes.Codeclient,customes.noms,dateexercice.IDdate, date_format(e.DateExec,'%d-%m-%Y') as  DateExec ,MontantDepot,MontantRetrait
from customes,dateexercice,toperations r inner join dateexercice e on r.IDdate=e.IDdate 
where  r.Codeclient=customes.Codeclient and
		r.IDdate=dateexercice.IDdate);

create view Rlivre as(
select usedb.NomUser,customes.Codeclient,customes.noms,dateexercice.IDdate, date_format(DateExec,'%d-%m-%Y') as  DateExec,pesage.NumPese,
numerateur,indice,pourcentage,PU,poids,reference,PT,bourse,teneur
from customes,usedb,dateexercice,descpese,pesage
where pesage.Codeclient=customes.Codeclient and
	  pesage.compteUser=usedb.compteUser and
      pesage.NumPese=descpese.NumPese and
	  pesage.IDdate=dateexercice.IDdate);

select * from Rlivre where DateExec='20-05-2024';



--procedure du solde de caisse par date
call CalculerSoldeCaisse('2024-08-20');-- test de la procedure
DELIMITER $$
CREATE PROCEDURE CalculerSoldeCaisse(IN DateE DATE)
BEGIN
    -- Initialiser la variable @solde avec le montant initial de la table approvcaisse
    SET @solde := (SELECT sum(Montant) FROM approvcaisse, dateexercice where approvcaisse.IDdate=dateexercice.IDdate and dateexercice.DateExec=DateE);
    -- Requête principale
    SELECT 
        result.DateExec,
        result.client_nom,
        result.montant_entree,
        result.montant_sortie,
        @solde := @solde + result.montant_entree - result.montant_sortie AS solde
    FROM (
        -- Ajouter le montant initial comme première ligne
        SELECT
            '00-00-000' AS DateExec,  -- Date fictive pour le montant initial
            'Montant Initial' AS client_nom,
            0 AS montant_entree,
            0 AS montant_sortie,
            @solde AS solde

        UNION ALL

        -- Récupérer les montants d'entrée un par un
        SELECT
            date_format(d.DateExec,'%d-%m-%Y') AS DateExec,
            c.noms AS client_nom,
            IFNULL(o.MontantDepot, 0) AS montant_entree,
            0 AS montant_sortie,
            NULL AS solde  -- Utiliser NULL pour les lignes des opérations
        FROM
            dateexercice d
        LEFT JOIN
            toperations o ON d.IDdate = o.IDdate AND o.MontantDepot IS NOT NULL
        LEFT JOIN
            customes c ON o.Codeclient = c.Codeclient

        UNION ALL

        -- Récupérer les montants de sortie un par un
        SELECT
            date_format(d.DateExec,'%d-%m-%Y') AS DateExec,
            c.noms AS client_nom,
            0 AS montant_entree,
            IFNULL(o.MontantRetrait, 0) AS montant_sortie,
            NULL AS solde  -- Utiliser NULL pour les lignes des opérations
        FROM
            dateexercice d
        LEFT JOIN
            toperations o ON d.IDdate = o.IDdate AND o.MontantRetrait IS NOT NULL
        LEFT JOIN
            customes c ON o.Codeclient = c.Codeclient
    ) AS result
    -- Filtrer les lignes où montant_entree et montant_sortie sont tous les deux nuls
    WHERE (result.montant_entree <> 0 OR result.montant_sortie <> 0) 
    AND (result.DateExec = DATE_FORMAT(DateE, '%d-%m-%Y') OR result.DateExec = '00-00-000')
    ORDER BY
        result.DateExec;
END$$

DELIMITER ;


--procedure stocker pour le calcul du releve du client
call CalculerSoldeClient( '1','2024-04-20','2024-08-31');

DELIMITER //
CREATE PROCEDURE CalculerSoldeClient(
    IN p_Codeclient INT,
    IN p_DateDebut DATE,
    IN p_DateFin DATE
)
BEGIN
    -- Initialiser la variable pour le solde avec le solde initial, ou 0 si le solde initial est NULL
    SET @solde := IFNULL(
        (
            SELECT 
                SUM(IFNULL(MontantDepot, 0)) - SUM(IFNULL(MontantRetrait, 0))
            FROM 
                toperations t
            INNER JOIN 
                dateexercice d ON t.IDdate = d.IDdate
            WHERE 
                d.DateExec < p_DateDebut
            AND 
                t.Codeclient = p_Codeclient
        ), 0
    );

    -- Requête principale
    SELECT 
        Codeclient,
        noms,
        DateOperation,
        MontantDepot,
        MontantRetrait,
        @solde := @solde + IFNULL(MontantDepot, 0) - IFNULL(MontantRetrait, 0) AS Solde
    FROM (
        -- Récupérer le solde avant la date de début et l'initialiser
        SELECT 
            c.Codeclient,
            c.noms,
            'Solde initial' AS DateOperation,
            NULL AS MontantDepot,
            NULL AS MontantRetrait,
            @solde AS Solde  -- Afficher le solde initial
        FROM 
            customes c
        WHERE 
            c.Codeclient = p_Codeclient

        UNION ALL

        -- Récupérer les informations des clients et opérations après la date de début
        SELECT 
            c.Codeclient,
            c.noms,
            date_format(d.DateExec, '%d-%m-%Y') AS DateOperation,
            t.MontantDepot,
            t.MontantRetrait,
            NULL AS Solde  -- Le solde sera calculé à ce niveau
        FROM 
            customes c
        INNER JOIN 
            toperations t ON c.Codeclient = t.Codeclient
        INNER JOIN 
            dateexercice d ON t.IDdate = d.IDdate
        WHERE 
            d.DateExec >= p_DateDebut 
        AND 
            d.DateExec <= p_DateFin
        AND 
            c.Codeclient = p_Codeclient
    ) AS transactions
    ORDER BY 
        CASE 
            WHEN DateOperation = 'Solde initial' THEN '0000-00-00'
            ELSE STR_TO_DATE(DateOperation, '%d-%m-%Y')
        END ASC;

END //

DELIMITER ;

