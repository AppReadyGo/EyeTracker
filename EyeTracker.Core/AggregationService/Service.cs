using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker.Core.AggregationService
{
    public class Service
    {
        public Service()
        {
        }

        /*
         * 1. Wait for exactly hour for example 10:01
         * 2. Get All users settings for local time
         * 3. Take users that have now 24:01
         * 4. Run on selected users 
         * 4.1. Get messages for user from messages database that date is older than today hour:00
         * 4.2. Run clicks and lines merging by min time defined by plan of user, for example if it regular user, so all data can be merged for the day.
         * 4.4. Insert to aggregated database
         * 4.5. Delete old data from aggregated database, defined by plan of user, for example if it is regular user, so delete data older than year
         * 4.5. Delete all messages older than today hour:00 from message database
         * 5. Sleep till next hour
         */
    }
}
