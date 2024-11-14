using DesktopApplication.Classes;
using DesktopApplication.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            Manager.MainFrame = MainFrame;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 780;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }
        private void Switch_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
            Login auth = new Login();
            auth.ShowDialog();
        }
        private void ButtonReq_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new RequersPage());
        }
        private void ButtonUserReq_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UserReqPage());
        }
        private void ButtonMasters_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MastersPage());
        }
        private void ButtonClients_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ClientsPage());
        }
        private void ButtonQR_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new QRCode());
        }

    }
}
