using CoffeeShop.REPO.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Text;
using System.Windows.Controls;

namespace CoffeeShop.REPO.DAL
{
    class FileLogger
    {
        private static readonly string _logName = "CoffeeLog.log";
        private static readonly string _coffeeShop = "CoffeeData.dat";
        private static readonly string _imageFile = "ImageData.imgdt";

        public static void WriteToLog(string logmessage)
        {
            string writeMsg = $"{DateTime.Now} {logmessage} \n";
            File.AppendAllTextAsync(_logName, writeMsg);
        }

        public static void SaveShop(List<Coffee> arg)
        {
            string writeMsg = "";
            foreach (var item in arg)
            {
                if (item is SuperiorCoffee)
                {
                    writeMsg += item.AmountInStock + ";" + item.CoffeeID + ";" + item.CoffeeName + ";" + item.Description + ";" + item.FirstAddedToStock + ";" +
                        item.ImageID + ";" + item.InStock + "; " + item.OriginCountry + ";" + item.Price + ";" + ((SuperiorCoffee)item).ExtraDescription + "\n";
                }
                else
                {
                    writeMsg += item.AmountInStock + ";" + item.CoffeeID + ";" + item.CoffeeName + ";" + item.Description + ";" + item.FirstAddedToStock + ";" + 
                        item.ImageID + ";" + item.InStock + "; " + item.OriginCountry + ";" + item.Price + "\n";
                }
            }

            File.WriteAllTextAsync(_coffeeShop, writeMsg);
        }

        public static void SaveImg(List<ImageEnum> arg)
        {
            string writeMsg = "";
            foreach (var item in arg)
            {
                writeMsg += item.ImageID + ";" + item.ImageString + "\n";
            }
            File.WriteAllTextAsync(_imageFile, writeMsg);
        }

        public static string ReadFromLog()
        {
            DirectoryInfo findLog = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (FileInfo filer in findLog.GetFiles())
            {
                if (filer.Extension == ".log")
                {
                    return File.ReadAllText(filer.FullName);
                }
            }
            return "No log found??!!!";
        }

        public static List<Coffee> ReadFromShop()
        {
            string[] shopInfo;
            List<Coffee> _ = new List<Coffee>();

            DirectoryInfo findShop = new DirectoryInfo(Environment.CurrentDirectory);
            shopInfo = null;
            foreach (FileInfo park in findShop.GetFiles())
            {
                if (park.Extension == ".dat") shopInfo = File.ReadAllLines(park.FullName);
            }

            foreach (string item in shopInfo)
            {
                string[] split = item.Split(';');
                if (split.Length == 9)
                {
                    _.Add(new Coffee(split[0], split[1], split[2], split[3], split[4], split[5], split[6], split[7], split[8]));
                }
                if (split.Length == 10)
                {
                    _.Add(new SuperiorCoffee(split[0], split[1], split[2], split[3], split[4], split[5], split[6], split[7], split[8], split[9]));
                }
            }
            return _;
        }

        public static List<ImageEnum> ReadFromImage()
        {
            string[] imgInfo;
            List<ImageEnum> _ = new List<ImageEnum>();

            DirectoryInfo findImage = new DirectoryInfo(Environment.CurrentDirectory);
            imgInfo = null;
            foreach (FileInfo img in findImage.GetFiles())
            {
                if (img.Extension == ".imgdt") imgInfo = File.ReadAllLines(img.FullName);
            }

            foreach (string item in imgInfo)
            {
                string[] split = item.Split(';');
                _.Add(new ImageEnum(Convert.ToInt16(split[0]), split[1]));
            }
            return _;
        }
    }
}
