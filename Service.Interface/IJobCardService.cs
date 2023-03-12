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
    public interface IJobCardService : IServiceBase
    {
        public Task<JobCard_SparPart> RequestSparPartForJobCard(ReqSparPartForJobCard req);
        Task<JobCard> AssignJobToMechanic(Guid jobId, string mechanicId);
        Task<List<ResReqJobCard>> GetAllJobcardByMechanicId(string mechanicId);
        Task<List<ResReqJobCard>> GetAllJobcardByTicketId(Guid ticketId);
        Task<JobCard> ChangeStatus(Guid JobcardId, Infrastructure.ViewModel.VM.StatusCar status);
    }
}
