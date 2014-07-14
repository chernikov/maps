/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DELETE FROM [dbo].[Role] 
GO
SET IDENTITY_INSERT[dbo].[Role] ON
GO
SET IDENTITY_INSERT[dbo].[Role] OFF
GO

INSERT INTO [dbo].[Role] (Code,Name)
VALUES('admin',N'Админ'),
('moderator',N'Модератор')
GO
INSERT INTO [dbo].[User] (Email,	[Password],	AddedDate,	ActivatedDate,	ActivatedLink,	LastVisitDate,	AvatarPath,	FirstName,	LastName)
VALUES
(N'admin',	N'admin', '2001-01-01 00:00:00.000', '2001-01-01 00:00:00.000', '', '2012-09-28 07:18:05.497',	NULL,	NULL,	NULL),
(N'chernikov@gmail.com',	N'123456', '2012-04-22 16:26:12.033', '2012-04-22 16:30:04.190', 'b79d67ae3e414aa798d3b438af8fab', '2012-09-28 07:18:05.497',	NULL,	N'Андрей',	N'Черников')

GO

INSERT INTO [dbo].[UserRole]
(RoleID, UserID )
VALUES(1, 1)
GO