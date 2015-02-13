CREATE TABLE [dbo].[RouteSection]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
    [RouteID] INT NOT NULL, 
    [StationID] INT NOT NULL, 
    [NextStationID] INT NOT NULL,
	CONSTRAINT [PK_RouteSection] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_RouteSection_Route] FOREIGN KEY ([RouteID]) REFERENCES [dbo].[Route] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_RouteSection_Station] FOREIGN KEY ([StationID]) REFERENCES [dbo].[Station] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_RouteSection_NextStation] FOREIGN KEY ([NextStationID]) REFERENCES [dbo].[Station] ([ID])


)
