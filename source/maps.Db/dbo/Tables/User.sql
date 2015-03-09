CREATE TABLE [dbo].[User] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
	[CityID]		INT NOT NULL DEFAULT 1,
	[Login]         NVARCHAR (150) NOT NULL,
	[Mobile]        NVARCHAR (150) NULL,
    [Email]         NVARCHAR (150) NULL,
    [Password]      NVARCHAR (50)  NOT NULL,
    [AddedDate]     DATETIME       NOT NULL,
    [LastVisitDate] DATETIME       NOT NULL,
    [AvatarPath]    NVARCHAR (150) NULL,
    [FirstName]     NVARCHAR (500) NULL,
    [LastName]      NVARCHAR (500) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_User_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
);

