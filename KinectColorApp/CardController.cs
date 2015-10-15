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

        // constructor that takes no arguments
        public CardController()
        {
            isPressed = false;
            topYcoordinate = 0;
            bottomYcoordinate = 0;
            leftXcoordinate = 0;
            rightXcoordinate = 0;
            centerXcoordinate = 0;
            centerYcoordinate = 0;
        }

        // constructor that takes (more) arguments
        public CardController(bool isPrssd, int topY, int botY, int leftX, int rightX)
        {
            isPressed = isPrssd;
            topYcoordinate = topY;
            bottomYcoordinate = botY;
            leftXcoordinate = leftX;
            rightXcoordinate = rightX;
            centerXcoordinate = (leftXcoordinate + rightXcoordinate) / 2;
            centerYcoordinate = (topYcoordinate + bottomYcoordinate) / 2;
        }


        // create methods for each value
        public void setPressed(bool press) { isPressed = press; }
        public void setTopY(int newTopY) { topYcoordinate = newTopY; }
        public void setBotY(int newBotY) { bottomYcoordinate = newBotY; }
        public void setLeftX(int newLeftX) { leftXcoordinate = newLeftX; }
        public void setRightX(int newRightX) { rightXcoordinate = newRightX; }

        // later on, make this pass by reference
        public List<CardController> initializeCards ()
        {
            List<CardController> cards = new List<CardController>(20);

            /* --------------- THE GAME BOARD IS ORGANIZED ASSUMING THE FOLLOWING CARD PLACEMENT -------------------

                0       1       2       3       4
                5       6       7       8       9
                10      11      12      13      14
                15      16      17      18      19

                note that not all 20 cards need be displayed at once, however their locations and sizes are absolute
            ------------------------------------------------------------------------------------------------------*/
            // Hardcode in 20 cards, directly into the list that's returned
            // Code x and y coordinates as a function of xResolution and yResolution
            cards[0]  = new CardController(false, 0, 1, 2, 3);  // card 0 is top left
            cards[1]  = new CardController(false, 0, 1, 2, 3);  // card 1 is second from left, top row
            cards[2]  = new CardController(false, 0, 1, 2, 3);
            cards[3]  = new CardController(false, 0, 1, 2, 3); 
            cards[4]  = new CardController(false, 0, 1, 2, 3);  // card 4 is the top right
            cards[5]  = new CardController(false, 0, 1, 2, 3);  // card 5 is the leftmost card in the second-from-the-top row
            cards[6]  = new CardController(false, 0, 1, 2, 3);
            cards[7]  = new CardController(false, 0, 1, 2, 3);
            cards[8]  = new CardController(false, 0, 1, 2, 3);
            cards[9]  = new CardController(false, 0, 1, 2, 3);
            cards[10] = new CardController(false, 0, 1, 2, 3);
            cards[11] = new CardController(false, 0, 1, 2, 3);
            cards[12] = new CardController(false, 0, 1, 2, 3);
            cards[13] = new CardController(false, 0, 1, 2, 3);
            cards[14] = new CardController(false, 0, 1, 2, 3);
            cards[15] = new CardController(false, 0, 1, 2, 3);
            cards[16] = new CardController(false, 0, 1, 2, 3);
            cards[17] = new CardController(false, 0, 1, 2, 3);
            cards[18] = new CardController(false, 0, 1, 2, 3);
            cards[19] = new CardController(false, 0, 1, 2, 3);  // card 19 is the bottom right card
            return cards;   // is this necessary?  does C# pass by reference?
        }
        

        // later on, make this pass by reference
        // This function is only called if the depth is sufficient enough to constitute a press
        // It takes in the location of a press and the list of card locations
        // will have to later update so that it's only looking at cards that are still on the board
        // if the location of the touch is within a card's boundaries, isPressed flips to true.
        // Not sure where to reset isPressed to false yet though
        public List<CardController> updatePressed (List <CardController> cards, double x, double y)
        {

            // I am not positive - needs further testing
            // This logic assumes that the bottom left of the screen is x=0,y=0, top right x=640, y=480
            for (int i = 0; i < 20; i++)
            {
                // if x < rightXcoord && x > leftxcoord && y < topYcord && y > bottomYcoord
                if ( x < cards[i].rightXcoordinate && x > cards[i].leftXcoordinate && y < cards[i].topYcoordinate && y > cards[i].bottomYcoordinate)
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

