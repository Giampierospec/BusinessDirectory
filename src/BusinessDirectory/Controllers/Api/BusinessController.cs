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
    public class BusinessController : Controller
    {
        private ILogger<BusinessController> _logger;
        private IBusinessRepository _repository;

        public BusinessController(ILogger<BusinessController> logger, IBusinessRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        [HttpGet("api/business")]
        public IActionResult Get()
        {
            try
            {
                var result = _repository.GetAllBusinesses();
                return Ok(Mapper.Map<IEnumerable<BusinessViewModel>>(result));
            }
            catch (Exception ex)
            {
                //TODO logging
                _logger.LogError($"No se pudieron encontrar los negocios: {ex}");
                return BadRequest("Ocurrio un error");
            }

        }
        [HttpGet("api/business/{companyName}")]
        public IActionResult Get(string companyName)
        {
            try
            {
                var result = _repository.GetBusinessByName(companyName);
                return Ok(Mapper.Map<BusinessViewModel>(result));
            }
            catch (Exception ex)
            {
                //TODO logging
                _logger.LogError($"No se pudieron encontrar los negocios: {ex}");
                return BadRequest("Ocurrio un error");
            }
        }

    }
}
