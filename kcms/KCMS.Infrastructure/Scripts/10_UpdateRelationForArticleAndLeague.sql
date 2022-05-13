USE kcms;

CREATE INDEX `IX_Articles_LeagueId` ON `Articles` (`LeagueId`);

ALTER TABLE `Articles` ADD CONSTRAINT `FK_Articles_Leagues_LeagueId` FOREIGN KEY (`LeagueId`) REFERENCES `Leagues` (`Id`) ON DELETE RESTRICT;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211212030803_UpdateRelationForArticleAndLeague', '3.1.21');

