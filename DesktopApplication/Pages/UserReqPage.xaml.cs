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
    /// Логика взаимодействия для UserReqPage.xaml
    /// </summary>
    public partial class UserReqPage : Page
    {
        public UserReqPage()
        {
            InitializeComponent();
            RequestDG.ItemsSource = repairingShopEntities.GetContext().userRequests.ToList();
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageToEdit.UserReq((sender as Button).DataContext as userRequests));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new PageToAdd.UserReqAdd(null));
        }


        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var selectedHotel = RequestDG.SelectedItems.Cast<userRequests>().ToList();

            if (MessageBox.Show($"Удалить заявку?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    repairingShopEntities.GetContext().userRequests.RemoveRange(selectedHotel);
                    repairingShopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Заявка удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    RequestDG.ItemsSource = repairingShopEntities.GetContext().userRequests.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new repairingShopEntities())
            {
                var searchText = textBoxFilter.Text; var requests = context.requests.Where(r =>
                    r.computerTechType.Contains(searchText) || r.computerTechModel.Contains(searchText) ||
                    r.problemDescryption.Contains(searchText) || r.requestStatus.Contains(searchText) ||
                    r.repairParts.Contains(searchText) || r.masterID.ToString().Contains(searchText) ||
                    r.clientID.ToString().Contains(searchText)).ToList();
                RequestDG.ItemsSource = requests;
            }
        }
    }
}
