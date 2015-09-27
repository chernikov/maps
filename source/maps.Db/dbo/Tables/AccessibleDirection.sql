CREATE TABLE [dbo].[AccessibleDirection]
(
	[ID]        INT           IDENTITY (1, 1) NOT NULL,
    [CityID]    INT           DEFAULT ((1)) NOT NULL,
    [UserID]    INT           NOT NULL,
    [Waypoints] VARCHAR (MAX) NOT NULL,
    [PolyLine]  VARCHAR (MAX) NOT NULL,
    [Length]    FLOAT (53)    DEFAULT ((0)) NOT NULL,
    [AddedDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_AccessibleDirection] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccessibleDirection_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_AccessibleDirection_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID])
)
