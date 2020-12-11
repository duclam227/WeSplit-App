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
        private List<DTO_Expense> _tripExpenseList;
        private List<string> _tripImages;
        #endregion

        #region Public Properties
        public int TripId { get => _tripId; set => _tripId = value; }
        public string TripName { get => _tripName; set => _tripName = value; }
        public string TripStartDate { get => _tripStartDate; set => _tripStartDate = value; }
        public string TripDescription { get => _tripDescription; set => _tripDescription = value; }
        public double TripBudget { get => _tripBudget; set => _tripBudget = value; }
        public double TripAverage { get => _tripAverage; set => _tripAverage = value; }
        public string TripEndDate { get => _tripEndDate; set => _tripEndDate = value; }
        public List<DTO_Place> TripDestinationList { get => _tripDestinationList; set => _tripDestinationList = value; }
        public List<DTO_Member> TripMemberList { get => _tripMemberList; set => _tripMemberList = value; }
        public List<DTO_Expense> TripExpenseList { get => _tripExpenseList; set => _tripExpenseList = value; }
        public List<string> TripImages { get => _tripImages; set => _tripImages = value; }

        #endregion

        #region Constructor
        public DTO_Trip()
        {
            TripDestinationList = new List<DTO_Place>();
            TripMemberList = new List<DTO_Member>();
        }

        #endregion

    }
}
