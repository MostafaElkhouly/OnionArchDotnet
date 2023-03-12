using AutoMapper;
using Domain.Entities.Models;
using Infrastructure.ViewModel.VM;
using Microsoft.EntityFrameworkCore;
using Persistence.IRepository;
using Persistence.IRepository.IEntityRepository;
using Service.Data.Service_Base;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Data
{
    public class CarService : ServiceBase<Car, ResReqCar>, ICarService
    {
        public CarService(IRepository<Car> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public async Task<List<ResCarWithStatus>> GetAllCarWithStatus()
        {

            var cars = await Repository.ToListAsync(include: e => e.Include(x => x.Tickets));

            var result = mapper.Map<List<ResCarWithStatus>>(cars);
            foreach(var car in result)
            {
                car.Tickets = car.Tickets.OrderByDescending(x => x.DateOfCreate).Take(1).ToList();
            }
            return result;
        }
    }
}
