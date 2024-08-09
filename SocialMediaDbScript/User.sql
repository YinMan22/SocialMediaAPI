CREATE TABLE [dbo].[User] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Username]       NVARCHAR (50)  NOT NULL,
    [HashedPassword] CHAR (128)     NOT NULL,
    [FullName]       NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE NONCLUSTERED INDEX [IX_User_Username]
    ON [dbo].[User]([Username] ASC);
