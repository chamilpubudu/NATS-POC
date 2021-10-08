using NATS.POC.Common.Domain;
using System;

namespace NATS.POC.Chat.Domain.Models.MessageAggregate
{
    public class Message : Entity
    {
        public string SenderId { get; }
        public string ReceiverId { get; }
        public string Content { get; }

        public DateTime CreatedOn { get; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; }

        public Message(string senderId, string receiverId, string content, DateTime sentOn, DateTime createdOn) : this(senderId, receiverId, content)
        {
            SentOn = sentOn;
            ReceivedOn = DateTime.UtcNow;
            CreatedOn = createdOn;
        }

        private Message(string senderId, string receiverId, string content)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content;
        }

        private Message()
        {
            CreatedOn = CreatedOn == default ? DateTime.UtcNow : CreatedOn;
        }

        public static Message NewMessage(string senderId, string receiverId, string content)
        {
            return new Message(senderId, receiverId, content);
        }

        public override string ToString()
        {
            if (ReceivedOn != default)
                return $"{ReceivedOn} : {SenderId} : {Content}";
            return $"{CreatedOn} : {ReceiverId} : {Content}";
        }
    }
}
