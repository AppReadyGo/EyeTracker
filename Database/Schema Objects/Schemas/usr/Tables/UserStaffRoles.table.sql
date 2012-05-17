CREATE TABLE [usr].[UserStaffRoles] (
    [UserID] INT NOT NULL,
    [RoleID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC, [RoleID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);
GO

ALTER TABLE [usr].[UserStaffRoles]
    ADD CONSTRAINT [FK_UserStaffRoles_Users] FOREIGN KEY ([UserID]) REFERENCES [usr].[Users] ([ID]);
GO

ALTER TABLE [usr].[UserStaffRoles]
    ADD CONSTRAINT [FK_UserStaffRoles_StaffRoles] FOREIGN KEY ([RoleID]) REFERENCES [usr].[StaffRoles] ([ID]);
GO


