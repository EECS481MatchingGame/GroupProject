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
using System.Windows.Media.Imaging;
using System.IO;

namespace KinectColorApp
{
    public partial class Restart : Window
    {
        private Menu menu;

        public Restart(Menu menuToReturnTo, int s)
        {
            InitializeComponent();
            menu = menuToReturnTo;
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\..\..\Resources\retina_wood_@2X.png", UriKind.Absolute));
            backgroundImage.ImageSource = image.Source;
            Score.Text = s.ToString(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Window_Size_Did_Change(object sender, RoutedEventArgs e)
        {

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void restartGame(object sender, RoutedEventArgs e)
        {
            menu.startGame(null, null);
            this.Close();
        }

        private void returnToMenu(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow = menu;
            menu.Show();
            this.Close();
        }
    }
}
