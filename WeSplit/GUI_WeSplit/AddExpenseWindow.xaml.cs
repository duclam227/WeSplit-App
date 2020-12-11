using DTO_WeSplit;
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
using System.Windows.Shapes;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for AddExpenseWindow.xaml
    /// </summary>
    public partial class AddExpenseWindow : Window
    {
        public string ExpenseDescription;
        public string ExpenseAmount;

        public AddExpenseWindow()
        {
            InitializeComponent();
            ComboBox_MemberListExpense.ItemsSource = BUS_WeSplit.BUS_Member.Instance.GetAllMembers();
        }

        
        private void Button_AddExpense_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
