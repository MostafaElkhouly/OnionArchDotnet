using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ReqSparPartForJobCard
    {
        public Guid SparPartId { get; set; }
        public Guid JobCardId { get; set; }
        public int Quantity { get; set; }
    }
}
