CREATE VIEW [dbo].[vUserTracks]
	AS SELECT ISNULL(ut.UserId, -999) AS UserId,
	ISNULL(ut.TaskId, -999) AS TaskId,
	ISNULL(tr.TaskTrackId, -999) AS TaskTrackId,
	t.Name AS TaskName,
	tr.TrackNote AS TrackNote,
	tr.TrackDate AS TrackDate
	FROM [UserTasks] ut JOIN [TaskTracks] tr ON ut.UserTaskId=tr.UserTaskId JOIN [Tasks] t ON ut.TaskId=t.TaskId