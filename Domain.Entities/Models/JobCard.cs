using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class JobCard : EntityBase
    {
        public DateTime DateTimeIn { get; set; }
        public DateTime DateTimeOut { get; set; }
        public string Note { get; set; }
        public StatusCar Status { get; set; }

        public Guid TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        public string? MechanicId { get; set; }
        [ForeignKey("MechanicId")]
        public Account Account { get; set; }
        public ICollection<JobCard_SparPart> JobCard_SparParts { get; set; }
    }

    public enum StatusCar
    {
        Pending,
        Check,
        Repairing,
        Done,
        Close,
    }
}
