using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Domain.Models.MessageAggregate
{
    public interface IMessageProcessorService
    {
        Task ProcessAsync(Message message, CancellationToken cancellationToken = default);
    }
}
