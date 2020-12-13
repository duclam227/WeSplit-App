using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_WeSplit;

namespace DAO_WeSplit
{
    public class DAO_Trip : DBConnect
    {
        private static DAO_Trip _instance = null;

        public static DAO_Trip Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_Trip();
                }

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

        public DataTable GetUnfinishedTrips()
        {
            DataTable data = new DataTable();
            string query = "select * from Trip where TripStatus = 0";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public DataTable GetFinishedTrips()
        {
            DataTable data = new DataTable();
            string query = "select * from Trip where TripStatus = 1";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public DataTable SearchTripsByName(String name)
        {
            DataTable data = new DataTable();
            string query = $"Select * from Trip where TripName like N'%{name}%'";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;

        }

        public DataTable SearchTripsByMember(String memberName)
        {
            DataTable data = new DataTable();
            string query = $"Select t.*, m.MemberName from Trip t, Member m, MemberPerTrip mpt " +
                            $"where t.TripID = mpt.TripID and m.MemberID = mpt.MemberID and " +
                            $"m.MemberName like N'%{memberName}%'";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;

        }

        public DataTable GetTripByID(int tripId)
        {
            DataTable data = new DataTable();
            string query = $"select * from Trip where TripID = {tripId}";

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
