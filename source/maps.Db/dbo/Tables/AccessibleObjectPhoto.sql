CREATE TABLE [dbo].[AccessibleObjectPhoto]
(
	[ID]        INT            IDENTITY (1, 1) NOT NULL,
    [AccessibleObjectID]  INT            NULL,
    [ImagePath] NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_AccessibleObjectPhoto] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccessibleObjectPhoto_AccessibleObject] FOREIGN KEY ([AccessibleObjectID]) REFERENCES [dbo].[AccessibleObject] ([ID])

)
