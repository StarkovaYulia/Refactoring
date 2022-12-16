using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using EverySquareDescription;

namespace ClassMap
{
    public class Map
    {
        private PictureBox locationPicture;
        public Bitmap Location;
        public Map(PictureBox pictureBox)
        {
            locationPicture = pictureBox;
            locationPicture.Height = 1000;
            locationPicture.Width = 1000;
            Location = new Bitmap(locationPicture.Width, locationPicture.Height);  
        }
        public void GenerateSquares()
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    Location.SetPixel(j, i, Color.FromArgb(255, 255, 0));   
                }
            }
            SetMap();
        }
        public void SetMap()
        {
            locationPicture.Image = Location;
        }   
    }
} 
