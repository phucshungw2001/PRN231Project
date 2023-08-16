using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
            Invoices = new HashSet<Invoice>();
        }

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
