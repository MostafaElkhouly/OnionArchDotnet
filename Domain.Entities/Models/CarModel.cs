using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class CarModel : EntityBase
    {
        public string Model { get; set; }

        public Guid? CarModelId { get; set; }
        [ForeignKey("CarModelId")]
        public CarModel Parent { get; set; }
        public ICollection<CarModel> Children { get; set; }
        public ICollection<Car> Cars { get; set;}

    }
}
