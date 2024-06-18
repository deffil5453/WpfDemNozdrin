using Microsoft.EntityFrameworkCore;
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

namespace WpfDemNozdrin.Views.Requests
{
    /// <summary>
    /// Логика взаимодействия для IndexRequestExecutor.xaml
    /// </summary>
    public partial class IndexRequestExecutor : Window
    {
        private int _userId;
        public IndexRequestExecutor(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadRequests();
        }
        private void LoadRequests()
        {
            using (var dbContext = new DemDbContext())
            {
                requestsDataGrid.ItemsSource = dbContext.Requests.Where(r => r.ExecutorId == _userId && r.Status== "В работе")
                    .Include(r => r.Client)
                    .Include(r => r.Manager)
                    .Include(r => r.Executor)
                    .ToList();
            }
        }
        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var requestId = (int)button.CommandParameter;
            var reportForm = new ReportForm(requestId, _userId);
            reportForm.Show();
            this.Close();
            LoadRequests();
        }
        private void Label_Click(object sender, MouseButtonEventArgs e)
        {
            var window = new MainWindow(); 
            window.Show();
    
            this.Close();
        }
    }
}
