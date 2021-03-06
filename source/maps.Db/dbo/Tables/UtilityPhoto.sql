﻿CREATE TABLE [dbo].[UtilityPhoto]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
	[UtilityIssueID] INT NULL, 
	[UserID] INT NULL, 
	[Image] NVARCHAR(150) NOT NULL,
	[AddedDate] DATETIME NOT NULL,
	[State] INT NOT NULL,
	[IsRemoved] BIT NOT NULL, 
	CONSTRAINT [PK_UtilityPhoto] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_UtilityPhoto_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UtilityPhoto_UtilityIssue] FOREIGN KEY ([UtilityIssueID]) REFERENCES [dbo].[UtilityIssue] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
	
)
