using DTO_WeSplit;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Globalization;
using System.Collections.ObjectModel;
using BUS_WeSplit;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for CreateTripPage.xaml
    /// </summary>
    public partial class CreateTripPage : Page
    {
        private DTO_Trip newTrip;
        private ObservableCollection<DTO_Member> AddedMemberList;
        private ObservableCollection<DTO_Member> MemberList;
        private ObservableCollection<DTO_Member> AvailableMemberList;
        private ObservableCollection<DTO_Expense> ExpenseList;

        private double _expenseAmount;
        private string _expenseDescription;
        private string _tripName;


        public EventHandler<AddNewTripEventArgs> AddNewTripEventHandler;

        public string ExpenseAmount
        {
            get => _expenseAmount.ToString();
            set
            {
                try
                {
                    _expenseAmount = Double.Parse(value);
                }
                catch
                {

                }
            }
        }
        public string ExpenseDescription { get => _expenseDescription; set => _expenseDescription = value; }
        public string TripName { get => _tripName; set => _tripName = value; }

        private CreateTripPage()
        {
            InitializeComponent();

        }

        public CreateTripPage(int tripID)
        {
            InitializeComponent();
            AddedMemberList = new ObservableCollection<DTO_Member>();
            MemberList = new ObservableCollection<DTO_Member>(BUS_Member.Instance.GetAllMembers());
            AvailableMemberList = new ObservableCollection<DTO_Member>(BUS_Member.Instance.GetAllMembers());
            ExpenseList = new ObservableCollection<DTO_Expense>();
            newTrip = new DTO_Trip();
            newTrip.TripId = tripID;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListView_Destination.ItemsSource = newTrip.TripDestinationList;
            ListView_MemberList.ItemsSource = AddedMemberList;
            ComboBox_MemberList.ItemsSource = AvailableMemberList;
            ComboBox_MemberListExpense.ItemsSource = MemberList;
            ListView_Expense.ItemsSource = ExpenseList;
        }
        private void CheckBox_Description_Checked(object sender, RoutedEventArgs e)
        {
            LabelTextBox_Description.Visibility = Visibility.Visible;
        }

        private void CheckBox_Description_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelTextBox_Description.Visibility = Visibility.Collapsed;
        }
        private void Button_CreateNewTrip_Click(object sender, RoutedEventArgs e)
        {
            if (AddNewTripEventHandler != null)
            {
                if (LabelTextBox_TripName.Text != "")
                {

                }
                AddNewTripEventHandler(this, new AddNewTripEventArgs(this.newTrip));
            }
            this.NavigationService.GoBack();
        }

        private void ListView_Destination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_Destination_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_AddExpense_Click(object sender, RoutedEventArgs e)
        {
            double amount = Convert.ToDouble(LabelTextBox_ExpenseAmount.Text);
            int memberId = ((DTO_Member)ComboBox_MemberListExpense.SelectedItem).MemberID;
            string description = LabelTextBox_ExpenseDescription.Text;
            DTO_Expense expense;
            if (description != "" && amount != -1)
            {
                expense = new DTO_Expense(newTrip.TripId, amount, memberId, description);
                ExpenseList.Add(expense);
            }

        }

        private void Button_AddMember_Click(object sender, RoutedEventArgs e)
        {
            DTO_Member selected = (DTO_Member)ComboBox_MemberList.SelectedItem;
            if (selected != null)
            {
                AddedMemberList.Add(selected);
                AvailableMemberList.Remove(selected);
            }
        }

        public class AddNewTripEventArgs : EventArgs
        {
            public DTO_Trip NewTrip;

            public AddNewTripEventArgs(DTO_Trip newTrip)
            {
                this.NewTrip = newTrip;
            }
        }
    }
}