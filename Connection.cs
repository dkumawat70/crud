using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Connection
    {
        public static string SqlConnectionString
        {
            get
            {
                // return string.Empty;
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            }
        }

    }
}
