CREATE TABLE [dbo].[Notify]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
    [ReportID] INT NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(150) NULL, 
    [Sender] NVARCHAR(50) NULL, 
    [Header] NVARCHAR(150) NULL, 
    [Text] NVARCHAR(MAX) NULL, 
    [AddedDate] DATETIME NOT NULL, 
    [IsSent] BIT NOT NULL, 
    [Result] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_Notify] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_Notify_Report] FOREIGN KEY ([ReportID]) REFERENCES [dbo].[Report] ([ID])
)
