using AutoMapper;
using BusinessDirectory.Models;
using BusinessDirectory.Services;
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
        private GeoCoordService _coordService;
        private ILogger<BusinessController> _logger;
        private IBusinessRepository _repository;

        public BusinessController(ILogger<BusinessController> logger, IBusinessRepository repository, GeoCoordService coordService)
        {
            _logger = logger;
            _repository = repository;
            _coordService = coordService;
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
        [HttpGet("api/category/{categoryName}/business")]
        public IActionResult GetBusinessByCategory(string categoryName)
        {
            var result = _repository.GetCategoryByName(categoryName);
            return Ok(Mapper.Map<IEnumerable<BusinessViewModel>>(result.Businesses).ToList());
        }
        [HttpGet("api/business/userName")]
        public IActionResult GetBusinessByUserName()
        {
            var result = _repository.GetBusinessesByUsername(User.Identity.Name);
            return Ok(Mapper.Map<IEnumerable<BusinessViewModel>>(result));
        }
        /// <summary>
        /// This will Insert all my businesses and align them into a category
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost("api/category/{categoryName}/business")]
        public async Task<IActionResult> Post(string categoryName, [FromBody]BusinessViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newBusiness = Mapper.Map<Business>(vm);
                    var coordResult = await _coordService.GetCoordByAddress(newBusiness.Address);
                    if (!coordResult.Success)
                    {
                        _logger.LogError(coordResult.Message);
                    }
                    else
                    {
                        newBusiness.Latitude = coordResult.Latitude;
                        newBusiness.Longitude = coordResult.Longitude;
                        var getBusiness = _repository.GetBusinessByName(newBusiness.CompanyName);
                        if (getBusiness == null)
                        {
                            _repository.AddBusiness(categoryName, newBusiness, User.Identity.Name);
                            //Save to the database
                            if (await _repository.SaveChangesAsync())
                            {
                                return Created($"/api/category/{categoryName}/business/{newBusiness.CompanyName}",
                                    Mapper.Map<BusinessViewModel>(newBusiness));
                            }

                        }
                        else
                        {
                            //You have tried to put something that already exists
                            _logger.LogError($"El nombre {newBusiness.CompanyName} ya existe, intente denuevo");
                            return BadRequest("El nombre que usted introdujo es invalido");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"fallo en guardar nuevo negocio {ex}");
            }
            return BadRequest("Fallo en guardar negocio negocio ya existe");
        }
        [HttpPost("api/business/delete")]
        public async Task<IActionResult> DeleteBusiness([FromBody]BusinessViewModel vm)
        {
           
                try
                {
                    var delBusiness = _repository.GetBusinessByName(vm.CompanyName);
                    _repository.DeltBusiness(delBusiness);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(Mapper.Map<BusinessViewModel>(delBusiness));
                    }
                    

                }
                catch(Exception ex)
                {
                    _logger.LogError($"Ocurrio un error al eliminar {ex}");
                }
                
            return BadRequest("El negocio no se pudo eliminar");
        }
        [HttpPost("api/business/update")]
        public async Task<IActionResult> Update([FromBody] BusinessViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updBusiness= Mapper.Map<Business>(vm);
                    var coordResult = await _coordService.GetCoordByAddress(updBusiness.Address);
                    if (!coordResult.Success)
                    {
                        _logger.LogError(coordResult.Message);
                    }
                    else
                    {
                        updBusiness.Latitude = coordResult.Latitude;
                        updBusiness.Longitude = coordResult.Longitude;
                        _repository.UpdateBusiness(updBusiness);
                        //Save to the database
                        if (await _repository.SaveChangesAsync())
                        {
                            return Ok(Mapper.Map<BusinessViewModel>(updBusiness));
                        }

                    }

                    }
                catch(Exception ex)
                {
                    _logger.LogError($"Ocurrio un error al actualizar los datos {ex}");
                }

            }
            return BadRequest("No se pudo actualizar los datos del negocio");
        }


    }
}
