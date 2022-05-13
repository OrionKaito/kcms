USE kcms;

CREATE TABLE `Leagues` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedDate` datetime NOT NULL,
    `CreatedBy` text NULL,
    `UpdatedDate` datetime NULL,
    `UpdatedBy` text NULL,
    `Name` text NULL,
    PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211130161004_AddLeague', '3.1.21');

