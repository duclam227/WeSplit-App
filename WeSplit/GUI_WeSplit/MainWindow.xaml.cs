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

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomePage homePage = new HomePage();
        SearchPage searchPage;
        //TripDetailPage tripPage = new TripDetailPage();
        //MemberListPage memberListPage = new MemberListPage();

        public MainWindow()
        {
            InitializeComponent();
            HandyControl.Tools.ConfigHelper.Instance.SetLang("en");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(homePage);
            homePage.eventPassCommand += homePage_eventPassCommand;
        }

        private void homePage_eventPassCommand(string command)
        {
            //ActiveIndicator.Visibility = Visibility.Collapsed;

            switch (command)
            {
                case "newtrip":
                    {
                        int noOfTrips = BUS_WeSplit.BUS_Trip.Instance.GetNumberOfTrips();
                        CreateTripPage addTrip = new CreateTripPage(noOfTrips);
                        MainFrame.Navigate(addTrip);
                        break;
                    }
                case "newmember":
                    {
                        int noOfMembers = BUS_WeSplit.BUS_Member.Instance.GetAmountOfMember();
                        AddMemberWindow addMember = new AddMemberWindow(noOfMembers);                       
                        addMember.ShowDialog();
                        break;
                    }
                case "explore":
                    {
                        Grid.SetColumn(ActiveIndicator, 4);
                        searchPage = new SearchPage();
                        searchPage.eventPassIDToMain += SearchPage_eventPassIDToMain;
                        MainFrame.Navigate(searchPage);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }

        private void SearchPage_eventPassIDToMain(int id)
        {
            TripDetailPage detailPage = new TripDetailPage(id);
            MainFrame.Navigate(detailPage);
        }

        private void ExitButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn thực sự muốn thoát WeSplit?", "Thoát WeSplit", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        this.Close();
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void MinimizeButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ResizeButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void HomePageButton_Click(object sender, MouseButtonEventArgs e)
        {
            //MainFrame.Visibility = Visibility.Collapsed;

            ActiveIndicator.Visibility = Visibility.Visible;
            Grid.SetColumn(ActiveIndicator, 0);

            //HomePage hp = new HomePage();
            MainFrame.Navigate(homePage);
        }

        private void TripListButton_Click(object sender, MouseButtonEventArgs e)
        {
            //MainFrame.Visibility = Visibility.Collapsed;

            ActiveIndicator.Visibility = Visibility.Visible;
            Grid.SetColumn(ActiveIndicator, 2);

            //TripDetailPage tripPage = new TripDetailPage();
            //MemberListPage memberList = new MemberListPage();
            TripListPage tripListPage = new TripListPage();
            tripListPage.eventPassIDToMain += TripListPage_eventPassIDToMain;
            MainFrame.Navigate(tripListPage);
        }

        private void TripListPage_eventPassIDToMain(int id)
        {
            TripDetailPage detailPage = new TripDetailPage(id);
            MainFrame.Navigate(detailPage);
        }

        private void MemberListButton_Click(object sender, MouseButtonEventArgs e)
        {
            //MainFrame.Visibility = Visibility.Collapsed;

            ActiveIndicator.Visibility = Visibility.Visible;
            Grid.SetColumn(ActiveIndicator, 4);

            MemberListPage memberListPage = new MemberListPage();
            MainFrame.Navigate(memberListPage);
        }

        private void AboutUsButton_Click(object sender, MouseButtonEventArgs e)
        {
            //MainFrame.Visibility = Visibility.Collapsed;

            ActiveIndicator.Visibility = Visibility.Visible;
            Grid.SetColumn(ActiveIndicator, 6);
        }

        private void SearchButton_Click(object sender, MouseButtonEventArgs e)
        {
            ActiveIndicator.Visibility = Visibility.Visible;
            Grid.SetColumn(ActiveIndicator, 8);

            searchPage = new SearchPage();
            searchPage.eventPassIDToMain += SearchPage_eventPassIDToMain;
            MainFrame.Navigate(searchPage);
        }
    }
}
