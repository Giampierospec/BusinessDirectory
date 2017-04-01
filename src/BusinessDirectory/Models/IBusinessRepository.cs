using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessDirectory.Models
{
    public interface IBusinessRepository
    {
        IEnumerable<Business> GetAllBusinesses();
        Business GetBusinessByName(string companyName);
        IEnumerable<Business> GetBusinessesByUserName(string userName);
        IEnumerable<Category> GetCategoriesWithBusinesses(string category);
        Task<bool> SaveChangesAsync();
    }
}