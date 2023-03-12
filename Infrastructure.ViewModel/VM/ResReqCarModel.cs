using Domain.Entities.Base;
using Infrastructure.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ResReqCarModel : BaseViewModel
    {
        public string Model { get; set; }

        public Guid? ParentId { get; set; }
    }
}
