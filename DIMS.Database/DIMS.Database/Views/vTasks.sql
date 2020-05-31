CREATE VIEW [dbo].[vTasks]
	AS SELECT ISNULL(t.TaskId, -999) AS TaskId,
	t.Name AS Name,
	t.Description AS Description,
	t.StartDate AS StartDate,
	t.DeadlineDate AS DeadlineDate
	FROM [Tasks] t
