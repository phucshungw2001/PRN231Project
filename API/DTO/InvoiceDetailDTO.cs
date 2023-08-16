namespace API.DTO
{
    public class InvoiceDetailDTO
    {
        public int InvoiceDetailId { get; set; }
        public int? InvoiceId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
    }
}
