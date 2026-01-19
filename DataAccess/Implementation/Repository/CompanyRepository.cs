using DataAccess.Context;
using DataAccess.Interfaces.Repository;
using Entities.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation.Repository
{
    public class CompanyRepository : Repository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }
        private AppDbContext AppDbContext => Context as AppDbContext;
        public async Task<string> GetCompanyId(string OwnerId)
        {
            return (await AppDbContext.Company.Where(o => o.CompanyOwnerId == OwnerId && o.IsActive == true).Select(x => x.Id).FirstOrDefaultAsync()).ToString();
        }
        public async Task<Company> GetCompanyProfile(int id)
        {
            var result = await AppDbContext.Company.FirstOrDefaultAsync(o => o.Id == id && o.IsActive == true);
            if (result == null)
            {
                return null;
            }
            return result;
        }
        public async Task<Company> GetCompanyProfileByUserId(string userId)
        {
            var result = await AppDbContext.Company.FirstOrDefaultAsync(o => o.CompanyOwnerId == userId && o.IsActive == true);
            if (result == null)
            {
                return null;
            }
            return result;
        }

    }
}
