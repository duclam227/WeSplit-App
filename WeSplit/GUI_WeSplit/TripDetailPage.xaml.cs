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

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for TripDetailPage.xaml
    /// </summary>
    public partial class TripDetailPage : Page
    {
        private DTO_WeSplit.DTO_Trip trip = new DTO_WeSplit.DTO_Trip();
        private List<Tuple<DTO_WeSplit.DTO_Member, double?, double?>> listOfMember = new List<Tuple<DTO_WeSplit.DTO_Member, double?, double?>>();

        public TripDetailPage()
        {
            InitializeComponent();
        }

        public TripDetailPage(int id)
        {
            InitializeComponent();
            trip = BUS_WeSplit.BUS_Trip.Instance.GetTripByID(id);
            listOfMember = BUS_WeSplit.BUS_Member.Instance.GetMembersOfTrip(id);

            this.DataContext = trip;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = trip;
            PlaceListDataGrid.ItemsSource = trip.TripDestinationList;
            ExpenseListDataGrid.ItemsSource = trip.TripExpenseList;

            //--------
            List<string> temp = new List<string>();
            temp.Add(@"D:\LL\001.png");
            temp.Add(@"D:\LL\002.png");
            temp.Add(@"D:\LL\003.png");

            TripImagesCarousel.ItemsSource = temp;
            //--------

            MemberListDataGrid.ItemsSource = listOfMember.ToList();

        }

   
    }
}
