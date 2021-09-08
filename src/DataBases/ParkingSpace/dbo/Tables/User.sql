CREATE TABLE [dbo].[User]
(
  [Id] INT NOT NULL IDENTITY,
  [Firstname] NVARCHAR(100) NOT NULL,
  [Lastname] NVARCHAR(100) NULL,
  [Email] NVARCHAR(200) NOT NULL,
  [IsEnabled] BIT NOT NULL CONSTRAINT [DF_User_IsEnabled_True] DEFAULT (1),
  [DateCreated] DATETIME NOT NULL CONSTRAINT [DF_Posting_DateCreated_UtcDate] DEFAULT (GETUTCDATE()),

  CONSTRAINT [PK_User_Id] PRIMARY KEY ([Id]),
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Email_Unique] 
    ON [dbo].[User] ([Email])

GO