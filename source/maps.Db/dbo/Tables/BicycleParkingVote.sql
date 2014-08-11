CREATE TABLE [dbo].[BicycleParkingVote]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[UserID] INT NOT NULL,
	[BicycleParkingID] INT NOT NULL,
	[Mark] INT NOT NULL,
	CONSTRAINT [PK_BicycleParkingVote] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_BicycleParkingVote_BicycleParking] FOREIGN KEY ([BicycleParkingID]) REFERENCES [dbo].[BicycleParking] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_BicycleParkingVote_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) 
	
)
