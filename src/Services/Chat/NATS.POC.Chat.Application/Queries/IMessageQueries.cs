using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Application.Queries
{
    public interface IMessageQueries
    {
        Task<List<Message>> GetMessagesFromUserAsync(string userId, CancellationToken cancellationToken = default);
        Task<List<Message>> GetMessagesFromCurrentUserAsync(CancellationToken cancellationToken = default);
    }
}
