CREATE TABLE [dbo].[VisualizationUser]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[VisualizationID] INT NOT NULL,
	[UserID] INT NOT NULL,
	CONSTRAINT [PK_VisualizationUser] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_VisualizationUser_Visualization] FOREIGN KEY ([VisualizationID]) REFERENCES [dbo].[Visualization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_VisualizationUser_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)
