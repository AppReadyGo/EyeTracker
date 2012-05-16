ALTER TABLE [log].[CategoryLog]
    ADD CONSTRAINT [FK_CategoryLog_Category] FOREIGN KEY ([CategoryID]) REFERENCES [log].[Category] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

