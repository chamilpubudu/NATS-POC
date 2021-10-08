using MediatR;
using NATS.POC.Chat.Domain.Models.MessageAggregate;

namespace NATS.POC.Chat.Domain.Events
{
    public class MessageReceivedEvent
        : INotification
    {
        public MessageReceivedEvent(Message message)
        {
            Message = message;
        }
        public Message Message { get; }
    }
}
