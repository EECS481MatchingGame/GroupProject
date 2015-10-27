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
       }

 

        public void setCardBackgrounds()
        {
           for (int i = 0; i < cards.Count(); i++) { 
                var img = new Image { Width = 50, Height = 72};
                var bitmapImage = new BitmapImage(new Uri(backgrounds.ElementAt(i)));

                img.Source = bitmapImage;

                Canvas.SetLeft(img, cards.ElementAt(i).rightXcoordinate);
                Canvas.SetTop(img, cards.ElementAt(i).topYcoordinate);
                grid.Children.Add(img);

                
            }
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
