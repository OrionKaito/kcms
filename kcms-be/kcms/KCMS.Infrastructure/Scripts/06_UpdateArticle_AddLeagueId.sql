USE kcms;

ALTER TABLE `Articles` ADD `LeagueId` bigint NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211204153711_UpdateArticle_AddLeagueId', '3.1.21');

