﻿INSERT INTO [dbo].[Sample]
		([Name], [Description])
	VALUES
		('Sample Record #1', 'This is a simple test record for testing purposes.'),
		('Sample Record #2', 'This is a simple test record for testing purposes too.');

INSERT INTO [dbo].Direction
		([Name], [Description])
	VALUES
		('Front-end', 'You need to learn front-end here'),
		('.Net', 'You need to learn .net here');

INSERT INTO [dbo].[UserProfile]
		([DirectionId], [Name], [Email], [LastName], [Sex], [Education], [BirthDate], [UniversityAverageScore], [MathScore], [Address], [MobilePhone], [Skype], [StartDate])
	VALUES
		(1, 'Alex', 'alex@m.ru', 'Kireev', 'M', 'BNTU', '01-07-2000', 7.7, 8.2, 'Pr. Pobedi 38-19', '+37529324546', 'alexxx', '11-10-2018'),
		(2, 'Kiril', 'kiril@freedom.info', 'Andreev', 'M', 'BGTU', '22-08-2002', 8.7, 6.2, 'Pr. Gazeti Pravda 12-51', '+37525333667', 'kirilxxx', '31-12-2018')

