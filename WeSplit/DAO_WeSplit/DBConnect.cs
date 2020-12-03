using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DAO_WeSplit
{
    public class DBConnect
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["WeSplitDBConnectionString"].ConnectionString;
        protected SqlConnection _conn = new SqlConnection(connectionString);
    }
}
