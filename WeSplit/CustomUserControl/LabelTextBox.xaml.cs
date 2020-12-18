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
        //string _localLabel = "";

        public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register("Text", typeof(string), typeof(LabelTextBox));

        public LabelTextBox()
        {
            InitializeComponent();
        }

        //public string Label
        //{
        //    get { return _localLabel; }
        //    set
        //    {
        //        _localLabel = value;
        //        BaseLabel.Content = value;
        //    }
        //}

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
              DependencyProperty.Register("Title", typeof(string), typeof(LabelTextBox));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set 
            { 
                SetValue(TitleProperty, value);
            }
        }

        private void BaseTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            BindingExpression mainTxtBxBinding = BindingOperations.GetBindingExpression(BaseTextBox, TextBox.TextProperty);
            BindingExpression textBinding = BindingOperations.GetBindingExpression(this, TextProperty);

            if (textBinding != null && mainTxtBxBinding != null && 
                textBinding.ParentBinding != null && 
                textBinding.ParentBinding.ValidationRules.Count > 0 && 
                mainTxtBxBinding.ParentBinding.ValidationRules.Count < 1)
            {
                foreach (ValidationRule vRule in textBinding.ParentBinding.ValidationRules)
                    mainTxtBxBinding.ParentBinding.ValidationRules.Add(vRule);
            }
        }
    }
}
