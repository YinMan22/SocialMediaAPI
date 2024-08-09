CREATE TABLE [dbo].[LikePost] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [UserId]      INT      NOT NULL,
    [PostId]      INT      NOT NULL,
    [IsLike]      BIT      NOT NULL,
    [CreatedTime] DATETIME NOT NULL,
    CONSTRAINT [PK_LikePost] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LikePost_Post_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post] ([Id]),
    CONSTRAINT [FK_LikePost_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);