﻿CREATE TABLE [dbo].[AccessiblePlace]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[UserID] INT NOT NULL,
	[CityID] INT NOT NULL DEFAULT 1,
	[Lat] FLOAT NOT NULL, 
    [Lng] FLOAT NOT NULL, 
	[Description] NVARCHAR(MAX),
	[Address] NVARCHAR(MAX) NULL,
	[AddedDate] DATETIME NOT NULL, 
    [VerifiedDate] DATETIME NULL, 
    CONSTRAINT [PK_AccessiblePlace] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_AccessiblePlace_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_AccessiblePlace_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
)
