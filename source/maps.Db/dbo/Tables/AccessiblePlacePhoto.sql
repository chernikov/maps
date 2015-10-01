CREATE TABLE [dbo].[AccessiblePlacePhoto]
(
	[ID]        INT            IDENTITY (1, 1) NOT NULL,
    [AccessiblePlaceID]  INT            NULL,
    [ImagePath] NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_AccessiblePlacePhoto] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccessiblePlacePhoto_AccessiblePlace] FOREIGN KEY ([AccessiblePlaceID]) REFERENCES [dbo].[AccessiblePlace] ([ID])

)
