-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: db_japhet
-- ------------------------------------------------------
-- Server version	5.7.10-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `approvcaisse`
--

DROP TABLE IF EXISTS `approvcaisse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `approvcaisse` (
  `IDApprov` int(11) NOT NULL AUTO_INCREMENT,
  `Montant` double DEFAULT NULL,
  `compteUser` int(11) DEFAULT NULL,
  `compteCaisse` int(11) DEFAULT NULL,
  `IDdate` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDApprov`),
  KEY `fk_user` (`compteUser`),
  KEY `fk_date` (`IDdate`),
  KEY `compteCaisse` (`compteCaisse`),
  CONSTRAINT `approvcaisse_ibfk_1` FOREIGN KEY (`compteUser`) REFERENCES `usedb` (`compteUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `approvcaisse_ibfk_2` FOREIGN KEY (`IDdate`) REFERENCES `dateexercice` (`IDdate`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `approvcaisse_ibfk_3` FOREIGN KEY (`compteCaisse`) REFERENCES `caisse` (`compteCaisse`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `approvcaisse_ibfk_4` FOREIGN KEY (`compteCaisse`) REFERENCES `caisse` (`compteCaisse`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_date` FOREIGN KEY (`IDdate`) REFERENCES `dateexercice` (`IDdate`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_user` FOREIGN KEY (`compteUser`) REFERENCES `usedb` (`compteUser`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=83 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `approvcaisse`
--

LOCK TABLES `approvcaisse` WRITE;
/*!40000 ALTER TABLE `approvcaisse` DISABLE KEYS */;
INSERT INTO `approvcaisse` VALUES (81,1000,2,3,4),(82,200,2,3,4);
/*!40000 ALTER TABLE `approvcaisse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `caisse`
--

DROP TABLE IF EXISTS `caisse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `caisse` (
  `compteCaisse` int(11) NOT NULL AUTO_INCREMENT,
  `intituleCaisse` varchar(255) NOT NULL,
  `Montantcaisse` float DEFAULT NULL,
  PRIMARY KEY (`compteCaisse`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caisse`
--

LOCK TABLES `caisse` WRITE;
/*!40000 ALTER TABLE `caisse` DISABLE KEYS */;
INSERT INTO `caisse` VALUES (3,'CAISSE PRINICIPALE',200000);
/*!40000 ALTER TABLE `caisse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customes`
--

DROP TABLE IF EXISTS `customes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customes` (
  `Codeclient` int(11) NOT NULL AUTO_INCREMENT,
  `noms` varchar(255) NOT NULL,
  `genre` varchar(50) NOT NULL,
  `Lieu_Naiss` varchar(255) DEFAULT NULL,
  `DateNaisse` varchar(255) DEFAULT NULL,
  `piece` varchar(255) DEFAULT NULL,
  `Numpiece` varchar(255) DEFAULT NULL,
  `adresse` varchar(255) NOT NULL,
  `fonction` varchar(255) NOT NULL,
  `telephone` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`Codeclient`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customes`
--

LOCK TABLES `customes` WRITE;
/*!40000 ALTER TABLE `customes` DISABLE KEYS */;
INSERT INTO `customes` VALUES (1,'KATEMBO LIKO HERVAIN','M','BUTEMBO','2024-4-19','CE BB','330244','MONDO','EXPLOITANT','0993254536'),(5,'KAKULE MURANDYA ','M','KYONDO','1999-12-19 00:00:00','CE','333333','MONDO','ENSEIGNANT','9932524536'),(6,'muhesi','M','','2024-6-23','','','','','');
/*!40000 ALTER TABLE `customes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dateexercice`
--

DROP TABLE IF EXISTS `dateexercice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `dateexercice` (
  `IDdate` int(11) NOT NULL AUTO_INCREMENT,
  `DateExec` datetime DEFAULT NULL,
  PRIMARY KEY (`IDdate`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dateexercice`
--

LOCK TABLES `dateexercice` WRITE;
/*!40000 ALTER TABLE `dateexercice` DISABLE KEYS */;
INSERT INTO `dateexercice` VALUES (1,'2024-04-19 00:00:00'),(2,'2024-04-20 00:00:00'),(3,'2024-05-19 00:00:00'),(4,'2024-05-20 00:00:00');
/*!40000 ALTER TABLE `dateexercice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `descpese`
--

DROP TABLE IF EXISTS `descpese`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `descpese` (
  `IdDesc` int(11) NOT NULL AUTO_INCREMENT,
  `numerateur` float NOT NULL,
  `indice` float NOT NULL,
  `pourcentage` float NOT NULL,
  `PU` float NOT NULL,
  `poids` float DEFAULT NULL,
  `NumPese` int(11) DEFAULT NULL,
  `PT` double DEFAULT NULL,
  `reference` double DEFAULT NULL,
  `bourse` double DEFAULT NULL,
  `teneur` double DEFAULT NULL,
  PRIMARY KEY (`IdDesc`),
  KEY `NumPese` (`NumPese`),
  CONSTRAINT `descpese_ibfk_1` FOREIGN KEY (`NumPese`) REFERENCES `pesage` (`NumPese`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `descpese`
--

LOCK TABLES `descpese` WRITE;
/*!40000 ALTER TABLE `descpese` DISABLE KEYS */;
INSERT INTO `descpese` VALUES (33,34,45,56.78,67.89,2.91,1446968639,197.55,234,NULL,NULL),(35,65656,588,778,887,5628.94,779845315,4992869.77,65656,787,NULL),(36,65656,6565,656,65,5628.94,1738097049,365881.1,565,56,10);
/*!40000 ALTER TABLE `descpese` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mouvement_caisse`
--

DROP TABLE IF EXISTS `mouvement_caisse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mouvement_caisse` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `caisse` int(11) DEFAULT NULL,
  `debut` double DEFAULT NULL,
  `credit` double DEFAULT NULL,
  `dates` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mouvement_caisse`
--

LOCK TABLES `mouvement_caisse` WRITE;
/*!40000 ALTER TABLE `mouvement_caisse` DISABLE KEYS */;
INSERT INTO `mouvement_caisse` VALUES (31,3,0,200000,3),(32,3,0,30000,4),(33,3,0,70000,4);
/*!40000 ALTER TABLE `mouvement_caisse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pesage`
--

DROP TABLE IF EXISTS `pesage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pesage` (
  `NumPese` int(11) NOT NULL,
  `Codeclient` int(11) DEFAULT NULL,
  `compteUser` int(11) DEFAULT NULL,
  `IDdate` int(11) DEFAULT NULL,
  `total` double DEFAULT NULL,
  `mention` text,
  PRIMARY KEY (`NumPese`),
  KEY `Codeclient` (`Codeclient`),
  KEY `compteUser` (`compteUser`),
  KEY `IDdate` (`IDdate`),
  CONSTRAINT `pesage_ibfk_1` FOREIGN KEY (`Codeclient`) REFERENCES `customes` (`Codeclient`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `pesage_ibfk_2` FOREIGN KEY (`compteUser`) REFERENCES `usedb` (`compteUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `pesage_ibfk_3` FOREIGN KEY (`IDdate`) REFERENCES `dateexercice` (`IDdate`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pesage`
--

LOCK TABLES `pesage` WRITE;
/*!40000 ALTER TABLE `pesage` DISABLE KEYS */;
INSERT INTO `pesage` VALUES (779845315,1,1,4,4992870,'Non Payé'),(1446968639,1,1,4,197.55,'Non Payé'),(1472355802,1,1,4,4197916,'Non Payé'),(1738097049,1,1,4,365881.1,'Non Payé');
/*!40000 ALTER TABLE `pesage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `recu`
--

DROP TABLE IF EXISTS `recu`;
/*!50001 DROP VIEW IF EXISTS `recu`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `recu` AS SELECT 
 1 AS `NumPese`,
 1 AS `IDdate`,
 1 AS `Codeclient`,
 1 AS `noms`,
 1 AS `idDesc`,
 1 AS `numerateur`,
 1 AS `poids`,
 1 AS `pourcentage`,
 1 AS `PU`,
 1 AS `PT`,
 1 AS `total`,
 1 AS `DateExec`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `recupese`
--

DROP TABLE IF EXISTS `recupese`;
/*!50001 DROP VIEW IF EXISTS `recupese`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `recupese` AS SELECT 
 1 AS `Codeclient`,
 1 AS `noms`,
 1 AS `IDdate`,
 1 AS `DateExec`,
 1 AS `IdDesc`,
 1 AS `numerateur`,
 1 AS `indice`,
 1 AS `poids`,
 1 AS `pourcentage`,
 1 AS `PU`,
 1 AS `NumPese`,
 1 AS `PT`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `releveclient`
--

DROP TABLE IF EXISTS `releveclient`;
/*!50001 DROP VIEW IF EXISTS `releveclient`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `releveclient` AS SELECT 
 1 AS `Codeclient`,
 1 AS `noms`,
 1 AS `genre`,
 1 AS `telephone`,
 1 AS `adresse`,
 1 AS `fonction`,
 1 AS `IDdate`,
 1 AS `DateExec`,
 1 AS `IDoperation`,
 1 AS `libelle`,
 1 AS `MontantDepot`,
 1 AS `MontantRetrait`,
 1 AS `SOLDE`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `releveepargne`
--

DROP TABLE IF EXISTS `releveepargne`;
/*!50001 DROP VIEW IF EXISTS `releveepargne`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `releveepargne` AS SELECT 
 1 AS `Codeclient`,
 1 AS `noms`,
 1 AS `genre`,
 1 AS `telephone`,
 1 AS `adresse`,
 1 AS `fonction`,
 1 AS `IDEPARG`,
 1 AS `libelle`,
 1 AS `montentdepot`,
 1 AS `Montretrait`,
 1 AS `IDdate`,
 1 AS `DateExec`,
 1 AS `SOLDE`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `rjournalier`
--

DROP TABLE IF EXISTS `rjournalier`;
/*!50001 DROP VIEW IF EXISTS `rjournalier`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `rjournalier` AS SELECT 
 1 AS `Codeclient`,
 1 AS `noms`,
 1 AS `IDdate`,
 1 AS `DateExec`,
 1 AS `MontantDepot`,
 1 AS `MontantRetrait`,
 1 AS `Montant`,
 1 AS `solde`,
 1 AS `RESTANT`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `rlivre`
--

DROP TABLE IF EXISTS `rlivre`;
/*!50001 DROP VIEW IF EXISTS `rlivre`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `rlivre` AS SELECT 
 1 AS `NomUser`,
 1 AS `Codeclient`,
 1 AS `noms`,
 1 AS `IDdate`,
 1 AS `DateExec`,
 1 AS `NumPese`,
 1 AS `numerateur`,
 1 AS `indice`,
 1 AS `pourcentage`,
 1 AS `PU`,
 1 AS `poids`,
 1 AS `reference`,
 1 AS `PT`,
 1 AS `bourse`,
 1 AS `teneur`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `role_attribute`
--

DROP TABLE IF EXISTS `role_attribute`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role_attribute` (
  `idAttrib` int(11) NOT NULL AUTO_INCREMENT,
  `dateRole` int(11) DEFAULT NULL,
  `compteUser` int(11) DEFAULT NULL,
  `idRole` int(11) DEFAULT NULL,
  PRIMARY KEY (`idAttrib`),
  KEY `compteUser` (`compteUser`),
  KEY `idRole` (`idRole`),
  CONSTRAINT `role_attribute_ibfk_1` FOREIGN KEY (`compteUser`) REFERENCES `usedb` (`compteUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `role_attribute_ibfk_2` FOREIGN KEY (`idRole`) REFERENCES `roles` (`idRole`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_attribute`
--

LOCK TABLES `role_attribute` WRITE;
/*!40000 ALTER TABLE `role_attribute` DISABLE KEYS */;
INSERT INTO `role_attribute` VALUES (2,1,1,2),(3,3,2,1);
/*!40000 ALTER TABLE `role_attribute` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `roles` (
  `idRole` int(11) NOT NULL AUTO_INCREMENT,
  `NameRole` varchar(255) NOT NULL,
  PRIMARY KEY (`idRole`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'caisse'),(2,'pesage');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `toperations`
--

DROP TABLE IF EXISTS `toperations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `toperations` (
  `IDoperation` int(11) NOT NULL AUTO_INCREMENT,
  `Codeclient` int(11) DEFAULT NULL,
  `MontantDepot` float DEFAULT NULL,
  `MontantRetrait` float DEFAULT NULL,
  `IDdate` int(11) DEFAULT NULL,
  `libelle` varchar(255) NOT NULL,
  `compteUser` int(11) DEFAULT NULL,
  `IDApprov` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDoperation`),
  KEY `Codeclient` (`Codeclient`),
  KEY `compteUser` (`compteUser`),
  KEY `IDdate` (`IDdate`),
  KEY `FK_APP` (`IDApprov`),
  CONSTRAINT `FK_APP` FOREIGN KEY (`IDApprov`) REFERENCES `approvcaisse` (`IDApprov`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `toperations_ibfk_1` FOREIGN KEY (`Codeclient`) REFERENCES `customes` (`Codeclient`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `toperations_ibfk_2` FOREIGN KEY (`compteUser`) REFERENCES `usedb` (`compteUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `toperations_ibfk_3` FOREIGN KEY (`IDdate`) REFERENCES `dateexercice` (`IDdate`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `toperations`
--

LOCK TABLES `toperations` WRITE;
/*!40000 ALTER TABLE `toperations` DISABLE KEYS */;
INSERT INTO `toperations` VALUES (52,1,200,0,4,'',2,81),(53,1,0,400,4,'',2,81);
/*!40000 ALTER TABLE `toperations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tplanepargne`
--

DROP TABLE IF EXISTS `tplanepargne`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tplanepargne` (
  `IDEPARG` int(11) NOT NULL AUTO_INCREMENT,
  `IDdate` int(11) DEFAULT NULL,
  `libelle` varchar(255) DEFAULT NULL,
  `montentdepot` float DEFAULT NULL,
  `Montretrait` float DEFAULT NULL,
  `Codeclient` int(11) DEFAULT NULL,
  `compteUser` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDEPARG`),
  KEY `Codeclient` (`Codeclient`),
  KEY `compteUser` (`compteUser`),
  KEY `IDdate` (`IDdate`),
  CONSTRAINT `tplanepargne_ibfk_1` FOREIGN KEY (`Codeclient`) REFERENCES `customes` (`Codeclient`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `tplanepargne_ibfk_2` FOREIGN KEY (`compteUser`) REFERENCES `usedb` (`compteUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `tplanepargne_ibfk_3` FOREIGN KEY (`IDdate`) REFERENCES `dateexercice` (`IDdate`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tplanepargne`
--

LOCK TABLES `tplanepargne` WRITE;
/*!40000 ALTER TABLE `tplanepargne` DISABLE KEYS */;
/*!40000 ALTER TABLE `tplanepargne` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usedb`
--

DROP TABLE IF EXISTS `usedb`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usedb` (
  `compteUser` int(11) NOT NULL AUTO_INCREMENT,
  `NomUser` varchar(255) NOT NULL,
  `UserPass` varchar(20) NOT NULL,
  PRIMARY KEY (`compteUser`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usedb`
--

LOCK TABLES `usedb` WRITE;
/*!40000 ALTER TABLE `usedb` DISABLE KEYS */;
INSERT INTO `usedb` VALUES (1,'muhesi','1234'),(2,'herve','1234');
/*!40000 ALTER TABLE `usedb` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Final view structure for view `recu`
--

/*!50001 DROP VIEW IF EXISTS `recu`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `recu` AS (select `pesage`.`NumPese` AS `NumPese`,`dateexercice`.`IDdate` AS `IDdate`,`customes`.`Codeclient` AS `Codeclient`,`customes`.`noms` AS `noms`,`descpese`.`IdDesc` AS `idDesc`,`descpese`.`numerateur` AS `numerateur`,`descpese`.`poids` AS `poids`,`descpese`.`pourcentage` AS `pourcentage`,`descpese`.`PU` AS `PU`,`descpese`.`PT` AS `PT`,`pesage`.`total` AS `total`,`dateexercice`.`DateExec` AS `DateExec` from (((`descpese` join `pesage`) join `customes`) join `dateexercice`) where ((`descpese`.`NumPese` = `pesage`.`NumPese`) and (`customes`.`Codeclient` = `pesage`.`Codeclient`) and (`dateexercice`.`IDdate` = `pesage`.`IDdate`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `recupese`
--

/*!50001 DROP VIEW IF EXISTS `recupese`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `recupese` AS (select `customes`.`Codeclient` AS `Codeclient`,`customes`.`noms` AS `noms`,`dateexercice`.`IDdate` AS `IDdate`,`dateexercice`.`DateExec` AS `DateExec`,`descpese`.`IdDesc` AS `IdDesc`,`descpese`.`numerateur` AS `numerateur`,`descpese`.`indice` AS `indice`,`descpese`.`poids` AS `poids`,`descpese`.`pourcentage` AS `pourcentage`,`descpese`.`PU` AS `PU`,`pesage`.`NumPese` AS `NumPese`,(`descpese`.`PU` * `descpese`.`poids`) AS `PT` from (((`customes` join `dateexercice`) join `pesage`) join `descpese`) where ((`pesage`.`Codeclient` = `customes`.`Codeclient`) and (`dateexercice`.`IDdate` = `pesage`.`IDdate`) and (`descpese`.`NumPese` = `pesage`.`NumPese`)) order by `descpese`.`IdDesc`) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `releveclient`
--

/*!50001 DROP VIEW IF EXISTS `releveclient`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `releveclient` AS (select `customes`.`Codeclient` AS `Codeclient`,`customes`.`noms` AS `noms`,`customes`.`genre` AS `genre`,`customes`.`telephone` AS `telephone`,`customes`.`adresse` AS `adresse`,`customes`.`fonction` AS `fonction`,`dateexercice`.`IDdate` AS `IDdate`,date_format(`dateexercice`.`DateExec`,'%d-%m-%Y') AS `DateExec`,`toperations`.`IDoperation` AS `IDoperation`,`toperations`.`libelle` AS `libelle`,`toperations`.`MontantDepot` AS `MontantDepot`,`toperations`.`MontantRetrait` AS `MontantRetrait`,(`toperations`.`MontantDepot` - `toperations`.`MontantRetrait`) AS `SOLDE` from ((`customes` join `toperations`) join `dateexercice`) where ((`customes`.`Codeclient` = `toperations`.`Codeclient`) and (`toperations`.`IDdate` = `dateexercice`.`IDdate`)) order by `toperations`.`IDoperation`) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `releveepargne`
--

/*!50001 DROP VIEW IF EXISTS `releveepargne`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `releveepargne` AS (select `customes`.`Codeclient` AS `Codeclient`,`customes`.`noms` AS `noms`,`customes`.`genre` AS `genre`,`customes`.`telephone` AS `telephone`,`customes`.`adresse` AS `adresse`,`customes`.`fonction` AS `fonction`,`tplanepargne`.`IDEPARG` AS `IDEPARG`,`tplanepargne`.`libelle` AS `libelle`,`tplanepargne`.`montentdepot` AS `montentdepot`,`tplanepargne`.`Montretrait` AS `Montretrait`,`dateexercice`.`IDdate` AS `IDdate`,date_format(`dateexercice`.`DateExec`,'%d-%m-%Y') AS `DateExec`,(`tplanepargne`.`montentdepot` - `tplanepargne`.`Montretrait`) AS `SOLDE` from ((`customes` join `tplanepargne`) join `dateexercice`) where ((`customes`.`Codeclient` = `tplanepargne`.`Codeclient`) and (`dateexercice`.`IDdate` = `tplanepargne`.`IDdate`)) order by `tplanepargne`.`IDEPARG`) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `rjournalier`
--

/*!50001 DROP VIEW IF EXISTS `rjournalier`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `rjournalier` AS (select `customes`.`Codeclient` AS `Codeclient`,`customes`.`noms` AS `noms`,`dateexercice`.`IDdate` AS `IDdate`,date_format(`e`.`DateExec`,'%d-%m-%Y') AS `DateExec`,`r`.`MontantDepot` AS `MontantDepot`,`r`.`MontantRetrait` AS `MontantRetrait`,`approvcaisse`.`Montant` AS `Montant`,(`r`.`MontantDepot` - `r`.`MontantRetrait`) AS `solde`,((`approvcaisse`.`Montant` - `r`.`MontantRetrait`) + `r`.`MontantDepot`) AS `RESTANT` from (((`customes` join `dateexercice`) join `approvcaisse`) join (`toperations` `r` join `dateexercice` `e` on((`r`.`IDdate` = `e`.`IDdate`)))) where ((`r`.`Codeclient` = `customes`.`Codeclient`) and (`r`.`IDdate` = `dateexercice`.`IDdate`) and (`approvcaisse`.`IDdate` = `dateexercice`.`IDdate`) and (`r`.`IDApprov` = `approvcaisse`.`IDApprov`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `rlivre`
--

/*!50001 DROP VIEW IF EXISTS `rlivre`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `rlivre` AS (select `usedb`.`NomUser` AS `NomUser`,`customes`.`Codeclient` AS `Codeclient`,`customes`.`noms` AS `noms`,`dateexercice`.`IDdate` AS `IDdate`,date_format(`dateexercice`.`DateExec`,'%d-%m-%Y') AS `DateExec`,`pesage`.`NumPese` AS `NumPese`,`descpese`.`numerateur` AS `numerateur`,`descpese`.`indice` AS `indice`,`descpese`.`pourcentage` AS `pourcentage`,`descpese`.`PU` AS `PU`,`descpese`.`poids` AS `poids`,`descpese`.`reference` AS `reference`,`descpese`.`PT` AS `PT`,`descpese`.`bourse` AS `bourse`,`descpese`.`teneur` AS `teneur` from ((((`customes` join `usedb`) join `dateexercice`) join `descpese`) join `pesage`) where ((`pesage`.`Codeclient` = `customes`.`Codeclient`) and (`pesage`.`compteUser` = `usedb`.`compteUser`) and (`pesage`.`NumPese` = `descpese`.`NumPese`) and (`pesage`.`IDdate` = `dateexercice`.`IDdate`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-24 15:22:59
