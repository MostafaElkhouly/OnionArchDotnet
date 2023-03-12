using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Base
{
    public class EntityBase
    {

        public Guid Id { set; get; } = Guid.NewGuid();
        public string UserName { get; set; }
        public bool IsDeleted { set; get; }
        public DateTime DateOfCreate { set; get; } = DateTime.UtcNow;

    }
}
