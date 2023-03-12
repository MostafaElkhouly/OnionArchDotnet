using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.VM
{
    public class ReqUpdateUser
    {
        public string Id { set; get; }
        public string Email { set; get; }
        public string RoleId { set; get; }
        public string Image { set; get; }
        public bool IsMale { set; get; }
        public DateTime BirthDate { set; get; }
        public string UserName { set; get; }
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public string PhoneNumber { set; get; }
    }
}
