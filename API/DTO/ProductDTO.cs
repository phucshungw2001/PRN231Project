using API.Models;

namespace API.DTO
{
    public class ProductDTO
    {
        public ProductDTO() { }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Describe { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public int? WarehouseId { get; set; }
        public int? CategoryId { get; set; }
        public bool? Status { get; set; }
        public int? SuppliersId { get; set; }
        //public List<QuantityChangeDTO> QuantityChanges { get; set; }

        /* public virtual Category? Category { get; set; }
         public virtual Supplier? Suppliers { get; set; }
         public virtual Warehouse? Warehouse { get; set; }
         public virtual ICollection<InvoiceDetail>? InvoiceDetails { get; set; }
         public virtual ICollection<ReceiptDetail>? ReceiptDetails { get; set; }

         public virtual ICollection<Warehouse>? Warehouses { get; set; }*/
    }
}
