using Domain.Entities.Models;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.Profiles
{
    public class CarModelProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ResReqCarModel, CarModel>()
                .ForMember(dst => dst.CarModelId, opt => opt.MapFrom(src => src.ParentId));
        }

        public override void Response()
        {
            CreateMap<CarModel, ResReqCarModel>()
                .ForMember(dst => dst.ParentId, opt => opt.MapFrom(src => src.CarModelId));
        }
    }
}
