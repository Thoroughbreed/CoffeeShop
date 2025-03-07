﻿using CoffeeShop.REPO.BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoffeeShop
{
    public partial class MainWindow : Window
    {
        private bool _admin;
        private readonly CovfefeShop _shop;

        public MainWindow()
        {
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag); //Sets locale to da-DK (system specific)
            _shop = new CovfefeShop();
            InitializeComponent();
            CoffeeList.ItemsSource = _shop.GetCoffees();
            edtCntry.ItemsSource = Enum.GetValues(typeof(Country));
        }

        /// <summary>
        /// Sets image source from the ID of the object
        /// </summary>
        private void GetImages(int id)
        {
            CoffeeImage.Source = new BitmapImage(new Uri(_shop.GetImages(id), UriKind.Relative));
        }

        /// <summary>
        /// Changes image when selection changes
        /// </summary>
        private void CoffeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GetImages(Convert.ToInt32(CheatCode.Text));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Simulates login to Admin-mode. password not yet set - only as proof of concept
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (_admin)
            { 
                _admin = false;
                btnDelete.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Hidden;
                btnCreate.Visibility = Visibility.Hidden;
                btnDrink.Visibility = Visibility.Visible;
                UserPanel.Visibility = Visibility.Visible;
                UserPanel2.Visibility = Visibility.Visible;
                UserCName.Visibility = Visibility.Visible;
                AdminCName.Visibility = Visibility.Hidden;
                AdminPanel.Visibility = Visibility.Hidden;
                AdminPanel2.Visibility = Visibility.Hidden;
                btnLogin.Content = "Log ind";
            }
            else if (!_admin)
            {
                _admin = true;
                btnDelete.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                btnCreate.Visibility = Visibility.Visible;
                btnDrink.Visibility = Visibility.Hidden;
                UserPanel.Visibility = Visibility.Hidden;
                UserPanel2.Visibility = Visibility.Hidden;
                UserCName.Visibility = Visibility.Hidden;
                AdminCName.Visibility = Visibility.Visible;
                AdminPanel.Visibility = Visibility.Visible;
                AdminPanel2.Visibility = Visibility.Visible;
                btnLogin.Content = "Log ud";
            }
            if (CoffeeList.SelectedIndex == -1) BtnCreate_Click(sender, e);
        }

        /// <summary>
        /// Saves coffee when creating a new one
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _shop.CreateCoffee(AdminCName.Text, edtDesc.Text, (Country)edtCntry.SelectedItem, Convert.ToInt32(edtPrice.Text), (bool)edtInStock.IsChecked, Convert.ToInt32(edtAmount.Text), (bool)Superior.IsChecked, AdminExtraDesc.Text);
                CoffeeList.ItemsSource = null;
                CoffeeList.ItemsSource = _shop.GetCoffees();
                CoffeeList.SelectedIndex = 0;
                CoffeeList_SelectionChanged(sender, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Hov, du har indtastet forkert", "FEJL!", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            
        }

        /// <summary>
        /// Deletes coffee if user answers "yes" in prompt
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                Coffee _ = _shop.GetCofeeByID(Convert.ToInt32(edtCID.Text));
                var prompt = MessageBox.Show($"Er du nu helt sikker på at du vil slette {_.CoffeeName}? ", "Slet kaffe", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (prompt == MessageBoxResult.Yes)
                {
                    _shop.DeleteCoffee(_);
                    CoffeeList.ItemsSource = null;
                    CoffeeList.ItemsSource = _shop.GetCoffees();
                    CoffeeList.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// "Drinks" a coffee. If !InStock then user is told that they cannot do that Dave
        /// </summary>
        private void BtnDrink_Click(object sender, RoutedEventArgs e)
        {
            int i = CoffeeList.SelectedIndex;
            Coffee _ = _shop.GetCofeeByID(Convert.ToInt32(edtCID.Text));
            if (_.InStock)
            {
                MessageBox.Show("Velbekomme", "Hello World.", MessageBoxButton.OK, MessageBoxImage.Information);
                CoffeeList.ItemsSource = null;
                _shop.GetACoffee(_);
                CoffeeList.ItemsSource = _shop.GetCoffees();
                CoffeeList.SelectedIndex = i;
                CoffeeList_SelectionChanged(sender, null);
            }
            else if (!_.InStock) MessageBox.Show("Der er desværre ingen kaffe på lager.\nPrøv igen i morgen. Vi beklager", "I'm sorry Dave. I cannot let you do that", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Calls the "Save" method when exiting application
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _shop.SaveCoffees();
        }

        /// <summary>
        /// Creates a new cuppa, prepares fields. 
        /// Is called when logging in if no item is selected
        /// </summary>
        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            AdminCName.Text = "[Indtast kaffens navn]";
            edtDesc.Text = "[Indtast beskrivelse af kaffen her.]";
            edtPrice.Text = "[Pris]";
            edtCID.Text = "";
            AdminExtraDesc.Text = "[Ekstra beskrivelse]";
            CoffeeImage.Source = null;

            CoffeeList_SelectionChanged(sender, null); ;
        }
    }
}