using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.Models
{
    public class BusinessRepository : IBusinessRepository
    {
        private BusinessDbContext _context;
        private ILogger<BusinessRepository> _logger;

        public BusinessRepository(BusinessDbContext context, ILogger<BusinessRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Business> GetAllBusinesses()
        {
            _logger.LogInformation("Buscando todos los negocios");
            return _context.Businesses.ToList();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Business GetBusinessByName(string companyName)
        {
            return _context.Businesses
                .Where(b => b.CompanyName == companyName)
                .FirstOrDefault();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            _logger.LogInformation("Buscando todas las categorias...");
            return _context
                .Categories
                .Include(c => c.Businesses)
                .ToList();
        }

        public void AddBusiness(string category, Business newBusiness, string userName)
        {
            var cat= GetCategoryByName(category);
            if(cat != null)
            {
                //This will get the current username
                newBusiness.UserName = userName;
                cat.Businesses.Add(newBusiness);
                _context.Businesses.Add(newBusiness);
            }
        }

        public Category GetCategoryByName(string category)
        {
            return _context.Categories
                .Include(c => c.Businesses)
                .Where(c => c.Name == category)
                .FirstOrDefault();
        }

        public IEnumerable<Business> GetBusinessesByUsername(string userName)
        {
            return _context
                .Businesses
                .Where(b => b.UserName == userName).ToList();
        }
        public IEnumerable<BusinessUser> GetAllBusinessUsers()
        {
           return  _context.Users.ToList();
        }

        public BusinessUser GetBusinessUserByName(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
        }

        public void DeltBusiness(Business delBusiness)
        {
            var bsList = _context.Businesses.Where(b => b.Id == delBusiness.Id).FirstOrDefault();
            var cat = GetAllCategories();
            foreach (var ct in cat)
            {
                ct.Businesses.Remove(bsList);
            }
            _context.Businesses.Remove(bsList);
        }

        public Category GetCategoryByBusiness(Business bs)
        {
            var result = _context.Categories.Include(c => c.Businesses).Where(c => c.Businesses == bs).FirstOrDefault();
            return result;
        }

        public void UpdateBusiness(Business bs)
        {
            _context.Businesses.Update(bs);

        }

        public Business GetBusinessById(int id)
        {
            return _context.Businesses.Where(b => b.Id == id).FirstOrDefault();       
        }
    }
}
