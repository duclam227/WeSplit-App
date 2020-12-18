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

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for MemberListPage.xaml
    /// </summary>
    public partial class MemberListPage : Page
    {
        public MemberListPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MemberDataGrid.ItemsSource = BUS_Member.Instance.GetAllMembers().ToList();
        }
    }
}
