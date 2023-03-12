using AutoMapper;
using Domain.Entities.Models;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel.Profiles
{
    public class ColorProfile : ProfileBase
    {
        public override void Request()
        {
            CreateMap<ResReqColor, Color>().ConvertUsing(new ColorConverter());
            CreateMap<ResReqColor, Color>();
        }

        public override void Response()
        {
            CreateMap<Color, ResReqColor>().ConvertUsing(new ResReqColorConverter());
            CreateMap<Color, ResReqColor>();
        }
    }


    public class ResReqColorConverter : ITypeConverter<Color, ResReqColor>
    {
        public ResReqColor Convert(Color source, ResReqColor destination, ResolutionContext context)
        {
            return new ResReqColor
            {
                IsDeleted= source.IsDeleted,
                ColorName= source.ColorName,
                DateOfCreate= source.DateOfCreate,
                HEX= source.HEX,
                Id= source.Id,
                RGB = source.RGB,
                UserName = source.UserName
            };
        }
    }

    public class ColorConverter : ITypeConverter<ResReqColor, Color>
    {
        public Color Convert(ResReqColor source, Color destination, ResolutionContext context)
        {
            return new Color
            {
                IsDeleted = source.IsDeleted,
                ColorName = source.ColorName,
                DateOfCreate = source.DateOfCreate,
                HEX = source.HEX,
                Id = source.Id,
                RGB = source.RGB,
                UserName = source.UserName
            };
        }
    }

}
