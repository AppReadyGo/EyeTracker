﻿ALTER TABLE [dbo].[Application]
    ADD CONSTRAINT [FK6AF3367D1CF53E7] FOREIGN KEY ([PortfolioId]) REFERENCES [dbo].[Portfolio] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
