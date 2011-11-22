CREATE TABLE [dbo].[Countries] (
    [GeoId]      INT           NOT NULL,
    [Name]       NVARCHAR (70) NOT NULL,
    [Code]       SMALLINT      NOT NULL,
    [ISOCode]    NVARCHAR (2)  NULL,
    [NativeName] NVARCHAR (70) NULL,
    [TimeZone]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([GeoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

