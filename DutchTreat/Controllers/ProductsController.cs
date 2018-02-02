using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController: Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger){
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get(){
            try{
                return Ok(_repository.GetAllProducts());
            }catch(Exception er){
                _logger.LogError($"Faild to get all products {er}");
                return BadRequest();
            }
        }
    }
}