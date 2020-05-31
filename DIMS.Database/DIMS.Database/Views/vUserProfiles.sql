CREATE VIEW [dbo].[vUserProfiles]
		AS SELECT ISNULL(up.UserId, -999) AS UserId, --for Entity Framework primary key
	          CONCAT(up.Name, ' ', up.LastName) AS FullName,
			  up.Email,
			  d.Name AS Direction,
			  up.Sex,
			  up.Education,
			  DATEDIFF(hour, up.BirthDate, SYSDATETIME())/8760 AS Age,
			  up.UniversityAverageScore,
			  up.MathScore,
			  up.Address,
			  up.MobilePhone,
			  up.Skype,
			  up.StartDate
			  FROM [UserProfiles] up JOIN [Directions] d ON up.DirectionId=d.DirectionId
