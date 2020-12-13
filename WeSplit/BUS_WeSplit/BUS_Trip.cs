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
                DateTime tmpDate = (DateTime)row["TripStartDate"];
                //DateTime date = tmpDate.Date;
                string date = String.Format("{0:dd/MM/yyyy}", tmpDate);
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
                result.Add(tmpTrip);
            }
            return result;
        }

        private List<Tuple<DTO_Trip, String>> GetListDataFromTable_MemberNameSearch(DataTable data)
        {
            List<Tuple<DTO_Trip, String>> result = new List<Tuple<DTO_Trip, String>>();
            foreach (DataRow row in data.Rows)
            {
                int id = int.Parse(row["TripID"].ToString());
                string name = row["TripName"].ToString();
                DateTime tmpDate = (DateTime)row["TripStartDate"];
                //DateTime date = tmpDate.Date;
                string date = String.Format("{0:dd/MM/yyyy}", tmpDate);
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
            DateTime tmpDate = (DateTime)row["TripStartDate"];
            //DateTime date = tmpDate.Date;
            result.TripStartDate = String.Format("{0:dd/MM/yyyy}", tmpDate);
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

            return result;
        }
    }
}
