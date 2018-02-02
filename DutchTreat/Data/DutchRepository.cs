using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;
        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called!");
                return _context.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to GetAllProducts {e}");
                return null;
            }
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                _logger.LogInformation("GetProductsByCategory was called!");
                return _context.Products.Where(p => p.Category == category).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to GetProductsByCategory {e}");
                return null;
            }
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Items).ThenInclude(i=>i.Product).ToList();
        }
        public Order GetOrderById(int id)
        {
            return _context.Orders.Include(o=>o.Items).ThenInclude(i=>i.Product).Where(o=>o.Id == id).FirstOrDefault();
        }
        public void AddEntity(object model){
            _context.Add(model);
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}