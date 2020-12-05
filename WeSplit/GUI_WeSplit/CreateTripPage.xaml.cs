﻿using DTO_WeSplit;
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
using BUS_WeSplit;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for CreateTripPage.xaml
    /// </summary>
    public partial class CreateTripPage : Page
    {
        public EventHandler<AddNewTripEventArgs> AddNewTripEventHandler;

        private DTO_Trip newTrip;

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
            ComboBox_MemberList.ItemsSource = BUS_Member.Instance.GetAllMembers();
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

        private void ListView_Destination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_Destination_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}