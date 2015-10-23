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
    class CardController
    {
        // variables contained in card - we can add more!  Such as an image file, bool showOnBoard, etc
        public bool isPressed;

        public static int width = 50;
        public static int height = 72;

        public int topYcoordinate;
        public int bottomYcoordinate;
        public int leftXcoordinate;
        public int rightXcoordinate;
        public int centerXcoordinate;
        public int centerYcoordinate;

        // constructor that takes all arguments
        public CardController(bool isPrssd, int topY, int leftX)
        {
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
        
        public static void initializeCards (ref List<CardController> cards)
        {
            /* --------------- THE GAME BOARD IS ORGANIZED ASSUMING THE FOLLOWING CARD PLACEMENT -------------------

                0       1       2       3       4       5
                6       7       8       9       10      11
                12      13      14      15      16      17

                note that not all 20 cards need be displayed at once, however their locations and sizes are absolute
                
            board is 640w*480h, use margins of 20w and 20h
            ------------------------------------------------------------------------------------------------------*/
            // Hardcode in 20 cards, directly into the list that's returned

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cards[i * j + j] = new CardController(false, 20 + i * 2 * height, 20 + j * 2 * width);
                }
            }
        }
        

        // later on, make this pass by reference
        // This function is only called if the depth is sufficient enough to constitute a press
        // It takes in the location of a press and the list of card locations
        // will have to later update so that it's only looking at cards that are still on the board
        // if the location of the touch is within a card's boundaries, isPressed flips to true.
        // Not sure where to reset isPressed to false yet though
        public static void updatePressed (ref List <CardController> cards, short[] rawDepthData)
        {
            // change the argument when i figure out what constitutes a coordinate - probably more specific than raw depth data

            cards[1].isPressed(true);  // etc
        }

    }

}

