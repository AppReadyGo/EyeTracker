﻿ALTER TABLE [dbo].[PageView]
    ADD CONSTRAINT [FK5DAF59DAAF84173A] FOREIGN KEY ([OperationSystemId]) REFERENCES [dbo].[OperationSystem] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
