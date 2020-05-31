CREATE TABLE [dbo].[UserProfiles]
(
	[UserId] INT IDENTITY NOT NULL, 
    [DirectionId] INT NOT NULL, 
    [Name] NVARCHAR(25) NOT NULL, 
    [Email] VARCHAR(25) NOT NULL, 
    [LastName] NVARCHAR(25) NOT NULL, 
    [Sex] CHAR NOT NULL, 
    [Education] NVARCHAR(250) NOT NULL, 
    [BirthDate] DATE NOT NULL, 
    [UniversityAverageScore] FLOAT NULL, 
    [MathScore] FLOAT NULL, 
    [Address] NVARCHAR(100) NOT NULL, 
    [MobilePhone] NVARCHAR(15) NOT NULL, 
    [Skype] VARCHAR(50) NULL, 
    [StartDate] DATE NOT NULL

	CONSTRAINT [PK_UserProfile] PRIMARY KEY ([UserId])
	CONSTRAINT [FK_Direction_To_UserProfile] FOREIGN KEY ([DirectionId]) REFERENCES [Directions]([DirectionId]) ON DELETE CASCADE ON UPDATE CASCADE, 
)
