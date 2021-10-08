using MediatR;
using NATS.POC.Chat.Domain.Events;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Infrastructure.Services
{
    public class MessageProcessorService : IMessageProcessorService
    {
        private readonly IMediator _mediator;

        public MessageProcessorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task ProcessAsync(Message message, CancellationToken cancellationToken = default)
        {
            _mediator.Publish(new MessageReceivedEvent(message), cancellationToken);

            return Task.CompletedTask;
        }
    }
}
