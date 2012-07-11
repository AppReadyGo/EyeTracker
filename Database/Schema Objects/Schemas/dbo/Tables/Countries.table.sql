CREATE TABLE [dbo].[Countries] (
    [GeoID]      INT           NOT NULL,
    [Name]       NVARCHAR (70) NOT NULL,
    [Code]       SMALLINT      NOT NULL,
    [ISOCode]    NVARCHAR (2)  NULL,
    [NativeName] NVARCHAR (70) NULL,
    [TimeZone]   INT           NOT NULL,
    [Latitude]	FLOAT			NOT NULL,
    [Longitude] FLOAT			NOT NULL,
	[ContinentID]	INT			NOT NULL
    PRIMARY KEY CLUSTERED ([GeoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);
GO

ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [FK_Countries_Continent] FOREIGN KEY ([ContinentID]) REFERENCES [dbo].[Continents] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO


