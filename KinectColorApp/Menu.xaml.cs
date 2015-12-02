using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KinectColorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private int difficulty = 0;
        private String theme = "animals"; 
        public Menu()
        {

            InitializeComponent();

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
           
            GameBoard main = new GameBoard(difficulty, theme);
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
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
