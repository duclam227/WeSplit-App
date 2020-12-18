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
            listToShow.Clear();
            listToShow = BUS_WeSplit.BUS_Trip.Instance.GetAllTrips().ToList();
            TripListView.ItemsSource = listToShow;
        }

        private void FinishedTripListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tripID;
            int position = TripListView.SelectedIndex;

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
