using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace CoffeeShop.REPO.BLL
{
    class Coffee : ICoffee
    {
        public int CoffeeID { get; set; }
        public string CoffeeName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Country OriginCountry { get; set; }
        public bool InStock { get; set; }
        public int AmountInStock { get; set; }
        public DateTime FirstAddedToStock { get; set; }
        public int ImageID { get; set; }

        /// <summary>
        /// CTOR for new coffee types (application created)
        /// </summary>
        public Coffee(string cName, string desc, Country ct, int price, bool inStock, int amount)
        {
            Random CID = new Random();
            CoffeeID = CID.Next(1000, 9999);
            CoffeeName = cName;
            Price = price;
            Description = desc;
            OriginCountry = ct;
            InStock = inStock;
            AmountInStock = amount;
            FirstAddedToStock = DateTime.Now;
            ImageID = 3; // Image is hardcoded to a "404-Coffee not found".
        }

        /// <summary>
        /// CTOR for new coffee types (persistent data)
        /// </summary>
        public Coffee(string am, string cID, string cName, string desc, string fAdded, string iID, string inStock, string cntr, string price)
        {
            CoffeeID = Convert.ToInt32(cID);
            CoffeeName = cName;
            Price = Convert.ToDouble(price);
            Description = desc;
            OriginCountry = (Country)Enum.Parse(typeof(Country),cntr);
            InStock = Convert.ToBoolean(inStock);
            AmountInStock = Convert.ToInt32(am);
            FirstAddedToStock = DateTime.Parse(fAdded);
            ImageID = Convert.ToInt32(iID);
        }
    }
}
