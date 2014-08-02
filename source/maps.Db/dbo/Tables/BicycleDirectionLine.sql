CREATE TABLE [dbo].[BicycleDirectionLine]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[BicycleDirectionID] INT NOT NULL, 
    [BicycleLineID] INT NOT NULL, 
	CONSTRAINT [PK_BicycleDirectionLine] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_BicycleDirectionLine_BicycleDirection] FOREIGN KEY ([BicycleDirectionID]) REFERENCES [dbo].[BycicleDirection] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_BicycleDirectionLine_BicycleLine] FOREIGN KEY ([BicycleLineID]) REFERENCES [dbo].[BicycleLine] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
	
)
