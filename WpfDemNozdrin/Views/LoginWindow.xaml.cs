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
using WpfDemNozdrin.Data;
using WpfDemNozdrin.Views.Requests;

namespace WpfDemNozdrin.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string _userName = usernameTextBox.Text;
            string _password = passwordBox.Password;
            using (var dbContext = new DemDbContext())
            {
                var users = dbContext.UserDems.FirstOrDefault(u => u.UserName == _userName);
                if (users == null)
                {
                    MessageBox.Show("Пользователя не существует", "Ошибка  авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Авторизация прошла успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    RequestWindow requestWindow = new RequestWindow(users.Id);
                    IndexAllRequest indexAllRequest = new IndexAllRequest(users.Id);
                    IndexRequestExecutor indexRequest = new IndexRequestExecutor(users.Id);
                    if (users.RoleDemId == 1)
                    {
                        requestWindow.Show();
                    }
                    else if (users.RoleDemId == 2)
                    {
                        indexAllRequest.Show();
                    }
                    else 
                    {
                        indexRequest.Show();
                    }

                    this.Close();
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