﻿using System;
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MemberListPage memberList = new MemberListPage();
            TripDetailPage tripPage = new TripDetailPage();
            MainFrame.Navigate(tripPage);
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
    }
}
