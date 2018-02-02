using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using DutchTreat.ViewModels;
using AutoMapper;
namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _respository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger, IMapper mapper)
        {
            _respository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModels>>(_respository.GetAllOrders()));
            }
            catch (Exception er)
            {
                _logger.LogError($" Failed to get all Orders {er}");
                return BadRequest();
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _respository.GetOrderById(id);
                if (order != null) return Ok(_mapper.Map<Order,OrderViewModels>(order));
                else return NotFound();
            }
            catch (Exception er)
            {
                _logger.LogError($" Failed to get Order {id}: {er}");
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModels, Order>(model);
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    _respository.AddEntity(newOrder);
                    if (_respository.SaveAll()) 
                    { 
                        return Created($"api/Orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModels>(newOrder)); 
                    }
                    else return BadRequest($"Failed to save new order");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception er)
            {
                _logger.LogError($" Failed to save a new order {er}");
                return BadRequest();
            }
        }
    }
}