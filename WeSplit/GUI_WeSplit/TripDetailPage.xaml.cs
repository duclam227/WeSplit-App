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
using DTO_WeSplit;
using BUS_WeSplit;
using System.Collections.ObjectModel;
using System.IO;
using Path = System.IO.Path;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for TripDetailPage.xaml
    /// </summary>
    public partial class TripDetailPage : Page
    {
        private DTO_WeSplit.DTO_Trip trip = new DTO_WeSplit.DTO_Trip();
        private List<Tuple<DTO_WeSplit.DTO_Member, double?, double?>> listOfMember = new List<Tuple<DTO_WeSplit.DTO_Member, double?, double?>>();

        ObservableCollection<DTO_Place> PlaceList;
        ObservableCollection<DTO_Expense> ExpenseList;
        ObservableCollection<BitmapImage> ImagesList;
        ObservableCollection<DTO_Member> AvailableMemberList;
        ObservableCollection<Tuple<DTO_Member, double?, double?>> MemberList;

        public TripDetailPage()
        {
            InitializeComponent();
        }

        public TripDetailPage(int id)
        {
            InitializeComponent();

            AvailableMemberList = new ObservableCollection<DTO_Member>(BUS_Member.Instance.GetAvailableMembers(id));

            trip = BUS_WeSplit.BUS_Trip.Instance.GetTripByID(id);
            listOfMember = BUS_WeSplit.BUS_Member.Instance.GetMembersOfTrip(id);
            trip.TripMemberList = BUS_Member.Instance.GetMembersPerTrip(id);
            this.DataContext = trip;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = trip;
            PlaceList = new ObservableCollection<DTO_Place>(trip.TripDestinationList);
            ExpenseList  = new ObservableCollection<DTO_Expense>(trip.TripExpenseList);
            MemberList = new ObservableCollection<Tuple<DTO_Member, double?, double?>>(BUS_Member.Instance.GetMembersOfTrip(trip.TripId).ToList());
            ImagesList = new ObservableCollection<BitmapImage>(BUS_Trip.Instance.GetImagesOfTrip(trip.TripId));

            //var temp = Utilities.FilePathListToBitmapImageList(trip.TripImagesList);
            //if (temp != null)
            //{
            //    ImagesList = new ObservableCollection<BitmapImage>(temp);
            //} 
            //else
            //{
            //    ImagesList = new ObservableCollection<BitmapImage>();
            //}

            PlaceListDataGrid.ItemsSource = PlaceList;
            ExpenseListDataGrid.ItemsSource = ExpenseList;
            TripImagesCarousel.ItemsSource = ImagesList;
            MemberListDataGrid.ItemsSource = MemberList;
            AvailableMembersList.ItemsSource = AvailableMemberList;
        }

        private void Button_AddExpense_Click(object sender, RoutedEventArgs e)
        {
            AddExpenseWindow addExpenseWindow = new AddExpenseWindow(trip.TripId, ExpenseList.Count(), trip.TripMemberList);
            addExpenseWindow.AddExpenseEventHandler += (s, args) =>
             {
                 DTO_Expense newExpense = args.NewExpense;
                 BUS_Expense.Instance.AddExpense(newExpense);
                 ExpenseList.Add(newExpense);
             };
            addExpenseWindow.ShowDialog();

            RefreshMemberList();
        }

        private void Button_AddDestination_Click(object sender, RoutedEventArgs e)
        {
            AddDestinationWindow addDestinationWindow = new AddDestinationWindow(trip.TripId, trip.TripDestinationList.Count);
            addDestinationWindow.AddDestinationEventHandler += (s, args) =>
            {
                DTO_Place newDest = args.NewDestination;
                BUS_Place.Instance.AddPlace(newDest);
                PlaceList.Add(newDest);
            };
            addDestinationWindow.ShowDialog();
        }

        private void Button_AddMember_Click(object sender, RoutedEventArgs e)
        {
            //todo: Add Member bằng cách GetAll trừ đi đã có
            DTO_Member selected = (DTO_Member)AvailableMembersList.SelectedItem;
            if (selected != null)
            {
                AvailableMemberList.Remove(selected);
                BUS_Member.Instance.AddMemberPerTrip(selected.MemberID, trip.TripId);

                RefreshMemberList();
            }
        }

        private void Button_AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
          "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
          "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                BitmapImage img = new BitmapImage(new Uri(filename, UriKind.Absolute));

                string dir = System.AppDomain.CurrentDomain.BaseDirectory;
                dir += $@"resources\{trip.TripId}";
                Utilities.CopyFile(filename, dir);
                //trip.AddImage(filename);
                ImagesList.Add(img);
                BUS_Trip.Instance.AddImageToTrip(trip.TripId, ImagesList.Count() - 1, System.IO.Path.GetFileName(filename));
            }         

        }

        private void MemberListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MemberListDataGrid.SelectedItem != null)
            {
                Button_AddMember.Visibility = Visibility.Hidden;
                Button_DeleteMember.Visibility = Visibility.Visible;
            }
            else
            {
                Button_AddMember.Visibility = Visibility.Visible;
                Button_DeleteMember.Visibility = Visibility.Hidden;
            }
        }

        private void PlaceListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaceListDataGrid.SelectedItem != null)
            {
                Button_AddDestination.Visibility = Visibility.Hidden;
                Button_DeleteDestination.Visibility = Visibility.Visible;
            }
            else
            {
                Button_AddDestination.Visibility = Visibility.Visible;
                Button_DeleteDestination.Visibility = Visibility.Hidden;
            }
        }

        private void ExpenseListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExpenseListDataGrid.SelectedItem != null)
            {
                Button_AddExpense.Visibility = Visibility.Hidden;
                Button_DeleteExpense.Visibility = Visibility.Visible;
            }
            else
            {
                Button_AddExpense.Visibility = Visibility.Visible;
                Button_DeleteExpense.Visibility = Visibility.Hidden;
            }
        }

        private void Button_DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MemberListDataGrid.SelectedItems)
            {
                Tuple<DTO_Member, double?, double?> member = (Tuple<DTO_Member, double?, double?>)item;
                BUS_Member.Instance.DeleteMemberPerTrip(trip.TripId, member.Item1.MemberID);
            }

            RefreshMemberList();
        }

        private void Button_DeleteDestination_Click(object sender, RoutedEventArgs e)
        {
            while(PlaceListDataGrid.SelectedItems.Count>0)
            {
                var item = PlaceListDataGrid.SelectedItems[0];
                BUS_Place.Instance.DeletePlace((DTO_Place)item);
                PlaceList.Remove((DTO_Place)item);
            }
        }

        private void Button_DeleteExpense_Click(object sender, RoutedEventArgs e)
        {
            while(ExpenseListDataGrid.SelectedItems.Count>0)
            {
                var item = ExpenseListDataGrid.SelectedItems[0];
                BUS_Expense.Instance.DeleteExpense((DTO_Expense)item);
                ExpenseList.Remove((DTO_Expense)item);
            }

            RefreshMemberList();
        }

        private void PlaceListDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as System.Windows.Data.Binding).Path.Path;
                    int rowIndex = e.Row.GetIndex();      // rowIndex has the row index

                    System.Windows.Controls.TextBox element = e.EditingElement as System.Windows.Controls.TextBox;
                    // el.Text has the new, user-entered value

                    DTO_Place place = (DTO_Place)PlaceListDataGrid.Items[rowIndex];
                    BUS_Place.Instance.UpdatePlace(trip.TripId, place.PlaceId, (string)bindingPath, element.Text);
                }
            }
        }

        private void ExpenseListDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as System.Windows.Data.Binding).Path.Path;
                    int rowIndex = e.Row.GetIndex();      // rowIndex has the row index

                    System.Windows.Controls.TextBox element = e.EditingElement as System.Windows.Controls.TextBox;
                    // el.Text has the new, user-entered value

                    DTO_Expense expense = (DTO_Expense)ExpenseListDataGrid.Items[rowIndex];
                    BUS_Expense.Instance.UpdateExpense(trip.TripId, expense.ExpenseId, (string)bindingPath, element.Text);
                }
            }
        }

        private void RefreshMemberList()
        {
            BUS_Trip.Instance.AddAverageToTrip(trip.TripId, BUS_Trip.Instance.CalculateAverage(trip.TripId));

            trip.TripMemberList = BUS_Member.Instance.GetMembersPerTrip(trip.TripId);
            MemberList = new ObservableCollection<Tuple<DTO_Member, double?, double?>>(BUS_Member.Instance.GetMembersOfTrip(trip.TripId).ToList());
            MemberListDataGrid.ItemsSource = MemberList;

            AverageTextBlock.Text = BUS_Trip.Instance.CalculateAverage(trip.TripId).ToString();
            TotalTextBlock.Text = BUS_Trip.Instance.GetExpenseTotal(trip.TripId).ToString();
        }
    }

}
