
using Infrastructure.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ResReqJobCard : BaseViewModel
    {
        public DateTime DateTimeIn { get; set; }
        public DateTime DateTimeOut { get; set; }
        public string Note { get; set; }
        public StatusCar Status { get; set; }

        public Guid TicketId { get; set; }

        public string MechanicId { get; set; }
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
