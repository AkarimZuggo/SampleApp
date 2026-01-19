namespace DTOs.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Description { get; set; }
    }
}
