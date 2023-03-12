using Domain.Entities;
using Domain.Entities.Models;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ViewModel.Profiles
{
    public class SparPartProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ResReqSparPart, SparPart>();
        }

        public override void Response()
        {
            CreateMap<SparPart, ResReqSparPart>();
        }
    }
}
