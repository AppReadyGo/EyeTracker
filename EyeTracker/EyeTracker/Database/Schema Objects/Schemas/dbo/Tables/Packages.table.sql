CREATE TABLE [dbo].[Packages] (
    [ID]			INT IDENTITY (1, 1) NOT NULL,
    [FileName]		NVARCHAR (225) NOT NULL,
    [CreatedDate]	DATETIME       NOT NULL
);
GO;

ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;


