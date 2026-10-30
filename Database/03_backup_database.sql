/* =====================================================================
   Karnel Travel Guide - make the database backup for the final ZIP (T38)
   Change the folder if C:\KarnelBackup does not suit you; it must exist
   and SQL Server must be allowed to write there.
   ===================================================================== */
USE master;
GO

BACKUP DATABASE KarnelTravelDB
TO DISK = N'C:\KarnelBackup\KarnelTravelDB.bak'
WITH FORMAT, INIT,
     NAME = N'KarnelTravelDB full backup',
     STATS = 10;   -- prints progress every 10%
GO

/* To restore on another PC:
RESTORE DATABASE KarnelTravelDB
FROM DISK = N'C:\KarnelBackup\KarnelTravelDB.bak'
WITH REPLACE;
*/
