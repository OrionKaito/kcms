USE kcms;

ALTER TABLE `Articles` ADD `Video` text NULL;

CREATE TABLE `Teams` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedDate` datetime NOT NULL,
    `CreatedBy` text NULL,
    `UpdatedDate` datetime NULL,
    `UpdatedBy` text NULL,
    `TeamName` varchar(100) NULL,
    `Image` text NULL,
    `LeagueId` bigint NOT NULL,
    `ST` int NOT NULL,
    `T` int NOT NULL,
    `H` int NOT NULL,
    `B` int NOT NULL,
    `TG` int NOT NULL,
    `TH` int NOT NULL,
    `HS` int NOT NULL,
    `D` int NOT NULL,
    PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211127031537_AddTeam', '3.1.21');

