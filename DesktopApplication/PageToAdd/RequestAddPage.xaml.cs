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
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для RequestAddPage.xaml
    /// </summary>
    public partial class RequestAddPage : Page
    {
        private requests _currentRequest = new requests();
        public RequestAddPage(requests selectedRequest)
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
                var obj = new requests
                {
                    requestID = int.Parse(idTextBox.Text),
                    computerTechType = techTypeTextBox.Text,
                    computerTechModel = techmodelTextBox.Text,
                    problemDescryption = problemTextBox.Text,

                    masterID = int.Parse(numberTextBox.Text),
                    clientID = int.Parse(clientTextBox.Text)
                };
                repairingShopEntities.GetContext().requests.Add(obj);
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
