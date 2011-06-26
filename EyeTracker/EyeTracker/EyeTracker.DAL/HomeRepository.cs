using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace EyeTracker.DAL
{
    public class HomeRepository
    {
        public void Subscribe(string email)
        {
            var database = DatabaseFactory.CreateDatabase();
            using (DbCommand command = database.GetStoredProcCommand("Subscribtion"))
            {
                database.AddInParameter(command, "Subscribtion_Email", DbType.String, email);

                database.ExecuteNonQuery(command);
            }
        }
    }
}
