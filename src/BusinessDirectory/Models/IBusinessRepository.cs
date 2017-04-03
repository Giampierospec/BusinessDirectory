using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessDirectory.Models
{
    public interface IBusinessRepository
    {
        IEnumerable<Business> GetAllBusinesses();
        IEnumerable<Category> GetAllCategories();
        Business GetBusinessByName(string companyName);
        IEnumerable<Business> GetBusinessesByUserName(string userName);
        Category GetCategoryByName(string category);
        void AddBusiness(string category, Business newBusiness, string userName);
        Task<bool> SaveChangesAsync();
        IEnumerable<Business>GetBusinessesByUsername(string userName);
    }
}