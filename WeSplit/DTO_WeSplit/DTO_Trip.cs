using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_WeSplit
{
    class DTO_Trip
    {
        private int _tripId;
        private string _tripName;
        private string _tripDate;
        private string _tripDescription;
        private double _tripBudget;
        private double _tripAverage;
        private List<DTO_Place> _tripDestinationList;

        public int TripId { get => _tripId; set => _tripId = value; }
        public string TripName { get => _tripName; set => _tripName = value; }
        public string TripDate { get => _tripDate; set => _tripDate = value; }
        public string TripDescription { get => _tripDescription; set => _tripDescription = value; }
        public double TripBudget { get => _tripBudget; set => _tripBudget = value; }
        public double TripAverage { get => _tripAverage; set => _tripAverage = value; }
        public List<DTO_Place> TripDestinationList { get => _tripDestinationList; set => _tripDestinationList = value; }

        public DTO_Trip()
        {

        }

        public DTO_Trip(int id, string name, string date, string description, double budget, double average)
        {
            TripId = id;
            TripName = name;
            TripDate = date;
            TripDescription = description;
            TripBudget = budget;
            TripAverage = average;
        }
    }
}
