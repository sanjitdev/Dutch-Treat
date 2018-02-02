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
    [Route("api/Orders/{orderid}/items")]
    public class OrderItemController : Controller
    {
        private readonly IDutchRepository _respository;
        private readonly ILogger<OrderItemController> _logger;
        private readonly IMapper _mapper;
        public OrderItemController(IDutchRepository repository, ILogger<OrderItemController> logger, IMapper mapper)
        {
            _respository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get(int OrderId)
        {
            try
            {
                var order = _respository.GetOrderById(OrderId);
                if (order != null)
                {
                    return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModels>>(order.Items));
                }
                else return NotFound();
            }
            catch (Exception er)
            {
                _logger.LogError($"Faild to get order by Id {er}");
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetAction(int OrderId, int id)
        {
            try
            {
                var order = _respository.GetOrderById(OrderId);
                if (order != null)
                {
                    var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                    if (item != null)
                        return Ok(_mapper.Map<OrderItem, OrderItemViewModels>(item));
                    else
                        return NotFound();
                }
                else return NotFound();
            }
            catch (Exception er)
            {
                _logger.LogError($"Failed to get order {er}");
                return BadRequest();
            }

        }
    }
}