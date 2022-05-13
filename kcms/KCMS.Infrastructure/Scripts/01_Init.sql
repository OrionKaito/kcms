CREATE DATABASE IF NOT EXISTS `kcms`;

USE kcms;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Articles` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedDate` datetime NOT NULL,
    `CreatedBy` text NULL,
    `UpdatedDate` datetime NULL,
    `UpdatedBy` text NULL,
    `Title` varchar(100) NULL,
    `Content` text NULL,
    `Image` text NULL,
    `Type` int NOT NULL,
    PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211125101443_Init', '3.1.21');

