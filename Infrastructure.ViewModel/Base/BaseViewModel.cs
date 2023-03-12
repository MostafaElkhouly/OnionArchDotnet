using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.Base
{
    public class BaseViewModel
    {
        public Guid Id { set; get; }
        public string UserName { get; set; }
        public bool IsDeleted { set; get; }
        public DateTime DateOfCreate { set; get; }
    }
}
