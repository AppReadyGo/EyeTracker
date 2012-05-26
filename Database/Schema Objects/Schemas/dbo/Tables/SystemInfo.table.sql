CREATE TABLE [dbo].[SystemInfo]
(
	[Id] int IDENTITY (1, 1) NOT NULL,
	[OperationSystemId] int NOT NULL,
	[BrandName] NVARCHAR (100) NULL,
	[DeviceName] NVARCHAR (100) NULL,
	[DisplayName] NVARCHAR (100) NULL,
	[FingerprintName] NVARCHAR (100) NULL,
	[HardwareName] NVARCHAR (100) NULL,
	[ManufactureName] NVARCHAR (100) NULL,
	[ProductName] NVARCHAR (100) NULL,
	[SdkIdentName] NVARCHAR (100) NULL,
	[RealVersionName] NVARCHAR (100) NULL,
	[InternalName] NVARCHAR (100) NULL,
	[DevCodeName] NVARCHAR (100) NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
)
