using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Hangman
{
   abstract class Img
    {

        public Image Element { get; set; }
        private const double IMAGE_WIDTH = 500;
        private const double IMAGE_HIEGHT = 500;
        private const double IMAGE_X = 495;
        private const double IMAGE_Y = 250;

        public Queue<string> picsUri1 = new Queue<string>(
           new string[] {
                @"ms-appx:///Assets/Picture0.png",
                @"ms-appx:///Assets/Picture1.png",
                @"ms-appx:///Assets/Picture2.png",
                @"ms-appx:///Assets/Picture3.png",
                @"ms-appx:///Assets/Picture4.png",
                @"ms-appx:///Assets/Picture5.png",
                @"ms-appx:///Assets/Picture6.png",
                @"ms-appx:///Assets/Picture7.png",
                @"ms-appx:///Assets/Picture8.png",
                @"ms-appx:///Assets/Picture9.png"
           });
        
        private double X
        {/*
            get
            {
                return Canvas.GetLeft(Element);
            }*/
            set
            {
                Canvas.SetLeft(Element, value);
            }
        }

        private double Y
        {
            //get
            //{
            //    return Canvas.GetTop(Element);
            //}

            set
            {
                Canvas.SetTop(Element, value);
            }
        }

        private double Hieght
        {
            //get
            //{
            //    return Element.ActualHeight;
            //}
            set
            {
                Element.Height = value;
            }
        }

        private double Width
        {
            //get
            //{
            //    return Element.ActualWidth;
            //}
            set
            {
                Element.Width = value;
            }
        }

        public Img()
        {
            Element = new Image();
            Element.Stretch = Windows.UI.Xaml.Media.Stretch.Fill;
            this.Hieght = IMAGE_HIEGHT;
            this.Width = IMAGE_WIDTH;
            this.X = IMAGE_X;
            this.Y = IMAGE_Y;
            Element.Source = new BitmapImage(new Uri(picsUri1.Dequeue()));
        }

        public void UpdatetImage(string uri)
        {
            Element.Source = new BitmapImage(new Uri(uri));
        }

    }
}
