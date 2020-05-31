CREATE VIEW [dbo].[vUserTasks]
	AS SELECT ISNULL(ut.UserId, -999) AS UserId,
	ISNULL(ut.TaskId, -999) AS TaskId,
	t.Name AS TaskName,
	t.Description AS Description,
	t.StartDate AS StartDate,
	t.DeadlineDate AS DeadlineDate,
	ts.StateName AS State
	FROM [UserTasks] ut JOIN [Tasks] t ON ut.TaskId=t.TaskId JOIN [TaskStates] ts ON ut.StateId=ts.StateId
