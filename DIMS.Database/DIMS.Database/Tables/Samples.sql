﻿CREATE TABLE [dbo].[Samples]
(
	[SampleId] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(25) NOT NULL,
	[Description] NVARCHAR(255) NULL

	CONSTRAINT [PK_Test] PRIMARY KEY ([SampleId])
)