using BusinessDirectory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.Controllers.App
{
    public class AppController : Controller
    {
        private IConfigurationRoot _config;
        private BusinessDbContext _context;

        public AppController(IConfigurationRoot config, BusinessDbContext context)
        {
            _config = config;
            _context = context;
        }
       public IActionResult Index()
        {
            var data = _context.Businesses.ToList();
            return View(data);
        }
        [Authorize]
        public IActionResult PublishBusiness()
        {
            return View();
        }
        public IActionResult Categories()
        {
            return View();
        }
        [Authorize]
        public IActionResult MyBusinesses()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
