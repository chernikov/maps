CREATE TABLE [dbo].[VisualizationColumn]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[VisualizationID] INT NOT NULL,
	[Number] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL, 
    [Type] INT NOT NULL, 
	[FilterValues] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_VisualizationColumn] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_VisualizationColumn_Visualization] FOREIGN KEY ([VisualizationID]) REFERENCES [dbo].[Visualization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)
