using API.Models;

namespace API.DTO
{
    public class WarehouseDTO
    {
        public WarehouseDTO() { }
        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Product>? ProductsNavigation { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
