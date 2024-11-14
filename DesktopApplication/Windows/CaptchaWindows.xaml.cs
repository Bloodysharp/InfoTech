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
using System.Windows.Shapes;

namespace DesktopApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для CaptchaWindows.xaml
    /// </summary>
    public partial class CaptchaWindows : Window
    {
        string captcha;
        public CaptchaWindows()
        {
            InitializeComponent();
            GenerateCaptcha();
        }
        private void GenerateCaptcha()
        {
            string captchaText = GenerateRandomString(6);
            captcha = captchaText; // Генерируем случайную строку для капчи
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.LightGray, null, new Rect(0, 0, 200, 50));

                // Определяем массив с разными цветами для букв
                Brush[] brushes = new Brush[] { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Orange, Brushes.Purple, Brushes.Yellow };

                Random random = new Random();
                double x = 10;

                // Рисуем каждую букву с помехами, разными цветами и высотой
                for (int i = 0; i < captchaText.Length; i++)
                {
                    FormattedText formattedText = new FormattedText(captchaText[i].ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 20 + random.Next(5), brushes[random.Next(6)]);
                    drawingContext.DrawText(formattedText, new Point(x, 10 + random.Next(20)));

                    // Добавляем помехи в виде рандомных точек
                    for (int j = 0; j < 10; j++)
                    {
                        drawingContext.DrawEllipse(Brushes.Gray, null, new Point(x + random.Next(40), random.Next(50)), 1, 1);
                    }

                    // Добавляем помехи в виде рандомных линий
                    for (int k = 0; k < 5; k++)
                    {
                        Point start = new Point(x + random.Next(40), random.Next(50));
                        Point end = new Point(x + random.Next(40), random.Next(50));
                        drawingContext.DrawLine(new Pen(brushes[random.Next(6)], 1), start, end);
                    }

                    x += formattedText.WidthIncludingTrailingWhitespace + 5 + random.Next(10);
                }
            }

            RenderTargetBitmap bmp = new RenderTargetBitmap(200, 50, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            Image captchaImage = new Image();
            captchaImage.Source = bmp;
            CaptchaContainer.Children.Clear();
            CaptchaContainer.Children.Add(captchaImage);
        }
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (CaptchaUserText.Text != captcha)
            {
                MessageBox.Show("Капча не совпадает, попробуйте еще раз...");
                return;
            }
            else if (string.IsNullOrWhiteSpace(CaptchaUserText.Text))
            {
                MessageBox.Show("Введите капчу!");
                return;
            }
            else if (CaptchaUserText.Text == captcha)
            {
                this.Close();
                Login auth = new Login();
                auth.Show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }
    }
}
