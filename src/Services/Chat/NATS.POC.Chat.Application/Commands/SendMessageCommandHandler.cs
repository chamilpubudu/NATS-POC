using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Application.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, bool>
    {
        private readonly ILogger<SendMessageCommandHandler> _logger;
        private readonly IMessageSenderService _sender;
        private readonly string _userId;

        public SendMessageCommandHandler(ILogger<SendMessageCommandHandler> logger, IMessageSenderService sender, IOptions<ChatClientSettings> options)
        {
            _logger = logger;
            _sender = sender;
            _userId = options.Value.Username;
        }

        public async Task<bool> Handle(SendMessageCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug("----- Received Send Command - Message: {0}", command);
            var message = Message.NewMessage(_userId, command.ReceiverId, command.Content);
            await _sender.SendAsync(message, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
