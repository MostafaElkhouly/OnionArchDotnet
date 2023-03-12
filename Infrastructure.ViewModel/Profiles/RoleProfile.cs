using Domain.Entities;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ViewModel.Profiles
{
    public class RoleProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ResReqCreateGetRole, IdentityRole>();
        }

        public override void Response()
        {
            CreateMap<IdentityRole, ResReqCreateGetRole>();
        }
    }
}
