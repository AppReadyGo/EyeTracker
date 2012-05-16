CREATE TABLE [log].[CategoryLog] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CategoryID] INT NOT NULL,
    [LogID]      INT NOT NULL
);

