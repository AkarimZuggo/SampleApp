using DataAccess.Context;
using Entities.DBEntities;
using Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AppSeedDataInitializer
    {
        public static void SeedData(AppDbContext appDbManager)
        {
            SeedCompanies(appDbManager);
        }
        private static void SeedCompanies(AppDbContext appDbManager)
        {
            // Check if companies already exist
            if (!appDbManager.Company.Any())
            {
                appDbManager.AddRange(
                    new Company { Id = 1, CompanyOwnerId = "1", Name = "NewCompany", Description = "ITC", PhoneNumber = "123", CreatedBy = "SuperAdmin", CreatedOn = DateTime.UtcNow },
                    new Company { Id = 2, CompanyOwnerId = "2", Name = "NewCompany2", Description = "ITC", PhoneNumber = "123", CreatedBy = "SuperAdmin", CreatedOn = DateTime.UtcNow });

                appDbManager.SaveChanges();
            }
        }
    }
}
