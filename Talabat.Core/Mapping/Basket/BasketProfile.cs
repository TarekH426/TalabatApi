using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.Basket;
using Talabat.Core.Entities;

namespace Talabat.Core.Mapping.Basket
{
    public class BasketProfile:Profile
    {

        public BasketProfile()
        {
            CreateMap<CustomerBasketDto, CustomerBasket>();
        }
    }
}
