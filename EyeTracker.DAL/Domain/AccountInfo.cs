using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.DAL.Domain
{
    public class AccountInfo
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public short TimeZone { get; set; }
    }
}
