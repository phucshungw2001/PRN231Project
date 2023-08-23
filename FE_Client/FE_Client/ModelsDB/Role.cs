using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Role
    {
        public int? AccountId { get; set; }
        public string Roles { get; set; } = null!;
    }
}
