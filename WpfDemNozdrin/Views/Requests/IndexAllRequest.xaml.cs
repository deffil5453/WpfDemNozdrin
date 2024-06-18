using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для IndexAllRequest.xaml
    /// </summary>
    public partial class IndexAllRequest : Window
    {
        private readonly int _userId;
        public IndexAllRequest(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadRequests();
        }
        private void LoadRequests()
        {
            using (var dbContext = new DemDbContext())
            {
                var requests = dbContext.Requests.Where(r => r.ManagerId == _userId)
                    .Include(r => r.Client)
                    .Include(r => r.Manager)
                    .Include(r => r.Executor)
                    .ToList();
                requestsDataGrid.ItemsSource = requests;
            }
        }
        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var requestId = (int)button.CommandParameter;
            var editWindow = new EditRequestWindow(requestId, _userId);
            editWindow.Show();
            this.Close();
            LoadRequests();
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var requestId = (int)button.CommandParameter;
            var result = MessageBox.Show("Вы уверены, что хотите удалить заявку?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (var dbContext = new DemDbContext())
                {
                    var requestToDelete = new Request { Id = requestId };
                    dbContext.Entry(requestToDelete).State = EntityState.Deleted;
                    dbContext.SaveChanges();
                    LoadRequests();
                }
            }
        }
        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var requestId = (int)button.CommandParameter;

            // для примера предполагаем, что у вас есть окно просмотра отчета ReportViewWindow
            var reportViewWindow = new ReportViewForm(requestId);
            reportViewWindow.ShowDialog();
        }
        private void Label_Click(object sender, MouseButtonEventArgs e)
        {
            var window = new MainWindow();
            window.Show();

            this.Close();
        }
    }
}

