using API.Models;

namespace API.DTO
{
    public class WarehouseDTO
    {
        public WarehouseDTO() { }
        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? Address { get; set; }
        public int TotalProducts { get; set; }
        public virtual ICollection<ProductDTO>? Products { get; set; }
    }
}
