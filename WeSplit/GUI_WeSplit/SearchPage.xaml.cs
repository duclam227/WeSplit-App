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
using BUS_WeSplit;
using DTO_WeSplit;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        private BUS_Trip bus_trip = BUS_Trip.Instance;

        private GridViewColumn columnMember = null;
        private List<DTO_Trip> searchingTrips;
        private List<Tuple<DTO_Trip, String>> searchByMember;

        public delegate void PassIDToMain(int id);
        public event PassIDToMain eventPassIDToMain;

        public SearchPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            String text = SearchBar.Text;


            if (text != "")
            {
                if (radionBtn_SearchTrip.IsChecked == true)
                {
                    searchingTrips = bus_trip.SearchTripsByName(text);
                    ResultListView.ItemsSource = searchingTrips;
                }
                else if (radioBtn_SearchMember.IsChecked == true)
                {
                    List<Trip_MemberName> trip_MemberNames = new List<Trip_MemberName>();
                    searchByMember = bus_trip.SearchTripsByMember(text);
                   
                    foreach (var item in searchByMember)
                    {
                        Trip_MemberName tempObj = new Trip_MemberName();
                        tempObj.TripId = item.Item1.TripId;
                        tempObj.TripName = item.Item1.TripName;
                        tempObj.TripStartDate = item.Item1.TripStartDate;
                        tempObj.TripDescription = item.Item1.TripDescription;
                        tempObj.TripBudget = item.Item1.TripExpenseTotal;
                        tempObj.TripAverage = item.Item1.TripAverage;
                        tempObj.MemberName = item.Item2;
                        trip_MemberNames.Add(tempObj);
                    }    

                    ResultListView.ItemsSource = trip_MemberNames;
                }
            }
            else
            {
                ResultListView.ItemsSource = null;
            }

        }

        private class Trip_MemberName {
            public int TripId { get ; set; }
            public string TripName { get; set; }
            public string TripStartDate { get; set; }
            public string TripDescription { get; set; }
            public double TripBudget { get; set; }
            public double TripAverage { get; set; }
            public string MemberName { get; set; }


        }

        private void radionBtn_SearchTrip_Checked(object sender, RoutedEventArgs e)
        {
            if (columnMember != null)
            {
                myGridView.Columns.Remove(columnMember);
            }
        }

        private void radioBtn_SearchMember_Checked(object sender, RoutedEventArgs e)
        {
            if (columnMember == null)
            {
                columnMember = new GridViewColumn();
                columnMember.Width = 150;
                columnMember.Header = "Member's Name";
                columnMember.DisplayMemberBinding = new Binding("MemberName");
            }

            myGridView.Columns.Add(columnMember);

        }

        private void ResultListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tripID;
            TripDetailPage tripDetailPage = new TripDetailPage();
            int position = ResultListView.SelectedIndex;

            if(radioBtn_SearchMember.IsChecked == false)
            {
                tripID = searchingTrips[position].TripId;
                  
            }
            else
            {
                tripID = searchByMember[position].Item1.TripId;
            }

            eventPassIDToMain(tripID);
        }
    }
}
