using EyeTracker.Domain.Model.BackOffice;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace EyeTracker.Domain.Mapping.BackOffice
{
    public class LogMapping : ClassMapping<Log>
    {
        public LogMapping()
        {
            Schema("log");
            Table("Log");
            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.EventID, map => { map.NotNullable(false); });
            Property(x => x.Priority, map => { map.NotNullable(true); });
            Property(x => x.Severity, map => { map.Length(32); map.NotNullable(true); });
            Property(x => x.Title, map => { map.Length(256); map.NotNullable(true); });
            Property(x => x.Timestamp, map => { map.Column("TIMESTAMP"); map.NotNullable(true); });
            Property(x => x.MachineName, map => { map.Length(32); map.NotNullable(true); });
            Property(x => x.AppDomainName, map => { map.Length(512); map.NotNullable(true); });
            Property(x => x.ProcessID, map => { map.Length(256); map.NotNullable(true); });
            Property(x => x.ProcessName, map => { map.Length(512); map.NotNullable(true); });
            Property(x => x.ThreadName, map => { map.Length(512); });
            Property(x => x.Win32ThreadId, map => { map.Length(128); });
            Property(x => x.Message, map => { map.Length(1500); });
            Property(x => x.FormattedMessage, map => { });
            Set(
               x => x.Categories,
               map =>
               {
                   map.Schema("log");
                   map.Table("CategoryLog");
                   map.Access(Accessor.Field);
                   map.Key(x => x.Column("LogID"));
               },
               r => r.ManyToMany(mmp => mmp.Column("CategoryID")));
        }
    }
}
