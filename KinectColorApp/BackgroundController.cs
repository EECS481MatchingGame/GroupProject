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
    struct BackgroundController
    {
        private List<CardController> cards;
        private string[] backgrounds;
        private Canvas grid;

        private HashSet<int> selected;
        private HashSet<int> matched;

        public BackgroundController(Canvas g)
        {
            grid = g;
            backgrounds = null;
            cards = null;
            selected = new HashSet<int>();
            matched = new HashSet<int>();
        }

        public BackgroundController(Canvas g, string[] b, List<CardController> c)
        {
            grid = g;
            backgrounds = b;
            cards = c;
            selected = new HashSet<int>();
            matched = new HashSet<int>();
            setCardBackgrounds();
        }

        public void setCardBackgrounds()
        {
            Random rng = new Random();
            var randBackgrounds = backgrounds; 
            //.OrderBy(a => rng.Next());
            for (int i = 0; i < cards.Count(); i++)
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

                Canvas.SetLeft(myButton, cards.ElementAt(i).rightXcoordinate);
                Canvas.SetTop(myButton, cards.ElementAt(i).topYcoordinate);
                cards.ElementAt(i).setButton(myButton);

                grid.Children.Add(cards.ElementAt(i).getButton());
            }
        }

        public void restartBoard(Canvas grid)
        {
            string path = Directory.GetCurrentDirectory();
            Image winningImg = new Image
            {
                Source = new BitmapImage(new Uri(path))
            };
            Canvas.SetLeft(winningImg, 100);
            Canvas.SetTop(winningImg, 10);
            grid.Children.Add(winningImg);

            Button quit = new Button { Content = "Quit" };
            Button restart = new Button { Content = "Restart" };
            Canvas.SetLeft(quit, 100);
            Canvas.SetBottom(quit, 100);
            Canvas.SetRight(restart, 100);
            Canvas.SetBottom(restart, 100);
            grid.Children.Add(quit);
            grid.Children.Add(restart);
        }       

        public void flipCard(int index)
        {

        }

        private void setCardBorder(int index, Brush brush)
        {
            cards.ElementAt(index).getButton().BorderBrush = brush;
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
                foreach (int i in selected)
                {
                    setCardBorder(i, isMatching ? Brushes.Green : Brushes.Black);
                    if (isMatching)
                    {
                        matched.Add(i);
                    }
                }
                selected.Clear();
            }
        }

        public bool finishedARound() // pre requirement is cards.Count > 0
        {
            if (cards.Count() == matched.Count())
                return true;
            else
                return false;
        }
    }
}
