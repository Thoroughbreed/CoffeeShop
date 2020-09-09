using CoffeeShop.REPO.BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShop.REPO.DAL
{
    interface ICoffeeRepository
    {
        void GetACoffee(Coffee coffee);
        Coffee GetCofeeByID(int id);
        List<Coffee> GetCoffees();
        void DeleteCoffee(Coffee coffee);
        void UpdateCoffee(Coffee coffee);
    }
}
