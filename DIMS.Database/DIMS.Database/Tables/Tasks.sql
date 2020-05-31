CREATE TABLE [dbo].[Tasks]
(
	[TaskId] INT IDENTITY NOT NULL , 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(256) NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [DeadlineDate] DATE NOT NULL

	CONSTRAINT [PK_Task] PRIMARY KEY ([TaskId])
)
