using DesktopApplication.Classes;
using DesktopApplication.CustomNotify;
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

namespace DesktopApplication.PageToAdd
{
    /// <summary>
    /// Логика взаимодействия для UserReqAdd.xaml
    /// </summary>
    public partial class UserReqAdd : Page
    {
        private userRequests _currentRequest = new userRequests();
        public UserReqAdd(userRequests selectedRequest)
        {
            InitializeComponent();
            if (selectedRequest != null) _currentRequest = selectedRequest;
            DataContext = _currentRequest;

        }
        private SolidColorBrush HextoSolidBrush(string Hex)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Hex));
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = new userRequests
                {
                    userID = int.Parse(techTypeTextBox.Text),
                   fio = techmodelTextBox.Text,
                   problemDescryption = problemTextBox.Text,
                  phone = clientTextBox.Text,

                   computerTechType = technikTextBox.Text,
                   computerTechModel = NaimTextBox.Text,
                   
                };
                repairingShopEntities.GetContext().userRequests.Add(obj);
                repairingShopEntities.GetContext().SaveChanges();
                Manager.MainFrame.GoBack();
                Notify success = new Notify(
                "Success",
                "All changes saved!",
                "/Resources/success_icon.png",
                (LinearGradientBrush)this.Resources["GreenGradient"],
                HextoSolidBrush("#36AE3B")
                );
                success.Show();
            }
            catch (Exception ex)
            {
                Notify error = new Notify(
                    "Error!",
                    "Try again!",
                    "/Resources/Error_icon.png",
                     (LinearGradientBrush)this.Resources["RedGradient"],
                    HextoSolidBrush("#F24A50")
                    );
                error.Show();
            }
        }
    }
}
