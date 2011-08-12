using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using System.Configuration;
using EyeTracker.DAL.Domain;

namespace EyeTracker.Tests
{
    public static class Utilites
    {
        public static WindsorContainer Container = new WindsorContainer(new XmlInterpreter(ConfigurationManager.AppSettings["WindsorConfigFile"]));

        private static Random random = new Random((int)DateTime.Now.Ticks);
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static decimal RandomDecimal()
        {
            var a = (int)(uint.MaxValue * random.NextDouble());
            var b = (int)(uint.MaxValue * random.NextDouble());
            var c = (int)(uint.MaxValue * random.NextDouble());
            var n = random.NextDouble() > 0.5;
            var s = (byte)(29 * random.NextDouble());
            var res = new Decimal(a, b, c, n, s);
            return res < 0 ? res * -1 : res;
        }

        public static DateTime RandomPastDate()
        {
            return DateTime.Now.AddDays(-random.Next(5, 700));
        }

        public static DateTime RandomFutureDate()
        {
            return DateTime.Now.AddDays(random.Next(5, 700));
        }

        public static UserActivity GenerateUserActivity()
        {
            return new UserActivity()
            {
                Date = Utilites.RandomPastDate(),
                Description = Utilites.RandomString(10),
                LinkedObjectId = null,
                ActivityType = UserActivityType.AddApplication
            };
        }

    }
}
