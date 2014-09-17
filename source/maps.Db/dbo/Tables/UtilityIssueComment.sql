CREATE TABLE [dbo].[UtilityIssueComment]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
	[UtilityIssueID] INT NOT NULL, 
	[CommentID] INT NOT NULL, 
    CONSTRAINT [PK_UtilityIssueComment] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_UtilityIssueComment_CommentID] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comment] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_UtilityIssueComment_UtilityIssue] FOREIGN KEY ([UtilityIssueID]) REFERENCES [dbo].[UtilityIssue] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
)
