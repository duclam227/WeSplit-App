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
        public MainWindow()
        {
            InitializeComponent();
            HandyControl.Tools.ConfigHelper.Instance.SetLang("en");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MemberListPage memberList = new MemberListPage();
            //TripDetailPage tripPage = new TripDetailPage();
            //MainFrame.Navigate(tripPage);
            CreateTripPage createTripPage = new CreateTripPage(1);
            MainFrame.Navigate(createTripPage);
            //AddMemberWindow window = new AddMemberWindow(1)
            //{
            //    Owner = this
            //};
            //window.Show();
        }
    }
}
