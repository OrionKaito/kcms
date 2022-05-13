USE kcms;

CREATE TABLE `Advertisings` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedDate` datetime NOT NULL,
    `CreatedBy` text NULL,
    `UpdatedDate` datetime NULL,
    `UpdatedBy` text NULL,
    `Type` text NULL,
    `Position` varchar(767) NULL,
    `Url` varchar(767) NULL,
    `Image` text NULL,
    `Title` text NULL,
    `Priority` int NOT NULL,
    `Options` int NOT NULL,
    `Status` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE INDEX `IX_Advertisings_Position` ON `Advertisings` (`Position`);

CREATE INDEX `IX_Advertisings_Url` ON `Advertisings` (`Url`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211129140826_AddAdvertising', '3.1.21');

