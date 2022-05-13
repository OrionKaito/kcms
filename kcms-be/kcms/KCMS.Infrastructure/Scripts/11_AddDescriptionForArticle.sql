ALTER TABLE `Articles` ADD `Description` text NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211212055746_AddDescriptionForArticle', '3.1.21');

