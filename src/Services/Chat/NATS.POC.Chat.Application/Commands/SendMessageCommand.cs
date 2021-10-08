using MediatR;
using System.Runtime.Serialization;

namespace NATS.POC.Chat.Application.Commands
{
    [DataContract]
    public class SendMessageCommand : IRequest<bool>
    {
        [DataMember]
        public string ReceiverId { get; set; }

        [DataMember]
        public string Content { get; set; }

        public SendMessageCommand(string receiverId, string content)
        {
            ReceiverId = receiverId;
            Content = content;
        }
    }
}
