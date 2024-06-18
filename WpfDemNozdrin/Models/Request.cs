using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemNozdrin.Models
{
    public class Request
    {
        public int Id { get; set; } 
        public DateTime CreateRequest {  get; set; }
        public string Instruments { get; set; }
        public string TypeProblem { get; set; }
        public string DescriptionProblem{ get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
        public UserDem? Client { get; set; }
        public int ExecutorId { get; set; }
        public UserDem? Executor { get; set; }
        public int ManagerId { get; set; }
        public UserDem? Manager { get; set; }
    }
}
