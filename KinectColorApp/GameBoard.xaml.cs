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
using System.Windows.Media.Effects;

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
            Image image = new Image(); 
            image.Source = new BitmapImage( new Uri(Directory.GetCurrentDirectory() + @"\..\..\Resources\retina_wood_@2X.png", UriKind.Absolute));
            backgroundImage.ImageSource = image.Source;
            difficulty = d;
            backgrounds = b;
            cards = c;
            selected = new HashSet<int>();
            matched = new HashSet<int>();

            menu = previousMenu;


            if (difficulty.Equals("Easy"))
            {
                numCards = 3;
            }
            else if (difficulty.Equals("Medium"))
            {
                numCards = 6;
            }
            else if (difficulty.Equals("Hard"))
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
            //string[] randBackgrounds = backgrounds + backgrounds;
            string[] randBackgrounds = new string[numCards * 2];
            // make randBackgrounds an array of the filenames that will be on the board
            for (int x = 0; x < numCards; x++)
            {
                randBackgrounds[x] = backgrounds[x];
                randBackgrounds[x + numCards] = backgrounds[x];
            }

            // if numCards == 18:
            if (numCards == 9)
            {
                // can swap all: 0 - 17
                for (int i = 0; i < 20; i++)
                {
                    int x = rng.Next(0, 18); // creates a number between 0 and 17
                    int y = rng.Next(0, 18); // creates a number between 0 and 17
                    swapBackgrounds(randBackgrounds, x, y);
                }
            }

            // if numCards == 12:
            if (numCards == 6)
            {
                // can swap  0 - 11
                for (int i = 0; i < 20; i++)
                {
                    int x = rng.Next(0, 12); // creates a number between 0 and 11
                    int y = rng.Next(0, 12); // creates a number between 0 and 11
                    swapBackgrounds(randBackgrounds, x, y);
                }
            }
            // if numCards == 6:
            if (numCards == 3)
            {
                // can swap 0 - 5
                for (int i = 0; i < 20; i++)
                {
                    int x = rng.Next(0, 6); // creates a number between 0 and 5
                    int y = rng.Next(0, 6); // creates a number between 0 and 5
                    swapBackgrounds(randBackgrounds, x, y);
                }
            }

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
                myButton.Background = Brushes.White;
                myButton.BorderBrush = Brushes.Black;
                myButton.BorderThickness = new Thickness(5);
                myButton.IsHitTestVisible = false;

                DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
                // Set the color of the shadow to Black.
                Color myShadowColor = new Color();
                myShadowColor.ScA = 1;
                myShadowColor.ScB = 0;
                myShadowColor.ScG = 0;
                myShadowColor.ScR = 0;
                myDropShadowEffect.Color = myShadowColor;

                // Set the direction of where the shadow is cast to degrees.
                myDropShadowEffect.Direction = 350;

                // Set the depth of the shadow being cast.
                myDropShadowEffect.ShadowDepth = 15;

                // Set the shadow softness to the maximum (range of 0-1).
                myDropShadowEffect.Softness = .2;
                // Set the shadow opacity to half opaque or in other words - half transparent.
                // The range is 0-1.
                myDropShadowEffect.Opacity = 0.3;
                myButton.BitmapEffect = myDropShadowEffect;

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
