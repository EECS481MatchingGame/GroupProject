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
        public Menu()
        {
            InitializeComponent();
            string dropBox = Directory.GetCurrentDirectory() + @"\..\..\Resources\sprites\flags";       // flags look really good
            string[] fileEntries = Directory.GetFiles(dropBox);
            List<CardController> cards = new List<CardController>(18);
            gameBoard = gameBoard = new GameBoard(difficulty, "animals", fileEntries, cards);

        }

        public void setGameBoard(GameBoard g)
        {
         //   gameBoard = g; 
         
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
            App.Current.MainWindow = gameBoard;
            this.Close();
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
            // HeaderMenu obj = new HeaderMenu(sender);
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
