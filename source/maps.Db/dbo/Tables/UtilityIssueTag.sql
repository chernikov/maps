CREATE TABLE [dbo].[UtilityIssueTag]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
	[UtilityIssueID] INT NOT NULL,
	[UtilityTagID] INT NOT NULL,
	CONSTRAINT [PK_UtilityIssueTag] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_UtilityIssueTag_UtilityIssue] FOREIGN KEY ([UtilityIssueID]) REFERENCES [dbo].[UtilityIssue] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_UtilityIssueTag_UtilityTag] FOREIGN KEY ([UtilityTagID]) REFERENCES [dbo].[UtilityTag] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
)
