
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManage;
        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting, UserManager <StoreUser> userManager){
            _hosting = hosting;
            _ctx = ctx;
            _userManage = userManager;
        }
        public async Task Seed(){
            _ctx.Database.EnsureCreated();
             var user = await _userManage.FindByEmailAsync("vuta@gmail.com");
             if(user == null){
                 user = new StoreUser(){
                     FirstName = "Vu",
                     LastName = "tran",
                     UserName = "vuta@gmail.com",
                     Email = "vuta@gmail.com"
                 };
                 var result = await _userManage.CreateAsync(user,"Echo@1927");
                 if (result != IdentityResult.Success)
                 {
                     throw new InvalidOperationException("Failed to create default user!");
                 }
             }
            if (!_ctx.Products.Any()){
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);
                var order = new Order(){
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    User = user,
                    Items = new List<OrderItem>(){
                        new OrderItem(){
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };
                _ctx.Orders.Add(order);
                _ctx.SaveChanges();
            }
        }
    }
}