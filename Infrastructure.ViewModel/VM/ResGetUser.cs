using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ResGetUser
    {
        public string Id { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public bool IsMale { set; get; }
        public string UserName { set; get; }
        public bool EmailConfirmed { get; set; }
        public bool HasPassword { get; set; } = true;
    }
}
