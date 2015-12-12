using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KinectColorApp
{
    // Interaction logic for App.xaml
    public partial class Menu : Window
    {
        private String difficulty = "Hard"; 
        private String theme = "animals";
        private GameBoard gameBoard; 
        private KinectController kinectController;

        public Menu()
        {
            InitializeComponent();
        }         

        public void setGameBoard(GameBoard g)
        {
         //   gameBoard = g; 
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
            CardController c = new CardController();
            c.setNumCards(difficulty);
            List<CardController> cards = c.initializeCards(); 
            gameBoard = new GameBoard(difficulty, theme, fileEntries, cards, this);

            kinectController.setGameBoard(gameBoard);
            App.Current.MainWindow = gameBoard;
            gameBoard.Show();
        }
      
        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void Difficulty_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;

        }

        private void changeTheme(object sender, RoutedEventArgs e)
        {
            theme = (sender as MenuItem).Header.ToString();
            themeMenu.Content = theme;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
            //HeaderMenu obj = new HeaderMenu(sender);
        }

        public void clickDifficulty(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Console.WriteLine(item.Header);
            difficulty = (String)item.Header;
            difficultyMenu.Content = difficulty; 
        }
    }
}
