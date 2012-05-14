using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace EyeTracker.Common
{
    public enum ApplicationEvent
    {
        FatalError = 1,
        Error
    }

    public enum DataGrouping
    {
        Minute,
        Hour,
        Day,
        Month,
        Year
    }

    public enum ErrorCode
    {
        WrongNameParameter,
        WrongImportData,
        WrongEmail,
        WrongPassword,
        EmailExists,
        TagExists,
        WrongCollection,
        WrongParameter
    }
}
