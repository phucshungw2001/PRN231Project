using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class QuantityChangeHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Action { get; set; } = null!;
        public int Change { get; set; }
        public DateTime Date { get; set; }
    }
}
