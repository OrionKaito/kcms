USE kcms;

ALTER TABLE `Leagues` ADD `Image` text NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211204154941_UpdateLeague_AddImage', '3.1.21');

