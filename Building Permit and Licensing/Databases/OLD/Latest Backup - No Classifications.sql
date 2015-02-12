-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.1.73-community


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema dbbpfau
--

CREATE DATABASE IF NOT EXISTS dbbpfau;
USE dbbpfau;

--
-- Definition of table `tblassessmentapplicant`
--

DROP TABLE IF EXISTS `tblassessmentapplicant`;
CREATE TABLE `tblassessmentapplicant` (
  `ACN` varchar(45) NOT NULL,
  `Date` varchar(45) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `Project` varchar(300) NOT NULL,
  `Lot` varchar(45) DEFAULT NULL,
  `Block` varchar(45) DEFAULT NULL,
  `Phase` varchar(45) DEFAULT NULL,
  `SubdivisionCode` varchar(45) DEFAULT '0',
  `OtherInfo` varchar(300) DEFAULT NULL,
  `Zone` varchar(45) DEFAULT NULL,
  `BarangayCode` varchar(45) NOT NULL,
  `Remarks` longtext,
  `PermitPre` varchar(45) DEFAULT NULL,
  `PermitSub` varchar(45) DEFAULT NULL,
  `ORNumber` varchar(45) DEFAULT NULL,
  `Encoder` varchar(45) NOT NULL,
  `Additional` varchar(45) DEFAULT 'NA',
  PRIMARY KEY (`ACN`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblassessmentapplicant`
--

/*!40000 ALTER TABLE `tblassessmentapplicant` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblassessmentapplicant` ENABLE KEYS */;


--
-- Definition of table `tblassessmentmerge`
--

DROP TABLE IF EXISTS `tblassessmentmerge`;
CREATE TABLE `tblassessmentmerge` (
  `ACN` varchar(45) NOT NULL,
  `BuildConst` double DEFAULT '0',
  `ElecInst` double DEFAULT '0',
  `MechIns` double DEFAULT '0',
  `PlumbIns` double DEFAULT '0',
  `ElectroIns` double DEFAULT '0',
  `BuildAcc` double DEFAULT '0',
  `OtherAcc` double DEFAULT '0',
  `BuildOcc` double DEFAULT '0',
  `BuildInsp` double DEFAULT '0',
  `CertFee` double DEFAULT '0',
  `Fines` double DEFAULT '0',
  `TOTALAssess` double DEFAULT '0',
  `Local` double DEFAULT '0',
  `Natl` double DEFAULT '0',
  `OBO` double DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblassessmentmerge`
--

/*!40000 ALTER TABLE `tblassessmentmerge` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblassessmentmerge` ENABLE KEYS */;


--
-- Definition of table `tblassessmentsummary`
--

DROP TABLE IF EXISTS `tblassessmentsummary`;
CREATE TABLE `tblassessmentsummary` (
  `ACN` varchar(45) NOT NULL,
  `BuildConst` double DEFAULT '0',
  `ElecInst` double DEFAULT '0',
  `MechIns` double DEFAULT '0',
  `PlumbIns` double DEFAULT '0',
  `ElectroIns` double DEFAULT '0',
  `BuildAcc` double DEFAULT '0',
  `OtherAcc` double DEFAULT '0',
  `BuildOcc` double DEFAULT '0',
  `BuildInsp` double DEFAULT '0',
  `CertFee` double DEFAULT '0',
  `Fines` double DEFAULT '0',
  `TOTALAssess` double DEFAULT '0',
  `Local` double DEFAULT '0',
  `Natl` double DEFAULT '0',
  `OBO` double DEFAULT '0',
  PRIMARY KEY (`ACN`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblassessmentsummary`
--

/*!40000 ALTER TABLE `tblassessmentsummary` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblassessmentsummary` ENABLE KEYS */;


--
-- Definition of table `tblbarangays`
--

DROP TABLE IF EXISTS `tblbarangays`;
CREATE TABLE `tblbarangays` (
  `brgyID` varchar(45) NOT NULL,
  `brgyName` varchar(100) NOT NULL,
  PRIMARY KEY (`brgyID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblbarangays`
--

/*!40000 ALTER TABLE `tblbarangays` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblbarangays` ENABLE KEYS */;


--
-- Definition of table `tblclassifications`
--

DROP TABLE IF EXISTS `tblclassifications`;
CREATE TABLE `tblclassifications` (
  `ClassID` varchar(3) NOT NULL,
  `Division` varchar(45) NOT NULL,
  `GenClass` longtext NOT NULL,
  `OccupancyChars` longtext,
  `Principal` longtext NOT NULL,
  PRIMARY KEY (`ClassID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblclassifications`
--

/*!40000 ALTER TABLE `tblclassifications` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblclassifications` ENABLE KEYS */;


--
-- Definition of table `tbloccupancy`
--

DROP TABLE IF EXISTS `tbloccupancy`;
CREATE TABLE `tbloccupancy` (
  `OCN` varchar(45) NOT NULL,
  `Date` varchar(45) NOT NULL,
  `Lastname` varchar(45) NOT NULL,
  `FirstName` varchar(45) NOT NULL,
  `MiddleName` varchar(45) DEFAULT NULL,
  `OwnerAddress` varchar(800) NOT NULL,
  `Lot` varchar(45) DEFAULT NULL,
  `Block` varchar(45) DEFAULT NULL,
  `Phase` varchar(45) DEFAULT NULL,
  `SubdivisionCode` varchar(45) DEFAULT NULL,
  `OtherInfo` varchar(100) DEFAULT NULL,
  `Zone` varchar(45) DEFAULT NULL,
  `Barangay` varchar(45) NOT NULL,
  `OccupancyCode` varchar(45) NOT NULL,
  `StartDatePro` varchar(45) NOT NULL,
  `StartDateAct` varchar(45) NOT NULL,
  `CompleteDatePro` varchar(45) NOT NULL,
  `CompleteDateAct` varchar(45) NOT NULL,
  `TotalAreaEst` varchar(45) NOT NULL,
  `TotalAreaAct` varchar(45) NOT NULL,
  `Storeys` int(10) unsigned NOT NULL,
  `EstCost` double NOT NULL,
  PRIMARY KEY (`OCN`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbloccupancy`
--

/*!40000 ALTER TABLE `tbloccupancy` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbloccupancy` ENABLE KEYS */;


--
-- Definition of table `tblsubdivisions`
--

DROP TABLE IF EXISTS `tblsubdivisions`;
CREATE TABLE `tblsubdivisions` (
  `subdID` varchar(45) NOT NULL,
  `subdName` varchar(100) NOT NULL,
  PRIMARY KEY (`subdID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblsubdivisions`
--

/*!40000 ALTER TABLE `tblsubdivisions` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblsubdivisions` ENABLE KEYS */;


--
-- Definition of table `tblusers`
--

DROP TABLE IF EXISTS `tblusers`;
CREATE TABLE `tblusers` (
  `UserID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserType` int(10) unsigned NOT NULL,
  `UserDescription` varchar(45) NOT NULL,
  `Username` varchar(45) NOT NULL,
  `FirstName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  `MiddleName` varchar(45) DEFAULT NULL,
  `UserPwd` varchar(45) NOT NULL,
  `UserImage` longtext,
  PRIMARY KEY (`UserID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblusers`
--

/*!40000 ALTER TABLE `tblusers` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblusers` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
