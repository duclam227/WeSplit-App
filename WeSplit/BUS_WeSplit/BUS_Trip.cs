using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_WeSplit;
using DAO_WeSplit;

namespace BUS_WeSplit
{
    public class BUS_Trip
    {
        private static BUS_Trip _instance = new BUS_Trip();

        public static BUS_Trip Instance { get => _instance; }

        public List<DTO_Trip> GetAllTrips()
        {
            List<DTO_Trip> result;
            result = DAO_Trip.Instance.GetAllTrips();
            return result;
        }
    }
}
