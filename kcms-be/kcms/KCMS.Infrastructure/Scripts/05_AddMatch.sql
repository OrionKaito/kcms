USE kcms; 

CREATE TABLE `Matchs` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedDate` datetime NOT NULL,
    `CreatedBy` text NULL,
    `UpdatedDate` datetime NULL,
    `UpdatedBy` text NULL,
    `Video` text NULL,
    `MatchType` int NOT NULL,
    `Status` int NOT NULL,
    `Time` datetime NOT NULL,
    `HomePoints` float NOT NULL,
    `GuestPoints` float NOT NULL,
    `HomeTeamId` bigint NOT NULL,
    `GuestTeamId` bigint NOT NULL,
    `LeagueId` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Matchs_Teams_GuestTeamId` FOREIGN KEY (`GuestTeamId`) REFERENCES `Teams` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Matchs_Teams_HomeTeamId` FOREIGN KEY (`HomeTeamId`) REFERENCES `Teams` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Matchs_Leagues_LeagueId` FOREIGN KEY (`LeagueId`) REFERENCES `Leagues` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Matchs_GuestTeamId` ON `Matchs` (`GuestTeamId`);

CREATE INDEX `IX_Matchs_HomeTeamId` ON `Matchs` (`HomeTeamId`);

CREATE INDEX `IX_Matchs_LeagueId` ON `Matchs` (`LeagueId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211202123130_AddMatch', '3.1.21');

