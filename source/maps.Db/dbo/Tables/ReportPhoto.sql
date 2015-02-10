CREATE TABLE [dbo].[ReportPhoto]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
    [ReportID] INT NOT NULL, 
    [ImagePath] NVARCHAR(150) NOT NULL,
	CONSTRAINT [PK_ReportPhoto] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_ReportPhoto_Report] FOREIGN KEY ([ReportID]) REFERENCES [dbo].[Report] ([ID]),
)
