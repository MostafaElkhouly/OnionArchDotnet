
using Domain.Entities.Models;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ViewModel.Profiles
{
    public class CarProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ResReqCar, Car>();
        }

        public override void Response()
        {
            CreateMap<Car, ResReqCar>();
            CreateMap<Car, ResCarWithStatus>()
                .ForMember(dst => dst.Tickets, opt => opt.MapFrom(src => src.Tickets));
        }
    }
}
