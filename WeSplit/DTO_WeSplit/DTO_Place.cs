using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_WeSplit
{
    public class DTO_Place
    {
        private int _tripId;
        private int _placeId;
        private string _placeName;
        private string _placeAddress;
        private string _placeDescription;

        public int TripId { get => _tripId; set => _tripId = value; }
        public int PlaceId { get => _placeId; set => _placeId = value; }
        public string PlaceName { get => _placeName; set => _placeName = value; }
        public string PlaceAddress { get => _placeAddress; set => _placeAddress = value; }
        public string PlaceDescription { get => _placeDescription; set => _placeDescription = value; }
        public DTO_Place()
        {

        }

        public DTO_Place(int id, string name, string address, string description)
        {
            PlaceId = id;
            PlaceName = name;
            PlaceAddress = address;
            PlaceDescription = description;
        }
    }
}
