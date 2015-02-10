﻿CREATE TABLE [dbo].[UtilityIssueHistory]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
	[UtilityIssueID] INT NOT NULL,
	[UserID] INT NOT NULL, 
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
	[WorkFlow] NVARCHAR(MAX) NULL, 
    [Solution] NVARCHAR(MAX) NULL, 
    [Status] INT NOT NULL, 
    [IsRemoved] BIT NOT NULL, 
    CONSTRAINT [PK_UtilityIssueHistory] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_UtilityIssueHistory_UtilityIssue] FOREIGN KEY ([UtilityIssueID]) REFERENCES [dbo].[UtilityIssue] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_UtilityIssueHistory_MainUtilityIssue] FOREIGN KEY ([MainUtilityIssueID]) REFERENCES [dbo].[UtilityIssueHistory] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UtilityIssueHistory_UtilityDepartment] FOREIGN KEY ([UtilityDepartmentID]) REFERENCES [dbo].[UtilityDepartment] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_UtilityIssueHistory_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
)