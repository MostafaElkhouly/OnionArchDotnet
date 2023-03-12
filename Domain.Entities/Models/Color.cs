using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class Color : EntityBase
    {
        public string ColorName { get; set; }
        public string HEX { get; set; }
        public string RGB { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
