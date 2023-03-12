using Domain.Entities.Models;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.ViewModel.Profiles
{
    public class UserProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ReqCreateUser, Account>();

            CreateMap<ReqAddUser, Account>();

            CreateMap<ReqUpdateUser, Account>();
        }

        public override void Response()
        {
            CreateMap<Account, ResGetUser>();
        }
    }
}
