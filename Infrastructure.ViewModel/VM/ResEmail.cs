using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ViewModel.VM
{
    public  class ResEmail
    {
        public Guid Id { set; get; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Category { get; set; }
        public bool IsSent { get; set; }
    }
}
