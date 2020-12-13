using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO_WeSplit;

namespace DAO_WeSplit
{
    public class DAO_Place : DBConnect
    {
        private static DAO_Place _instance = null;

        public static DAO_Place Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_Place();
                }

                return _instance;
            }
        }

        public DataTable GetAllPlacesOfTrip(int tripID)
        {
            DataTable data = new DataTable();
            string query = $"select * from Place where TripID = {tripID}";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

    }
}
