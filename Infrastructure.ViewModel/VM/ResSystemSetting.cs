using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ViewModel.VM
{
    public class ResSystemSetting
    {
        public Guid Id { set; get; }
        public string Category { get; set; }
        public string PropertyKey { get; set; }
        public string PropertyValue { get; set; }
        public string Notes { get; set; }
        public string PropertyType { get; set; }
    }
}
