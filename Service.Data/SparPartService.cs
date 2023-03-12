using AutoMapper;
using Domain.Entities.Models;
using Infrastructure.ViewModel.VM;
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
    public class SparPartService : ServiceBase<SparPart>, ISparPartService
    {
        public SparPartService(IRepository<SparPart> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
