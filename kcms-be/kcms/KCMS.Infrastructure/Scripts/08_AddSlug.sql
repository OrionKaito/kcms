ALTER TABLE `Matchs` ADD `Slug` text NULL;

ALTER TABLE `Articles` ADD `Slug` text NULL;

ALTER TABLE `Advertisings` MODIFY `Status` int NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211205132208_AddSlug', '3.1.21');

