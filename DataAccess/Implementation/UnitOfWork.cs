

using DataAccess.Context;
using DataAccess.Implementation.Repository;
using DataAccess.Interfaces;
using DataAccess.Interfaces.Repository;
using Identity;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ICompanyRepository _companyRepository;

        private readonly UserManager<AppUser> _userManager;
        public UnitOfWork(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public ICompanyRepository CompanyRepository => _companyRepository ?? new CompanyRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
