using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Domain.Models.MessageAggregate
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetMessagesAsync(string userId, CancellationToken cancellationToken = default);
        Task<List<Message>> UpdateMessagesAsync(string userId, List<Message> messages, CancellationToken cancellationToken = default);
        Task DeleteMessagesAsync(string userId, CancellationToken cancellationToken = default);
    }
}
