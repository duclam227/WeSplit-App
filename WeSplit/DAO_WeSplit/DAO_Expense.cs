using DTO_WeSplit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_WeSplit
{
    public class DAO_Expense : DBConnect
    {
        private static DAO_Expense _instance = new DAO_Expense();

        public static DAO_Expense Instance
        {
            get
            {
                return _instance;
            }
        }
        public DataTable GetAllExpense(int tripId)
        {
            DataTable data = new DataTable();
            string query = $"select * from ExpensePerTrip where TripID = '{tripId}'";

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
