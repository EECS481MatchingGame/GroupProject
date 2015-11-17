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
        private static int boardWidth = 640;
        private static int boardHeight = 480;

        public static int width = 50;
        public static int height = 72;

        // variables contained in card - we can add more!  Such as an image file, bool showOnBoard, etc
        public bool isPressed;
        public int topYcoordinate;
        public int bottomYcoordinate;
        public int leftXcoordinate;
        public int rightXcoordinate;
        public int centerXcoordinate;
        public int centerYcoordinate;
        public bool isShown;
        public Button button;
        public int index;       // a unique identifier for the card - maybe add another for "matched index?"

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
            index = -1;
        }

        // constructor that takes (more) arguments
        public CardController(bool isShwn, bool isPrssd, int topY, int leftX, int indx)
        {
            isShown = isShwn;
            isPressed = isPrssd;
            topYcoordinate = topY;
            leftXcoordinate = leftX;
            bottomYcoordinate = topY + height;
            rightXcoordinate = leftX + width;
            centerXcoordinate = (leftXcoordinate + rightXcoordinate) / 2;
            centerYcoordinate = (topYcoordinate + bottomYcoordinate) / 2;
            index = indx;
        }


        // create methods for each value
        public void setPressed(bool press) { isPressed = press; }
        public void setTopY(int newTopY) { topYcoordinate = newTopY; }
        public void setBotY(int newBotY) { bottomYcoordinate = newBotY; }
        public void setLeftX(int newLeftX) { leftXcoordinate = newLeftX; }
        public void setRightX(int newRightX) { rightXcoordinate = newRightX; }

        public void setButton(Button i)
        {
            button = i;
            Canvas.SetLeft(button, rightXcoordinate);
            Canvas.SetTop(button, topYcoordinate);
        }

        public Button getButton()
        {
            return button;
        }

        // later on, make this pass by reference
        public List<CardController> initializeCards()
        {
            List<CardController> cards = new List<CardController>();

            // Hardcode in 20 cards, directly into the list that's returned
            // Code x and y coordinates as a function of xResolution and yResolution

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int setIndex = (i * 6) + j; // gives each card a unique identifier
                    // We have an issue here where the largest x is 570 and the largest y is 308
                    // we want the largest x to be 640 and the largest y to be 480
                    cards.Add(new CardController(true, false, 20 + i * 2 * height, 20 + j * 2 * width, setIndex));
                }
            }

            // shown, pressed, topY, leftX, index 
            /*cards.Add( new CardController(true, false, 0, 0, 0));  // card 0 is top left from OUR perspective
            cards.Add(new CardController(true, false, 0, 100, 1));  // card 1 is second from left, top row
            cards.Add(new CardController(true, false, 0, 200, 2));
            cards.Add(new CardController(true, false, 0, 300, 3));
            cards.Add(new CardController(true, false, 0, 400, 4));  // card 4 is the top right
            cards.Add(new CardController(true, false, 0, 500, 5));  // card 5 is the leftmost card in the second-from-the-top row
            cards.Add(new CardController(true, false, 150, 0, 6));
            cards.Add(new CardController(true, false, 150, 100, 7));
            cards.Add(new CardController(true, false, 150, 200, 8));
            cards.Add(new CardController(true, false, 150, 300, 9));
            cards.Add(new CardController(true, false, 150, 400, 10));
            cards.Add(new CardController(true, false, 150, 500, 11));
            cards.Add(new CardController(true, false, 300, 0, 12));
            cards.Add(new CardController(true, false, 300, 100, 13));
            cards.Add(new CardController(true, false, 300, 200, 14));
            cards.Add(new CardController(true, false, 300, 300, 15));
            cards.Add(new CardController(true, false, 300, 400, 16));
            cards.Add(new CardController(true, false, 300, 500, 17));*/


            return cards;
        }


        // This function is only called if the depth is sufficient enough to constitute a press
        // It takes in the location of a press and the list of card locations
        // will have to later update so that it's only looking at cards that are still on the board
        public int updatePressed(List<CardController> cards, double x, double y)
        {

            /*
                Bottom right from the user's perspective is roughly 640, 450
                Middle from the user's perspective is roughly 530, 307
                top left from the user's perspective is 640, 450

            */




            // TODO - does cards.Count() only look at cards that are still on the board?  does it even matter?
            
            // This logic assumes that the bottom left of the screen is x=0,y=0, top right x=640, y=480
            for (int i = 0; i < cards.Count(); i++)
            {
                //Console.WriteLine("Given x= " + x + ", should be less than " + cards[i].rightXcoordinate + " and greater than " + cards[i].leftXcoordinate);
                // if x < rightXcoord && x > leftxcoord && y < topYcord && y > bottomYcoord

                //Console.WriteLine("Given y= " + y + ", should be greater than " + cards[i].topYcoordinate + " and less than " + cards[i].bottomYcoordinate);
                if (x < cards[i].rightXcoordinate && x > cards[i].leftXcoordinate && y > cards[i].topYcoordinate && y < cards[i].bottomYcoordinate)
                {
                    //card is pressed is set to true
                    //cards[i].setPressed(true);
                    Console.WriteLine("Card " + i + " is pressed ");
                    return cards[i].index;  // return the index num of the card in these coordinates
                }

            }

            // no card was found at these coordinates, returns -1
            return -1;
        }

        // determines whether or not this card matches with another
        public Boolean checkMatch(CardController other)
        {
            // TODO this function

            // Need to get the filename or some association of what matches with what, 
            // and compare this.index with other.matchingIndex

            // how are files imported/do they have any association to a card?

            return ((Image) button.Content).Source == ((Image)other.button.Content).Source;
        }

    }

}
