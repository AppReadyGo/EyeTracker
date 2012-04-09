CREATE TABLE [dbo].[aspnet_SchemaVersions] (
    [Feature]                 NVARCHAR (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [CompatibleSchemaVersion] NVARCHAR (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [IsCurrentVersion]        BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Feature] ASC, [CompatibleSchemaVersion] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

