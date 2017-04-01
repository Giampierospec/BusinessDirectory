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

        public IEnumerable<Category> GetCategoriesWithBusinesses(string category)
        {
          return  _context.Categories.
                Include(c => c.Businesses)
                .Where(c=> c.Name == category)
                .ToList();
        }

        public IEnumerable<Business> GetBusinessesByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
