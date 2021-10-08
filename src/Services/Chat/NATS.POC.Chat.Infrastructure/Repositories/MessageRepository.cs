using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ILogger<MessageRepository> _logger;
        private readonly IDistributedCache _cache;

        public MessageRepository(ILogger<MessageRepository> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task DeleteMessagesAsync(string userId, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(userId, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Message>> GetMessagesAsync(string userId, CancellationToken cancellationToken = default)
        {
            var data = await _cache.GetStringAsync(userId, cancellationToken).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(data))
            {
                return null;
            }

            return JsonSerializer.Deserialize<List<Message>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Message>> UpdateMessagesAsync(string userId, List<Message> messages, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cache.SetStringAsync(userId, JsonSerializer.Serialize(messages), cancellationToken).ConfigureAwait(false);
                _logger.LogDebug("User Messages persisted succesfully.");
                return await GetMessagesAsync(userId, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problem occur persisting the item.");
                return null;
            }
        }
    }
}
