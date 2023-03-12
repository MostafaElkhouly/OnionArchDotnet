using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class Car : EntityBase
    {
        public string CarNumber { get; set; }
        public string CarEngineNumber { get; set; }
        public int HoursePower { get; set; }
        public string RegistrationNumber { get; set; }
        public string YearOfVersion { get; set; }

        public Guid CarModelId { get; set; }
        [ForeignKey("CarModelId")]
        public CarModel CarModel { get; set; }
        public Guid ColorId { get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }

        public string AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

    }
}
