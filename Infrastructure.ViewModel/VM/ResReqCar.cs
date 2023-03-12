
using Infrastructure.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ResReqCar : BaseViewModel
    {
        [Required]
        public string CarNumber { get; set; }
        public string CarEngineNumber { get; set; }
        public Guid ColorId { get; set; }
        public int HoursePower { get; set; }
        public string RegistrationNumber { get; set; }
        public Guid CarModelId { get; set; }
        public string YearOfVersion { get; set; }
        public string AccountId { get; set; }


    }
}
