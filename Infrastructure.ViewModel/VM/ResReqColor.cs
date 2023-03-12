using Domain.Entities.Base;
using Infrastructure.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ResReqColor : BaseViewModel
    {
        public string ColorName { get; set; }

        public string HEX { get; set; }
        public string RGB { get; set; }
    }
}
