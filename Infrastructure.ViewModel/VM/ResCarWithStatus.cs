using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ResCarWithStatus
    {
        public Guid Id { get; set; }
        public string CarNumber { get; set; }
        public string CarEngineNumber { get; set; }
        public string Color { get; set; }
        public int HoursePower { get; set; }
        public string RegistrationNumber { get; set; }
        public string CarModel { get; set; }
        public string YearOfVersion { get; set; }
        public string AccountId { get; set; }
        public List<ResReqTicket> Tickets { get; set; }
    }
}
