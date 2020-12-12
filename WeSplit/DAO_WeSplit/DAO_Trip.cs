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
        public DataTable GetAllTrips()
        {
            DataTable data = new DataTable();
            string query = "select * from Trip";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public void AddTrip(DTO_Trip trip)
        {

            string addTrip = 
                "insert into dbo.Trip(TripID, TripName, TripDescription, TripStartDate, TripEndDate, TripExpenseTotal, TripAverage, TripStatus) values " +
                $"('{trip.TripId}', N'{trip.TripName}', N'{trip.TripDescription}', '{trip.TripStartDate}', '{trip.TripEndDate}', 0, 0, '{trip.TripStatus}')";

            SqlCommand cmd = new SqlCommand(addTrip, _conn);
            cmd.ExecuteNonQuery();
        }
    }
}
