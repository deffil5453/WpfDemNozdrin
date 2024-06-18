using System.Windows;
using System.Windows.Input;
using WpfDemNozdrin.Data;
using WpfDemNozdrin.Models;

namespace WpfDemNozdrin.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            
            string _userName = usernameTextBox.Text;
            string _password = passwordBox.Password;
            using (var dbContext = new DemDbContext())
            {
                var users = dbContext.UserDems.FirstOrDefault(u => u.UserName == _userName);
                if (users == null)
                {
                    var newUser = new UserDem { UserName = _userName, Password = _password, RoleDemId =1 };
                    dbContext.UserDems.Add(newUser);
                    dbContext.SaveChanges();
                    MessageBox.Show("Регистрация прошла успешно!", "Регистрация");
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь существует", "Ошибка  регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Label_Click(object sender, MouseButtonEventArgs e)
        {
            var window = new MainWindow(); 
            window.Show();

            this.Close();
        }
    }
}
