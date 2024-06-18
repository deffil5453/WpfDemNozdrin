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
    /// Логика взаимодействия для ReportForm.xaml
    /// </summary>
    public partial class ReportForm : Window
    {
        private int _requestId;
        private int _userId;
        public ReportForm(int requestId, int userId)
        {
            InitializeComponent();
            _requestId = requestId;
            _userId = userId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DemDbContext())
            {
                Report report = new Report();
                report.ReportDate = DateTime.Now;
                report.TimeSpent = timeSpentTextBox.Text;
                report.UsedMaterials = usedMaterialsTextBox.Text;
                report.Cost = Decimal.Parse(costTextBox.Text);
                report.FaultReason = faultReasonTextBox.Text;
                report.AssistanceProvided = assistanceProvidedTextBox.Text;
                report.RequestId = _requestId;
                db.Reports.Add(report);

                var request = db.Requests.Find(_requestId);
                if (request != null)
                {
                    request.Status = "выполнен";
                }

                db.SaveChanges();
                MessageBox.Show("Отчёт создан", "Успех");
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IndexRequestExecutor indexRequestExecutor = new IndexRequestExecutor(_userId);
            indexRequestExecutor.Show();
            this.Close();
        }
    }
}
