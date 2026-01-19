namespace DTOs.Response
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Description { get; set; }
    }
}
