CREATE TABLE [dbo].[User] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Email]         NVARCHAR (150) NOT NULL,
    [Password]      NVARCHAR (50)  NOT NULL,
    [AddedDate]     DATETIME       NOT NULL,
    [ActivatedDate] DATETIME       NULL,
    [ActivatedLink] NVARCHAR (50)  NOT NULL,
    [LastVisitDate] DATETIME       NOT NULL,
    [AvatarPath]    NVARCHAR (150) NULL,
    [FirstName]     NVARCHAR (500) NULL,
    [LastName]      NVARCHAR (500) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC)
);

