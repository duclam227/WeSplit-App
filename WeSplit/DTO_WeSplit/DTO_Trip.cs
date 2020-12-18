using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DTO_WeSplit
{
    public class DTO_Trip
    {

        #region Fields
        private int _tripId;
        private string _tripName;
        private DateTime _tripStartDate;
        private DateTime? _tripEndDate;
        private string _tripDescription;
        private double _tripExpenseTotal;
        private double? _tripAverage;
        private bool _tripStatus;
        List<string> _tripImagesList;

        private List<DTO_Expense> _tripExpenseList;
        private List<DTO_Place> _tripDestinationList;
        private List<DTO_Member> _tripMemberList;
        #endregion

        #region Public Properties
        public int TripId { get => _tripId; set => _tripId = value; }
        public string TripName { get => _tripName; set => _tripName = value; }
        public DateTime TripStartDate { get => _tripStartDate; set => _tripStartDate = value; }
        public string TripDescription { get => _tripDescription; set => _tripDescription = value; }
        public double TripExpenseTotal { get => _tripExpenseTotal; set => _tripExpenseTotal = value; }
        public double? TripAverage { get => _tripAverage; set => _tripAverage = value; }
        public List<DTO_Place> TripDestinationList { get => _tripDestinationList; set => _tripDestinationList = value; }
        public DateTime? TripEndDate { get => _tripEndDate; set => _tripEndDate = value; }
        public List<DTO_Member> TripMemberList { get => _tripMemberList; set => _tripMemberList = value; }
        public List<DTO_Expense> TripExpenseList { get => _tripExpenseList; set => _tripExpenseList = value; }
        public bool TripStatus { get => _tripStatus; set => _tripStatus = value; }
        public List<string> TripImagesList { get => _tripImagesList; set => _tripImagesList = value; }

        #endregion

        #region Constructor
        public DTO_Trip()
        {
            _tripDestinationList = new List<DTO_Place>();
            _tripMemberList = new List<DTO_Member>();
            _tripImagesList = new List<string>();
        }

        public DTO_Trip(int tripID, string tripName, DateTime tripStartDate, string tripDescription, double tripExpenseTotal, double? tripAverage, bool status)
        {
            _tripDestinationList = new List<DTO_Place>();
            _tripMemberList = new List<DTO_Member>();
            _tripImagesList = new List<string>();
            TripId = tripID;
            TripName = tripName;
            TripStartDate = tripStartDate;
            TripDescription = tripDescription;
            TripExpenseTotal = tripExpenseTotal;
            TripAverage = tripAverage;
            TripStatus = status;
        }

        #endregion

        public void AddImage(string imagePath)
        {
            _tripImagesList.Add(imagePath);           
        }
    }
}
