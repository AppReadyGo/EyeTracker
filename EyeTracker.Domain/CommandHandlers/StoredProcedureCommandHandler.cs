using System;
using System.Text;
using EyeTracker.Common.Commands.Admin;
using NHibernate;

namespace EyeTracker.Domain.CommandHandlers
{
    public abstract class StoredProcedureCommandHandler
    {
        protected void ExecuteStoredProcedure(ISession session, ClearLogCommand cmd, string scheme = null)
        {
            var sql = new StringBuilder("EXEC ");
            if (!string.IsNullOrEmpty(scheme))
            {
                sql.AppendFormat("{0}.", scheme);
            }
            var cmdType = cmd.GetType();
            sql.Append(cmdType.Name.Replace("Command", string.Empty));
            foreach (var propInfo in cmdType.GetProperties(System.Reflection.BindingFlags.GetProperty))
            {
                string value = string.Empty;
                switch (propInfo.DeclaringType.Name)
                {
                    case "Int":
                        value = propInfo.GetValue(cmd, null).ToString();
                        break;
                    case "DateTime":
                        value = "'" + ((DateTime)propInfo.GetValue(cmd, null)).ToString("yyyyMMdd HH:mm:ss") + "'";
                        break;
                    case "String":
                        value = "'" + propInfo.GetValue(cmd, null).ToString() + "'";
                        break;
                }
                sql.AppendFormat(" @{0}={1}", propInfo.Name, value);
            }
            var query = session.CreateSQLQuery(sql.ToString());
            query.ExecuteUpdate();
        }
    }
}
