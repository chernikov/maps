CREATE TABLE [dbo].[BicycleParking]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[UserID] INT NOT NULL,
	[Position] NVARCHAR(100) NOT NULL,
	[PhotoUrl] NVARCHAR(150) NULL,
	[Exist]    BIT NOT NULL,
	[Type]	   INT NOT NULL, 
	[Lock]	   BIT NOT NULL,
	[Camera]   BIT NOT NULL,
	[Rent]     BIT NOT NULL,
	[Quality]  INT NOT NULL,
	[Capacity]  INT NOT NULL,
	[VotesCount] INT NOT NULL,
	[Description] NVARCHAR(MAX),
	[AddedDate] DATETIME NOT NULL, 
    [VerifiedDate] DATETIME NULL, 
    [CreatedDate] DATETIME NULL, 
    CONSTRAINT [PK_BicycleParking] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_BicycleParking_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)
