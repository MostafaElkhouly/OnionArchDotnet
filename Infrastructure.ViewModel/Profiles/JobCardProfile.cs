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
    public class JobCardProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ResReqJobCard, JobCard>();
        }

        public override void Response()
        {
            CreateMap<JobCard, ResReqJobCard>();
        }
    }
}
