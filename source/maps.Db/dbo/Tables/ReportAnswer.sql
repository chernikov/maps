CREATE TABLE [dbo].[ReportAnswer]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[TransporteurID] INT NOT NULL,
	[ReportID] INT NOT NULL,
	[AddedDate] DATETIME NOT NULL, 
	[Answer] NVARCHAR(MAX) NULL, 
	CONSTRAINT [PK_ReportAnswer] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_ReportAnswer_Transporteur] FOREIGN KEY ([TransporteurID]) REFERENCES [dbo].[Transporteur] ([ID]),
	CONSTRAINT [FK_ReportAnswer_Report] FOREIGN KEY ([ReportID]) REFERENCES [dbo].[Report] ([ID])

)
