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

namespace DesktopApplication.PageToEdit
{
    /// <summary>
    /// Логика взаимодействия для RequersEdit.xaml
    /// </summary>
    /// private requests _currentRequest = new requests();
  
   
  
public partial class RequersEdit : Page
    {
        private requests _currentRequest = new requests();
        public RequersEdit(requests selectedRequest)
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
                _currentRequest.computerTechType = techTypeTextBox.Text;
                _currentRequest.computerTechModel = techmodelTextBox.Text;
                _currentRequest.problemDescryption = problemTextBox.Text;
                _currentRequest.masterID = int.Parse(numberTextBox.Text);
                _currentRequest.clientID = int.Parse(clientTextBox.Text);
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

