CREATE TABLE [dbo].[Station]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Lat] FLOAT NOT NULL, 
    [Lng] FLOAT NOT NULL, 
    [IsEndStation] BIT NOT NULL, 
    [HasPocket] BIT NOT NULL, 
    [HasNewTimetable] BIT NOT NULL,
	CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED ([ID] ASC)
)
