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
using System.Windows.Forms;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for CreateTripPage.xaml
    /// </summary>
    public partial class CreateTripPage : Page
    {
        private DTO_Trip newTrip;

        private ObservableCollection<DTO_Member> AvailableMemberList;
        private ObservableCollection<BitmapImage> Images;
        public ObservableCollection<DTO_Place> DestinationList;
        public ObservableCollection<DTO_Expense> ExpenseList;
        public ObservableCollection<DTO_Member> MemberList;

        private List<string> imagesList = new List<string>();

        private string _tripName;
        private string _tripDescription;
        public string TripName { get => _tripName; set => _tripName = value; }
        public string TripDescription { get => _tripDescription; set => _tripDescription = value; }

        private CreateTripPage()
        {
            InitializeComponent();

        }

        public CreateTripPage(int tripID)
        {
            InitializeComponent();

            Images = new ObservableCollection<BitmapImage>();
            MemberList = new ObservableCollection<DTO_Member>();
            AvailableMemberList = new ObservableCollection<DTO_Member>(BUS_Member.Instance.GetAllMembers());
            DestinationList = new ObservableCollection<DTO_Place>();
            ExpenseList = new ObservableCollection<DTO_Expense>();
            DestinationList = new ObservableCollection<DTO_Place>();

            newTrip = new DTO_Trip()
            {
                TripId = tripID
            };

            DataContext = this;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_Destination.ItemsSource = DestinationList;
            DataGrid_MemberList.ItemsSource = MemberList;
            ComboBox_MemberList.ItemsSource = AvailableMemberList;
            DataGrid_ExpenseList.ItemsSource = ExpenseList;
            carouselImages.ItemsSource = Images;
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
            bool canReturn = true;

            if(imagesList.Count() == 0)
            {
                canReturn = false;
            }

            if (String.IsNullOrWhiteSpace(TripName))
            {
                canReturn = false;
            }

            if (CheckBox_Description.IsChecked == true && String.IsNullOrWhiteSpace(TripDescription))
            {
                canReturn = false;
            }

            if (DatePicker_EndDate.SelectedDate == null || DatePicker_StartDate.SelectedDate == null)
            {
                canReturn = false;
            }

            if (canReturn)
            {
                newTrip.TripName = TripName;
                newTrip.TripDescription = TripDescription;
                newTrip.TripStartDate = (DateTime)DatePicker_StartDate.SelectedDate;
                if(DatePicker_EndDate.SelectedDate != null)
                {
                    newTrip.TripEndDate = (DateTime)DatePicker_EndDate.SelectedDate;
                    newTrip.TripStatus = false;
                }
                else
                {
                    newTrip.TripEndDate = null;
                    newTrip.TripStatus = true;
                }
                newTrip.TripExpenseList = ExpenseList.ToList<DTO_Expense>();
                newTrip.TripDestinationList = DestinationList.ToList<DTO_Place>();
                newTrip.TripStatus = true;
                newTrip.TripImagesList = imagesList;
                newTrip.TripMemberList = MemberList.ToList();

                BUS_Trip.Instance.AddTrip(newTrip);
                BUS_Trip.Instance.AddAverageToTrip(newTrip.TripId, BUS_Trip.Instance.CalculateAverage(newTrip.TripId));

                this.NavigationService.GoBack();
            }
            else
            {
                System.Windows.MessageBox.Show("Bạn chưa điền đầy đủ thông tin");
            }
        }
        private void Button_AddExpense_Click(object sender, RoutedEventArgs e)
        {
            AddExpenseWindow addExpenseWindow = new AddExpenseWindow(ExpenseList.Count, MemberList);
            addExpenseWindow.AddExpenseEventHandler += (s, args) =>
                {
                    ExpenseList.Add(args.NewExpense);
                };
            addExpenseWindow.ShowDialog();
        }
        private void Button_AddMember_Click(object sender, RoutedEventArgs e)
        {
            DTO_Member selected = (DTO_Member)ComboBox_MemberList.SelectedItem;
            if (selected != null)
            {
                MemberList.Add(selected);
                AvailableMemberList.Remove(selected);
            }
        }
        private void Button_AddDestination_Click(object sender, RoutedEventArgs e)
        {
            int placeId = this.DestinationList.Count();
            AddDestinationWindow addDestinationWindow = new AddDestinationWindow(newTrip.TripId, placeId);
            addDestinationWindow.AddDestinationEventHandler += (s, args) =>
              {
                  DestinationList.Add(args.NewDestination);
              };
            addDestinationWindow.ShowDialog();
        }
        private void Button_AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
          "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
          "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                BitmapImage img = new BitmapImage(new Uri(filename, UriKind.Absolute));
                imagesList.Add(filename);
                Images.Add(img);
                
            }
        }
        private void LabelTextBox_TripName_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
    
}