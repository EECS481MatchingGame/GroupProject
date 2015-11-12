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
    // internal class?
    class CardController
    {
        // variables contained in card - we can add more!  Such as an image file, bool showOnBoard, etc
        public bool isPressed;
        public int topYcoordinate;
        public int bottomYcoordinate;
        public int leftXcoordinate;
        public int rightXcoordinate;
        public int centerXcoordinate;
        public int centerYcoordinate;
        private static int xResolution = 640;
        private static int yResolution = 480;
        public static int width = 50;
        public static int height = 72;
        public bool isShown;
        public Button img; 

        // constructor that takes no arguments
        public CardController()
        {
            isShown = true;
            isPressed = false;
            topYcoordinate = 0;
            bottomYcoordinate = 0;
            leftXcoordinate = 0;
            rightXcoordinate = 0;
            centerXcoordinate = 0;
            centerYcoordinate = 0;
        }

        // constructor that takes (more) arguments
        public CardController(bool isShwn, bool isPrssd, int topY, int leftX)
        {
            isShown = isShwn;
            isPressed = isPrssd;
            topYcoordinate = topY;
            leftXcoordinate = leftX;
            bottomYcoordinate = topY + height;
            rightXcoordinate = leftX + width;
            centerXcoordinate = (leftXcoordinate + rightXcoordinate) / 2;
            centerYcoordinate = (topYcoordinate + bottomYcoordinate) / 2;
        }


        // create methods for each value
        public void setPressed(bool press) { isPressed = press; }
        public void setTopY(int newTopY) { topYcoordinate = newTopY; }
        public void setBotY(int newBotY) { bottomYcoordinate = newBotY; }
        public void setLeftX(int newLeftX) { leftXcoordinate = newLeftX; }
        public void setRightX(int newRightX) { rightXcoordinate = newRightX; }

        public void setImage(Button i)
        {
            img = i;
            Canvas.SetLeft(img, rightXcoordinate);
            Canvas.SetTop(img, topYcoordinate);
        }

        public Button getImage()
        {
            return img;
        }

        // later on, make this pass by reference
        public List<CardController> initializeCards()
        {
            List<CardController> cards = new List<CardController>(20);

            // Hardcode in 20 cards, directly into the list that's returned
            // Code x and y coordinates as a function of xResolution and yResolution

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    cards.Add(new CardController(true, false, 20 + i * 2 * height, 20 + j * 2 * width));
                }
            }

            return cards;
        }


        // This function is only called if the depth is sufficient enough to constitute a press
        // It takes in the location of a press and the list of card locations
        // will have to later update so that it's only looking at cards that are still on the board
        // if the location of the touch is within a card's boundaries, isPressed flips to true.
        // Not sure where to reset isPressed to false yet though
        public List<CardController> updatePressed(List<CardController> cards, double x, double y)
        {

            // I am not positive - needs further testing
            // This logic assumes that the bottom left of the screen is x=0,y=0, top right x=640, y=480
            for (int i = 0; i < cards.Count(); i++)
            {
                // if x < rightXcoord && x > leftxcoord && y < topYcord && y > bottomYcoord
                if (x < cards[i].rightXcoordinate && x > cards[i].leftXcoordinate && y < cards[i].topYcoordinate && y > cards[i].bottomYcoordinate)
                {
                    //card is pressed is set to true
                    cards[i].setPressed(true);
                    Console.WriteLine("Card ", i, " is pressed ");
                }

            }

            return cards;
        }

    }

}
