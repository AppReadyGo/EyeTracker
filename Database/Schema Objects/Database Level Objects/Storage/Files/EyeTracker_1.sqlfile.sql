ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [EyeTracker_1], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

