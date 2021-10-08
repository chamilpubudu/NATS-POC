using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Domain.Models.MessageAggregate
{
    public interface IMessageSenderService
    {
        Task SendAsync(Message message, CancellationToken cancellationToken = default);
    }
}
