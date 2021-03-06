﻿CREATE TABLE [dbo].[Report]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
    [UserID] INT NOT NULL, 
	[RouteID] INT NULL, 
	[BusID] INT NULL,
    [Status] INT NOT NULL, 
	[Type] INT NOT NULL,
    [AddedDate] DATETIME NOT NULL, 
    [DeadlineDate] DATETIME NOT NULL, 
    [DateTime] DATETIME NULL, 
    [StationID] INT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [NotifyTransporteurID] INT NULL, 
    [NotifyReporterID] INT NULL, 
    [Link] NVARCHAR(500) NULL, 
    [FacebookLink] NVARCHAR(500) NULL,
	CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_Report_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]),
	CONSTRAINT [FK_Report_Bus] FOREIGN KEY ([BusID]) REFERENCES [dbo].[Bus] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_Report_Route] FOREIGN KEY ([RouteID]) REFERENCES [dbo].[Route] ([ID]),
	CONSTRAINT [FK_Report_Station] FOREIGN KEY ([StationID]) REFERENCES [dbo].[Station] ([ID]),
	CONSTRAINT [FK_Report_NotifyTransporteur] FOREIGN KEY ([NotifyTransporteurID]) REFERENCES [dbo].[Transporteur] ([ID]),
	CONSTRAINT [FK_Report_NotifyReporter] FOREIGN KEY ([NotifyReporterID]) REFERENCES [dbo].[User] ([ID]),

)
