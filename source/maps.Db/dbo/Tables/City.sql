﻿CREATE TABLE [dbo].[City]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CenterLat] FLOAT NOT NULL, 
	[CenterLng] FLOAT NOT NULL, 
    [Zoom] INT NOT NULL,
	CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([ID] ASC)
)
