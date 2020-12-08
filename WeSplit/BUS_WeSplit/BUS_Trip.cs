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
                DateTime tmpDate = (DateTime)row["TripDate"];
                string date = String.Format("{0:dd/MM/yyyy}", tmpDate);
                string description = row["TripDescription"].ToString();
                double budget = double.Parse(row["TripBudget"].ToString());
                double average = double.Parse(row["TripAverage"].ToString());

                DTO_Trip tmpTrip = new DTO_Trip(id, name, date, description, budget, average);
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
                DateTime tmpDate = (DateTime)row["TripDate"];
                string date = String.Format("{0:dd/MM/yyyy}", tmpDate);
                string description = row["TripDescription"].ToString();
                double budget = double.Parse(row["TripBudget"].ToString());
                double average = double.Parse(row["TripAverage"].ToString());
                String memberName = row["MemberName"].ToString();

                DTO_Trip tmpTrip = new DTO_Trip(id, name, date, description, budget, average);
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
    }
}
