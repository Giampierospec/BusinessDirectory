using AutoMapper;
using BusinessDirectory.Models;
using BusinessDirectory.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.Controllers.Api
{
    public class CategoryController: Controller
    {
        private ILogger<CategoryController> _logger;
        private IBusinessRepository _repository;

        public CategoryController(IBusinessRepository repository, ILogger<CategoryController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("api/category")]
        public IActionResult Get()
        {
            var result = _repository.GetAllCategories();
            return Ok(Mapper.Map<IEnumerable<CategoryViewModel>>(result));
        }
        
    }
}
