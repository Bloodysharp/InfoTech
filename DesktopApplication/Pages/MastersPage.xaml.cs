﻿using DesktopApplication.Classes;
using DesktopApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для MastersPage.xaml
    /// </summary>
    public partial class MastersPage : Page
    {
        public MastersPage()
        {
            InitializeComponent();
            RequestDG.ItemsSource = repairingShopEntities.GetContext().masters.ToList();
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageToEdit.MastersEdit((sender as Button).DataContext as masters));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new PageToAdd.MastersAdd(null));
        }


        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var selectedHotel = RequestDG.SelectedItems.Cast<masters>().ToList();

            if (MessageBox.Show($"Удалить заявку?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    repairingShopEntities.GetContext().masters.RemoveRange(selectedHotel);
                    repairingShopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Заявка удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    RequestDG.ItemsSource = repairingShopEntities.GetContext().masters.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}