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
    class BackgroundController
    {
        private List<CardController> cards;
        private string[] backgrounds;
        private Canvas grid;

        private HashSet<int> selected = new HashSet<int>();
        private HashSet<int> matched = new HashSet<int>();

        public BackgroundController(Canvas g, string[] b, List<CardController> c)
        {
            grid = g;
            backgrounds = b;
            cards = c;


            setCardBackgrounds();

        }



        public void setCardBackgrounds()
        {
            Random rng = new Random();
            var randBackgrounds = backgrounds.OrderBy(a => rng.Next());
            for (int i = 0; i < cards.Count(); i++)
            {
                Button myButton = new Button
                {
                    Width = 50,
                    Height = 72,
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

        public void restartBoard()
        {

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
            setCardBorder(index, Brushes.Red);
            selected.Add(index);
            if (selected.Count() == 2)
            {
                bool isMatching = cards.ElementAt(index).checkMatch(cards.ElementAt(selected.First()));
                foreach (int i in selected)
                {
                    setCardBorder(i, isMatching ? Brushes.Green : Brushes.Black);
                    if (isMatching)
                    {
                        matched.Add(index);
                    }
                }
                selected.Clear();
            }
        }

    }
}
