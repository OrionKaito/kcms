USE kcms;

CREATE TABLE `User` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedDate` datetime NOT NULL,
    `CreatedBy` text NULL,
    `UpdatedDate` datetime NULL,
    `UpdatedBy` text NULL,
    `Username` varchar(100) NULL,
    `Password` varchar(100) NULL,
    `Name` varchar(100) NULL,
    PRIMARY KEY (`Id`)
);

CREATE UNIQUE INDEX `IX_User_Username` ON `User` (`Username`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211210190645_AddUser', '3.1.21');

