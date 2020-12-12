using DTO_WeSplit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_WeSplit
{
    public class DAO_Place : DBConnect
    {
        private static DAO_Place _instance = new DAO_Place();

        public static DAO_Place Instance
        {
            get
            {
                return _instance;
            }
        }

        public DataTable GetAllPlace(int tripId)
        {
            DataTable data = new DataTable();
            string query = $"select * from PlacePerTrip where TripId = '{tripId}'";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public void AddPlace(DTO_Place place)
        {
            string addPlace =
                "insert into dbo.Place(PlaceID, PlaceName, PlaceAddress, PlaceDescription) values " +
                $"({place.PlaceId}, N'{place.PlaceName}', N'{place.PlaceAddress}', N'{place.PlaceDescription}'";

            SqlCommand cmd = new SqlCommand(addPlace, _conn);
            cmd.ExecuteNonQuery();
        }
    }
}
