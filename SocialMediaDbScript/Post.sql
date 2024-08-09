CREATE TABLE [dbo].[Post] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [UserId]      INT             NOT NULL,
    [Content]     NVARCHAR (1000) NOT NULL,
    [Image]       VARBINARY (500)   NULL,
    [CreatedTime] DATETIME        NOT NULL,
    [Likes]       INT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Post_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);