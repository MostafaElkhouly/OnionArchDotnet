using AutoMapper;
using Domain.Entities.Models;
using Infrastructure.ViewModel.VM;
using Persistence.IRepository;
using Persistence.IRepository.IEntityRepository;
using Service.Data.Helpers;
using Service.Data.Service_Base;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Data
{
    public class TicketService : ServiceBase<Ticket>, ITicketService
    {
        public TicketService(IRepository<Ticket> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        

        public async Task<Ticket> ChangeStatus(Guid ticketId, Infrastructure.ViewModel.VM.StatusCar status)
        {
            var ticket = await Repository.GetSingleAsync(ticketId);

            if (ticket == null)
                throw new Exception("This Jobcard is not found!");

            ticket.Status = (Domain.Entities.Models.StatusCar)status;

            Repository.Update(ticket);

            return ticket;
        }

        public async Task<List<ResReqTicket>> GetTicketByCarId(Guid CarId)
        {
            var list = await Repository.ToListAsync(e => e.CarId == CarId);

            var result = mapper.Map<List<ResReqTicket>>(list);

            return result;
        }

        public async Task<bool> SendEmail(string emailTo, string message, string userName)
        {
            
            Dictionary<string, string> emailSettings = new Dictionary<string, string>();
            emailSettings.Add("EnableSsl", "");
            emailSettings.Add("From", "");
            emailSettings.Add("FromTitle", "");
            emailSettings.Add("SupportEmail", "");
            emailSettings.Add("Password", "");
            emailSettings.Add("Port", "");
            emailSettings.Add("Host", "");
            var Service = new SendEmailService(emailSettings);
            string body = string.Empty;
            string subject = string.Empty;
            
                subject = "CSMS Notification.";

                body = $"Notification Sended <br> {message}";


            var email = new ResEmail
            {
                Subject = subject,
                Body = body,
                EmailTo = emailTo,

            };

            var result = await Service.SendEmail(email);
            return result.Item1;
        }

    }
}
