﻿ALTER TABLE [dbo].[ViewPart]
    ADD CONSTRAINT [FK4AD899C9F9D3FC1C] FOREIGN KEY ([PageViewId]) REFERENCES [dbo].[PageView] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
