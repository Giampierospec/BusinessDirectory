using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessDirectory.Models
{
    public interface IBusinessRepository
    {
        IEnumerable<Business> GetAllBusinesses();
        IEnumerable<Category> GetAllCategories();
        IEnumerable<BusinessUser> GetAllBusinessUsers();
        BusinessUser GetBusinessUserByName(string userName);
        Business GetBusinessByName(string companyName);
        Category GetCategoryByName(string category);
        void AddBusiness(string category, Business newBusiness, string userName);
        void DeltBusiness(Business delBusiness);
        void UpdateBusiness(Business bs);
        Task<bool> SaveChangesAsync();
        IEnumerable<Business>GetBusinessesByUsername(string userName);
        Business GetBusinessById(int id);
    }
}