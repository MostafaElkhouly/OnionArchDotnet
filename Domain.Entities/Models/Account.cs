using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class Account : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool IsMale { set; get; }
        public bool IsDeleted { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<JobCard> JobCards { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
