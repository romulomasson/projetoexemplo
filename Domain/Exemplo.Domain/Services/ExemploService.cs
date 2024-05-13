using Azure.Messaging.ServiceBus;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Repository;
using Exemplo.Domain.Interfaces.Service;
using Exemplo.Domain.Interfaces.Services;

namespace Exemplo.Domain.Services
{
    public class ExemploService<TContext> : BaseService<TContext, Exemplo>, IExemploService<TContext>
     where TContext : IUnitOfWork<TContext>
    {

        private readonly IExemploRepository<TContext> _Repository;
        private readonly IBusService _bus;
        private readonly ServiceBusSender _serviceBusSender;
        private readonly ServiceBusReceiver _serviceBusReceiver;
        public ExemploService(
            IExemploRepository<TContext> Repository,
            IBusService bus,            
            IUnitOfWork<TContext> unitOfWork,
            IUserHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
            _bus = bus;
       }
       
    }
}







