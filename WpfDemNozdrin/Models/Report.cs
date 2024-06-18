using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemNozdrin.Models
{
    public class Report
    {
        public int Id { get; set; } 
        public DateTime ReportDate { get; set; } 
        public string TimeSpent { get; set; } 
        public string UsedMaterials { get; set; }
        public decimal Cost { get; set; } 
        public string FaultReason { get; set; } 
        public string AssistanceProvided { get; set; } 
        public int RequestId { get; set; } 
        public virtual Request Request { get; set; }
    }
}
