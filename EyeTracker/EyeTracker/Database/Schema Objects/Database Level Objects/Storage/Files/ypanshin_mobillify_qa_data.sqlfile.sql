ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [ypanshin_mobillify_qa_data], FILENAME = '$(DefaultDataPath)$(DatabaseName)_data.mdf', FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

