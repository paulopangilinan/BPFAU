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
  `PermitDate` varchar(45) DEFAULT NULL,
  `Encoder` varchar(45) NOT NULL,
  `Additional` varchar(45) DEFAULT 'NA',
  PRIMARY KEY (`ACN`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblassessmentapplicant`
--

/*!40000 ALTER TABLE `tblassessmentapplicant` DISABLE KEYS */;
INSERT INTO `tblassessmentapplicant` (`ACN`,`Date`,`LastName`,`FirstName`,`MiddleName`,`Project`,`Lot`,`Block`,`Phase`,`SubdivisionCode`,`OtherInfo`,`Zone`,`BarangayCode`,`Remarks`,`PermitPre`,`PermitSub`,`PermitDate`,`Encoder`,`Additional`) VALUES 
 ('0000001','1/6/2015','AAA','AAA','AAA','AAA','1','2','3','01','4','5','01','sdada','213','3221','1/6/2015','001','NA'),
 ('0000002','1/6/2015','AAA','AAA','AAA','AAA','1','2','3','01','4','5','01','Additional Assessment for 0000001','213','3221','1/6/2015','001','0000001');
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
INSERT INTO `tblassessmentmerge` (`ACN`,`BuildConst`,`ElecInst`,`MechIns`,`PlumbIns`,`ElectroIns`,`BuildAcc`,`OtherAcc`,`BuildOcc`,`BuildInsp`,`CertFee`,`Fines`,`TOTALAssess`,`Local`,`Natl`,`OBO`) VALUES 
 ('0000001',50000,1000,200,300,0,0,500,0,0,0,0,52000,41600,2600,7800),
 ('0000001',3000,0,0,0,0,0,0,0,0,0,0,3000,2400,150,450);
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
INSERT INTO `tblassessmentsummary` (`ACN`,`BuildConst`,`ElecInst`,`MechIns`,`PlumbIns`,`ElectroIns`,`BuildAcc`,`OtherAcc`,`BuildOcc`,`BuildInsp`,`CertFee`,`Fines`,`TOTALAssess`,`Local`,`Natl`,`OBO`) VALUES 
 ('0000001',50000,1000,200,300,0,0,500,0,0,0,0,52000,41600,2600,7800),
 ('0000002',3000,0,0,0,0,0,0,0,0,0,0,3000,2400,150,450);
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
INSERT INTO `tblbarangays` (`brgyID`,`brgyName`) VALUES 
 ('01','BBB');
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
INSERT INTO `tblclassifications` (`ClassID`,`Division`,`GenClass`,`OccupancyChars`,`Principal`) VALUES 
 ('1','A-1','Residential (Dwellings)','Residential buildings/structure for exclusive use for single family occupants','1. Indigeneous family dwelling units\r\n2. Single-detached units\r\n3. School or company staff housing\r\n4. Single (nuclear) family dwellings\r\n5. Churches or similar places of worship\r\n6. Church rectories\r\n7. Community facilities and social centers\r\n8. Parks, playgrounds,pocket parks, parkways, promenades and\r\n     playlots\r\n9. Clubhouses and recreational uses such as golf courses, tennis\r\n     courts, basketball courts, swimming pools and  similar uses\r\n     operated by the government or private  individuals as\r\n     membership organizations for the benefit of their members,\r\n     families, and guests and not operated primarily for gain.\r\n  '),
 ('2','A-2','Residential (Dwellings)','Residential building for the occupants not exceeding 10 exclusive use of non-leasing  persons','1. Single-attached or duplex or town-houses, each privately\r\n     owned\r\n2. School dormitories (on campus)\r\n3. Convents and monasteries\r\n4. Military or police barracks/dormitories\r\n5. All uses allowed in division A-1 (or for R-1 class) buildings/\r\n     structures\r\n6. Pre-schools, elementary and high schools, provided that they do\r\n     not exceed sixteen (16) classrooms\r\n7. Outpatient clinics, family planning clinics, lying-in clinics,\r\n     diagnostic clinics, medical and clinical laboratories\r\n8. Branch library and museum\r\n9. Steam/ dry cleaning outlets\r\n10. Party needs and accessories (leasing of tables and chairs,\r\n       etc.)        '),
 ('3','B-1','Residential (Buildings/Structues, Hotels and Apartments)','','1. All uses permitted in Division A-1 and A-2 (or for R-1 class and\r\n     R-2 class) building/structures\r\n2. Leased single-detached dwelling unit, cottage with more than\r\n     one (1) independent unit and depluxes  \r\n3. Boarding and lodging houses  \r\n4. Multiple-housing units lease or still for sale\r\n5. Townhouses, each privately owned\r\n6. Boarding houses\r\n7. Accessorias (shop-houses), rowhouses, townhouses, tenements\r\n     and apartments.\r\n8. Multiple privately-owned condominium units or tenement houses\r\n     (residential building for the exclusive use of non-leasing\r\n     occupants not exceeding ten (10) persons and of low-rise type\r\n     (up to five (5) storeys maximum building height)\r\n9. Hotels, motels, inns, pension houses and apartels\r\n10. Private or off-campus dormitories\r\n11. Elementary schools and high school, provided that these will\r\n       not exceed twenty (20) classrooms\r\n12. Multi-Family residential buildings such as condominium, high-\n\n       rise residential buildings/structures, multi-level apartments,\n\n       tenements, mass housing, etc. taller than five (5) storeys but\n\n       not more than twelve (12) storeys'),
 ('4','C-1',' Education and Recreation ','','1. Educational institutions like schools, colleges, universities,\r\n     vocational, institutions, semminaries, convents, including school\r\n     auditoriums, gymnasia, reviewing stands, little theaters, concert\r\n     halls, opera houses.\r\n2. Seminar/workshop facilities\r\n3. Training centers/facilities\r\n4. Libraries, museums, exhibition halls and art galleries\r\n5. Civic centers, clubhouses, lodges, community ceters\r\n6. Churches, mosque, temples, shrines, chapels and similar places\r\n     of worship\r\n7. Civic or government centers\r\n8. Other types of government buildings '),
 ('5','C-2',' Education and Recreation ','','1. Amusement halls and parlors\r\n2. Massage and sauna parlors\r\n3. Health studios and reducing salons\r\n4. Billiard halls, pool rooms, bowling alleys and golf clubhouses\r\n5. Dancing schools, disco pads, dance and amusement  halls\r\n6. Gymnasia, pelota courts and sports complex'),
 ('6','D-1',' Institutional (Government and Health Services)','(Institutional,where personal liberties of inmates are restrained, or quarters of those rendering public assistance and maintaining peace and order) ','1. Mental hospitals, mental sanitaria and mental asylums\r\n2. Police and fire stations, guard houses\r\n3. Jails, prisons, reformatories and correctional institutions \r\n4. Rehabilitation centers\r\n5. Leprosaria and quarantine station  '),
 ('7','D-2',' Institutional (Government and Health Services)','(Institutional, building for health care)','1. Hospitals, sanitaria, and homes for the aged\r\n2. Nurseries for children of kindergarten age or non- ambulatory\r\n     patients accomodating more than five (5) persons'),
 ('8','D-3',' Institutional (Government and Health Services)','(Institutional, for ambulatory patients  or children kindergarten age)','1. Nursing homes for ambulatory patients\r\n2. school and home, for children over kindergarten age\r\n3. Ophanages  '),
 ('9','E-1','Business and Mercantile (Commercial)','(Business and Mercantile, where no work is done except change of parts and maintenance requiring no open flames, welding, or use of highly flammable liquids)','1. All uses allowed in division B-1 (or for R-3 class) buildings/\r\n     structures.\r\n2. Gasoline filling and service stations.\r\n3. Storage garage and boat storage.\r\n4. Commercial garages and parking buildings, display for cars,\r\n     tractors, etc.\r\n5. Bus and railways depots and terminals and offices\r\n6. Port facilities\r\n7. Airports and heliports facilities\r\n8. All other types of transportation complexes\r\n9. All other types of large complexes for public services\r\n10. Pawnshops, money shops, photo and portrait studios, shoe-\r\n       shine/repair stands, retail drugstores, tailoring and dress shops\r\n11. Bakeshops and bakery goods stores\r\n12. Stores for construction supplies and building materials such as\r\n       electrical and electronics,plumbing supplies, ceramic clay\r\n       cement and other similar products except CHBs, gravel and\r\n       sand and other concrete products.'),
 ('10','E-2','Business and Mercantile (Commercial)','(Business and Mercantile in nature)','1. Wholesale and retail stores\r\n2. Shopping centers, malls and supermarkets\r\n3. Wet and dry markets\r\n4. Restaurants, drinking and dining establishments with less than\r\n     one hundred (100) occupancies\r\n5. Day/night clubs, bars, cocktails, sing-along\r\n    lounges, bistros, pubs, beer gardens\r\n6. Bakeries, pastry and bakeshops\r\n7. Office buildings\r\n8. Financial institutions\r\n9. Printing & publishing plants and offices\r\n10. Engraving, photo developing and printing shops\r\n11. Photographer and painter studios, tailoring and\r\n      haberdashery shops\r\n12. Factories and workshops, using less flammable or non-\r\n      combustible materials\r\n13. Battery shops and auto repair shops\r\n14. Paint stores without bulk handling\r\n15. Funerals parlors\r\n16. Memorial and mortuary chapels, crematories\r\n17. Columbarium\r\n18. Telephone and telegraph exchanges\r\n19. Telecommunications, media and public information complexes\n\n       including radio and TV broadcasting studios \n\n20. Cell (mobile) phone towers\n\n21. Battery shops and auto repair shops\n\n22. Bakeries, pastry and bakeshops\n\n23. Police and fire stations \n\n24. Glassware and metalware stores, household equipment and\n\n       appliance shops\n\n25. Manufacture or insignia, badges and similar emblems except\n\n       metal\n\n26. General retail establishments such as curio or antique shops,\n\n       pet shops and aquarium stores, bookstores, art supplies and\n\n       novelties, jewelry shops, liquor wine stores and flower shops\n\n27. Employment/recruitment agencies, news syndicate services\n\n       and office equipment and repair shops and other offices\n\n28. Watch sales and services, locksmith and related services\n29. Other stores and shops for conducting retail business and\n\n       local shopping establishments.\n\n30. Radio, television and other electrical appliance repair shops\n\n31. Furniture, repair, upholstering job.\n\n32. Computer stores and video shops, including repair\n\n33. Internet cafes and cyber stations\n\n34. Garment manufacturing with no more than twenty (20)\n\n       machines\n\n35. Signboard and streamer painting silk screening\n\n36. Car barns for jeepneys and taxis not more than six (6) units\n\n37. Lotto terminals, off-fronton, on-line bingo outlets and off-\n\n      track  betting stations\n\n38. Gardens and landscapping supply/ contractors\n\n39. Printing, typesetting, copiers and duplicating services\n\n40. Photo supply and developing\n\n41. Restaurants, canteens, eateries, delicatessen shops,\n\n       confectionery shops and automats/fastfoods\n\n42. Groceries\n\n43. Laundries and laundromats\n\n'),
 ('11','E-3','Business and Mercantile (Commercial)','(Business and Mercantile, where no\r\n repair work is done, except change of parts and maintenance requiring no open flames, welding or use of highly flammable liquids)','1. All permitted uses in Division E-1 (or for C-1 and C-2 class)\r\n     buildings/structures\r\n2. Aircraft hangars\r\n3. Commercial parking lots and garages\r\n4. Department stores, shopping malls/centers, commercial and\r\n     sports complexes/areas\r\n5. Institutional uses as university complexes\r\n6. Other commercial/business activities not elsewhere classified\r\n     (n.e.c.)\r\n    '),
 ('12','F-1','Industrial (Non-pollutive/ Non-Hazardous\r\nIndusties and Non-Pollutive/ Hazardous\r\nIndustries)','(Light Industrial)','1. Ice plants and cold storage buildings\r\n2. Power plants (thermal, hydro or geothermal)\r\n3. Pumping plants (water supply, storm drainage, sewerage,\r\n     irrigation, and waste treatment plants)\r\n4. Dairies and creameries\r\n5. Rice mills and sugar centrals\r\n6. Breweries, bottling plants, canneries and tanneries\r\n7. Factories and workshops using incombustile or non- explosive\r\n     materials'),
 ('13','G-1','Storage and Hazardous Industrial (Pollutive/ Non-Hazardous Industries and Pollutive/ Hazardous Industries only)','(Medium Industrial, which shall include storage and handling of hazardous and highly flammable materials)','1. Storage tanks, buildings for storing gasoline, acetylene, LPG,\r\n     calcium, carbides, oxygen, hydrogen and the like\r\n2. Armories, arsenals and munition factories\r\n3. Match and firework factories\r\n4. Plastics resin plants (monomer and polymer)\r\n5. Plastics compounding and processing plants\r\n6. Acetylene and oxygen and generating plants\r\n7. Cooking oil and soap processing plants\r\n8. Factories for highly flammable chemicals\r\n9. Water and power generation/distribution complexes\r\n10. Liquid and solid waste management facilities\r\n11. All other types of large complexes for public utilities'),
 ('14','G-2','Storage and Hazardous Industrial (Pollutive/ Non-Hazardous Industries and Pollutive/ Hazardous Industries only)','(Medium Industrial buildings for  storage and handling flammable materials)','1. All uses permitted in l-1 class\r\n2. Dry cleaning plants using flammable liquids\r\n3. Paint stores with bulk handling\r\n4. Paint shops and spray painting rooms\r\n5. Sign and billboard painting shops'),
 ('15','G-3','Storage and Hazardous Industrial (Pollutive/ Non-Hazardous Industries and Pollutive/ Hazardous Industries only)','(Medium Industrial buildings for wood working activities, papers cardboard manufacturess, textile and garment factories','1. Wood working establishments, lumber and timber yards.\r\n2. Planning mills and sawmills, veneer plants\r\n3. Wood drying kilns\r\n4. Pulp, paper and paperboard factories\r\n5. Wood and cardboard box factories\r\n6. Textile and fiber spinning mills\r\n7. Grains and cement silos\r\n8. Warehouse where highly combustible materials are stored.\r\n9. Factories where loose combustible fiber or dirt are\r\n     manufactured, processed or generated\r\n10. Garment and undergarment factories '),
 ('16','G-4','Storage and Hazardous Industrial (Pollutive/ Non-Hazardous Industries and Pollutive/ Hazardous Industries only)','(Medium Industrial, for repair garages and engine manufacture)','1. Repair garages and shops\r\n2. Factories for engines and turbines and attached testing\r\n     facilities. '),
 ('17','G-5','Storage and Hazardous Industrial (Pollutive/ Non-Hazardous Industries and Pollutive/ Hazardous Industries only)','(Medium Industrial, for aircraft facilities)','1. Hangars\r\n2. Manufacture and assembly plants of aircraft engine\r\n3. Repairs and testing shops for aircraft engines and parts '),
 ('18','H-1','Assembly for less than 1,000 (Cultural and/or Recreational)','(Recreational, which are assembly buildings with stage and having an occupant load of less than 1,000)','1. Theaters and auditoriums\r\n2. Concert halls and open houses\r\n3. Convention halls\r\n4. Little theaters, audio-visual rooms'),
 ('19','H-2','Assembly for less than 1,000 (Cultural and/or Recreational)','(Recreational, which are assembly buildings with stage and having an occupant load of 300 or more)','1. Dance halls, cabarets, ballrooms\r\n2. Skating rinks\r\n3. Cockfighting arenas '),
 ('20','H-3','Assembly for less than 1,000 (Cultural and/or Recreational)','(Recreational, which are assmebly buildings with stage and having an occupant load of less than 300)','1. Dance halls, ballrooms\r\n2. Skating rinks '),
 ('21','H-4','Assembly for less than 1,000 (Cultural and/or Recreational)','(Recreational, tourism estate developments or tourism-oriented establishments, which are structures not included in Divisions H-1)','1. Sports stands\r\n2. Reviewing stands\r\n3. Grandstand and bleachers\r\n4. Covered amusement parks\r\n5. Boxing arenas, jai-alai stadiums\r\n6. Race tracks and hippodromes\r\n7. All types of resort complexes\r\n8. All other types of amusement and entertainment complexes '),
 ('22','I-1','Assembly for More than 1,000 (Cultural and/or Recreational)','(Recreational, Assembly Buildings with stage and an occupant load of 1,000 or more in the building)','1. Colisea and sports complexes\r\n2. Theater and convention centers\r\n3. Concert halls and open houses\r\n4. Convention centers '),
 ('23','J-1','Accesory (Agricultural and Other Occupancies/Uses not Specifically Mentioned Under Groups A through I)','','1. Agricultural structures:\r\n    a. Sheds\r\n    b. Barns\r\n    c. Poultry houses\r\n    d. Piggeries\r\n    e. Hatcheries\r\n    f. Stables\r\n    g. greenhouses\r\n    h. Granaries\r\n    i. Silos '),
 ('24','J-2','Accesory (Agricultural and Other Occupancies/Uses not Specifically Mentioned Under Groups A through I)','(Accessory)','1. Private garages, carports\r\n2. Towers, smokestacks and chimneys\r\n3. Swimming pools including shower and locker room\r\n4. Fence over 1.80 meters high, separate fire walls\r\n5. Steel and/or concrete tanks '),
 ('25','J-3','Accesory (Agricultural and Other Occupancies/Uses not Specifically Mentioned Under Groups A through I)','','1. Stages, platforms and similar structures\r\n2. Pelota, tennis, badminton or basketball courts\r\n3. Tombs, mausoleums and niches\r\n4. Aviaries and aquariums and zoo structures\r\n5. Banks and record vaults ');
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
-- Definition of table `tblpayments`
--

DROP TABLE IF EXISTS `tblpayments`;
CREATE TABLE `tblpayments` (
  `PaymentID` varchar(45) NOT NULL,
  `PaymentDate` varchar(45) NOT NULL,
  `ACN` varchar(45) NOT NULL,
  `PaymentOR` varchar(45) NOT NULL,
  `ORDate` varchar(45) NOT NULL,
  `PaymentAmount` double NOT NULL,
  `PaymentAssessment` double NOT NULL,
  PRIMARY KEY (`PaymentID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblpayments`
--

/*!40000 ALTER TABLE `tblpayments` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblpayments` ENABLE KEYS */;


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
INSERT INTO `tblsubdivisions` (`subdID`,`subdName`) VALUES 
 ('01','SSSS');
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
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblusers`
--

/*!40000 ALTER TABLE `tblusers` DISABLE KEYS */;
INSERT INTO `tblusers` (`UserID`,`UserType`,`UserDescription`,`Username`,`FirstName`,`LastName`,`MiddleName`,`UserPwd`,`UserImage`) VALUES 
 (1,3,'Information Staff','admin','Administrator','Staff','M','21232f297a57a5a743894a0e4a801fc3',NULL);
/*!40000 ALTER TABLE `tblusers` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
