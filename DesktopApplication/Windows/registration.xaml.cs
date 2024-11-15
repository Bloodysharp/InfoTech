using DesktopApplication.CustomNotify;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        private readonly Random _random = new Random();
        private readonly List<AnimatedTriangle> _triangles = new List<AnimatedTriangle>();


        private const int _triangleCount = 7; //Number of triangles
        public registration()
        {
            InitializeComponent();
            txtUsername.TextChanged += TextBox_TextChanged;
            txtLogin.TextChanged += TextBox_TextChanged;

            txtPassword.TextChanged += TextBox_TextChanged;

            txtRole.TextChanged += TextBox_TextChanged;

            Loaded += Main_Load;
        }
        private AnimatedTriangle CreateTriangle()
        {
            double size = _random.Next(3, 12); //Random triangle size between 3 and 12
            double xPos = _random.NextDouble() * (canvas.ActualWidth - size) + (size / 2);
            var triangle = new Polygon
            {
                Points = new PointCollection
                {
                    new Point(size / 2, 0),
                    new Point(size, Math.Sqrt(3) / 2 * size),
                    new Point(0, Math.Sqrt(3) / 2 * size)
                },
                Fill = Brushes.White,
                Effect = new DropShadowEffect //Triangle outer glow.
                {
                    Color = Colors.White,
                    BlurRadius = 13,
                    ShadowDepth = 0,
                    Direction = 0,
                    Opacity = 0.7
                }
            };

            var animatedTriangle = new AnimatedTriangle(triangle, size, _random, xPos);
            animatedTriangle.StartRotationAnimation(_random);
            return animatedTriangle;
        }

        private void InitializeTriangles()
        {
            for (int i = 0; i < _triangleCount; i++)
            {
                var triangle = CreateTriangle();
                _triangles.Add(triangle);
                canvas.Children.Add(triangle.Polygon);
            }
        }
        private void Main_Load(object sender, RoutedEventArgs e)
        {
            InitializeTriangles();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            Storyboard storyboard = (Storyboard)this.Resources["WindowWidthAnimation"];
            storyboard.Begin(this);

        }
        private void UpdateCaretPosition(TextBox textBox) //Smooth caret movement
        {
            var caretIndex = textBox.CaretIndex;
            var rect = textBox.GetRectFromCharacterIndex(caretIndex, true);

            var caretRectangle = FindCaretRectangle(textBox);

            if (caretRectangle != null)
            {
                var targetMargin = new Thickness(rect.X, 0, 0, 0);

                var marginAnimation = new ThicknessAnimation
                {
                    To = targetMargin,
                    Duration = TimeSpan.FromMilliseconds(100),
                    EasingFunction = new QuadraticEase()
                };

                caretRectangle.BeginAnimation(Rectangle.MarginProperty, marginAnimation);
            }
        }

        private Rectangle FindCaretRectangle(TextBox textBox)
        {
            var template = textBox.Template;
            var animatedCaretRectangle = template.FindName("animatedCaretRectangle", textBox) as Rectangle;
            return animatedCaretRectangle;
        }
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            foreach (var triangle in _triangles)
            {
                triangle.UpdatePosition(canvas.ActualWidth, canvas.ActualHeight, _random);
                Canvas.SetLeft(triangle.Polygon, triangle.X);
                Canvas.SetTop(triangle.Polygon, triangle.Y);
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                Application.Current.Shutdown();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCaretPosition((TextBox)sender);
        }
        private void regbtn_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                Notify success = new Notify(
           "Notification",
           "Please, give to us some information about you. Thanks!",
           "/Resources/bell_icon.png",
           (LinearGradientBrush)this.Resources["YellowGradient"],
           HextoSolidBrush("#E7BC06")
           );
                success.Show();
                txtUsername.Focus();
                return;
            }

            // Получаем введенные данные
            string userName = txtUsername.Text;
            string password = txtPassword.Text;
            string userLogin = txtLogin.Text;
            string userType = txtRole.Text;

            // Строка подключения к базе данных
            string connectionString = "Server=HOME-PC\\SQLEXPRESS;Initial Catalog=repairingShop;Integrated Security=True";

            // SQL-запрос для добавления данных
            string query = "INSERT INTO users (fio, userLogin, userPassword, userType) " +
                          "VALUES (@fio, @userLogin, @userPassword, @userType)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                // Параметры запроса
                command.Parameters.AddWithValue("@fio", userName);
                command.Parameters.AddWithValue("@userLogin", userLogin);
                command.Parameters.AddWithValue("@userPassword", password);
                command.Parameters.AddWithValue("@userType", userType);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    Notify success = new Notify(
                 "Success",
                 "You got the access.",
                 "/Resources/success_icon.png",
                 (LinearGradientBrush)this.Resources["GreenGradient"],
                 HextoSolidBrush("#36AE3B")
                 );
                    success.Show();
                    var login = new Login();
                    login.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    Notify error = new Notify(
             "Error!",
             "Please try again!",
             "/Resources/Error_icon.png",
             (LinearGradientBrush)this.Resources["RedGradient"],
             HextoSolidBrush("#F24A50")
             );
                    error.Show();
                }
            }
        }
        private SolidColorBrush HextoSolidBrush(string Hex)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Hex));
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            Notify success = new Notify(
          "Notification",
          "We will remember your choose.",
          "/Resources/bell_icon.png",
          (LinearGradientBrush)this.Resources["YellowGradient"],
          HextoSolidBrush("#E7BC06")
          );
            success.Show();
        }
    }
}
