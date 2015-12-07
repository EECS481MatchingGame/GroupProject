using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace KinectColorApp
{
    // Interaction logic for App.xaml
    public partial class Menu : Window
    {
        private int difficulty = 0; // 0 is easy, 1 is middle, 2 is hard
        private String theme = "Animals";
        private KinectController kinectController;

        public Menu()
        {
            InitializeComponent();
        }

        public void setKinectController(KinectController kC)
        {
            kinectController = kC;
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

        public void startGame(object sender, RoutedEventArgs e)
        {
            // animals - look good
            // cars - look good but need more color variance
            // colors - yellowgreen/green, blue/purple/ red/orange need variance
            // flags - look really good
            string themeDirectory = Directory.GetCurrentDirectory() + @"\..\..\Resources\sprites\" + theme.ToLower();
            string[] fileEntries = Directory.GetFiles(themeDirectory);
            List<CardController> cards = new CardController().initializeCards();
            GameBoard gameBoard = new GameBoard(0, theme, fileEntries, cards);
            kinectController.setGameBoard(gameBoard);
            App.Current.MainWindow = gameBoard;
            gameBoard.Show();
            this.Close();
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void changeTheme(object sender, RoutedEventArgs e)
        {
            theme = (sender as MenuItem).Header.ToString();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // need to uncheck other boxes 
            CheckBox chk = (CheckBox)sender;
            if (chk.Name == "easyCheck") difficulty = 0; 
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

    }
}
