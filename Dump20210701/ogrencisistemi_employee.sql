CREATE DATABASE  IF NOT EXISTS `ogrencisistemi` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ogrencisistemi`;
-- MySQL dump 10.13  Distrib 8.0.25, for Win64 (x86_64)
--
-- Host: localhost    Database: ogrencisistemi
-- ------------------------------------------------------
-- Server version	8.0.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `id` int NOT NULL AUTO_INCREMENT,
  `employee_name` varchar(64) NOT NULL,
  `employee_password` blob NOT NULL,
  `employee_email` varchar(64) NOT NULL,
  `employee_tckn` varchar(11) NOT NULL,
  `employee_phoneno` varchar(13) DEFAULT NULL,
  `emoployee_status` bit(1) NOT NULL DEFAULT b'1',
  `employee_lastchange` datetime DEFAULT NULL,
  `employee_position` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_position` (`employee_position`),
  CONSTRAINT `fk_position` FOREIGN KEY (`employee_position`) REFERENCES `position` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (1,'Mehmet Dikmen',_binary 'Teacher123.','mehmetdikmen@baskent.edu.tr','70344403140','5111111112',_binary '',NULL,1),(2,'Ayƒ±≈üƒ±ƒüƒ± Ba≈üak Sevdik √áallƒ±',_binary 'Teacher123.','ayisigicalli@baskent.edu.tr','80514944410','5111111113',_binary '',NULL,1),(3,'Nizami Gasilov',_binary 'Teacher123.','gasilovnizam97@yahoo.com','95608432636','5111111114',_binary '',NULL,1),(4,'Oƒüul G√∂√ßmen',_binary 'Teacher123.','ogulgocmen@baskent.edu.tr','48613954426','5111111115',_binary '',NULL,1),(5,'Ziya Akta≈ü',_binary 'Teacher123.','ziyaaktas@baskent.edu.tr','50267103532','5111111116',_binary '',NULL,1),(6,'Tofik Mamedov',_binary 'Teacher123.','mamedovtofik@yahoo.com','77461716504','5111111117',_binary '',NULL,1),(7,'Musa G√∂kdeniz √áolak',_binary 'Teacher123.','mgcolak@gmail.com','89640212912','5111111118',_binary '',NULL,1),(8,'Deniz Be≈üiroƒülu',_binary 'Teacher123.','denizbesiroglu@baskent.edu.tr','39151102912','5111111119',_binary '',NULL,1),(9,'ƒ∞rem Balƒ±cƒ±',_binary 'Teacher123.','irembalici@baskent.edu.tr','40663686470','5111111120',_binary '',NULL,1),(10,'Ayberk Dering√∂z',_binary 'Test123.','ayberkderingoz@gmail.com','49515912354','5075259837',_binary '',NULL,2),(11,'Test5 Test7',_binary '\”\‡p\”*Ü¶¬´˚ˆ¯âRéV£Ÿùå','test57@gmail.com','69638271428','5111111126',_binary '','2021-06-24 16:13:55',1),(13,'test6 test6',_binary '\”\‡p\”*Ü¶¬´˚ˆ¯âRéV£Ÿùå','test6@gmail.com','34502817460','5111111126',_binary '','2021-06-25 09:48:52',1);
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-07-01 11:52:57
