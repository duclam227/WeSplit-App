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
    public class BUS_Expense
    {
        private static BUS_Expense _instance = null;
        public static BUS_Expense Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BUS_Expense();
                }
                return _instance;
            }
        }

        public static List<DTO_Expense> GetExpensesOfTrip(int tripID)
        {
            List<DTO_Expense> result = new List<DTO_Expense>();
            DataTable data = new DataTable();

            data = DAO_Expense.Instance.GetAllExpensesOfTrip(tripID);

            foreach (DataRow row in data.Rows)
            {
                DTO_Expense tmpExpense = new DTO_Expense();
                tmpExpense.TripId = tripID;
                tmpExpense.ExpenseDescription = row["ExpenseDescription"].ToString();
                tmpExpense.ExpenseMember = int.Parse(row["ExpenseMember"].ToString());
                tmpExpense.ExpenseMoney = double.Parse(row["ExpenseMoney"].ToString());
                tmpExpense.ExpenseId = int.Parse(row["ExpenseID"].ToString());
                tmpExpense.ExpenseDate = (DateTime)row["ExpenseDate"];
                result.Add(tmpExpense);
            }

            return result;
        }
    }
}
