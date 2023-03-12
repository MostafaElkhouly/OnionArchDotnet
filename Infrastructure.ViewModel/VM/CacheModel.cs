using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ViewModel.VM
{
    public class CacheModel
    {
        public string UserId { set; get; }
        public string UserName { set; get; }
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public DateTime? BirthDate { set; get; }
        public string Token { set; get; }
        public string RefreshToken { set; get; }
        public string Role { set; get; }
        public Guid RoleId { set; get; }
        public bool PhoneNumberConfirmed { set; get; }
        public string PhoneNumber { set; get; }
        public bool EmailConfirmed { set; get; }
        public string Email { set; get; }
    }
}
