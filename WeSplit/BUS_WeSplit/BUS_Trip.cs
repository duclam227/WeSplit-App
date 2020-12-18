using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_WeSplit;
using DAO_WeSplit;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;

namespace BUS_WeSplit
{
    public class BUS_Trip
    {
        private static BUS_Trip _instance = null;

        public BUS_Trip() { }
        public static BUS_Trip Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BUS_Trip();
                }
                return _instance;
            }
           
        }

        private List<DTO_Trip> GetListDataFromTable(DataTable data)
        {
            List<DTO_Trip> result = new List<DTO_Trip>();
            foreach (DataRow row in data.Rows)
            {
                int id = int.Parse(row["TripID"].ToString());
                string name = row["TripName"].ToString();
                DateTime date = (DateTime)row["TripStartDate"];
                //DateTime date = tmpDate.Date;
                string description = row["TripDescription"].ToString();
                double expenseTotal = double.Parse(row["TripExpenseTotal"].ToString());
                string tmpAverage = row["TripAverage"].ToString();
                double? average = new double();
                if (tmpAverage != "")
                {
                    average = double.Parse(tmpAverage);
                }
                string tmpStatus = row["TripStatus"].ToString();
                bool status = false;
                if (tmpStatus == "True")
                {
                    status = true;
                }
                else
                {
                    //do nothing
                }

                DTO_Trip tmpTrip = new DTO_Trip(id, name, date, description, expenseTotal, average, status);
                tmpTrip.TripImagesList = GetImagesOfTripAsStrings(id);

                result.Add(tmpTrip);
            }
            return result;
        }

        public int GetNumberOfTrips()
        {
            int result;
            DataTable data = new DataTable();

            data = DAO_Trip.Instance.GetNumberOfTrips();
            DataRow row = data.Rows[0];

            result = int.Parse(row["Amount"].ToString());

            return result;
        }

        private List<Tuple<DTO_Trip, String>> GetListDataFromTable_MemberNameSearch(DataTable data)
        {
            List<Tuple<DTO_Trip, String>> result = new List<Tuple<DTO_Trip, String>>();
            foreach (DataRow row in data.Rows)
            {
                int id = int.Parse(row["TripID"].ToString());
                string name = row["TripName"].ToString();
                DateTime date = (DateTime)row["TripStartDate"];
                //DateTime date = tmpDate.Date;
                string description = row["TripDescription"].ToString();
                double expenseTotal = double.Parse(row["TripExpenseTotal"].ToString());
                string tmpAverage = row["TripAverage"].ToString();
                double? average = new double();
                if (tmpAverage != "")
                {
                    average = double.Parse(tmpAverage);
                }
                String memberName = row["MemberName"].ToString();
                string tmpStatus = row["TripStatus"].ToString();
                bool status = false;
                if (tmpStatus == "True")
                {
                    status = true;
                }
                else
                {
                    //do nothing
                }

                DTO_Trip tmpTrip = new DTO_Trip(id, name, date, description, expenseTotal, average, status);
                Tuple<DTO_Trip, String> temp = new Tuple<DTO_Trip, String>(tmpTrip, memberName);
                result.Add(temp);
            }
            return result;
        }

        public List<DTO_Trip> GetAllTrips()
        {
            DataTable data;
            data = DAO_Trip.Instance.GetAllTrips();

            List<DTO_Trip> result;
            result = GetListDataFromTable(data);

            return result;
        }

        public List<DTO_Trip> GetUnfinishedTrips()
        {
            List<DTO_Trip> result = new List<DTO_Trip>();
            DataTable data = new DataTable();

            data = DAO_Trip.Instance.GetUnfinishedTrips();

            result = GetListDataFromTable(data);

            return result;
        }

        public List<DTO_Trip> GetFinishedTrips()
        {
            List<DTO_Trip> result = new List<DTO_Trip>();
            DataTable data = new DataTable();

            data = DAO_Trip.Instance.GetFinishedTrips();

            result = GetListDataFromTable(data);

            return result;
        }

        public List<DTO_Trip> SearchTripsByName(String tripName)
        {
            DataTable data;
            data = DAO_Trip.Instance.SearchTripsByName(tripName);
            var resultFromDB = GetListDataFromTable(data);
            return resultFromDB;
        }

        public List<Tuple<DTO_Trip,String>> SearchTripsByMember(String memberName)
        {
            DataTable data;
            data = DAO_Trip.Instance.SearchTripsByMember(memberName);

            var resultFromDB = GetListDataFromTable_MemberNameSearch(data);
            return resultFromDB;
        }

        public DTO_Trip GetTripByID(int id)
        {
            DTO_Trip result = new DTO_Trip();

            DataTable data = DAO_Trip.Instance.GetTripByID(id);
            DataRow row = data.Rows[0];

            result.TripId = int.Parse(row["TripID"].ToString());
            result.TripName = row["TripName"].ToString();
            result.TripStartDate = (DateTime)row["TripStartDate"];
            result.TripDescription = row["TripDescription"].ToString();
            result.TripExpenseTotal = double.Parse(row["TripExpenseTotal"].ToString());
            string tmpAverage = row["TripAverage"].ToString();
            double? average = new double();
            if (tmpAverage != "")
            {
                average = double.Parse(tmpAverage);
            }
            result.TripAverage = average;

            result.TripDestinationList = BUS_Place.GetPlacesOfTrip(id);
            result.TripExpenseList = BUS_Expense.GetExpensesOfTrip(id);
            result.TripImagesList = GetImagesOfTripAsStrings(id);

            return result;
        }

        public void AddTrip(DTO_Trip trip)
        {
            DAO_Trip.Instance.AddTrip(trip);

            foreach (DTO_Place place in trip.TripDestinationList)
            {
                DAO_Place.Instance.AddPlace(place);
            }

            foreach (DTO_Member member in trip.TripMemberList)
            {
                DAO_Member.Instance.AddMemberPerTrip(member.MemberID, trip.TripId);
            }

            double amount = 0;
            foreach (DTO_Expense expense in trip.TripExpenseList)
            {
                amount += expense.ExpenseMoney;
                DAO_Expense.Instance.AddExpense(expense, trip.TripId);
            }

            int i = 0;
            foreach (string imagePath in trip.TripImagesList)
            {
                string dir = System.AppDomain.CurrentDomain.BaseDirectory;
                dir += $@"resources\{trip.TripId}";
                Utilities.CopyFile(imagePath, dir);

                DAO_Trip.Instance.AddImageToTrip(trip.TripId, i, Path.GetFileName(imagePath));
                i++;
            }
        }

        public double CalculateAverage(int tripID)
        {
            double result;
            int noOfMembers = BUS_Member.Instance.GetAmountOfMember(tripID);
            if(noOfMembers == 0)
            {
                result = 0;
            }
            else
            {
                int expenseTotal = GetExpenseTotal(tripID);

                result = expenseTotal / noOfMembers;
            }
            
            return result;
        }

        public void AddAverageToTrip(int tripID, double average)
        {
            DAO_Trip.Instance.AddAverageToTrip(tripID, average);
        }

        public int GetExpenseTotal(int tripID)
        {
            int result;

            DataTable data = DAO_Trip.Instance.GetExpenseTotal(tripID);

            DataRow row = data.Rows[0];

            result = int.Parse(row["TripExpenseTotal"].ToString());

            return result;
        }
    
        public void AddImageToTrip(int tripID, int imageID, string filename)
        {
            DAO_Trip.Instance.AddImageToTrip(tripID, imageID, filename);
        }

        public List<BitmapImage> GetImagesOfTrip(int tripID)
        {
            List<BitmapImage> result = new List<BitmapImage>();

            DataTable data = DAO_Trip.Instance.GetImagesOfTrip(tripID);

            foreach(DataRow row in data.Rows)
            {
                string tmpName = row["ImageName"].ToString();
                string dir = System.AppDomain.CurrentDomain.BaseDirectory;
                dir += $@"resources\{tripID}\";
                string filepath = dir + tmpName;
                BitmapImage tmpImage = new BitmapImage(new Uri(filepath, UriKind.Absolute));
                result.Add(tmpImage);
            }

            return result;
        }

        public List<string> GetImagesOfTripAsStrings(int tripID)
        {
            List<string> result = new List<string>();

            DataTable data = DAO_Trip.Instance.GetImagesOfTrip(tripID);

            foreach (DataRow row in data.Rows)
            {
                string tmpName = row["ImageName"].ToString();
                string dir = System.AppDomain.CurrentDomain.BaseDirectory;
                dir += $@"resources\{tripID}\";
                string filepath = dir + tmpName;
                result.Add(filepath);
            }

            return result;
        }

        public string GetHeaderImage(int tripID)
        {
            string result;
            DataTable data = DAO_Trip.Instance.GetImagesOfTrip(tripID);
            DataRow row = data.Rows[0];

            string tmpName = row["ImageName"].ToString();
            string dir = System.AppDomain.CurrentDomain.BaseDirectory;
            dir += $@"resources\{tripID}\";
            result = dir + tmpName;

            return result;
        }
    }
}
