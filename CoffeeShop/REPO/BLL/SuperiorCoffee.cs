using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShop.REPO.BLL
{
    class SuperiorCoffee : Coffee, ICoffee
    {
        public string ExtraDescription { get; set; }

        public SuperiorCoffee(string eDesc, string cName, string desc, Country ct, int price, int imgID, bool inStock, int amount) : base(cName, desc, ct, price, imgID, inStock, amount)
        {
            ExtraDescription = eDesc;
        }

        public SuperiorCoffee(string am, string cID, string cName, string desc, string fAdded, string iID, string inStock, string cntr, string price, string eDesc) : base(am, cID, cName, desc, fAdded, iID, inStock, cntr, price)
        {
            ExtraDescription = eDesc;
        }
    }
}
