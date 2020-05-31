CREATE TABLE [dbo].[TaskStates]
(
	[StateId] INT NOT NULL, 
    [StateName] VARCHAR(50) NOT NULL 

	CONSTRAINT [PK_State] PRIMARY KEY ([StateId])
)
