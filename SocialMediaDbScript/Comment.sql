CREATE TABLE [dbo].[Comment] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [UserId]      INT             NOT NULL,
    [PostId]      INT             NOT NULL,
    [Text]        NVARCHAR (1000) NOT NULL,
    [CreatedTime] DATETIME        NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comment_Post_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post] ([Id]),
    CONSTRAINT [FK_Comment_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);