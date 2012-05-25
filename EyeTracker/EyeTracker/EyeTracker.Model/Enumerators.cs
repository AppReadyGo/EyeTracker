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

    [Flags]
    public enum StaffRole
    {
        Administrator = 1
    }

    public enum UserType : byte
    {
        Staff = 1,
        Member
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
