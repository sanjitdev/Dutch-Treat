using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.Data;
using Microsoft.AspNetCore.Authorization;
namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IDutchRepository _repository;
        public AppController(IDutchRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(object model)
        {
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
        [Authorize]
        public IActionResult Shop()
        {
            // var result = _context.Products.OrderBy(p=>p.Category).ToList();
            var result = _repository.GetAllProducts();
            return View(result);
        }
    }
}