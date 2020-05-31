CREATE TABLE [dbo].[Directions]
(
	[DirectionId] INT IDENTITY NOT NULL, 
	[Name] NVARCHAR(50) NOT NULL, 
	[Description] NVARCHAR(MAX) NOT NULL

	CONSTRAINT [PK_Direction] PRIMARY KEY ([DirectionId])
)
