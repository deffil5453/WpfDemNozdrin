using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemNozdrin.Models
{
    public class UserDem
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? TelePhoneNumber { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public int RoleDemId { get; set; }
        public RoleDem RoleDem { get; set; }
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
    }
}
