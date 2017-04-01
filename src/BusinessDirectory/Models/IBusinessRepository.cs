using System.Collections.Generic;

namespace BusinessDirectory.Models
{
    public interface IBusinessRepository
    {
        IEnumerable<Business> GetAllBusinesses();
        IEnumerable<Business> GetBusinessesByUserName(string userName);
        IEnumerable<Category> GetCategoriesWithBusinesses(string category);
        Task<bool> SaveChangesAsync();
    }
}