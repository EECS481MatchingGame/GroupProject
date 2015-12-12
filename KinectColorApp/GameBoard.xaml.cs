using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows;



namespace KinectColorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class GameBoard : Window
    {
        private string difficulty;
        private string theme;
        private int numCards = 9;

        private List<CardController> cards;
        private string[] backgrounds;

        private HashSet<int> selected;
        private HashSet<int> matched;

        private Menu menu;

        public GameBoard(string d, string t, string[] b, List<CardController> c, Menu previousMenu)
        {
            InitializeComponent();
            difficulty = d;
            backgrounds = b;
            cards = c;
            selected = new HashSet<int>();
            matched = new HashSet<int>();

            menu = previousMenu;

            if (difficulty.Equals("Easy"))
            {
                numCards = 3;
            } else if (difficulty.Equals("Medium"))
            {
                numCards = 6;
            } else if(difficulty.Equals("Hard"))
            {
                numCards = 9;
            }
            setCardBackgrounds();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void Window_Size_Did_Change(object sender, RoutedEventArgs e)
        {

        }
        
        private void swapBackgrounds(string[] array, int a, int b)
        {
            string temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }


        public void setCardBackgrounds()
        {
            // TODO - If this only randomized the first 9 cards (and then the second 9 are in the same order as the first 9):
            // first duplicate the string of backgrounds, so it's twice as long.  then remove the loop below that has it run
            // twice, and only run it once.  This will be able to randomize all 18 cards then.

            Random rng = new Random();
            // shuffling this array (maybe just swap two random numbers a few times) should work
            string[] randBackgrounds = backgrounds;
            int backgroundSize = randBackgrounds.Count();
            // call the swap function a few times, should randomize the inputs
            swapBackgrounds(randBackgrounds, 0, 2); // swaps elements 0 and 2
            swapBackgrounds(randBackgrounds, 1, 2);

            //.OrderBy(a => rng.Next());
            for (int i = 0; i < numCards * 2; i++)
            {
                Button myButton = new Button
                {
                    Width = CardController.width,
                    Height = CardController.height,
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri(randBackgrounds.ElementAt(i % randBackgrounds.Count()))),
                        VerticalAlignment = VerticalAlignment.Center
                    }
                };
                myButton.Background = Brushes.Transparent;
                myButton.BorderBrush = Brushes.Black;
                myButton.IsHitTestVisible = false;

                Canvas.SetLeft(myButton, cards.ElementAt(i).rightXcoordinate);
                Canvas.SetTop(myButton, cards.ElementAt(i).topYcoordinate);
                cards.ElementAt(i).setButton(myButton);

                gameCanvas.Children.Add(cards.ElementAt(i).getButton());
            }
        }

        public void restartBoard(Canvas gameCanvas)
        {
            string path = Directory.GetCurrentDirectory();
            Image winningImg = new Image
            {
                Source = new BitmapImage(new Uri(path))
            };
            Canvas.SetLeft(winningImg, 100);
            Canvas.SetTop(winningImg, 10);
            gameCanvas.Children.Add(winningImg);

            Button quit = new Button { Content = "Quit" };
            Button restart = new Button { Content = "Restart" };
            Canvas.SetLeft(quit, 100);
            Canvas.SetBottom(quit, 100);
            Canvas.SetRight(restart, 100);
            Canvas.SetBottom(restart, 100);
            gameCanvas.Children.Add(quit);
            gameCanvas.Children.Add(restart);
        }

        public void flipCard(int index)
        {

        }

        private void setCardBorder(int index, Brush brush)
        {
            cards.ElementAt(index).getButton().BorderBrush = brush;
        }

        public List<CardController> getCards()
        {
            return cards;
        }

        public void selectCard(int index)
        {
            if (matched.Contains(index))
            {
                return;
            }
            setCardBorder(index, Brushes.Yellow);
            selected.Add(index);
            if (selected.Count() == 2)
            {
                bool isMatching = cards.ElementAt(index).checkMatch(cards.ElementAt(selected.First()));
                //Console.WriteLine("Checking to see if card " + cards.ElementAt(index).index + " is equal to card " + cards.ElementAt(selected.First()).index);
                //if (isMatching)
                //    Console.WriteLine("!!!!!! We have a match ");
                foreach (int i in selected)
                {
                    setCardBorder(i, isMatching ? Brushes.Green : Brushes.Black);
                    if (isMatching)
                    {
                        matched.Add(i);
                        // remove the card from list
                        Console.WriteLine("remove card at index " + i);
                        // card can only be removed by replacement of another blank card at the same spot on the game canvas grid
                        CardController blankCard = new CardController(true, true, cards.ElementAt(i).topYcoordinate, cards.ElementAt(i).leftXcoordinate, i);
                        Button blank = new Button
                        {
                            Width = CardController.width,
                            Height = CardController.height
                        };
                        Canvas.SetLeft(blank, blankCard.rightXcoordinate);
                        Canvas.SetTop(blank, blankCard.topYcoordinate);
                        cards.ElementAt(i).setButton(blank);
                        gameCanvas.Children.Add(cards.ElementAt(i).getButton());
                    }
                }
                selected.Clear();
                if (allCardsMatched())
                {
                    Restart restartMenu = new Restart(menu);
                    App.Current.MainWindow = restartMenu;
                    restartMenu.Show();
                    this.Close();
                }
            }
        }

        public bool allCardsMatched()
        {
            return cards.Count() == matched.Count();
        }
    }

}
