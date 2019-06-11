-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.3.15-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win32
-- HeidiSQL Version:             10.1.0.5464
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for atr
CREATE DATABASE IF NOT EXISTS `atr` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `atr`;

-- Dumping structure for table atr.atr
CREATE TABLE IF NOT EXISTS `atr` (
  `Kode` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(255) NOT NULL DEFAULT '',
  `KodeProvinsi` int(11) NOT NULL,
  `KodeKabupatenKota` int(11) NOT NULL,
  `Nomor` varchar(50) NOT NULL DEFAULT '',
  `KodeJenisAtr` int(11) NOT NULL,
  `Tanggal` date NOT NULL,
  `KodeProgressAtr` int(11) NOT NULL,
  `Aoi` varchar(255) NOT NULL,
  `Luas` int(11) NOT NULL,
  `Tahun` year(4) NOT NULL,
  PRIMARY KEY (`Kode`),
  KEY `FK_atr_provinsi` (`KodeProvinsi`),
  KEY `FK_atr_kabupaten_kota` (`KodeKabupatenKota`),
  KEY `FK_atr_jenis_atr` (`KodeJenisAtr`),
  KEY `FK_atr_progress_atr` (`KodeProgressAtr`),
  CONSTRAINT `FK_atr_jenis_atr` FOREIGN KEY (`KodeJenisAtr`) REFERENCES `jenis_atr` (`kode`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_atr_kabupaten_kota` FOREIGN KEY (`KodeKabupatenKota`) REFERENCES `kabupaten_kota` (`Kode`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_atr_progress_atr` FOREIGN KEY (`KodeProgressAtr`) REFERENCES `progress_atr` (`Kode`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_atr_provinsi` FOREIGN KEY (`KodeProvinsi`) REFERENCES `provinsi` (`kode`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table atr.atr_dokumen
CREATE TABLE IF NOT EXISTS `atr_dokumen` (
  `Kode` int(11) NOT NULL AUTO_INCREMENT,
  `KodeAtr` int(11) NOT NULL,
  `KodeDokumen` int(11) NOT NULL,
  `Status` char(1) NOT NULL DEFAULT '',
  `Nomor` varchar(50) NOT NULL DEFAULT '',
  `Tanggal` date NOT NULL,
  `FilePath` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`Kode`),
  KEY `FK_atr_dokumen_dokumen` (`KodeDokumen`),
  KEY `FK_atr_dokumen_atr` (`KodeAtr`),
  CONSTRAINT `FK_atr_dokumen_atr` FOREIGN KEY (`KodeAtr`) REFERENCES `atr` (`Kode`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_atr_dokumen_dokumen` FOREIGN KEY (`KodeDokumen`) REFERENCES `dokumen` (`Kode`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table atr.dokumen
CREATE TABLE IF NOT EXISTS `dokumen` (
  `Kode` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(100) NOT NULL DEFAULT '',
  `KodeKelompokDokumen` int(11) NOT NULL,
  PRIMARY KEY (`Kode`),
  KEY `FK_dokumen_kelompok_dokumen` (`KodeKelompokDokumen`),
  CONSTRAINT `FK_dokumen_kelompok_dokumen` FOREIGN KEY (`KodeKelompokDokumen`) REFERENCES `kelompok_dokumen` (`Kode`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table atr.jenis_atr
CREATE TABLE IF NOT EXISTS `jenis_atr` (
  `Kode` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(20) NOT NULL DEFAULT '',
  PRIMARY KEY (`Kode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table atr.kabupaten_kota
CREATE TABLE IF NOT EXISTS `kabupaten_kota` (
  `Kode` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(100) NOT NULL DEFAULT '',
  `KodeProvinsi` int(11) NOT NULL,
  PRIMARY KEY (`Kode`),
  KEY `FK_kabupaten_kota_provinsi` (`KodeProvinsi`),
  CONSTRAINT `FK_kabupaten_kota_provinsi` FOREIGN KEY (`KodeProvinsi`) REFERENCES `provinsi` (`kode`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table atr.kelompok_dokumen
CREATE TABLE IF NOT EXISTS `kelompok_dokumen` (
  `Kode` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(50) NOT NULL DEFAULT '',
  `KodeJenisAtr` int(11) NOT NULL,
  PRIMARY KEY (`Kode`),
  KEY `FK_kelompok_dokumen_jenis_atr` (`KodeJenisAtr`),
  CONSTRAINT `FK_kelompok_dokumen_jenis_atr` FOREIGN KEY (`KodeJenisAtr`) REFERENCES `jenis_atr` (`kode`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table atr.progress_atr
CREATE TABLE IF NOT EXISTS `progress_atr` (
  `Kode` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(50) NOT NULL DEFAULT '',
  `KodeJenisAtr` int(11) NOT NULL,
  PRIMARY KEY (`Kode`),
  KEY `FK_progress_atr_jenis_atr` (`KodeJenisAtr`),
  CONSTRAINT `FK_progress_atr_jenis_atr` FOREIGN KEY (`KodeJenisAtr`) REFERENCES `jenis_atr` (`kode`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table atr.provinsi
CREATE TABLE IF NOT EXISTS `provinsi` (
  `Kode` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`Kode`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

-- Manually added 
-- Create database user
CREATE USER 'atr-user'@'localhost' IDENTIFIED BY ';M)kvb6a|lq$M?Nl[9ir';
GRANT ALL PRIVILEGES ON atr.* TO 'atr-user'@'localhost';

