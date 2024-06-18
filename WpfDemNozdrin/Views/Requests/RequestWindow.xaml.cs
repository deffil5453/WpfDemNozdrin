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
using WpfDemNozdrin.Models;

namespace WpfDemNozdrin.Views.Requests
{
    /// <summary>
    /// Логика взаимодействия для RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        private readonly int _userId;
        public RequestWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void CreateRequestButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new DemDbContext())
            {
                var executors = dbContext.UserDems.Where(u => u.RoleDemId == 3).ToList();
                var managers = dbContext.UserDems.Where(u => u.RoleDemId == 2).ToList();
                Random random = new Random();
                int randomExecutorIndex = random.Next(executors.Count);
                int randomManagerIndex = random.Next(managers.Count);
                Request newRequest = new Request
                {
                    CreateRequest = DateTime.Now,
                    ClientId = _userId,
                    DescriptionProblem = descriptionProblemTextBox.Text,
                    Status = "В работе",
                    TypeProblem = typeProblemTextBox.Text,
                    Instruments = instrumentsTextBox.Text,
                    ExecutorId = executors[randomExecutorIndex].Id,
                    ManagerId = managers[randomManagerIndex].Id,
                };
                dbContext.Requests.Add(newRequest);
                dbContext.SaveChanges();
                MessageBox.Show("Заявка успешно создана!", "Успех");

            }
        }
    }
}
