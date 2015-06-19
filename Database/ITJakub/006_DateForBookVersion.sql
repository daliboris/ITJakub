SET XACT_ABORT ON;
USE ITJakubDB

BEGIN TRAN

    ALTER TABLE dbo.ManuscriptDescription ADD 
	   [NotBefore] date,
	   [NotAfter] date
        

    INSERT INTO [dbo].[DatabaseVersion]
		(DatabaseVersion)
	VALUES
		('006' )
		-- DatabaseVersion - varchar

--ROLLBACK
COMMIT 