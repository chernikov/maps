﻿CREATE TABLE [dbo].[UtilityIssue]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
	[UserID] INT NOT NULL, 
	[CityID] INT NOT NULL,
	[AddedDate] DATETIME NOT NULL, 
    [AcceptedDate] DATETIME NULL, 
    [ResolvedDate] DATETIME NULL, 
    [ClosedDate] DATETIME NULL, 
    [UtilityDepartmentID] INT NULL, 
    [MainUtilityIssueID] INT NULL, 
    [Lat] FLOAT NOT NULL, 
    [Lng] FLOAT NOT NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Solution] NVARCHAR(MAX) NULL, 
    [Status] INT NOT NULL, 
    [IsRemoved] BIT NOT NULL, 
    CONSTRAINT [PK_UtilityIssue] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_UtilityIssue_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UtilityIssue_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UtilityIssue_UtilityDepartment] FOREIGN KEY ([UtilityDepartmentID]) REFERENCES [dbo].[UtilityDepartment] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UtilityIssue_UtilityIssue] FOREIGN KEY ([MainUtilityIssueID]) REFERENCES [dbo].[UtilityIssue] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
)