CREATE TABLE [api].[Roles] (
    [ID] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL
);
GO;

ALTER TABLE [api].[Roles]
ADD PRIMARY KEY CLUSTERED ([Id] ASC);
GO;
