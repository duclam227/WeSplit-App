using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO_WeSplit;

namespace DAO_WeSplit
{
    public class DAO_Expense : DBConnect
    {
        private static DAO_Expense _instance = null;

        public static DAO_Expense Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_Expense();
                }

                return _instance;
            }
        }

        public DataTable GetAllExpensesOfTrip(int tripID)
        {
            DataTable data = new DataTable();
            string query = $"select * from Expense where TripID = {tripID}";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public void AddExpense(DTO_Expense expense)
        {
            string addExpense =
                "insert into dbo.Expense(TripID, ExpenseMoney, ExpenseMember, ExpenseDescription) values " +
                $"({expense.TripId}, '{expense.ExpenseMoney}', {expense.ExpenseMember}', N'{expense.ExpenseDescription}'";

            SqlCommand cmd = new SqlCommand(addExpense, _conn);
            cmd.ExecuteNonQuery();
        }
    }
}
