CREATE TABLE [log].[Log] (
    [ID]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EventID]          INT             NULL,
    [Priority]         INT             NOT NULL,
    [Severity]         NVARCHAR (32)   NOT NULL,
    [Title]            NVARCHAR (256)  NOT NULL,
    [TIMESTAMP]        DATETIME        NOT NULL,
    [MachineName]      NVARCHAR (32)   NOT NULL,
    [AppDomainName]    NVARCHAR (512)  NOT NULL,
    [ProcessID]        NVARCHAR (256)  NOT NULL,
    [ProcessName]      NVARCHAR (512)  NOT NULL,
    [ThreadName]       NVARCHAR (512)  NULL,
    [Win32ThreadId]    NVARCHAR (128)  NULL,
    [Message]          NVARCHAR (1500) NULL,
    [FormattedMessage] NVARCHAR (MAX)  NULL
);

