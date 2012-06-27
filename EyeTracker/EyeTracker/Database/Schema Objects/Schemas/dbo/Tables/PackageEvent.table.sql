/*
CREATE TABLE [dbo].[PackageEvent] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Key]          NVARCHAR (225) NOT NULL,
	[Ip]           NVARCHAR (15)  NULL,
	[LanguageId]   INT			  NULL,
	[CountryId]    INT			  NULL,
	[City]		   NVARCHAR(50)   NULL,
	[OperationSystemId] INT       NULL,
	[BrowserId]    INT			  NULL,
	[ApplicationId] INT           NOT NULL,
    [ScreenWidth]  INT            NOT NULL,
    [ScreenHeight] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);
*/

