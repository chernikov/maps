﻿CREATE TABLE [dbo].[Transporteur]
(
	[ID] INT IDENTITY (1, 1) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Patronymic] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NULL, 
    [PrimaryPhone] NVARCHAR(50) NOT NULL, 
    [AdditionalPhone] NVARCHAR(50) NULL,
	CONSTRAINT [PK_Transporteur] PRIMARY KEY CLUSTERED ([ID] ASC)
)