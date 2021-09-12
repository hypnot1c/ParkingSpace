CREATE TABLE [dbo].[Parking_Place]
(
  [Id] INT NOT NULL IDENTITY,
  [GroupId] INT NOT NULL,
  [Number] NVARCHAR(100) NOT NULL,
  [Comment] NVARCHAR(MAX) NULL,

  CONSTRAINT [PK_Parking_Place_Id] PRIMARY KEY ([Id]),
  CONSTRAINT [FK_Parking_Group_Id_Parking_Place_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Parking_Group]([Id]),
)
