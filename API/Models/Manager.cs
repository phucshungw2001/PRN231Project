using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Manager
    {
        public Manager()
        {
            Accounts = new HashSet<Account>();
        }

        public int ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
