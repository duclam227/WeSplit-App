using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_WeSplit
{
    public class DTO_Expense
    {
        private int _tripId;
        private double _expenseMoney;
        private int _expenseMember;
        private string _expenseDescription;

        public int TripId { get => _tripId; set => _tripId = value; }
        public double ExpenseMoney { get => _expenseMoney; set => _expenseMoney = value; }
        public int ExpenseMember { get => _expenseMember; set => _expenseMember = value; }
        public string ExpenseDescription { get => _expenseDescription; set => _expenseDescription = value; }
        public DTO_Expense()
        {

        }

        public DTO_Expense(int id, double money, int member, string description)
        {
            TripId = id;
            ExpenseMoney = money;
            ExpenseMember = member;
            ExpenseDescription = description;
        }
    }
}
