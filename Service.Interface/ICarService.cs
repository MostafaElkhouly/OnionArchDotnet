using Domain.Entities.Models;
using Infrastructure.ViewModel.VM;
using Service.Interface.IService_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICarService : IServiceBase<Car, ResReqCar>
    {
        public Task<List<ResCarWithStatus>> GetAllCarWithStatus();
    }
}
