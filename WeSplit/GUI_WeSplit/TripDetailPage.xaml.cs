using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DTO_WeSplit;
using BUS_WeSplit;
using System.Collections.ObjectModel;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for TripDetailPage.xaml
    /// </summary>
    public partial class TripDetailPage : Page
    {
        private DTO_WeSplit.DTO_Trip trip = new DTO_WeSplit.DTO_Trip();
        private List<Tuple<DTO_WeSplit.DTO_Member, double?, double?>> listOfMember = new List<Tuple<DTO_WeSplit.DTO_Member, double?, double?>>();

        ObservableCollection<DTO_Place> PlaceList;
        ObservableCollection<DTO_Expense> ExpenseList;
        ObservableCollection<BitmapImage> ImagesList;
        
        public TripDetailPage()
        {
            InitializeComponent();
        }

        public TripDetailPage(int id)
        {
            InitializeComponent();
            trip = BUS_WeSplit.BUS_Trip.Instance.GetTripByID(id);
            listOfMember = BUS_WeSplit.BUS_Member.Instance.GetMembersOfTrip(id);
            trip.TripMemberList = BUS_Member.Instance.GetMembersPerTrip(id);
            this.DataContext = trip;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = trip;
            PlaceList = new ObservableCollection<DTO_Place>(trip.TripDestinationList);
            ExpenseList  = new ObservableCollection<DTO_Expense>(trip.TripExpenseList);
            ImagesList = new ObservableCollection<BitmapImage>(Utilities.FilePathListToBitmapImageList(trip.TripImagesList));
            PlaceListDataGrid.ItemsSource = PlaceList;
            ExpenseListDataGrid.ItemsSource = ExpenseList;

            //--------

            TripImagesCarousel.ItemsSource = ImagesList;
            //--------

            MemberListDataGrid.ItemsSource = listOfMember.ToList();

        }

        private void Button_AddExpense_Click(object sender, RoutedEventArgs e)
        {
            AddExpenseWindow addExpenseWindow = new AddExpenseWindow(trip.TripId, trip.TripExpenseList.Count(), trip.TripMemberList);
            addExpenseWindow.AddExpenseEventHandler += (s, args) =>
             {
                 DTO_Expense newExpense = args.NewExpense;
                 BUS_Expense.Instance.AddExpense(newExpense);
                 ExpenseList.Add(newExpense);
             };
            addExpenseWindow.ShowDialog();
        }

        private void Button_AddDestination_Click(object sender, RoutedEventArgs e)
        {
            AddDestinationWindow addDestinationWindow = new AddDestinationWindow(trip.TripId, trip.TripDestinationList.Count);
            addDestinationWindow.AddDestinationEventHandler += (s, args) =>
            {
                DTO_Place newDest = args.NewDestination;
                BUS_Place.Instance.AddPlace(newDest);
                PlaceList.Add(newDest);
            };
            addDestinationWindow.ShowDialog();
        }

        private void Button_AddMember_Click(object sender, RoutedEventArgs e)
        {
            //todo: Add Member bằng cách GetAll trừ đi đã có
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
                trip.TripImagesList.Add(filename);
                ImagesList.Add(img);
            }
        }
    }
}
