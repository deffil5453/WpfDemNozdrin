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
    /// Логика взаимодействия для ReportViewForm.xaml
    /// </summary>
    public partial class ReportViewForm : Window
    {
        private int _requestId;
        public ReportViewForm(int requestId)
        {
            InitializeComponent();
            _requestId = requestId;
            LoadReport();
        }
        private void LoadReport()
        {
            using (var dbContext = new DemDbContext())
            {
                var report = dbContext.Reports.FirstOrDefault(r => r.RequestId == _requestId);
                if (report != null)
                {
                    timeSpentTextBlock.Text = report.TimeSpent;
                    usedMaterialsTextBlock.Text = report.UsedMaterials;
                    costTextBlock.Text = report.Cost.ToString();
                    faultReasonTextBlock.Text = report.FaultReason;
                    assistanceProvidedTextBlock.Text = report.AssistanceProvided;
                }
                else
                {
                    MessageBox.Show("К данной заявке не прикреплен отчет.");
                }

            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
