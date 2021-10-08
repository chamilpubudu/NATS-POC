using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.POC.Chat.Domain.Events;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Application.Events
{
    public class MessageReceivedEventHandler : INotificationHandler<MessageReceivedEvent>
    {
        private readonly ILogger<MessageReceivedEventHandler> _logger;
        private readonly IMessageRepository _reposiotry;
        private readonly string _userId;

        public MessageReceivedEventHandler(ILogger<MessageReceivedEventHandler> logger, IMessageRepository reposiotry, IOptions<ChatClientSettings> options)
        {
            _logger = logger;
            _reposiotry = reposiotry;
            _userId = options.Value.Username;
        }

        public async Task Handle(MessageReceivedEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogDebug("----- Received Event - Received Message: {0}", @event.Message);
            _logger.LogInformation("----- Message: {0}", @event.Message);
            var messages = await _reposiotry.GetMessagesAsync(_userId, cancellationToken).ConfigureAwait(false) ?? new List<Message>();
            messages.Add(@event.Message);
            await _reposiotry.UpdateMessagesAsync(_userId, messages, cancellationToken).ConfigureAwait(false);
        }
    }
}
