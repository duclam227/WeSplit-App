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

        public string AvatarSrc { get => _avatarSrc; set => _avatarSrc = value; }
        public string MemberName { get => _memberName; set => _memberName = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }

        public AddMemberWindow(int memberId)
        {
            InitializeComponent();
            DataContext = this;
            _member = new DTO_Member()
            {
                MemberID = memberId + 1
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
                AvatarSrc = openFileDialog.FileName;
                AvatarImg.Visibility = Visibility.Visible;
                Gravatar.Visibility = Visibility.Hidden;
                Button_RemoveAvatar.Visibility = Visibility.Visible;
                Button_AddAvatar.Visibility = Visibility.Hidden;
            }
        }

        private void Button_RemoveAvatar_Click(object sender, RoutedEventArgs e)
        {
            AvatarImg.Source = null;
            AvatarSrc = null;
            AvatarImg.Visibility = Visibility.Hidden;
            Gravatar.Visibility = Visibility.Visible;
            Button_RemoveAvatar.Visibility = Visibility.Hidden;
            Button_AddAvatar.Visibility = Visibility.Visible;
        }

        private void Button_AddMember_Click(object sender, RoutedEventArgs e)
        {
            bool canReturn = true;

            if (String.IsNullOrWhiteSpace(AvatarSrc) || DateOfBirth == null
                || String.IsNullOrWhiteSpace(LabelTextBox_Name.Text))
                canReturn = false;

            if (canReturn)
            {
                string filename = System.IO.Path.GetFileName(AvatarSrc);
                string dir = System.AppDomain.CurrentDomain.BaseDirectory;
                dir += $@"resources\avatars\{_member.MemberID}\";
                BUS_WeSplit.Utilities.CopyFile(AvatarSrc, dir);
                dir += filename;
                _member.MemberAvatar = filename;
                _member.MemberDOB = DateOfBirth;
                _member.MemberName = MemberName;
                _member.MemberSex = (bool)Radio_Male.IsChecked;
                BUS_WeSplit.BUS_Member.Instance.AddMember(_member);
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Bạn chưa điền đủ thông tin");
            }

        }
    }
}
