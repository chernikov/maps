CREATE TABLE [dbo].[Rule]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[FundamentalRuleID] INT NULL,
	[Туре] INT NOT NULL,
	[IsRouteScope] BIT NOT NULL,
    [Name] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [UrlToLaw] NVARCHAR(500) NULL,
	CONSTRAINT [PK_Rule] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_Rule_FundamentalRule] FOREIGN KEY ([FundamentalRuleID]) REFERENCES [dbo].[FundamentalRule] ([ID]),
)
