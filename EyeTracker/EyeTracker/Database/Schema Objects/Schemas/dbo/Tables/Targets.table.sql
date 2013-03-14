CREATE TABLE [dbo].[Targets] (
    [ID]	INT				IDENTITY (1, 1) NOT NULL,
    [Name]	NVARCHAR (50)	NOT NULL,
);
GO;

ALTER TABLE [dbo].[Targets]
ADD CONSTRAINT [PK_Targets] PRIMARY KEY CLUSTERED ([Id] ASC);
GO;

