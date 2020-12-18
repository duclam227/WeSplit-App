using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_WeSplit
{
    public class DTO_Trip
    {

        #region Fields
        private int _tripId;
        private string _tripName;
        private string _tripStartDate;
        private string _tripEndDate;
        private string _tripDescription;
        private double _tripBudget;
        private double _tripAverage;
        private List<DTO_Place> _tripDestinationList;
        private List<DTO_Member> _tripMemberList;
        #endregion

        #region Public Properties
        public int TripId { get => _tripId; set => _tripId = value; }
        public string TripName { get => _tripName; set => _tripName = value; }
        public string TripStartDate { get => _tripStartDate; set => _tripStartDate = value; }
        public string TripDescription { get => _tripDescription; set => _tripDescription = value; }
        public double TripBudget { get => _tripBudget; set => _tripBudget = value; }
        public double TripAverage { get => _tripAverage; set => _tripAverage = value; }
        public List<DTO_Place> TripDestinationList { get => _tripDestinationList; }
        public string TripEndDate { get => _tripEndDate; set => _tripEndDate = value; }
        public List<DTO_Member> TripMemberList { get => _tripMemberList;}
        #endregion

        #region Constructor
        public DTO_Trip()
        {
            _tripDestinationList = new List<DTO_Place>();
            _tripMemberList = new List<DTO_Member>();
        }

        public DTO_Trip(int tripID, string tripName, string tripDate, string tripDescription, double tripBudget, double tripAverage)
        {
            TripId = tripID;
            TripName = tripName;
            TripStartDate = tripDate;
            TripDescription = tripDescription;
            TripBudget = tripBudget;
            TripAverage = tripAverage;
        }

        #endregion

        public void AddDestination(DTO_Place newDestination)
        {
            _tripDestinationList.Add(newDestination);
        }

        public void AddMember(DTO_Member newMember)
        {
            _tripMemberList.Add(newMember);
        }
    }
}
