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

namespace GUI_WeSplit.CustomUserControl
{
    /// <summary>
    /// Interaction logic for LabelTextBox.xaml
    /// </summary>
    public partial class LabelTextBox : UserControl
    {
        public LabelTextBox()
        {
            InitializeComponent();
        }

        string _defaultText = "";
        string _localLabel = "";
        string _localTextBox = "";

        public string Label
        {
            get { return _localLabel; }
            set
            {
                _localLabel = value;
                BaseLabel.Content = value;
            }
        }

        public string DefaultText
        {
            get { return _defaultText; }
            set
            {
                _defaultText = value;
                BaseTextBox.Text = value;
            }
        }

        public string TextBox
        {
            get 
            {
                if (BaseTextBox.Text.Equals(DefaultText))
                    return "";
                else
                    return BaseTextBox.Text; 
            }
        }

        public void Clear()
        {
            _localTextBox = "";
            BaseTextBox.Text = _defaultText;
        }

        private void BaseTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BaseTextBox.Text.Equals(DefaultText))
                BaseTextBox.Clear();
            BaseLabel.Foreground = Brushes.BlueViolet;
        }

        private void BaseTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BaseTextBox.Text == "")
            {
                if (_localTextBox == "")
                {
                    BaseTextBox.Text = _defaultText;
                }
                else
                {
                    BaseTextBox.Text = _localTextBox;
                }
            }
            else
            {
                _localTextBox = BaseTextBox.Text;
            }
            BaseLabel.Foreground = Brushes.Black;
        }
    }
}
