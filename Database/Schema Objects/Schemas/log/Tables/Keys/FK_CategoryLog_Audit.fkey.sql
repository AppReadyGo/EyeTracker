﻿ALTER TABLE [log].[CategoryLog]
    ADD CONSTRAINT [FK_CategoryLog_Audit] FOREIGN KEY ([LogID]) REFERENCES [log].[Log] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

