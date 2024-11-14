using DesktopApplication.Classes;
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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {

        public ClientsPage()
        {
            InitializeComponent();
            RequestDG.ItemsSource = repairingShopEntities.GetContext().clients.ToList();
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageToEdit.ClientEdit((sender as Button).DataContext as clients));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new PageToAdd.ClientAdd(null));
        }


        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var selectedHotel = RequestDG.SelectedItems.Cast<clients>().ToList();

            if (MessageBox.Show($"Удалить заявку?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    repairingShopEntities.GetContext().clients.RemoveRange(selectedHotel);
                    repairingShopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Заявка удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    RequestDG.ItemsSource = repairingShopEntities.GetContext().clients.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}