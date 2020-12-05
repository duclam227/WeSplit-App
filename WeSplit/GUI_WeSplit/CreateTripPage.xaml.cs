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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Globalization;
using System.Collections.ObjectModel;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for CreateTripPage.xaml
    /// </summary>
    public partial class CreateTripPage : Page, INotifyPropertyChanged
    {
        public EventHandler<AddNewTripEventArgs> AddNewTripEventHandler;
        public event PropertyChangedEventHandler PropertyChanged;

        private DTO_Trip newTrip;
        private MemberItem selectedItem;

        public MemberItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                this.RaisePropertyChanged("Selectedmodel");
            }
        }

        public ObservableCollection<MemberItem> Items { get; set; }


        private CreateTripPage()
        {
            InitializeComponent();

        }

        public CreateTripPage(int tripID)
        {
            InitializeComponent();
            newTrip = new DTO_Trip();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListView_Destination.ItemsSource = newTrip.TripDestinationList;
        }
        private void CheckBox_Description_Checked(object sender, RoutedEventArgs e)
        {
            StackPanel_Description.Visibility = Visibility.Visible;
        }

        private void CheckBox_Description_Unchecked(object sender, RoutedEventArgs e)
        {
            StackPanel_Description.Visibility = Visibility.Collapsed;
            TextBox_TripDescription.Clear();
        }
        private void Button_CreateNewTrip_Click(object sender, RoutedEventArgs e)
        {
            if (AddNewTripEventHandler != null)
            {
                AddNewTripEventHandler(this, new AddNewTripEventArgs(this.newTrip));
            }
            this.NavigationService.GoBack();
        }

        public class AddNewTripEventArgs : EventArgs
        {
            public DTO_Trip NewTrip;

            public AddNewTripEventArgs(DTO_Trip newTrip)
            {
                this.NewTrip = newTrip;
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class NullReplaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DTO_Trip com = (DTO_Trip)value;
            if (com.TripName == "None selected")
            {
                return null;
            }
            else
            {
                return value;
            }
            //return value.Equals(parameter) ? null : value;
        }
    }

    public class MemberItem
    {
        public string MemberName;
        public string MemberAvatar;
    }
       
}
