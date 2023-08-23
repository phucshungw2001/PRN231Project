using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }
        public int? ManagerId { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Manager? Manager { get; set; }
    }
}
