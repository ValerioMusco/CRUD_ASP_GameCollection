/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DELETE FROM Games;
GO

INSERT INTO Games VALUES
    ('Leagues of Legend', 'Un jeu de merde', 'Moba', '2009-10-27'),
    ('Call of Duty', null, 'FPS', '2007-11-5'),
    ('Escape From Tarkov', 'Meilleur jeu du monde.', 'Extract Shooter', '2016-8-4'),
    ('Smite', null, 'Moba', '2014-3-25'),
    ('World of Warcraft', 'Pour l''alliance !', 'MMORPG', '2004-11-23');
