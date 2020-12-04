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
        public TripDetailPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void GeneralInfoButton_Click(object sender, MouseButtonEventArgs e)
        {
            TripDetailFrame.Visibility = Visibility.Collapsed;
            Grid.SetRow(ActiveIndicator, 0);
        }

        private void MemberListButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TripDetailFrame.Visibility = Visibility.Collapsed;

            Grid.SetRow(ActiveIndicator, 1);

            MemberListPage memberList = new MemberListPage();
            TripDetailFrame.Navigate(memberList);

            TripDetailFrame.Visibility = Visibility.Visible;
        }

        private void DestinationListButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TripDetailFrame.Visibility = Visibility.Collapsed;
            Grid.SetRow(ActiveIndicator, 2);
        }

        private void ExpenseHistoryButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TripDetailFrame.Visibility = Visibility.Collapsed;
            Grid.SetRow(ActiveIndicator, 3);
        }
    }
}
