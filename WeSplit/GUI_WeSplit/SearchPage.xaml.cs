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

        private List<DTO_Trip> searchingTrips;
        private List<Tuple<DTO_Trip, String>> searchByMember;
        private DataGridTextColumn columnMember =  null;

        public delegate void PassIDToMain(int id);
        public event PassIDToMain eventPassIDToMain;

        public SearchPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            String text = SearchBar.Text;
            ResultDataGrid.UnselectAll();

            if (text != "")
            {
                ResultDataGrid.Visibility = Visibility.Visible;
                if (radionBtn_SearchTrip.IsChecked == true)
                {
                    searchingTrips = bus_trip.SearchTripsByName(text);
                    ResultDataGrid.ItemsSource = searchingTrips;
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

                    ResultDataGrid.ItemsSource = trip_MemberNames;
                }
            }
            else
            {
                ResultDataGrid.ItemsSource = null;
                ResultDataGrid.Visibility = Visibility.Collapsed;
            }

            if(ResultDataGrid.Items.Count == 0)
            {
                ResultDataGrid.Visibility = Visibility.Collapsed;
                SearchResultNotice.Visibility = Visibility.Visible;
            }
            else
            {
                SearchResultNotice.Visibility = Visibility.Collapsed;
            }

        }

        private class Trip_MemberName {
            public int TripId { get ; set; }
            public string TripName { get; set; }
            public DateTime TripStartDate { get; set; }
            public string TripDescription { get; set; }
            public double TripBudget { get; set; }
            public double? TripAverage { get; set; }
            public string MemberName { get; set; }


        }

        private void radionBtn_SearchTrip_Checked(object sender, RoutedEventArgs e)
        {
            if (columnMember != null)
            {
                ResultDataGrid.Columns.Remove(columnMember);
            }
        }

        private void radioBtn_SearchMember_Checked(object sender, RoutedEventArgs e)
        {
            if (columnMember == null)
            {
                columnMember = new DataGridTextColumn();
                columnMember.Header = "Tên thành viên";
                columnMember.Binding = new Binding("MemberName");
            }

            ResultDataGrid.Columns.Add(columnMember);

        }

        private void ResultDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tripID;
            int position = ResultDataGrid.SelectedIndex;
                   

            if (position > -1)
            {
                if (radioBtn_SearchMember.IsChecked == false)
                {
                    tripID = searchingTrips[position].TripId;

                }
                else
                {
                    tripID = searchByMember[position].Item1.TripId;
                }

                eventPassIDToMain(tripID);
            }
            else
            {
                //do nothing
            }
            
        }
    }
}
