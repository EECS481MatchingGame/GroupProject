using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using System.Windows.Shapes;
using System.Windows.Media.Effects;

namespace KinectColorApp
{
    public class KinectController : CardController
    {
        private Image debugImage;
        private DrawController drawController;
        private SoundController soundController;
        private GameBoard gameBoard;
        Ellipse[] buttons;

        DateTime last_background_change = DateTime.Now;
        private bool hasSetDepthThreshold = false;
        private int DepthThreshold = 9000000;
        const int TextileSpacing = 5; // How deep do we have to push in to start drawing?

        // Variables used for calibration
        public double[] calibration_coefficients;
        private Point topLeft;
        private Point bottomRight;

        public KinectController(DrawController dController, Image image, SoundController sController, Ellipse[] buttons)
        {
            debugImage = image;
            drawController = dController;
            soundController = sController;
            this.buttons = buttons;
        }

        public void Calibrate(int top_left_x, int top_left_y, int bottom_right_x, int bottom_right_y)
        {
            if (top_left_y > 480 || top_left_y < 0)
            {
                top_left_y = 0;
            }

            if (bottom_right_y > 480 || bottom_right_y < 0)
            {
                bottom_right_y = 480;
            }

            topLeft = new Point(top_left_x, top_left_y);
            bottomRight = new Point(bottom_right_x, bottom_right_y);
        }

        public void SensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            // Check if we need to change background
            if (drawController.backgroundAlreadySet == false)
            {
                drawController.ChangeBackground(drawController.background);
            }

            // Check if we need to change color
            if (drawController.shouldChangeColor != -1)
            {
                drawController.ChangeColor((Colors)drawController.shouldChangeColor);
            }

            using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
            {
                if (depthFrame != null)
                {
                    this.ParseDepthFrame(depthFrame);
                }
            }
        }

        #region Getting textile touches

        bool gotTouch = false;
        // Evertyhing below is reverted

        private void ParseDepthFrame(DepthImageFrame depthFrame)
        {
            Console.WriteLine("parsing new");

            short[] rawDepthData = new short[depthFrame.PixelDataLength];
            depthFrame.CopyPixelDataTo(rawDepthData);
            int minDepth = DepthThreshold;
            int bestDepthIndex = -1;
            int minDepthIndex = (int)this.topLeft.Y * depthFrame.Width;
            int maxDepthIndex = (int)this.bottomRight.Y * depthFrame.Width;

            minDepthIndex = 0;
            maxDepthIndex = 479 * depthFrame.Width;
            //Console.WriteLine("maxDepthIndex = " + maxDepthIndex);

            //Console.WriteLine(minDepthIndex + " " + depthFrame.Width);
            for (int depthIndex = minDepthIndex; depthIndex < maxDepthIndex; depthIndex++)
            {

                int depth = rawDepthData[depthIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                // Ignore invalid depth values
                if (depth == -1 || depth == 0) continue;



                if (depth < minDepth)
                {
                    minDepth = depth;
                    bestDepthIndex = depthIndex;
                }
            }

            // Draw if a touch was found
            if (bestDepthIndex >= 0)
            {
                if (!this.hasSetDepthThreshold)
                {
                    this.DepthThreshold = minDepth - TextileSpacing;
                    this.hasSetDepthThreshold = true;
                }
                else
                {
                    soundController.StartMusic();
                    double x_kinect = (bestDepthIndex % depthFrame.Width);
                    double y_kinect = (bestDepthIndex / depthFrame.Width);
                    double x = x_kinect * calibration_coefficients[0] + y_kinect * calibration_coefficients[1] + calibration_coefficients[2] + 3;
                    double y = x_kinect * calibration_coefficients[3] + y_kinect * calibration_coefficients[4] + calibration_coefficients[5] + 10;

                    //Console.WriteLine("KINECT: Touch registered at " + x_kinect + ", " + y_kinect + " at depth " + bestDepthIndex);
                    //Console.WriteLine("DEFAULT: Touch registered at " + x + ", " + y + " at depth " + bestDepthIndex);

                    gotTouch = true;
                    int matchedIndex = gameBoard != null ? updatePressed(gameBoard.getCards(), x, y) : -1;
                    if (matchedIndex >= 0)
                    {
                        Console.WriteLine("A press has been found at " + matchedIndex);
                        gameBoard.selectCard(matchedIndex);
                    }

                }
            }
            else
            {
                if (gotTouch == true)
                {
                    soundController.StopMusic();
                }

                gotTouch = false;
            }
        }
















        // Everything above is the reverted

        private void ParseDepthFrameOld(DepthImageFrame depthFrame)
        {
            Console.WriteLine("parsing");

            short[] rawDepthData = new short[depthFrame.PixelDataLength];
            depthFrame.CopyPixelDataTo(rawDepthData);

            int minDepth = DepthThreshold;
            int minDepthIndex = (int)this.topLeft.Y * depthFrame.Width;
            int maxDepthIndex = (int)this.bottomRight.Y * depthFrame.Width;
            int matchedIndex = -1;
            int bestDepthIndex = -1;
            double checkThisX = 0;
            double checkThisY = 0;

            minDepthIndex = 0;
            maxDepthIndex = 479 * depthFrame.Width;
            // This output was causin a crash I believe
            // Console.WriteLine("minDepthIndex: " + minDepthIndex + " depthFrame.Width" + depthFrame.Width);
            for (int depthIndex = minDepthIndex; depthIndex < maxDepthIndex; depthIndex++)
            {/*
                // Skip this depth index if it's horizontally outside of our textile
                int x_kinect = (int)((depthIndex) % depthFrame.Width);
                
                if (x_kinect < topLeft.X) { continue; }
                else if (x_kinect > bottomRight.X)
                {
                    //depthIndex += (depthFrame.Width - (int)(bottomRight.X - topLeft.X - 1));
                    continue;
                }*/

                int depth = rawDepthData[depthIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                // Ignore invalid depth values
                if (depth == -1 || depth == 0) continue;

                double x_kinect = (depthIndex % depthFrame.Width);
                double y_kinect = (depthIndex / depthFrame.Width);

                double x = x_kinect * calibration_coefficients[0] + y_kinect * calibration_coefficients[1] + calibration_coefficients[2] + 3;
                double y = x_kinect * calibration_coefficients[3] + y_kinect * calibration_coefficients[4] + calibration_coefficients[5] + 10;


                // TODO: For now this won't support multitouch - but should be okay for now
                if (depth < minDepth)
                {
                    minDepth = depth;
                    bestDepthIndex = depthIndex;
                }

                // if depth threshold is met: - set it to 200000, and why are x and y off?
                if (bestDepthIndex >= 150000)
                {
                    if (!this.hasSetDepthThreshold)
                    {
                        this.DepthThreshold = minDepth - TextileSpacing;
                        this.hasSetDepthThreshold = true;

                    }
                    else
                    {
                        checkThisX = x_kinect;
                        checkThisY = y_kinect;
                        //Console.WriteLine("Depth threshold of a press has been met");
                        // check if x and y are within a card boundary that's still on the board
                        //Console.WriteLine("X = " + x + " Y = " + y);
                        //Console.WriteLine("x_kinect = " + x_kinect + " y_kinect = " + y_kinect);
                        //matchedIndex = updatePressed(cards, x_kinect, y_kinect);
                    }
                }
                bestDepthIndex = -1;



                /*
                No longer going to use bestDepthIndex logic - but keep minDepth and calculations above it
                if (depth < minDepth)
                {
                    minDepth = depth;
                    bestDepthIndex = depthIndex;
                }*/
            }

            //matchedIndex = updatePressed(cards, checkThisX, checkThisY);
            // USE BACKGROUNDCONTROLLER TO SELECT CARDS HERE
            if (matchedIndex > -1)
            {
                Console.WriteLine("matchedIndex is not -1, it is " + matchedIndex);
                gameBoard.selectCard(matchedIndex);
            }
            matchedIndex = -1;

            /* --------------
            Commented out the below code - keep for later reference.  We don't need to draw if the touch was found yet,
            and when we do, it won't be in this implementation
            -----------------*/

            // Draw if a touch was found
            /*if (bestDepthIndex >= 0)
            {
                Console.WriteLine("here");
                if (!this.hasSetDepthThreshold) {
                    this.DepthThreshold = minDepth - TextileSpacing;
                    this.hasSetDepthThreshold = true;
                }
                else
                {
                    soundController.StartMusic();
                    DrawPoint(depthFrame, bestDepthIndex, minDepth);
                    gotTouch = true;
                } 
            }
            else
            {
                if (gotTouch == true)
                {
                    soundController.StopMusic();
                    drawController.SaveCanvas();
                }

                gotTouch = false;
            }*/
        }

        public void setGameBoard(GameBoard gBoard)
        {
            gameBoard = gBoard;
        }

        private void DrawPoint(DepthImageFrame depthFrame, int depthIndex, int minDepth)
        {
            double x_kinect = (depthIndex % depthFrame.Width);
            double y_kinect = (depthIndex / depthFrame.Width);

            double x = x_kinect * calibration_coefficients[0] + y_kinect * calibration_coefficients[1] + calibration_coefficients[2] + 3;
            double y = x_kinect * calibration_coefficients[3] + y_kinect * calibration_coefficients[4] + calibration_coefficients[5] + 10;

            foreach (Ellipse ellipse in buttons)

            {
                double top = Canvas.GetTop(ellipse);
                double left = Canvas.GetLeft(ellipse);

                if (y >= top && x >= left && y <= top + ellipse.Height && x <= left + ellipse.Width)
                {
                    DropShadowEffect glowEffect = new DropShadowEffect();
                    glowEffect.ShadowDepth = 0;
                    glowEffect.Opacity = 1;
                    glowEffect.BlurRadius = 30;

                    if (ellipse.Name != "refresh_selector" && ellipse.Name != "background_selector")
                    {
                        foreach (Ellipse el in buttons)
                        {
                            if (el.Name != "refresh_selector" && el.Name != "background_selector")
                            {
                                el.Fill.Opacity = 0.3;
                                el.Effect = null;
                            }
                        }
                    }

                    // Use this button
                    if (ellipse.Name == "red_selector")
                    {
                        ellipse.Fill.Opacity = 1;
                        glowEffect.Color = Color.FromArgb(255, 255, 44, 44);
                        ellipse.Effect = glowEffect;
                        drawController.ChangeColor(Colors.Red);
                    }
                    else if (ellipse.Name == "green_selector")
                    {
                        ellipse.Fill.Opacity = 1;
                        glowEffect.Color = Color.FromArgb(255, 53, 255, 53);
                        ellipse.Effect = glowEffect;
                        drawController.ChangeColor(Colors.Green);
                    }
                    else if (ellipse.Name == "blue_selector")
                    {
                        ellipse.Fill.Opacity = 1;
                        glowEffect.Color = Color.FromArgb(255, 115, 78, 255);
                        ellipse.Effect = glowEffect;
                        drawController.ChangeColor(Colors.Blue);
                    }
                    else if (ellipse.Name == "eraser_selector")
                    {
                        ellipse.Fill.Opacity = 1;
                        glowEffect.Color = Color.FromArgb(255, 255, 255, 255);
                        ellipse.Effect = glowEffect;
                        drawController.ChangeColor(Colors.White);
                    }
                    else if (ellipse.Name == "background_selector")
                    {
                        TimeSpan interval = DateTime.Now - last_background_change;
                        if (interval.Seconds >= 0.5)
                        {
                            drawController.CycleBackgrounds();
                            last_background_change = DateTime.Now;
                        }
                    }
                    else if (ellipse.Name == "refresh_selector")
                    {
                        drawController.ClearScreen();
                    }

                    return;
                }
            }
            drawController.DrawEllipseAtPoint(x, y, (DepthThreshold - minDepth));
        }

        #endregion

        #region Image creation

        // Call this function inside AllFramesReady to display a depth debugging feed
        void display_depth_feed(DepthImageFrame depthFrame)
        {
            byte[] pixels = this.GenerateColoredBytes(depthFrame);
            int stride = depthFrame.Width * 4;
            debugImage.Source = BitmapSource.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Bgr32, null, pixels, stride);
        }

        // Call this function inside AllFramesReady to display a color debugging feed
        void display_color_feed(ColorImageFrame colorFrame)
        {
            byte[] pixels = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(pixels);
            int stride = colorFrame.Width * 4;
            debugImage.Source = BitmapSource.Create(colorFrame.Width, colorFrame.Height, 96, 96, PixelFormats.Bgr32, null, pixels, stride);
        }

        // Generates a color image from the depth frame
        public byte[] GenerateColoredBytes(DepthImageFrame depthFrame)
        {
            short[] rawDepthData = new short[depthFrame.PixelDataLength];
            depthFrame.CopyPixelDataTo(rawDepthData);

            // Create the RGB pixel array
            Byte[] pixels = new byte[depthFrame.Height * depthFrame.Width * 4];

            const int BlueIndex = 0;
            const int GreenIndex = 1;
            const int RedIndex = 2;

            // Loop through data and set colors for each pixel
            for (int depthIndex = 0, colorIndex = 0;
                 depthIndex < rawDepthData.Length && colorIndex < pixels.Length;
                 depthIndex++, colorIndex += 4)
            {
                int player = rawDepthData[depthIndex] & DepthImageFrame.PlayerIndexBitmask;
                int depth = rawDepthData[depthIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                if (depth == -1 || depth == 0) continue;

                byte intensity = CalculateIntensityFromDepth(depth);
                pixels[colorIndex + BlueIndex] = intensity;
                pixels[colorIndex + GreenIndex] = intensity;
                pixels[colorIndex + RedIndex] = intensity;
            }

            return pixels;
        }

        private static byte CalculateIntensityFromDepth(int distance)
        {
            return (byte)(255 - (255 * Math.Max(distance - 800, 0) / 2000));
        }

        #endregion
    }
}
