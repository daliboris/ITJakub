﻿SET XACT_ABORT ON;

BEGIN TRAN;

    ALTER TABLE [dbo].[Favorites] ADD
	   [Title] varchar(255) NULL

    INSERT INTO [dbo].[DatabaseVersion]
		 ( DatabaseVersion )
    VALUES
		 ( '023' );
	-- DatabaseVersion - varchar
--ROLLBACK
COMMIT;
