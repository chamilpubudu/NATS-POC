using Microsoft.Extensions.Options;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Application.Queries
{
    public class MessageQueries : IMessageQueries
    {
        private readonly IMessageRepository _repository;
        private readonly string _userId;

        public MessageQueries(IMessageRepository repository, IOptions<ChatClientSettings> options)
        {
            _repository = repository;
            _userId = options.Value.Username;
        }

        public Task<List<Message>> GetMessagesFromCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            return GetMessagesFromUserAsync(_userId);
        }

        public Task<List<Message>> GetMessagesFromUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            return _repository.GetMessagesAsync(userId);
        }
    }
}
