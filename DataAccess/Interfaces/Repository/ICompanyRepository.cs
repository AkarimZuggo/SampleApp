using Entities.DBEntities;

namespace DataAccess.Interfaces.Repository
{
    public interface ICompanyRepository : IRepository<Company, int>
    {
        Task<string> GetCompanyId(string OwnerId);
        Task<Company> GetCompanyProfile(int id);
        Task<Company> GetCompanyProfileByUserId(string userId);
    }
}
