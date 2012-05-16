ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [ypanshin_mobillify_qa_log], FILENAME = '$(DefaultLogPath)$(DatabaseName)_log.ldf', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

