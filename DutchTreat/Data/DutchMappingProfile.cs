using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
namespace DutchTreat.Data
{
    public class DutchMappingProfile: Profile
    {
        public DutchMappingProfile(){
            CreateMap<Order, OrderViewModels>().ForMember(o=>o.OrderId,ex=>ex.MapFrom(o=>o.Id)).ReverseMap();
            CreateMap<OrderItem, OrderItemViewModels>().ReverseMap();
        }
    }
}