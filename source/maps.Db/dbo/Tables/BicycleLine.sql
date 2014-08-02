﻿CREATE TABLE [dbo].[BicycleLine]
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
    [Start] NVARCHAR(150) NOT NULL, 
    [End] NVARCHAR(150) NOT NULL, 
	[Quantity] INT NOT NULL,
	CONSTRAINT [PK_BicycleLine] PRIMARY KEY CLUSTERED ([ID] ASC),
)
