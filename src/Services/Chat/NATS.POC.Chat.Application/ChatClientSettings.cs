namespace NATS.POC.Chat.Application
{
    public class ChatClientSettings
    {
        public string RedisConnectionString { get; set; }
        public string NatsConnectionString { get; set; }
        public string Username { get; set; }
    }
}
