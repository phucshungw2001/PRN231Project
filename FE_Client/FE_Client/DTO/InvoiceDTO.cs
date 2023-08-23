namespace API.DTO
{
    public class InvoiceDTO
    {
        public int InvoicesId { get; set; }
        public DateTime? InvoicesDate { get; set; }
        public int? CustomerId { get; set; }
        public bool? InvoicesStatus { get; set; }

        public List<InvoiceDetailDTO> InvoiceDetais { get; set; } = new List<InvoiceDetailDTO>();
    }
}
