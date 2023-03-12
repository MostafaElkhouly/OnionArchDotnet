
using Infrastructure.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ResReqTicket : BaseViewModel
    {

        public Guid CarId { get; set; }

        public string Note { get; set; }
        public StatusCar Status { get; set; }

        public string AccountId { get; set; }

    }
}
