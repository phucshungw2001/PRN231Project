using API.Models;

namespace API.DTO
{
    public class SupplierDTO
    {
        public SupplierDTO() { }
        public int SuppliersId { get; set; }
        public string? SuppliersName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
