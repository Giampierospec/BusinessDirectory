using Microsoft.AspNetCore.Identity;
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
        private UserManager<BusinessUser> _userManager;

        /// <summary>
        /// This constructor will be injected with the context
        /// </summary>
        /// <param dbContext="context"></param>
        public BusinessSeedData(BusinessDbContext context, UserManager<BusinessUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// This will fill the database in case it is empty
        /// </summary>
        public async Task EnsureData()
        {
            //This will verify if that email already exists
            if (await _userManager.FindByEmailAsync("giampi_12@hotmail.com") == null)
            {
                var user = new BusinessUser()
                {
                    UserName = "giampi_12@hotmail.com",
                    Email = "giampi_12@hotmail.com",
                    Name = "Giampiero",
                    LastName = "Specogna",
                    PhoneNumber = "809-788-6865"
                };
               await _userManager.CreateAsync(user, "azulado@A12");

            }

            if (!_context.Categories.Any())
            {
                
                var servicio = new Category()
                {
                    //Services Category
                    Name= "Servicio",
                    Businesses = new List<Business>()
                    {
                        new Business() {CompanyName="Claro Dominicana",Address="Calle Josefa Brea, Santo Domingo 10301", Phone="809-536-9999", Description="Claro la empresa de telecomunicaciones de Carlos Slim", UserName="giampi_12@hotmail.com"},
                        new Business() {CompanyName="Orange Dominicana",Address="Av Sarasota, Santo Domingo", Phone="809-334-0003", Description="Orange la empresa de telecomunicaciones francesa en el pais", UserName="giampi_12@hotmail.com"},
                        new Business() {CompanyName="Viva Dominicana",Address="La Sirena de la Charles de Gaulle, Avenida Charles de Gaulle, Santo Domingo Este", Phone="849-631-2539", Description="Viva la empresa de telecomunicaciones", UserName="giampi_12@hotmail.com"}

                    }
                };
                _context.Add(servicio);
                _context.AddRange(servicio.Businesses);
                //Production Category
                var produccion = new Category() {
                    Name="Produccion",
                    Businesses = new List<Business>()
                    {
                        new Business() {CompanyName="MetalDom",Address="KM 6 ½, Av. Independencia, Santo Domingo 01469", Phone="809-533-5888", Description="Somos una metalera ubicada en santo domingo", UserName="giampi_12@hotmail.com"},
                        new Business() {CompanyName="Zona franca san isidro",Address="Santo Domingo Este", Phone="829-869-5184", Description="Somos zonas francas ubicadas en San Isidro", UserName="giampi_12@hotmail.com"},
                        new Business() {CompanyName="Empresas Nolvi",Address="Calle Rafael Augusto Sánchez 68, Santo Domingo", Phone="809-566-0338",Description="Esta es una empresa que provee servicios industriales", UserName="giampi_12@hotmail.com"}
                    }
                };
                _context.Add(produccion);
                _context.AddRange(produccion.Businesses);
                //Extraction Category
                var extraccion = new Category()
                {
                    Name = "Extraccion",
                    Businesses = new List<Business>()
                    {
                        new Business() {CompanyName="Barrick Gold",Address="Barrick Gold, 43000", Phone="809-533-5888", Description="Somos una empresa que extrae minerales basada en republica dominicana", UserName="giampi_12@hotmail.com"},
                        new Business() {CompanyName="dovemco",Address="Av. Lope de Vega 29, Santo Domingo", Phone="809-549-5815", Description="Extraemos bauxita", UserName="giampi_12@hotmail.com"},
                        new Business() {CompanyName="Falconbridge Dominicana",Address="Falconbridge Dominicana, 42000", Phone="809-525-3102", Description="Esta es una empresa que se dedica a extraer minerales y fue demandada muchas veces por la sociedad", UserName="giampi_12@hotmail.com"}
                    }
                };
                _context.Add(extraccion);
                _context.AddRange(extraccion.Businesses);
                //Sales category
                var ventas = new Category()
                {
                    Name = "Ventas",
                    Businesses = new List<Business>()
                    {
                         new Business() {CompanyName="Jumbo Carretera Mella",Address="Carretera Mella casi Esq. Charles de Gaulle, Carr. Mella, Santo Domingo Este", Phone="809-695-2268",Description="Vendemos productos para la familia dominicana", UserName="giampi_12@hotmail.com"},
                        new Business() {CompanyName="Tiendas Corripio",Address="Plaza Naco, Av. Tiradentes, Santo Domingo", Phone="809-563-4166", Description="Vendemos electrodomesticos y demas", UserName="giampi_12@hotmail.com"},
                        new Business() {CompanyName="Almacenes Unidos",Address="esquina A., Av Sarasota & Calle Pedro Antonio Bobea, Santo Domingo", Phone="809-472-6911", Description="Esta empresa vende varios productos para el hogar", UserName="giampi_12@hotmail.com"}
                    }
                };
                _context.Add(ventas);
                _context.AddRange(ventas.Businesses);
                await _context.SaveChangesAsync();

            }
            
        }
        
    }
}
