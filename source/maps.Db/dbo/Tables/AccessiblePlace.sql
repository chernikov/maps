﻿CREATE TABLE [dbo].[AccessiblePlace]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[UserID] INT NOT NULL,
	[CityID] INT NOT NULL DEFAULT 1,
	[Lat] FLOAT NOT NULL, 
    [Lng] FLOAT NOT NULL, 
	[Category] INT NOT NULL,
	[Type]	   INT NOT NULL, 
	[Description] NVARCHAR(MAX),
	[AddedDate] DATETIME NOT NULL, 
    [VerifiedDate] DATETIME NULL, 
    [CreatedDate] DATETIME NULL, 
	[Address] NVARCHAR(MAX) NULL,
	[CenterDistance] FLOAT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_AccessiblePlace] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_AccessiblePlace_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_AccessiblePlace_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
)
