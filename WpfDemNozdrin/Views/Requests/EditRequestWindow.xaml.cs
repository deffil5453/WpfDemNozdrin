using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic.ApplicationServices;
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
    /// Логика взаимодействия для EditRequestWindow.xaml
    /// </summary>
    public partial class EditRequestWindow : Window
    {
        private int _requestId;
        private readonly int _userId;
        private Request _requestToEdit;
        private readonly DemDbContext dbContext = new DemDbContext();
        public EditRequestWindow(int id, int userId)
        {
            InitializeComponent();

            _requestId = id;
            _userId = userId;
            LoadRequestData();
        }
        private void LoadRequestData()
        {
            var executors = dbContext.UserDems.Where(u => u.RoleDemId == 3).ToList();
            executorComboBox.ItemsSource = executors;
            _requestToEdit = dbContext.Requests.Include(r => r.Executor).FirstOrDefault(r => r.Id == _requestId);
            if (_requestToEdit == null)
            {
                MessageBox.Show("Заявка не найдена");
                this.Close();
            }
            else
            {
                idTextBox.Text = _requestToEdit.Id.ToString();
                createRequestDatePicker.SelectedDate = _requestToEdit.CreateRequest;
                instrumentsTextBox.Text = _requestToEdit.Instruments;
                typeProblemTextBox.Text = _requestToEdit.TypeProblem;
                descriptionProblemTextBox.Text = _requestToEdit.DescriptionProblem;
                statusComboBox.SelectedItem = _requestToEdit.Status;
                executorComboBox.SelectedItem = _requestToEdit.Executor;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedExecutor = executorComboBox.SelectedItem as UserDem;
            _requestToEdit.CreateRequest = createRequestDatePicker.SelectedDate ?? DateTime.Now;
            _requestToEdit.Instruments = instrumentsTextBox.Text;
            _requestToEdit.TypeProblem = typeProblemTextBox.Text;
            _requestToEdit.DescriptionProblem = descriptionProblemTextBox.Text;
            _requestToEdit.Status = statusComboBox.Text;
            _requestToEdit.ExecutorId = selectedExecutor.Id;
            dbContext.Requests.Update(_requestToEdit);
            dbContext.SaveChanges();
            MessageBox.Show("Данные о заявке обновлены успешно!");
            IndexAllRequest indexAllRequest = new IndexAllRequest(_userId);
            indexAllRequest.Show();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IndexAllRequest indexAllRequest = new IndexAllRequest(_userId);
            indexAllRequest.Show();
            this.Close();
        }
    }
}
