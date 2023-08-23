using API.Models;

namespace API.DTO
{
    public class AccountInfo
    {
        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }
        public int? ManagerId { get; set; }
        public int? CustomerId { get; set; }

        public virtual CustomerDTO? Customer { get; set; }
        public virtual ManagerDTO? Manager { get; set; }
    }
}
