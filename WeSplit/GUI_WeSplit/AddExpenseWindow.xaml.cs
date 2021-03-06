﻿using DTO_WeSplit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for AddExpenseWindow.xaml
    /// </summary>
    public partial class AddExpenseWindow : Window
    {
        private string _expenseDescription;
        private string _expenseAmount;
        private int _tripId;
        private int _expenseId;

        public EventHandler<AddNewExpenseEventArgs> AddExpenseEventHandler;
        public string ExpenseDescription { get => _expenseDescription; set => _expenseDescription = value; }
        public string ExpenseAmount { get => _expenseAmount; set => _expenseAmount = value; }

        private AddExpenseWindow()
        {
            InitializeComponent();
        }

        public AddExpenseWindow(int expenseId, ObservableCollection<DTO_Member> list)
        {
            InitializeComponent();
            DataContext = this;
            ComboBox_MemberListExpense.ItemsSource = list;
            _expenseId = expenseId;
        }

        public AddExpenseWindow(int tripId, int expenseId, List<DTO_Member> list)
        {
            InitializeComponent();
            _tripId = tripId;
            _expenseId = expenseId;
            DataContext = this;
            ComboBox_MemberListExpense.ItemsSource = list;
        }

        private void Button_AddExpense_Click(object sender, RoutedEventArgs e)
        {
            bool canReturn = true;
            double amount = -1;

            try
            {
                amount = Double.Parse(ExpenseAmount);
            }
            catch
            {
                canReturn = false;
            }

            if (ComboBox_MemberListExpense.SelectedItem == null || String.IsNullOrWhiteSpace(ExpenseDescription))
                canReturn = false;

            if (DatePicker_ExpenseDate.SelectedDate == null)
                DatePicker_ExpenseDate.SelectedDate = System.DateTime.Today;

            if (AddExpenseEventHandler != null && canReturn)
            {
                DTO_Expense newExpense = new DTO_Expense();
                newExpense.TripId = _tripId;
                newExpense.ExpenseId = _expenseId;
                newExpense.ExpenseDescription = ExpenseDescription;
                newExpense.ExpenseMoney = amount;
                newExpense.ExpenseMember = ((DTO_Member)ComboBox_MemberListExpense.SelectedItem).MemberID;
                newExpense.ExpenseDate = (DateTime)DatePicker_ExpenseDate.SelectedDate;
                AddExpenseEventHandler(this, new AddNewExpenseEventArgs(newExpense));
                this.Close();
            }
        }

        public class AddNewExpenseEventArgs : EventArgs
        {
            public DTO_Expense NewExpense;

            public AddNewExpenseEventArgs(DTO_Expense expense)
            {
                NewExpense = expense;
            }
        }

    }
}
