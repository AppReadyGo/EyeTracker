﻿ALTER TABLE [dbo].[ScrollEvent]
    ADD CONSTRAINT [FK5249FB21F2B6C1ED] FOREIGN KEY ([LastTouchId]) REFERENCES [dbo].[ClickEvent] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
