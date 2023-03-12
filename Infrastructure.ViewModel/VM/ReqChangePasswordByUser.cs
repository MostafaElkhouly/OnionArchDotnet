using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ReqChangePasswordByUser
    {
        public string NewPassword { set; get; }
        public string ConfirmedPassword { set; get; }
    }
}
