CREATE TABLE [dbo].[VisualizationItem]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
	[VisualizationID] INT NOT NULL,
	[Lat] FLOAT NOT NULL, 
    [Lng] FLOAT NOT NULL, 
	[Data] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [PK_VisualizationItem] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_VisualizationItem_Visualization] FOREIGN KEY ([VisualizationID]) REFERENCES [dbo].[Visualization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)
