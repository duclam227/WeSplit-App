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
    /// Interaction logic for AddDestinationWindow.xaml
    /// </summary>
    public partial class AddDestinationWindow : Window
    {
        private string _placeName;
        private string _placeAddress;
        private string _placeDescription;
        private int _placeId;

        public EventHandler<AddDestinationEventArgs> AddDestinationEventHandler;

        public string PlaceName { get => _placeName; set => _placeName = value; }
        public string PlaceAddress { get => _placeAddress; set => _placeAddress = value; }
        public string PlaceDescription { get => _placeDescription; set => _placeDescription = value; }

        public AddDestinationWindow()
        {
            InitializeComponent();
        }

        public AddDestinationWindow(int placeId)
        {
            InitializeComponent();
            DataContext = this;
            _placeId = placeId;
        }

        private void Button_AddDestination_Click(object sender, RoutedEventArgs e)
        {
            bool canReturn = true;

            if (String.IsNullOrEmpty(PlaceName) || String.IsNullOrEmpty(PlaceAddress) || String.IsNullOrEmpty(PlaceDescription))
                canReturn = false;

            if (AddDestinationEventHandler!=null && canReturn)
            {
                DTO_Place dest = new DTO_Place(_placeId, PlaceName, PlaceAddress, PlaceDescription);
                AddDestinationEventHandler(this, new AddDestinationEventArgs(dest));
                this.Close();
            }
        }

        public class AddDestinationEventArgs : EventArgs
        {
            public DTO_Place NewDestination;

            public AddDestinationEventArgs(DTO_Place newDestination)
            {
                NewDestination = newDestination;
            }
        }
    }
}
