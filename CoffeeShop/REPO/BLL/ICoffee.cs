using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CoffeeShop.REPO.BLL
{
    interface ICoffee
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
    }
}
