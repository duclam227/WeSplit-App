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

        public void AddPlace(DTO_Place place)
        {
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {

            }
            string addPlace =
                "insert into dbo.Place(PlaceID, TripID, PlaceName, PlaceAddress, PlaceDescription) values " +
                $"({place.PlaceId},{place.TripId}, N'{place.PlaceName}', N'{place.PlaceAddress}', N'{place.PlaceDescription}')";

            SqlCommand cmd = new SqlCommand(addPlace, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void DeletePlace(DTO_Place place)
        {
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {

            }
            string deletePlace = $"delete from dbo.Place where TripID = {place.TripId} and PlaceID = {place.PlaceId};";

            SqlCommand cmd = new SqlCommand(deletePlace, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdatePlace(int tripId, int placeId, string updateElement, string updateValue)
        {
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {

            }
            string update = $"update dbo.Place set {updateElement} = N'{updateValue}' where TripID = {tripId} and PlaceID = {placeId};";

            SqlCommand cmd = new SqlCommand(update, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
