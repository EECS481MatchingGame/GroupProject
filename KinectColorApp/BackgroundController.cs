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

        public BackgroundController(Canvas g, string[] b, List<CardController> c)
       {
            grid = g;
            backgrounds = b;
            cards = c;


            setCardBackgrounds();
            cards.ElementAt(0).getImage().BorderBrush = Brushes.Red;

        }



        public void setCardBackgrounds()
        {
           for (int i = 0; i < cards.Count(); i++) {
                Button myButton = new Button
                {
                    Width = 50,
                    Height = 72,
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri(backgrounds.ElementAt(i % backgrounds.Count()))),
                        VerticalAlignment = VerticalAlignment.Center
                    }
                };

                myButton.Background = Brushes.Transparent;
             
                Canvas.SetLeft(myButton, cards.ElementAt(i).rightXcoordinate);
                Canvas.SetTop(myButton, cards.ElementAt(i).topYcoordinate);
                //var img = new Image { Width = 50, Height = 72};
                //var bitmapImage = new BitmapImage(new Uri(backgrounds.ElementAt(i)));

                //img.Source = bitmapImage;
                cards.ElementAt(i).setImage(myButton);


                grid.Children.Add(cards.ElementAt(i).getImage());



            }

            cards.ElementAt(0).img.Background = Brushes.Red;
        }

        public void restartBoard()
        {

        }

        public void flipCard(int row, int col)
        {

        }

        public void selectCard(int row, int col)
        {
            //maybe change card border color
        }

        public void unselectCard(int row, int col)
        {

        }


    }
}
