CREATE PROCEDURE [dbo].[SampleEntriesAmount]
	@isAdmin bit = 0,
	@result int OUTPUT
AS
	BEGIN
		IF @isAdmin = 1
			BEGIN
				SELECT @result = COUNT(SampleId)
				FROM [Samples]
			END
		ELSE
			BEGIN
				SELECT @result = 0
			END
	END
RETURN 0
