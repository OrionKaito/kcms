USE kcms;

CREATE INDEX `IX_Teams_LeagueId` ON `Teams` (`LeagueId`);

ALTER TABLE `Teams` ADD CONSTRAINT `FK_Teams_Leagues_LeagueId` FOREIGN KEY (`LeagueId`) REFERENCES `Leagues` (`Id`) ON DELETE RESTRICT;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211215100803_UpdateRelationForTeamAndLeague', '3.1.21');

