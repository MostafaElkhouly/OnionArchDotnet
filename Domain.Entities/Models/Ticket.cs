using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class Ticket : EntityBase
    {

        public Guid CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
        public string Note { get; set; }
        public StatusCar Status { get; set; }

        public string AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public ICollection<JobCard> JobCards { get; set; }

    }
}
