using Entities.DBEntities.Base;

namespace Entities.DBEntities
{
    public class Company : BaseEntity<int>
    {
        public required string CompanyOwnerId { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Description { get; set; }
    }
}
