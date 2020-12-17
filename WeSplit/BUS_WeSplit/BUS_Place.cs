using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_WeSplit;
using DAO_WeSplit;
using System.Data;

namespace BUS_WeSplit
{
    public class BUS_Place
    {
        private static BUS_Place _instance = null;
        public static BUS_Place Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BUS_Place();
                }
                return _instance;
            }

        }

        public static List<DTO_Place> GetPlacesOfTrip(int tripID)
        {
            List<DTO_Place> result = new List<DTO_Place>();
             DataTable data = new DataTable();

            data = DAO_Place.Instance.GetAllPlacesOfTrip(tripID);
           
            foreach (DataRow row in data.Rows)
            {
                DTO_Place tmpPlace = new DTO_Place();
                tmpPlace.TripId = tripID;
                tmpPlace.PlaceId = int.Parse(row["PlaceID"].ToString());
                tmpPlace.PlaceDescription = row["PlaceDescription"].ToString();
                tmpPlace.PlaceName = row["PlaceName"].ToString();
                tmpPlace.PlaceAddress = row["PlaceAddress"].ToString();
                result.Add(tmpPlace);
            }

            return result;
        }

        public void AddPlace(DTO_Place newPlace)
        {
            DAO_Place.Instance.AddPlace(newPlace);
        }

        public void DeletePlace(DTO_Place place)
        {
            DAO_Place.Instance.DeletePlace(place);
        }

        public void UpdatePlace(int tripId, int placeId, string updateElement, string updateValue)
        {
            DAO_Place.Instance.UpdatePlace(tripId, placeId, updateElement, updateValue);
        }
    }
}
