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
            try
            {
                _conn.Open();
            }
            catch(Exception ex)
            {

            }
            string addExpense =
                "insert into dbo.Expense(TripID, ExpenseID, ExpenseMoney, ExpenseMember, ExpenseDescription, ExpenseDate) values " +
                $"({expense.TripId}, {expense.ExpenseId + 1}, {expense.ExpenseMoney}, {expense.ExpenseMember}, N'{expense.ExpenseDescription}', '{expense.ExpenseDate}')";

            SqlCommand cmd = new SqlCommand(addExpense, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void AddExpense(DTO_Expense expense, int tripID)
        {
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {

            }
            string addExpense =
                "insert into dbo.Expense(TripID, ExpenseID, ExpenseMoney, ExpenseMember, ExpenseDescription, ExpenseDate) values " +
                $"({tripID}, {expense.ExpenseId + 1}, {expense.ExpenseMoney}, {expense.ExpenseMember}, N'{expense.ExpenseDescription}', '{expense.ExpenseDate}')";

            SqlCommand cmd = new SqlCommand(addExpense, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteExpense(DTO_Expense expense)
        {
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {

            }
            string deleteExpense = $"delete from dbo.Expense where TripID = {expense.TripId} and ExpenseID = {expense.ExpenseId};";

            SqlCommand cmd = new SqlCommand(deleteExpense, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateExpenseMoney(int tripId, int expenseId, double v)
        {
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {

            }
            string update = $"update dbo.Expense set ExpenseMoney = {v} where TripID = {tripId} and ExpenseID = {expenseId};";

            SqlCommand cmd = new SqlCommand(update, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateExpenseDescription(int tripId, int expenseId, string updateValue)
        {

            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {

            }
            string update = $"update dbo.Expense set ExpenseDescription = N'{updateValue}' where TripID = {tripId} and ExpenseID = {expenseId};";

            SqlCommand cmd = new SqlCommand(update, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }

        }

        public void UpdateExpenseDate(int tripId, int expenseId, DateTime updateValue)
        {
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {

            }
            string update = $"update dbo.Expense set ExpenseDate = {updateValue} where TripID = {tripId} and ExpenseID = {expenseId};";

            SqlCommand cmd = new SqlCommand(update, _conn);
            cmd.ExecuteNonQuery();

            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
