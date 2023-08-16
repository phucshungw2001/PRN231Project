using API.Models;

namespace API.DTO
{
    public class StockReceiptDTO
    {
        public StockReceiptDTO() { }
        public int ReceiptId { get; set; }
        public DateTime? DateReceipt { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public virtual ICollection<ReceiptDetail>? ReceiptDetails { get; set; }
    }
}
