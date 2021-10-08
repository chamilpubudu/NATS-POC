using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client;
using NATS.POC.Chat.Application;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Client.Services
{
    public class MessageBackgroundService : BackgroundService
    {
        private readonly ILogger<MessageBackgroundService> _logger;
        private readonly IMessageProcessorService _messageProcessor;
        private readonly string _connectionString;
        private readonly string _userId;
        private readonly EventHandler<MsgHandlerEventArgs> _msgHandler;

        public MessageBackgroundService(ILogger<MessageBackgroundService> logger, IMessageProcessorService messageProcessor, IOptions<ChatClientSettings> options)
        {
            _logger = logger;
            _messageProcessor = messageProcessor;
            _connectionString = options.Value.NatsConnectionString;
            _userId = options.Value.Username;
            _msgHandler = async (sender, args) =>
            {
                try
                {
                    _logger.LogDebug("Message Received for User: {0}", _userId);
                    if (args?.Message?.Data != null)
                    {
                        var message = JsonSerializer.Deserialize<Message>(args?.Message?.Data);
                        await _messageProcessor.ProcessAsync(message).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "MsgHandle Error");
                }
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Message Listener started: User {0}", _userId);
            using (var c = new ConnectionFactory().CreateConnection(_connectionString))
            {
                var sAsync = c.SubscribeAsync(_userId);
                sAsync.MessageHandler += _msgHandler;
                sAsync.Start();

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken).ConfigureAwait(true);
                }
            }
            _logger.LogDebug("Closing Listener");
        }
    }
}
