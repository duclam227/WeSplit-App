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
using DTO_WeSplit;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for TripListPage.xaml
    /// </summary>
    public partial class TripListPage : Page
    {
        private List<DTO_Trip> listToShow = new List<DTO_Trip>();

        public delegate void PassIDToMain(int id);
        public event PassIDToMain eventPassIDToMain;
        public TripListPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FinishedTripListView.Visibility = Visibility.Visible;
            UnfinishedTripListView.Visibility = Visibility.Collapsed;
            FinishListButton.Opacity = 1;
            UnfinishListButton.Opacity = 0.3;

            listToShow.Clear();
            listToShow = BUS_WeSplit.BUS_Trip.Instance.GetFinishedTrips().ToList();
            FinishedTripListView.ItemsSource = listToShow;
        }

        private void FinishListButton_Click(object sender, RoutedEventArgs e)
        {
            FinishedTripListView.Visibility = Visibility.Visible;
            UnfinishedTripListView.Visibility = Visibility.Collapsed;
            FinishListButton.Opacity = 1;
            UnfinishListButton.Opacity = 0.5;

            listToShow.Clear();
            listToShow = BUS_WeSplit.BUS_Trip.Instance.GetFinishedTrips().ToList();
            FinishedTripListView.ItemsSource = listToShow;
        }

        private void UnfinishListButton_Click(object sender, RoutedEventArgs e)
        {
            UnfinishedTripListView.Visibility = Visibility.Visible;
            FinishedTripListView.Visibility = Visibility.Collapsed;
            FinishListButton.Opacity = 0.5;
            UnfinishListButton.Opacity = 1;

            listToShow.Clear();
            listToShow = BUS_WeSplit.BUS_Trip.Instance.GetUnfinishedTrips().ToList();
            UnfinishedTripListView.ItemsSource = listToShow;
        }

        private void UnfinishedTripListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tripID;
            int position = UnfinishedTripListView.SelectedIndex;

            if (position > -1)
            {
                tripID = listToShow[position].TripId;
                //MessageBox.Show($"{tripID}");
                eventPassIDToMain(tripID);
            }
            else
            {
                //do nothing
            }
        }

        private void FinishedTripListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tripID;
            int position = FinishedTripListView.SelectedIndex;

            if (position > -1)
            {
                tripID = listToShow[position].TripId;
                //MessageBox.Show($"{tripID}");
                eventPassIDToMain(tripID);
            }
            else
            {
                //do nothing
            }
        }
    }
}
