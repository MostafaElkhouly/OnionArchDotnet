using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class SparPart : EntityBase
    {
        public string SparName { get; set; }
        public int Quantity { get; set; }
        public ICollection<JobCard_SparPart> JobCard_SparParts { get; set; }

    }
}
