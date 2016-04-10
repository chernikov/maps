											

GO
PRINT N'Dropping [dbo].[FK_Visualization_User]...';


GO
ALTER TABLE [dbo].[Visualization] DROP CONSTRAINT [FK_Visualization_User];


GO
PRINT N'Dropping [dbo].[FK_VisualizationItem_Visualization]...';


GO
ALTER TABLE [dbo].[VisualizationItem] DROP CONSTRAINT [FK_VisualizationItem_Visualization];


GO
PRINT N'Dropping [dbo].[FK_VisualizationColumn_Visualization]...';


GO
ALTER TABLE [dbo].[VisualizationColumn] DROP CONSTRAINT [FK_VisualizationColumn_Visualization];


GO
PRINT N'Starting rebuilding table [dbo].[Visualization]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Visualization] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [ShareLink] NVARCHAR (10)  DEFAULT '' NOT NULL,
    [UserID]    INT            NOT NULL,
    [Name]      NVARCHAR (300) NULL,
    [AddedDate] DATETIME       NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Visualization] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Visualization])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Visualization] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Visualization] ([ID], [UserID], [Name], [AddedDate])
        SELECT   [ID],
                 [UserID],
                 [Name],
                 [AddedDate]
        FROM     [dbo].[Visualization]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Visualization] OFF;
    END

DROP TABLE [dbo].[Visualization];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Visualization]', N'Visualization';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Visualization]', N'PK_Visualization', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[VisualizationUser]...';


GO
CREATE TABLE [dbo].[VisualizationUser] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [VisualizationID] INT NOT NULL,
    [UserID]          INT NOT NULL,
    CONSTRAINT [PK_VisualizationUser] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Visualization_User]...';


GO
ALTER TABLE [dbo].[Visualization] WITH NOCHECK
    ADD CONSTRAINT [FK_Visualization_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]);


GO
PRINT N'Creating [dbo].[FK_VisualizationItem_Visualization]...';


GO
ALTER TABLE [dbo].[VisualizationItem] WITH NOCHECK
    ADD CONSTRAINT [FK_VisualizationItem_Visualization] FOREIGN KEY ([VisualizationID]) REFERENCES [dbo].[Visualization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_VisualizationColumn_Visualization]...';


GO
ALTER TABLE [dbo].[VisualizationColumn] WITH NOCHECK
    ADD CONSTRAINT [FK_VisualizationColumn_Visualization] FOREIGN KEY ([VisualizationID]) REFERENCES [dbo].[Visualization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_VisualizationUser_Visualization]...';


GO
ALTER TABLE [dbo].[VisualizationUser] WITH NOCHECK
    ADD CONSTRAINT [FK_VisualizationUser_Visualization] FOREIGN KEY ([VisualizationID]) REFERENCES [dbo].[Visualization] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_VisualizationUser_User]...';


GO
ALTER TABLE [dbo].[VisualizationUser] WITH NOCHECK
    ADD CONSTRAINT [FK_VisualizationUser_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [dbo].[Visualization] WITH CHECK CHECK CONSTRAINT [FK_Visualization_User];

ALTER TABLE [dbo].[VisualizationItem] WITH CHECK CHECK CONSTRAINT [FK_VisualizationItem_Visualization];

ALTER TABLE [dbo].[VisualizationColumn] WITH CHECK CHECK CONSTRAINT [FK_VisualizationColumn_Visualization];

ALTER TABLE [dbo].[VisualizationUser] WITH CHECK CHECK CONSTRAINT [FK_VisualizationUser_Visualization];

ALTER TABLE [dbo].[VisualizationUser] WITH CHECK CHECK CONSTRAINT [FK_VisualizationUser_User];


GO
PRINT N'Update complete.';


GO
