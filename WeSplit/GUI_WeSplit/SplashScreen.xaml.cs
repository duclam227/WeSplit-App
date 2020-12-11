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
        private int watingTime = 600;   //500 -> 6s
        private Random _rng = new Random();

        private string[] listOfTips = new string[]
        {
            "Nếu bạn và lũ bạn thân vừa lên kèo thì nên đi ngay nếu không sẽ bể!!",
            "Đi du lịch cùng với những người bạn thì vui hơn đấy!",
            "Có người yêu thì không nên đến Đà Lạt cùng họ, có lời nguyền đấy",
            "Đi du lịch thì đừng có nằm ngủ suốt ngày trong homestay, lết xác đi chơi đi",
            "Ăn uống chung với bạn thì tiền bạc nên rõ ràng và thống nhất với nhau!"
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
            index = _rng.Next(listOfBackground.Length);

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
                if(timer > watingTime)
                {
                    _timer.Stop();

                    var main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else {
                    timer += 1;
                    Console.WriteLine(timer);
                    ProgressBarSplashScreen.Value =  (double)timer/watingTime * 100;
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
