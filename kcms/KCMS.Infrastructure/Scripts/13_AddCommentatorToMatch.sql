USE kcms;

ALTER TABLE `Matchs` ADD `Commentator` text NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220104005043_AddCommentatorToMatch', '3.1.21');

