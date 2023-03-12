using Domain.Entities.Models;
using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interface.IService_Base;

namespace Service.Interface
{
    public interface ICarModelService : IServiceBase
    {
        public List<ResReqCarModel> GetCarModelByParentId(Guid parentId);

    }
}
