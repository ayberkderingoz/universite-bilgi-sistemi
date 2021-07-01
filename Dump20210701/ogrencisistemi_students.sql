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
-- Table structure for table `students`
--

DROP TABLE IF EXISTS `students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `students` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_no` varchar(45) NOT NULL,
  `student_password` blob NOT NULL,
  `student_name` varchar(64) NOT NULL,
  `student_email` varchar(64) NOT NULL,
  `student_tckn` varchar(45) NOT NULL,
  `student_phoneno` varchar(45) NOT NULL,
  `student_status` bit(1) NOT NULL DEFAULT b'1',
  `student_lastchange` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students`
--

LOCK TABLES `students` WRITE;
/*!40000 ALTER TABLE `students` DISABLE KEYS */;
INSERT INTO `students` VALUES (2,'test',_binary '\”\‡p\”*Ü¶¬´˚ˆ¯âRéV£Ÿùå','test test','testemail@gmail.com','80766035768','5111111111',_binary '',NULL),(3,'TestAccount',_binary '\”\‡p\”*Ü¶¬´˚ˆ¯âRéV£Ÿùå','Test Account','testemail2@gmail.com','51685852460','5111111122',_binary '',NULL),(4,'TestAccount2',_binary '\”\‡p\”*Ü¶¬´˚ˆ¯âRéV£Ÿùå','Test Test2','testemail3@gmail.com','76318919228','5111111123',_binary '',NULL),(5,'TestAccount4',_binary '\”\‡p\”*Ü¶¬´˚ˆ¯âRéV£Ÿùå','Test Account4','testemail4@gmail.com','99717536872','5111111124',_binary '',NULL),(6,'Test5',_binary '\”\‡p\”*Ü¶¬´˚ˆ¯âRéV£Ÿùå','Test5 Test5','test5@gmail.com','29124466172','5111111125',_binary '',NULL),(7,'Test6',_binary '\”\‡p\”*Ü¶¬´˚ˆ¯âRéV£Ÿùå','tes6 test6','test6@gmail.com','34502817460','5111111126',_binary '\0',NULL);
/*!40000 ALTER TABLE `students` ENABLE KEYS */;
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
