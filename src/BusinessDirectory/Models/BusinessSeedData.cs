using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.Models
{
    public class BusinessSeedData
    {
        private BusinessDbContext _context;
        /// <summary>
        /// This constructor will be injected with the context
        /// </summary>
        /// <param name="context"></param>
        public BusinessSeedData(BusinessDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// This will fill the database in case it is empty
        /// </summary>
        public async Task EnsureData()
        {
            if (!_context.Categories.Any())
            {
                var Servicio = new Category()
                {
                    Name= "Servicio",
                    Businesses = new List<Business>()
                    {
                        new Business() {CompanyName="Claro",Address="Calle Josefa Brea, Santo Domingo 10301", Phone = "809-536-9999",Latitude = 18.496264, Longitude=-69.893908, Description="En esta tienda se especializan en servicios de telecomunicaciones", UserName="giampierospec"},
                        new Business() {CompanyName="Orange",Address="Av Sarasota, Santo Domingo", Phone="809-334-0003",Latitude = 18.484720, Longitude=-69.937293, Description="En esta tienda se especializan en servicios de telecomunicaciones", UserName="giampierospec"}

                    }

                };
                _context.Add(Servicio);
                _context.AddRange(Servicio.Businesses);

            }
            await _context.SaveChangesAsync();
        }
        
    }
}
