using DTO_WeSplit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAO_WeSplit
{
    public class DAO_Trip : DBConnect
    {
        private static DAO_Trip _instance = new DAO_Trip();

        public static DAO_Trip Instance
        {
            get
            {
                return _instance;
            }
        }
        public List<DTO_Trip> GetAllTrips()
        {
            DataTable data = new DataTable();
            List<DTO_Trip> result = new List<DTO_Trip>();
            string query = "select * from Member";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);


            return result;
        }
    }
}
