CREATE DATABASE TimeTracker;
GO
CREATE LOGIN TimeTrackerManagerLogin WITH PASSWORD = 'TimeTrackerManagerPassword123'
go

USE TimeTracker;
go
IF NOT EXISTS (SELECT *
FROM sys.database_principals
WHERE name = N'TimeTrackerManager') BEGIN
    CREATE USER TimeTrackerManager FOR LOGIN TimeTrackerManagerLogin
    EXEC sp_addrolemember N'db_owner', N'TimeTrackerManager'
END;
GO