using CoffeeShop.REPO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShop.REPO.BLL
{
    class CovfefeShop : ICoffeeRepository
    {
        private List<Coffee> CoffeeList { get; set; }
        private List<ImageEnum> Images { get; set; }
        public CovfefeShop()
        {
            LoadCoffees();
        }
        public void DeleteCoffee(Coffee coffee)
        {
            CoffeeList.Remove(CoffeeList.Find(s => s.CoffeeID == coffee.CoffeeID));
        }

        public void GetACoffee(Coffee coffee)
        {
            coffee.AmountInStock--;
         }

        public Coffee GetCofeeByID(int id)
        {
            return CoffeeList.Find(s => s.CoffeeID == id);
        }

        public List<Coffee> GetCoffees()
        {
            return CoffeeList;
        }

        public string GetImages(int id)
        {
            ImageEnum _ = Images.Find(s => s.ImageID == id);
            return _.ImageString;
        }

        private void LoadCoffees()
        {
            #region Hardcoded demo data
            //CoffeeList = new List<Coffee>
            //{
            //    new Coffee("ABC", "Den mest populære kaffe i Indonesien", Country.Indonesia, 45, 1),
            //    new Coffee("Java Jive", "En virkelig lækker lysristet bønne fra Costa Rica", Country.CostaRica, 60, 2),
            //    new Coffee("Spier Gold", "En dejlig mørk kaffe baseret på Arabica bønnen fra det mørke Afrika i Stellenbosch området", Country.Ethiopia, 45, 3),
            //    new Coffee("Ecuador!", "En kaffe der er brandet fra en tyske DJ Sash. Kaffen er opkaldt efter en af hans hitsingler tilbage i 90'erne", Country.Ecuador, 40, 4),
            //    new SuperiorCoffee("Kaffebønnerne i denne special roast er ristet med valmueolie. Dette frigiver nogle opiater til kaffebønnerne og giver derved en meget speciel smag i kaffen.", "Vietnamese Opium Roast", "En lækker 'specialristet' kaffe fra Vietnam", Country.Vietnam, 89, 5),
            //    new SuperiorCoffee("Dette er nok den mest berømte kaffetype af alle. Kaffebærrene bliver spist af Luwaken, et lille dyr fra Indonesien, hvorved at de perfekt modne frugter passerer igennem dyrets tarmsystem, for tilsidst at komme ud som kaffebønner af bagdøren. Disse bliver" +
            //    "så samlet op af lokale, hvor bønnerne bliver renset og ristet nænsomt. Processen igennem fordøjelsessystemet gør disse bønner til en helt unik smagsoplevelse.", "Kopi Luwak (Luwak kaffe)", "Den nok mest berømte kaffe i verden, en rigtig unik gourmet oplevelse", Country.Bali, 135, 6)
            //};
            //Images = new List<ImageEnum>
            //{
            //    new ImageEnum(1, "../../Images/KopiABC.png"),
            //    new ImageEnum(2, "../../Images/JavaJive.png"),
            //    new ImageEnum(3, "../../Images/NotFound.png"),
            //    new ImageEnum(4, "../../Images/Ecuador.png"),
            //    new ImageEnum(5, "../../Images/Vietnam.png"),
            //    new ImageEnum(6, "../../Images/KopiLuwak.png")
            //};
            #endregion

            CoffeeList = FileLogger.ReadFromShop();
            Images = FileLogger.ReadFromImage();
        }

        public void SaveCoffees()
        {
            FileLogger.SaveShop(CoffeeList);
            FileLogger.SaveImg(Images);
        }

        public void UpdateCoffee(Coffee coffee)
        {
            CoffeeList.Remove(CoffeeList.Find(s => s.CoffeeID == coffee.CoffeeID));
            CoffeeList.Add(coffee);
        }

        public void CreateCoffee(string cName, string desc, Country ct, int price, bool stock, int amount, bool superior, string eDesc)
        {
            if (superior) CoffeeList.Add(new SuperiorCoffee(eDesc, cName, desc, ct, price, 3, stock, amount));
            else CoffeeList.Add(new Coffee(cName, desc, ct, price, 3, stock, amount));
        }
    }
}
