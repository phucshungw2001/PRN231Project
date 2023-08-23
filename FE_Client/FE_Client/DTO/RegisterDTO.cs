namespace API.DTO
{
    public class RegisterDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

    }
}
