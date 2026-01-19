using DTOs.Models;

namespace Services.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyModel> GetById(int id);
        Task<CompanyModel> GetCompanyProfile(int id);
        Task<List<CompanyModel>> Get();
        Task<CompanyModel> AddOrUpdate(CompanyModel model, string userId);
        Task<bool> Delete(int id);
        Task<string> GetCompanyId(string ownerId);
    }
}
