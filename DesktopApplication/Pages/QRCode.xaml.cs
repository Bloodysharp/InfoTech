using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
using ZXing;

namespace DesktopApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для QRCode.xaml
    /// </summary>
    public partial class QRCode : Page
    {
        public QRCode()
        {
            InitializeComponent();
        }
        private void GenerateQRCodeButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://docs.google.com/forms/d/e/1FAIpQLSdhZcExx6LSIXxk0ub55mSu-WIh23WYdGG9HY5EZhLDo7P8eA/viewform?usp=sf_link";
            GenerateQRCode(url);
        }

        private void GenerateQRCode(string url)
        {
            // Создаем экземпляр класса BarcodeWriter
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 0
                }
            };

            // Генерируем QR-код
            var bitmap = barcodeWriter.Write(url);
            imgQR.Source = ConvertBitmapToImageSource(bitmap);
        }

        private BitmapImage ConvertBitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
