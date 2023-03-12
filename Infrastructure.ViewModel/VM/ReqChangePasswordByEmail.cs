using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ReqChangePasswordByEmail
    {
        public string Email { set; get; }
        public string OTP { set; get; }
        public string NewPassword { set; get; }
        public string ConfirmedPassword { set; get; }
    }
}
