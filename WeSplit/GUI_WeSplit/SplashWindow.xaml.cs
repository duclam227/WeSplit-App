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
using System.Configuration;

namespace GUI_WeSplit
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {

        private System.Timers.Timer _timer;
        private int timer = 0;
        private int watingTime = 400;   //500 -> 6s
        private Random _rng = new Random();

        private string[] listOfTips = new string[]
        {
            "Vịnh Hạ Long là một vịnh thuộc bờ tây vịnh Bắc Bộ tại khu vực biển Đông Bắc Việt Nam" +
            "\nDiện tích khoảng 1.553 km² bao gồm 1.969 hòn đảo lớn nhỏ, phần lớn là đảo đá vôi.",

            "Hà Nội là thủ đô của nước CHXHCN Việt Nam, nằm giữa đồng bằng sông Hồng trù phú" +
            "\nLà một trung tâm chính trị, kinh tế và văn hóa ngay từ những buổi đầu của lịch sử Việt Nam.",

            "Đà Lạt là thành phố trực thuộc tỉnh ủy Lâm Đồng, nằm trên Cao nguyên Lâm Viên" +
            "\nHiện nay là thành phố nổi tiếng về du lịch bậc nhất của Việt Nam.",

            "Phố cổ Hội An trực thuộc tỉnh Quảng Nam, Việt Nam và từng là thương cảng quốc tế sầm uất" +
            "\nĐược UNESCO công nhận là di sản văn hóa thế giới từ năm 1999",

            "Cầu Vàng là tên một cây cầu bộ hành dài khoảng 150 m tại khu nghỉ dưỡng Bà Nà, Đà Nẵng." +
            "\nNằm ở độ cao khoảng 1.400 m trên núi Bà Nà, ở giữa cầu có hai bàn tay lớn được tạc từ đá.",

            "Vũng Tàu là một thành phố thuộc tỉnh Bà Rịa - Vũng Tàu, ở vùng Đông Nam Bộ, Việt Nam." +
            "\nSở hữu nhiều bãi biển đẹp và cơ sở hạ tầng được đầu tư hoàn chỉnh" +
            "\nVũng Tàu là một địa điểm du lịch nổi tiếng tại miền Nam."
        };

        private string[] listOfBackground = new string[]
        {
            "Images/SplashScreen01.jpg",
            "Images/SplashScreen02.jpg",
            "Images/SplashScreen03.jpg",
            "Images/SplashScreen04.jpg",
            "Images/SplashScreen05.jpg",
            "Images/SplashScreen06.jpg",
        };

        public SplashScreen()
        {
            InitializeComponent();

            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);

            if (showSplash == false)
            {
                var main = new MainWindow();
                main.Show();

                this.Close();
            }
            _timer = new System.Timers.Timer(1);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int index = _rng.Next(listOfTips.Length);
            Tip.Text = listOfTips[index];

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{listOfBackground[index]}", UriKind.Relative);
            bitmapImage.EndInit();

            BackgroundSplashScreen.Source = bitmapImage;

            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();

        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (timer > watingTime)
                {
                    _timer.Stop();

                    var main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    timer += 1;
                    Console.WriteLine(timer);
                    ProgressBarSplashScreen.Value = (double)timer / watingTime * 100;
                }

            });
        }



        private void SplashScreenCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void SplashScreenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }
    }
}