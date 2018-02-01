CREATE TABLE [dbo].[BuildingElectricity]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[BuildingID] INT NOT NULL, 
    [PowerOn] INT NOT NULL, 
	[Year] INT NOT NULL,
	[Consumed] INT NOT NULL
	CONSTRAINT [PK_BuildingElectricity] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_BuildingElectricity_Building] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Building] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)
