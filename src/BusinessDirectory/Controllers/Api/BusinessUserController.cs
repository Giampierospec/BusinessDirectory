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
    public class BusinessUserController : Controller
    {
        private ILogger<BusinessUserController> _logger;
        private IBusinessRepository _repository;

        public BusinessUserController(ILogger<BusinessUserController> logger, IBusinessRepository repository )
        {
            _logger = logger;
            _repository = repository;
        } 
        [HttpGet("api/businessUser")]
        public IActionResult Get()
        {
            var result = _repository.GetAllBusinessUsers();
            return Ok(Mapper.Map<IEnumerable<RegisterViewModel>>(result));
        }
        [HttpGet("api/BusinessUserByName")]
        public IActionResult GetBusinessUserByName()
        {
            var result = _repository.GetBusinessUserByName(User.Identity.Name);
            if(result != null)
            {
                return Ok(Mapper.Map<RegisterViewModel>(result));
            }
            else
            {
                return BadRequest("No hay usuarios conectados");
            }
            
        }
    }
}
