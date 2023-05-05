using API.DTOs;
using API.Entities;
using API.Entities.Orders;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<UserCartDto, UserCart>();
            CreateMap<CartItemDto, CartItem>();
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<Unit, UnitDto>().ReverseMap();
            CreateMap<OrderToReturnDto, Order>(MemberList.None).ReverseMap();     
                
            CreateMap<CustomerDto, OrderToReturnDto>(MemberList.None);
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(c => c.Unit, option => option.Ignore())
                .ReverseMap();
            CreateMap<OrderDetailDto, UnitDto>(MemberList.None);

            CreateMap<SalesHeader, InvoiceDto>(MemberList.None).ReverseMap();

          


        }
    }
}
