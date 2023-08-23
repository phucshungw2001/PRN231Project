using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class ReceiptDetail
    {
        public int ReceiptDetailId { get; set; }
        public int? ReceiptId { get; set; }
        public int? ProductId { get; set; }
        public double? EntryPrice { get; set; }
        public double? TotalValue { get; set; }
        public int? EntryUnit { get; set; }

        public virtual Product? Receipt { get; set; }
        public virtual StockReceipt? ReceiptNavigation { get; set; }
    }
}
