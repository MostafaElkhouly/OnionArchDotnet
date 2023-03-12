using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class JobCard_SparPart : EntityBase
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public Guid JobCardId { get; set; }
        [ForeignKey("JobCardId")]
        public JobCard JobCard { get; set; }

        public Guid SparPartId { get; set; }
        [ForeignKey("SparPartId")]
        public SparPart SparPart { get; set; }
    }
}
