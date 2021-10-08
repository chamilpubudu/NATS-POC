using Microsoft.Extensions.Configuration;
using NATS.Client;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Infrastructure.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly string _connectionString;

        public MessageSenderService(IConfiguration configuration)
        {
            _connectionString = configuration["NatsConnectionString"];
        }

        public Task SendAsync(Message message, CancellationToken cancellationToken = default)
        {
            using (var c = new ConnectionFactory().CreateConnection(_connectionString))
            {
                message.SentOn = DateTime.UtcNow;
                c.Publish(message.ReceiverId, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)));
                c.Flush();
            }

            return Task.CompletedTask;
        }
    }
}
