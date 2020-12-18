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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public delegate void PassCommandToMain(string command);

        public event PassCommandToMain eventPassCommand;

        public HomePage()
        {
            InitializeComponent();
        }

        private void AddNewTripButton_Click(object sender, RoutedEventArgs e)
        {
            eventPassCommand("newtrip");
        }

        private void ExploreButton_Click(object sender, RoutedEventArgs e)
        {
            eventPassCommand("explore");
        }

        private void AddNewMemberButton_Click(object sender, RoutedEventArgs e)
        {
            eventPassCommand("newmember");
        }
    }
}
