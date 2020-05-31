CREATE VIEW [dbo].[vUserProgresses]
	AS SELECT ISNULL(up.UserId, -999) AS UserId,
	ISNULL(ut.TaskId, -999) AS TaskId,
	ISNULL(tr.TaskTrackId, -999) AS TaskTrackId,
	CONCAT(up.Name, ' ', up.LastName) AS UserName,
	t.Name AS TaskName,
	tr.TrackNote AS TrackNote,
	tr.TrackDate AS TrackDate
	FROM [UserProfiles] up JOIN [UserTasks] ut ON up.UserId=ut.UserId JOIN [TaskTracks] tr ON ut.UserTaskId=tr.UserTaskId JOIN [Tasks] t ON ut.TaskId=t.TaskId