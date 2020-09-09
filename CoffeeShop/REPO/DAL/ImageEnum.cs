using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShop.REPO.DAL
{
    class ImageEnum
    {
        public int ImageID { get; set; }
        public string ImageString { get; set; }

        public ImageEnum(int ID, string IS)
        {
            ImageID = ID;
            ImageString = IS;
        }
    }
}
