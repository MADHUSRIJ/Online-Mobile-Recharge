using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Online_Mobile_Recharge
{
    public class Helpers
    {
        public Helpers() { }
        public string GetRDSConnectionString()
        {
            Console.WriteLine("hello there");
            var appConfig = System.Configuration.ConfigurationManager.AppSettings;

            string dbname = appConfig["RDS_DB_NAME"];

            Console.WriteLine(dbname);

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["RDS_USERNAME"];
            string password = appConfig["RDS_PASSWORD"];
            string hostname = appConfig["RDS_HOSTNAME"];
            string port = appConfig["RDS_PORT"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}