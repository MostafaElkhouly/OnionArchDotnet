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
using StatusCar = Infrastructure.ViewModel.VM.StatusCar;

namespace Service.Data
{
    public class JobCardService : ServiceBase<JobCard>, IJobCardService
    {
        private readonly ITicketService ticketService;
        private readonly IRepository<JobCard_SparPart> jobCardSparRepo;
        private readonly IRepository<SparPart> sparPartRepo;

        public JobCardService(
            IRepository<JobCard> repository, 
            IMapper mapper, 
            ITicketService ticketService,
            IRepository<JobCard_SparPart> jobCardSparRepo,
            IRepository<SparPart> sparPartRepo) : base(repository, mapper)
        {
            this.ticketService = ticketService;
            this.jobCardSparRepo = jobCardSparRepo;
            this.sparPartRepo = sparPartRepo;
        }

        public async Task<JobCard_SparPart> RequestSparPartForJobCard(ReqSparPartForJobCard req)
        {

            var sparPart = await sparPartRepo.GetFirstOrDefaultAsync(e => e.Id == req.SparPartId);

            if (sparPart == null)
                throw new Exception("This Spar part is not found!");

            if (sparPart.Quantity < req.Quantity && sparPart.Quantity > 0)
                throw new Exception("This quantity of spar part is not found!");

            sparPart.Quantity -= req.Quantity;

            sparPartRepo.Update(sparPart);
            var jobCardSpar = new JobCard_SparPart
            {
                JobCardId = req.JobCardId,
                SparPartId = req.SparPartId,
                Quantity = req.Quantity,
            };

            jobCardSparRepo.Add(jobCardSpar);

            return jobCardSpar;
        }

        public async Task<JobCard> AssignJobToMechanic(Guid jobId, string mechanicId)
        {
            var job = await Repository.GetSingleAsync(jobId);
            if (job == null)
                throw new Exception("This Jobcard is not found");

            job.MechanicId = mechanicId;

            Repository.Update(job);
            return job;
        }

        public async Task<List<ResReqJobCard>> GetAllJobcardByMechanicId(string mechanicId)
        {
            var list = await Repository.ToListAsync(predicate: e => e.MechanicId.Equals(mechanicId));
            var result = mapper.Map<List<ResReqJobCard>>(list);
            return result;
        }

        public async Task<List<ResReqJobCard>> GetAllJobcardByTicketId(Guid ticketId)
        {
            var list = await Repository.ToListAsync(e => e.TicketId == ticketId);
            var result = mapper.Map<List<ResReqJobCard>>(list);
            return result;
        }

        public async Task<JobCard> ChangeStatus(Guid JobcardId, StatusCar status)
        {
            var jobcard = await Repository.GetSingleAsync(JobcardId);

            if (jobcard == null)
                throw new Exception("This Jobcard is not found!");

            jobcard.Status = (Domain.Entities.Models.StatusCar)status;

            Repository.Update(jobcard);

            await changeTikcet(jobcard);

           return jobcard;
        }

        private async Task<Ticket> changeTikcet(JobCard jobcard)
        {
            var jobCards = await Repository.ToListAsync(e => e.TicketId == jobcard.TicketId);

            var isDone = true;

            foreach(var card in jobCards)
            {
                if ((int)StatusCar.Done != (int)card.Status)
                {
                    isDone = false;
                    break;
                }
            }

            if (isDone)
            {
              return await ticketService.ChangeStatus(jobcard.TicketId, StatusCar.Done);
            }
            else
            {
              return await ticketService.ChangeStatus(jobcard.TicketId, StatusCar.Repairing);
            }

        }
    }
}
