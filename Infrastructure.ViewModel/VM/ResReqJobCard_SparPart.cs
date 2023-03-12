
using Infrastructure.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ResReqJobCard_SparPart : BaseViewModel
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public Guid JobCardId { get; set; }
        public Guid SparPartId { get; set; }

    }
}
