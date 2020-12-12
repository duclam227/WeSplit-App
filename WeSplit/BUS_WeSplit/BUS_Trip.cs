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
        private static BUS_Trip _instance = new BUS_Trip();

        public static BUS_Trip Instance { get => _instance; }

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
                DAO_Expense.Instance.AddExpense(expense);
            }

            foreach (string imagePath in trip.TripImages)
            {
                string dir = System.AppDomain.CurrentDomain.BaseDirectory;
                dir += $"resources/{trip.TripId}";
                Utilities.CopyFile(imagePath, dir);
            }

            //todo: change trip expense total to variable 'amount' and change trip average
            
        }
    }
}
