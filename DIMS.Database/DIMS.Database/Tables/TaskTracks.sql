CREATE TABLE [dbo].[TaskTracks]
(
	[TaskTrackId] INT IDENTITY NOT NULL , 
    [UserTaskId] INT NOT NULL, 
    [TrackDate] DATE NOT NULL, 
    [TrackNote] VARCHAR(128) NOT NULL

	CONSTRAINT [PK_TaskTrack] PRIMARY KEY ([TaskTrackId])
	CONSTRAINT [FK_UserTask_To_TaskTrack] FOREIGN KEY ([UserTaskId]) REFERENCES [UserTasks]([UserTaskId]) ON DELETE CASCADE ON UPDATE CASCADE 
)
