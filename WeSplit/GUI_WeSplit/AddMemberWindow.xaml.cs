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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for AddMemberWindow.xaml
    /// </summary>
    public partial class AddMemberWindow : Window
    {
        DTO_Member _member;
        private string _avatarSrc;
        private string _memberName;
        private DateTime _dateOfBirth;

        public EventHandler<AddNewMemberEventArgs> AddNewMemberEventHandler;

        
        public string AvatarSrc { get => _avatarSrc; set => _avatarSrc = value; }
        public string MemberName { get => _memberName; set => _memberName = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }

        public AddMemberWindow(int memberId)
        {
            InitializeComponent();
            _member = new DTO_Member()
            {
                MemberID = memberId
            };
        }

        private void Button_AddAvatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
          "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
          "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BitmapImage img = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                AvatarImg.Source = img;
                AvatarImg.Visibility = Visibility.Visible;
                Gravatar.Visibility = Visibility.Hidden;
                Button_RemoveAvatar.Visibility = Visibility.Visible;
                Button_AddAvatar.Visibility = Visibility.Hidden;
            }
        }

        private void Button_RemoveAvatar_Click(object sender, RoutedEventArgs e)
        {
            AvatarImg.Source = null;
            AvatarImg.Visibility = Visibility.Hidden;
            Gravatar.Visibility = Visibility.Visible;
            Button_RemoveAvatar.Visibility = Visibility.Hidden;
            Button_AddAvatar.Visibility = Visibility.Visible;
        }

        private void Button_AddMember_Click(object sender, RoutedEventArgs e)
        {
            if (AddNewMemberEventHandler != null)
            {
                if (!String.IsNullOrWhiteSpace(AvatarSrc) &&
                    !String.IsNullOrWhiteSpace(_dateOfBirth.ToShortDateString()))
                {
                    _member.MemberAvatar = AvatarSrc;
                    _member.MemberDOB = _dateOfBirth.ToShortDateString();
                    _member.MemberName = MemberName;
                    _member.MemberSex = (bool)Radio_Male.IsChecked;
                    AddNewMemberEventHandler(this, new AddNewMemberEventArgs(_member));
                    this.Close();
                }
            }  
        }

        public class AddNewMemberEventArgs : EventArgs
        {
            DTO_Member newMember;
            public AddNewMemberEventArgs(DTO_Member member)
            {
                this.newMember = member;
            }
        }
    }
}
